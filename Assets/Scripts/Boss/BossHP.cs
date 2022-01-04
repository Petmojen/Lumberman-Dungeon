using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [SerializeField] MinionSpawning minionInvincibleScript;
    ForceToBossDarkness darknessScript;
    [SerializeField] Slider healthBar;
    DifficultyManager difficultyScript;
    public bool bossDead, healing, takeHit;
    public float bossHp = 100;
    static float maxHp = 100;
	bool hitCooldown = false;
	Timer timerScript;
	
    void Start()
    {
        darknessScript = GameObject.FindObjectOfType(typeof(ForceToBossDarkness)) as ForceToBossDarkness;
        difficultyScript = GameObject.FindObjectOfType(typeof(DifficultyManager)) as DifficultyManager;
        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
    }

    void Update()
    {
        if(healing) HealBoss();
        healthBar.value = bossHp / maxHp;
    }

    public void NextHealthLevel()
    {
        switch(DifficultyManager.difficultyLevel)
        {
                case 0:
                    maxHp = 100;
                    bossHp = maxHp;
                    break;
                case 1:
                    maxHp = 200;
                    bossHp = maxHp;
                    break;
                case 2: 
                    maxHp = 300;
                    bossHp = maxHp;
                    break;
        }
    }

    public void HealBoss()
    {
        Invoke(nameof(StopHealing), 2.5f);
        InvokeRepeating(nameof(Heal), 0, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		if (timerScript.timeOut && darknessScript.radiusOfLight < 13.51f)
		{
			if (collision.CompareTag("Axe") && !hitCooldown && !minionInvincibleScript.bossInvicible)
			{
				bossHp -= 2;
				hitCooldown = true;
                takeHit = true;
				if (bossHp == 0) bossDead = true;
			}

			if (collision.CompareTag("Melee") && !hitCooldown && !minionInvincibleScript.bossInvicible)
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
        if(bossHp < maxHp)
        {
            bossHp += 0.25f;
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
