using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    BossAttackManager activateBossScript;

    bool dashCooldown, dashing, bossCollide;

	float dashTime = 0.4f, dashCooldownTime = 1f, dashSpeed = 10;
    float movementSpeed = 7.5f, angle;
    Vector2 playerPosition;
	public enum Attack {Idle, Throw, AxeReturning, Melee};
	public Attack axeAttack;
    Rigidbody2D rgbd2D;
	Timer timerScript;

    //Animation States
    string currentState, holdIdleState, holdDashState, walkingState;
    SpriteRenderer flipSprite;
    Animator animator;


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
                holdDashState = "dash_Side";
                flipSprite.flipX = false;
            } else if(playerPosition.x < 0) {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                holdDashState = "dash_Side";
                flipSprite.flipX = true;
            } else if(playerPosition.y > 0 && playerPosition.x == 0) {
                walkingState = "walking_Up";
                holdIdleState = "idle_Up";
                holdDashState = "dash_Up";
                flipSprite.flipX = false;
            } else if(playerPosition.y < 0 && playerPosition.x == 0) {
                walkingState = "walking_Down";
                holdIdleState = "idle_Down";
                holdDashState = "dash_Down";
                flipSprite.flipX = false;
            }


            if((Input.GetButtonDown("Dash") || Input.GetKeyDown(KeyCode.LeftShift)) && !dashCooldown && playerPosition.magnitude != 0)
            {
                dashCooldown = true;
                dashing = true;
                rgbd2D.AddForce(playerPosition.normalized * dashSpeed, ForceMode2D.Impulse);
                ChangeAnimationState("Dash");
                Invoke(nameof(DashLength), dashTime);
            }

            if(!dashing) rgbd2D.velocity = playerPosition.normalized * movementSpeed;

            if(playerPosition.magnitude > 0)
            {
                ChangeAnimationState("Walking");
            } else if(playerPosition.magnitude == 0) {
                ChangeAnimationState("Idle");
            }

            if (bossCollide == true)
			{
                rgbd2D.AddForce(-playerPosition.normalized * (dashSpeed * 4), ForceMode2D.Impulse);
				rgbd2D.AddForce(Vector2.zero, ForceMode2D.Impulse);
				dashCooldown = false;
				dashing = false;
			}		
        }
    }

    void ChangeAnimationState(string newState)
    {
        //if(currentState == newState) return;
        if(newState == "Walking" && !dashing)
        {
            animator.Play(walkingState);
        } else if(newState == "Idle" && !dashing) {
            animator.Play(holdIdleState);
        } else if(newState == "Dash") {
            animator.Play(holdDashState);
        }

        currentState = newState;
    }

    void LookAtMouse()
    {
        rgbd2D.velocity = Vector2.zero;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        rgbd2D.AddForce(lookDirection.normalized * 7, ForceMode2D.Impulse);
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
		
        if(angle < 45 && angle > -45)
        {
            //Right
            flipSprite.flipX = false;
            animator.Play("melee_Side");
        } else if(angle > 45 && angle < 135) {
            //Up
            flipSprite.flipX = false;
            animator.Play("melee_Up");
        } else if(angle < -45 && angle > -135) {
            //Down
            flipSprite.flipX = false;
            animator.Play("melee_Down");
        } else if(angle > 135 || angle < -135) {
            //Left
            flipSprite.flipX = true;
            animator.Play("melee_Side");
        }
    }
	
	void DashLength()
	{
        dashing = false;
		rgbd2D.AddForce(Vector2.zero, ForceMode2D.Impulse);
        Invoke(nameof(DashCooldown), dashCooldownTime);
	}

    void DashCooldown()
    {
        dashCooldown = false;
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
