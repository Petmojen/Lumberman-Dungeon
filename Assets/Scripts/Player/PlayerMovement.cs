using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    BossAttackManager activateBossScript;

	bool dashCooldown = false, bossCollide = false, moveToBoss = true;

	float dashTime = 0.4f, dashCooldownTime = 2f, dashSpeed = 2;
    float movementSpeed = 7.5f, angle;
    Vector2 playerPosition;
    int dashTimer;

	public enum Attack {Idle, Throw, AxeReturning, Melee};
	public Attack axeAttack;
    Rigidbody2D rgbd2D;
	Timer timerScript;

    //temp until we gen animations
    [SerializeField] Sprite spriteRight, spriteLeft, spriteUp, spriteDown;
    SpriteRenderer changeSprite;

    void Start()
    {
        Time.timeScale = 1;
        changeSprite = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();
        dashTimer = 0;
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

        if(axeAttack != Attack.Melee)
        {
            playerPosition.x = Input.GetAxisRaw("Horizontal");
            playerPosition.y = Input.GetAxisRaw("Vertical");

            if(playerPosition.x > 0)
            {
                changeSprite.sprite = spriteRight;
            } else if(playerPosition.x < 0) {
                changeSprite.sprite = spriteLeft;
            }

            if(playerPosition.y > 0)
            {
                changeSprite.sprite = spriteUp;
            } else if(playerPosition.y < 0) {
                changeSprite.sprite = spriteDown;
            }

            if ((Input.GetButton("Dash") || Input.GetKey(KeyCode.LeftShift)) && !dashCooldown)
            {
				rgbd2D.velocity = new Vector2(playerPosition.x * movementSpeed * dashSpeed, playerPosition.y * movementSpeed * dashSpeed);
				Invoke ("Dashing", dashTime);
			} else {
				rgbd2D.velocity = new Vector2(playerPosition.x * movementSpeed, playerPosition.y * movementSpeed);
				Invoke ("DashCooldown", dashCooldownTime);
            }

			if (bossCollide == true)
			{
				rgbd2D.velocity = -rgbd2D.velocity;
				dashCooldown = true;
			}
        }
    }

    void LookAtMouse()
    {
        rgbd2D.velocity = new Vector2(0, 0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        if(angle < 45 && angle > -45)
        {
            changeSprite.sprite = spriteRight;
        } else if(angle > 45 && angle < 135) {
            changeSprite.sprite = spriteUp;
        } else if(angle < -45 && angle > -135) {
            changeSprite.sprite = spriteDown;
        } else if(angle > 135 || angle < -135) {
            changeSprite.sprite = spriteLeft;
        }
    }

	void Dashing()
	{
		dashCooldown = true;
		CancelInvoke();
	}

	void DashCooldown()
	{
		dashCooldown = false;
		CancelInvoke();
	}

	void PlayerBossCollisionCooldown()
	{
		bossCollide = false;
		CancelInvoke();
	}

	void OnTriggerStay2D(Collider2D collision)
	{
	    if(collision.CompareTag("Axe") && axeAttack == Attack.AxeReturning)
	    {
			axeAttack = Attack.Idle;
			Destroy(collision.gameObject);
	    }
	}

	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") && !bossCollide)
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

}
