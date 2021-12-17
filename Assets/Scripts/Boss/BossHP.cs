using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField] MinionSpawning minionInvincibleScript;
	bool hitCooldown = false;
    public int bossHp = 50;
	Timer timerScript;
	
    void Start()
    {

        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (timerScript.timeOut)
		{
			if (collision.CompareTag("Axe") && !hitCooldown && !minionInvincibleScript.bossInvicible)
			{
				bossHp--;
				hitCooldown = true;
				if (bossHp == 0)
				{
					Destroy(gameObject);
				}
			}
			if (collision.CompareTag("Melee"))
			{
				bossHp = bossHp - 5;
				if(bossHp == 0) Destroy(gameObject);
			}
			Invoke(nameof(BossHitCooldown), 0.5f);
		}
    }
	
	void BossHitCooldown()
	{
		hitCooldown = false;
	}	
}
