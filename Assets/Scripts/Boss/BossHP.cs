using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField] MinionSpawning minionInvincibleScript;
    public bool bossDead, healing;
	bool hitCooldown = false;
    public int bossHp = 40;
	Timer timerScript;
	
    void Start()
    {
        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
    }

    void Update()
    {
        if(healing) HealBoss();
        Debug.Log(bossHp);
    }

    public void HealBoss()
    {
        Invoke(nameof(StopHealing), 2.5f);
        InvokeRepeating(nameof(Heal), 0, 1f);
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
                    bossDead = true;
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

    void Heal()
    {
        if(bossHp < 50)
        {
            bossHp += 1;
        }
        CancelInvoke();
    }

    void StopHealing()
    {
        healing = false;
        CancelInvoke(nameof(StopHealing));
    }
	
	void BossHitCooldown()
	{
		hitCooldown = false;
	}	
}
