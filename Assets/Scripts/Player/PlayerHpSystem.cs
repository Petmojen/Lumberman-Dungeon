using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHpSystem : MonoBehaviour
{
    [SerializeField] Slider sliderHealth;
    float health = 100;

    [SerializeField] GameObject[] armorSprite;
    public int armor;

    public void TakeDamage(float damage)
    {
        if(armor > 0)
        {
            armor--;
            UpdateArmor();
        } else {
            health -= damage;
            sliderHealth.value = health / 100;
        }
    }

    public void UpdateArmor()
    {
        for(int i = 0; i < armorSprite.Length; i++)
        {
            if (i < armor)
            {
                armorSprite[i].SetActive(true);
            }
            else
            {
                armorSprite[i].SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
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