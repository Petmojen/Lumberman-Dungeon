using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSystem:MonoBehaviour
{
    [SerializeField] Slider sliderHealth;
    bool invincible = false;
    float health = 100;

    [SerializeField] GameObject[] armorSprite;
    public int armor;

    public void TakeDamage(float damage)
    {
        health -= damage;
        sliderHealth.value = health / 100;
    }

    public void UpdateArmor()
    {
        invincible = true;
        for(int i = 0; i < armorSprite.Length; i++)
        {
            if(i < armor)
            {
                armorSprite[i].SetActive(true);
            } else {
                armorSprite[i].SetActive(false);
            }
        }
        Invoke(nameof(Vincible), 2);
    }

    void Vincible()
    {
        invincible = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MinionShot") && !invincible)
        {
            if(armor > 0)
            {
                armor--;
                UpdateArmor();
            } else {
                TakeDamage(10);
            }
        }

        if(collision.CompareTag("Boss") && !invincible)
        {
            if(armor > 0)
            {
                armor--;
                UpdateArmor();
            } else {
                TakeDamage(25);
            }
        }

        if(collision.CompareTag("Leaf") && !invincible)
        {
            if(armor > 0)
            {
                armor--;
                UpdateArmor();
            } else {
                TakeDamage(10);
            }
        }
    }
}
