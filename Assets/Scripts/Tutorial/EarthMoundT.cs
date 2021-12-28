using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EarthMoundT:MonoBehaviour
{
	TutorialInventorySystem inventorySystemScript;
	TutorialTextScript textScript;
	SpriteRenderer earthMoundRenderer;
	Light2D lightSource;
	
    Animator animator;
    public bool taken, seedFull;

    void Start()
    {
        animator = GetComponent<Animator>();
		
		lightSource = gameObject.GetComponentInChildren<Light2D>();
		textScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
		inventorySystemScript = GameObject.FindObjectOfType(typeof(TutorialInventorySystem)) as TutorialInventorySystem;
		earthMoundRenderer = GetComponent<SpriteRenderer>();
    }
		void Update()
	{
		if (inventorySystemScript.seedBool)
		{
			earthMoundRenderer.color = Color.yellow;
		} else {
			earthMoundRenderer.color = Color.white;
		}
	}

    public void ChangeSprite(string animationName)
    {
        animator.Play(animationName);
    }


	void OnTriggerEnter2D(Collider2D collision)
	{

		if (!taken)
		{
			lightSource.intensity = 2f;
			textScript.typeOfItem = transform.tag;
		}
    }
    

    void OnTriggerExit2D(Collider2D collision)
    {
		lightSource.intensity = 0f;
		textScript.typeOfItem = "";
		
    }
}
