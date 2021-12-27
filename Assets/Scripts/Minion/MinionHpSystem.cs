using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionHpSystem : MonoBehaviour
{
    //3hp = 2 hp because it enters and reenters before coming back to the player.
    int minionHp = 3;
	//TutorialTextScript tutorialTextScript;

	//void Start()
	//{
	//	tutorialTextScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
	//}
	
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe"))
        {
            minionHp--;
            if(minionHp <= 0)
			{	
				Destroy(gameObject);
				//tutorialTextScript.tutorialStep++;
			}
        }
		if (collision.CompareTag("Melee"))
        {
            minionHp = minionHp - 5;
            if(minionHp <= 0) 
			{	
				Destroy(gameObject);
				//tutorialTextScript.tutorialStep++;
			}
        }
    }
}
