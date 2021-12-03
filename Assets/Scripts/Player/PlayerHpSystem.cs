using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHpSystem:MonoBehaviour
{
    [SerializeField] Slider sliderHealth;
    bool invincible = false;
    float health = 100;
    public bool isDead = false;
    [SerializeField] GameObject[] armorSprite;
    public int armor;
	Debugger debuggerScript;
	
	void Start()
	{
		debuggerScript = GameObject.FindObjectOfType(typeof(Debugger)) as Debugger; 
	}
	void Update()
	{
		if(debuggerScript.instaDeath)
        {
            isDead = true;
			debuggerScript.instaDeath = !debuggerScript.instaDeath;
        }
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
            sliderHealth.value = health / 100;
        }
        if(health <= 0)
        {
            isDead = true;
        }
        Invoke(nameof(Vincible), 2f);
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
        if(!invincible || !debuggerScript.immortal)
        {
            switch(collision.gameObject.tag)
            {
                case "MinionShot":
                    TakeDamage(10);
                    break;
                case "Boss":
                    TakeDamage(25);
                    break;
                case "Leaf":
                    TakeDamage(10);
                    break;
            }
        }
    }
}
