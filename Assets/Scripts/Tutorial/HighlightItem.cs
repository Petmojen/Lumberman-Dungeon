using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HighlightItem : MonoBehaviour
{
	TutorialTextScript textScript;
	Light2D lightSource;


    void Start()
    {
		lightSource = gameObject.GetComponentInChildren<Light2D>();
		textScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;

    }

	void OnTriggerEnter2D(Collider2D collision)
	{

		if (transform.CompareTag("Tourch"))
		{
				lightSource.intensity = 2f;
				textScript.typeOfItem = transform.tag;
		}
		
		if (transform.CompareTag("Seed"))
		{
		//	if (!earthMoundScript.taken)
		//	{
				lightSource.intensity = 2f;
				textScript.typeOfItem = transform.tag;
		//	}
		}
			
				if (transform.CompareTag("Log"))
		{
		//	if (!logScript.taken)
		//	{
				lightSource.intensity = 2f;
				textScript.typeOfItem = transform.tag;
		//	}
		}
    }
    

    void OnTriggerExit2D(Collider2D collision)
    {
		lightSource.intensity = 0f;
		textScript.typeOfItem = "";
		
    }
}
