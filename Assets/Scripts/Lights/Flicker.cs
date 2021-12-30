using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flicker : MonoBehaviour
{
	Light2D getLight;
    float flickerValue = 1f, flickCounter = 0f;

    void Start()
    {
		getLight = GetComponent<Light2D>();
    }

    void Update()
    {
		flickCounter = Random.Range(1f, 301f);
        if(flickCounter >= 300f)
        {
            flickerValue = 7f;
        } else {
			flickerValue = 0f;
        }

        getLight.intensity = flickerValue;
    }
}
