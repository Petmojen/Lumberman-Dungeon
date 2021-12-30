using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    float health = 100;

    void Update()
    {
        if(health <= 0) {
            Destroy(gameObject);
        } 
    }

    void TakeDamage(float amount)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        health -= amount;
        Invoke(nameof(ColorCorrection), 0.05f);
    }

    void ColorCorrection()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.gameObject.tag)
        {
            case "Axe":
                TakeDamage(25);
                break;
            case "Minion":
                TakeDamage(25);
                break;
            case "Leaf":
                TakeDamage(25);
                break;
            case "Branch":
                TakeDamage(25);
                break;
            case "Melee":
                TakeDamage(25);
                break;
        }
    }
}
