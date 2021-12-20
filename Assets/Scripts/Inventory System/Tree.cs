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
        health -= amount;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        
    }
}
