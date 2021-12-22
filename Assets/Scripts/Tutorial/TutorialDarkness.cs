using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class TutorialDarkness : MonoBehaviour
{
    [SerializeField] GameObject bossRoomLighting;
    [SerializeField] Timer timerScript;
	TutorialTextScript tutorialTextScript;
    public float radiusOfLight = 92f, testty;
    CircleCollider2D circleOfDeath;
    Light2D lightScript;
    float speed = 4f;


    void Start()
    {
		tutorialTextScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
        circleOfDeath = GetComponent<CircleCollider2D>();
        lightScript = GetComponent<Light2D>();
    }

    void Update()
    {

        if(timerScript.timeOut && lightScript.pointLightOuterRadius > 13.5f)
        {
            radiusOfLight -= Time.deltaTime * speed;
            lightScript.pointLightOuterRadius = radiusOfLight;
            circleOfDeath.radius -= Time.deltaTime * speed;
        }
		testty = transform.position.y;
		if (tutorialTextScript.tutorialStep == 1 && transform.position.y <= -111.5f)
		{
			lightScript.pointLightOuterRadius = 21f;
			circleOfDeath.radius = 21f;

			transform.position = transform.position + new Vector3(0f, 0.05f, 0);
		}
				if (tutorialTextScript.tutorialStep == 3 && transform.position.y <= -90.5f)
		{
			lightScript.pointLightOuterRadius = 21f;
			circleOfDeath.radius = 21f;

			transform.position = transform.position + new Vector3(0f, 0.05f, 0);
		}

        //if(lightScript.pointLightOuterRadius < 13.6f)
        //{
        //    circleOfDeath.enabled = false;
        //    lightScript.enabled = false;
        //    bossRoomLighting.SetActive(true);
        //}
    }
}