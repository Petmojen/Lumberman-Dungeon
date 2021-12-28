using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPT : MonoBehaviour
{
    [SerializeField] MinionSpawning minionInvincibleScript;
    TutorialDarkness darknessScript;
    [SerializeField] Slider healthBar;
    public bool bossDead, healing;
    public float bossHp = 100;
	bool hitCooldown = false;
	Timer timerScript;
	
    void Start()
    {
        darknessScript = GameObject.FindObjectOfType(typeof(TutorialDarkness)) as TutorialDarkness;
        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
    }

    void Update()
    {
        if(healing) HealBoss();
        healthBar.value = bossHp / 100;
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
			if (collision.CompareTag("Axe") && !hitCooldown)
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
