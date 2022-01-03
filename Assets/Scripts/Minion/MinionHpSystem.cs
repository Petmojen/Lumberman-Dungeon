using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionHpSystem : MonoBehaviour
{
    public Vector2 knockDirection;
    public bool knockBack, isDead;
    int minionHp = 4;
	
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !GetComponent<MinionAnimation>().spawning)
        {
            if(minionHp <= 0)
            {
                isDead = true;
            } else {
                knockDirection = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
                minionHp--;
                knockBack = true;
            }
        }

		if (collision.CompareTag("Melee") && !GetComponent<MinionAnimation>().spawning)
        {
            if(minionHp <= 0)
            {
                isDead = true;
            } else {
                knockDirection = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
                minionHp -= 2;
                knockBack = true;
            }
        }
    }
}
