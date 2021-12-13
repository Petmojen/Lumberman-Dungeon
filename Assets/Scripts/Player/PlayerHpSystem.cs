using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHpSystem:MonoBehaviour
{
    [SerializeField] Slider sliderHealth;
    public bool isDead = false;
    bool invincible = false, noPoison, healing;
    int lightCounter = 0;
    float health = 100;

    [SerializeField] GameObject[] armorSprite;
	Debugger debuggerScript;
	Timer timerScript;	
    public int armor;
	
	void Start()
	{
		debuggerScript = GameObject.FindObjectOfType(typeof(Debugger)) as Debugger;
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
	}

	void Update()
	{
		if(debuggerScript.instaDeath)
        {
            isDead = true;
			debuggerScript.instaDeath = !debuggerScript.instaDeath;
        }
        sliderHealth.value = health / 100;
        if(health <= 0)
        {
            isDead = true;
            timerScript.timeOut = false;
        }

        if(lightCounter == 0) Invoke(nameof(Poison), 1f);
        if(healing) Invoke(nameof(Heal), 0.2f);
    }

    void Heal()
    {
        if(health < 100)
        {
            health += 2;
        }
        CancelInvoke(nameof(Heal));

    }

    void Poison()
    {
        health -= Random.Range(2, 8);
        CancelInvoke(nameof(Poison));
    }

    public void TakeDamage(float damage)
    {
        invincible = true;
        if(armor > 0)
        {
            armor--;
            UpdateArmor();
        } else {
            health -= damage;
        }
        Invoke(nameof(Vincible), 0.2f);
    }

    public void UpdateArmor()
    {
        for(int i = 0; i < armorSprite.Length; i++)
        {
            if(i < armor)
            {
                armorSprite[i].SetActive(true);
            } else {
                armorSprite[i].SetActive(false);
            }
        }
    }

    void Vincible()
    {
        invincible = false;
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!invincible && !debuggerScript.immortal)
        {
            switch(collision.gameObject.tag)
            {
                case "Minion":
                    TakeDamage(10);
                    break;
                case "Boss":
                    TakeDamage(25);
                    break;
                case "Leaf":
                    TakeDamage(10);
                    break;
				//case "Blocker":
				//	if (timerScript.timeOut)
				//	{
				//		armor = 0;
				//		TakeDamage(50);
				//		//isDead = true;
				//	}
				//	break;
                case "Light":
                    lightCounter++;
                    break;
                case "Heal":
                    healing = true;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Light":
                lightCounter--;
                break;
            case "Heal":
                healing = false;
                break;
        }
    }
}
