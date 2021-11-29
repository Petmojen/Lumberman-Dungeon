using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSystem:MonoBehaviour
{
    [SerializeField] Slider sliderHealth;
    float health = 100;

    GameObject[] armorSprite;
    public int armor = 2;

    public void TakeDamage(float damage)
    {
        health -= damage;
        sliderHealth.value = health / 100;
    }

    public void UpdateArmor()
    {
        armor--;
        for(int i = 0; i < 3; i++)
        {
            if(i < armor)
            {
                armorSprite[i].SetActive(true);
            } else {
                armorSprite[i].SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MinionShot"))
        {
            if(armor >= 0)
            {
                UpdateArmor();
            } else {
                TakeDamage(10);
            }
        }

        if(collision.CompareTag("Boss"))
        {
            if(armor >= 0)
            {
                UpdateArmor();
            } else
            {
                TakeDamage(10);
            }
        }

        if(collision.CompareTag("Leaf"))
        {
            if(armor >= 0)
            {
                UpdateArmor();
            } else
            {
                TakeDamage(10);
            }
        }
    }
}
