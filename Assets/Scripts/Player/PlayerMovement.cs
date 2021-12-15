using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    BossAttackManager activateBossScript;

    bool dashCooldown = false, bossCollide = false;

	float dashTime = 0.4f, dashCooldownTime = 2f, dashSpeed = 2;
    float movementSpeed = 7.5f, angle;
    Vector2 playerPosition;
	public enum Attack {Idle, Throw, AxeReturning, Melee};
	public Attack axeAttack;
    Rigidbody2D rgbd2D;
	Timer timerScript;

    SpriteRenderer flipSprite;
    string currentState;
    Animator animator;

    //Animation States
    string holdIdleState, walkingState;

    void Start()
    {
        Time.timeScale = 1;
        flipSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rgbd2D = GetComponent<Rigidbody2D>();
		axeAttack = Attack.Idle;
        activateBossScript = GameObject.FindObjectOfType(typeof(BossAttackManager)) as BossAttackManager;
        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            LookAtMouse();
        }

        if(Input.GetAxisRaw("Melee") > 0f || Input.GetAxisRaw("Throw") > 0f)
        {
            Debug.Log("attack");
        }

        if(axeAttack != Attack.Melee)
        {
            playerPosition.x = Input.GetAxisRaw("Horizontal");
            playerPosition.y = Input.GetAxisRaw("Vertical");

            if(playerPosition.x > 0)
            {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                flipSprite.flipX = false;
            } else if(playerPosition.x < 0) {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                flipSprite.flipX = true;
            } else if(playerPosition.y > 0) {
                walkingState = "walking_Up";
                holdIdleState = "idle_Up";
            } else if(playerPosition.y < 0) {
                walkingState = "walking_Down";
                holdIdleState = "idle_Down";
            }

            if(playerPosition.magnitude != 0)
            {
                ChangeAnimationState("Walking");
            } else {
                ChangeAnimationState("Idle");
            }

            if ((Input.GetButtonDown("Dash") || Input.GetKeyDown(KeyCode.LeftShift)) && !dashCooldown)
            {
                movementSpeed *= dashSpeed;
				Invoke ("Dashing", dashTime);
			} else {
				Invoke ("DashCooldown", dashCooldownTime);
            }

            rgbd2D.velocity = playerPosition * movementSpeed;

            if (bossCollide == true)
			{
				rgbd2D.velocity = -rgbd2D.velocity;
				dashCooldown = true;
			}
        }
    }

    void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;
        if(newState == "Walking")
        {
            animator.Play(walkingState);
        } else if(newState == "Idle") {
            animator.Play(holdIdleState);
        }

        currentState = newState;
    }

    void LookAtMouse()
    {
        rgbd2D.velocity = new Vector2(0, 0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
		
        if(angle < 45 && angle > -45)
        {
            //flipSprite.sprite = spriteRight;
        } else if(angle > 45 && angle < 135) {
            //flipSprite.sprite = spriteUp;
        } else if(angle < -45 && angle > -135) {
            //flipSprite.sprite = spriteDown;
        } else if(angle > 135 || angle < -135) {
            //flipSprite.sprite = spriteLeft;
        }
    }

	void Dashing()
	{
        movementSpeed = 7.5f;
        dashCooldown = true;
		CancelInvoke();
	}
	
	void DashCooldown()
	{
        movementSpeed = 7.5f;
        dashCooldown = false;
		CancelInvoke();
	}

	 private void OnTriggerStay2D(Collider2D collision)
	{
	    if(collision.CompareTag("Axe") && axeAttack == Attack.AxeReturning)
	    {
			axeAttack = Attack.Idle;
			Destroy(collision.gameObject);
	    }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boss") && !bossCollide)
        {
            bossCollide = true;
            Invoke(nameof(PlayerBossCollisionCooldown), 0.1f);
        }

        if(collision.CompareTag("BossRoom"))
        {
            activateBossScript.bossAwake = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("BossRoom"))
        {
            activateBossScript.bossAwake = false;
        }
    }

    void PlayerBossCollisionCooldown()
	{
		bossCollide = false;
		CancelInvoke();
	}
}
