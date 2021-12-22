using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    BossAttackManager activateBossScript;

    public bool dashCooldown, dashing, bossCollide, rootSnared;

	float dashTime = 0.4f, dashCooldownTime = 1f, dashSpeed = 10, movementSpeed = 7.5f;
    public float angle;
    public Vector2 playerPosition;
	public enum Attack {Idle, Throw, AxeReturning, Melee, Dashing};
	public Attack axeAttack;
    Rigidbody2D rgbd2D;
	Timer timerScript;

    void Start()
    {
        Time.timeScale = 1;
        rgbd2D = GetComponent<Rigidbody2D>();
		axeAttack = Attack.Idle;
        activateBossScript = GameObject.FindObjectOfType(typeof(BossAttackManager)) as BossAttackManager;
        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            LookAtMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if(Input.GetAxisRaw("Melee") > 0f || Input.GetAxisRaw("Throw") > 0f)
        {
			LookAtMouse(transform.position + new Vector3(Input.GetAxisRaw("HorizontalAim"), Input.GetAxisRaw("VerticalAim"), 0));
        }

        if(axeAttack != Attack.Melee)
        {
            playerPosition.x = Input.GetAxisRaw("Horizontal");
            playerPosition.y = Input.GetAxisRaw("Vertical");

            if((Input.GetButtonDown("Dash") || Input.GetKeyDown(KeyCode.LeftShift)) && !dashCooldown && playerPosition.magnitude != 0 && !rootSnared)
            {
                dashCooldown = true;
                dashing = true;
				axeAttack = Attack.Dashing;
                rgbd2D.AddForce(playerPosition.normalized * dashSpeed, ForceMode2D.Impulse);
                Invoke(nameof(DashLength), dashTime);
            }

            if(!dashing && !rootSnared) rgbd2D.velocity = playerPosition.normalized * movementSpeed;
        }

		if (bossCollide == true)
		{
			rgbd2D.AddForce(-playerPosition.normalized * (dashSpeed * 2), ForceMode2D.Impulse);
			rgbd2D.AddForce(Vector2.zero, ForceMode2D.Impulse);
			dashCooldown = false;
			dashing = false;
		}
    }

    void LookAtMouse(Vector2 mousePos)
    {
        rgbd2D.velocity = Vector2.zero;
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        rgbd2D.AddForce(lookDirection.normalized * 3, ForceMode2D.Impulse);
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
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
		axeAttack = Attack.Idle;
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
            Invoke(nameof(PlayerBossCollisionCooldown), 0.2f);
        }

        if(collision.CompareTag("BossRoom"))
        {
            activateBossScript.bossAwake = true;
        }

        if(collision.CompareTag("Snare"))
        {
            rootSnared = true;
            rgbd2D.velocity = Vector3.zero;
            BossHP healBoss = GameObject.FindObjectOfType(typeof(BossHP)) as BossHP;
            healBoss.healing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("BossRoom"))
        {
            activateBossScript.bossAwake = false;
        }

        if(collision.CompareTag("Snare"))
        {
            rootSnared = false;
            BossHP healBoss = GameObject.FindObjectOfType(typeof(BossHP)) as BossHP;
            healBoss.healing = false;
        }
    }

    void PlayerBossCollisionCooldown()
	{
		bossCollide = false;
		CancelInvoke();
	}
}
