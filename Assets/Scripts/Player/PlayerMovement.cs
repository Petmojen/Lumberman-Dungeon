using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    bool attacking = false, dashCooldown = false;
	public bool colCooldown = false;
    PlayerAttack playerAttackScript;
	float dashTime = 0.1f, dashCooldownTime = 2f, dashSpeed = 4;
    float movementSpeed = 5, angle;
    Vector2 playerPosition;
    public int dashTimer;
    Rigidbody2D rgbd2D;

    //temp until we gen animations
    [SerializeField] Sprite spriteRight, spriteLeft, spriteUp, spriteDown;
    SpriteRenderer changeSprite;

    void Start()
    {
        playerAttackScript = GetComponentInChildren<PlayerAttack>();
        changeSprite = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();
        dashTimer = 0;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            LookAtMouse();
        }

        if(!attacking)
        {

            playerPosition.x = Input.GetAxis("Horizontal");
            playerPosition.y = Input.GetAxis("Vertical");

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

            if ((Input.GetKey("space") || Input.GetKey(KeyCode.LeftShift)) && !dashCooldown)
            {
				rgbd2D.velocity = new Vector2(playerPosition.x * movementSpeed * dashSpeed, playerPosition.y * movementSpeed * dashSpeed);
				Invoke ("Dashing", dashTime);
			} else {
				rgbd2D.velocity = new Vector2(playerPosition.x * movementSpeed, playerPosition.y * movementSpeed);
				Invoke ("DashCooldown", dashCooldownTime);
            }
        }
    }

    void LookAtMouse()
    {
        attacking = true;
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
        Invoke(nameof(DeactivateAttack), 0.25f);
    }

    void DeactivateAttack()
    {
        attacking = false;
        rgbd2D.rotation = 0;
        CancelInvoke();
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

	 private void OnTriggerStay2D(Collider2D collision)
	{
	    if(collision.CompareTag("Axe") && !attacking)
	    {
			Destroy(collision.gameObject);
			playerAttackScript.usingAxe = false;
	    }
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") && !colCooldown)
        {
            rgbd2D.velocity = -rgbd2D.velocity;
			colCooldown = true;
        }
		Invoke(nameof(PlayerBossCollisionCooldown), 1f);
    }
	void PlayerBossCollisionCooldown()
	{
		colCooldown = false;
		CancelInvoke();
	}
}
