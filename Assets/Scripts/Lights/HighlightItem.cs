using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HighlightItem : MonoBehaviour
{
	Light2D lightSource;
	
    void Start()
    {
		lightSource = gameObject.GetComponentInChildren<Light2D>();
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		lightSource.intensity = 2f;
    }
    

    void OnTriggerExit2D(Collider2D collision)
    {
		lightSource.intensity = 0f;
    }
}
