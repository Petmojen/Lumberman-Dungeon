using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPT : MonoBehaviour
{
    [SerializeField] MinionSpawning minionInvincibleScript;
    TutorialDarkness darknessScript;
    [SerializeField] Slider healthBar;
    public bool bossDead, healing, takeHit;
    public float bossHp = 50;
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
        healthBar.value = bossHp / 50;
    }


    public void HealBoss()
    {
        Invoke(nameof(StopHealing), 2.5f);
        InvokeRepeating(nameof(Heal), 0, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		if (timerScript.timeOut)// && darknessScript.radiusOfLight < 13.51f)
		{
			if (collision.CompareTag("Axe") && !hitCooldown)
			{
				bossHp -= 2;
				hitCooldown = true;
                takeHit = true;
				if (bossHp == 0) bossDead = true;
			}

			if (collision.CompareTag("Melee") && !hitCooldown)
			{
				bossHp -= 5;
                hitCooldown = true;
                takeHit = true;
                if(bossHp == 0) bossDead = true;
			}
			Invoke(nameof(BossHitCooldown), 0.5f);
		}
    }

    void Heal()
    {
        bossHp += 0.25f;
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
