using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HighlightItem : MonoBehaviour
{
	TutorialTextScript textScript;
	Light2D lightSource;
	Vine vineScript;
	EarthMound earthMoundScript;
	Log logScript;

    void Start()
    {
		lightSource = gameObject.GetComponentInChildren<Light2D>();
		textScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
		//vineScript = GameObject.FindObjectOfType(typeof(Vine)) as Vine;
		//earthMoundScript = GameObject.FindObjectOfType(typeof(EarthMound)) as EarthMound;
		//logScript = GameObject.FindObjectOfType(typeof(Log)) as Log;
    }

	void OnTriggerEnter2D(Collider2D collision)
	{	
		if (textScript.tutorialHighLight)
		{
			switch (transform.tag)
			{
				case "Tourch":
					lightSource.color = Color.white;
					textScript.typeOfItem = transform.tag;
					break;
					
				case "Seed":
				earthMoundScript = GetComponent<EarthMound>();
				if (!earthMoundScript.taken)
					{
						lightSource.intensity = 2f;
						textScript.typeOfItem = transform.tag;
					}
					break;
					
				case "Log":
				logScript = GetComponent<Log>();
					if (!logScript.taken)
					{
						lightSource.intensity = 2f;
						textScript.typeOfItem = transform.tag;
					}
					break;
					
				case "Vine":
					vineScript = GetComponent<Vine>();
					if (!vineScript.taken)
					{
						lightSource.intensity = 2f;
						textScript.typeOfItem = transform.tag;
					}
					break;
			}
		}
    }
    

    void OnTriggerExit2D(Collider2D collision)
    {
		textScript.typeOfItem = "";
		
		if (transform.tag != "Tourch")
		{
			lightSource.intensity = 0f;
		} else {
			lightSource.color = new Color(1f, 0.7305389f, 0);
		}
    }
}
