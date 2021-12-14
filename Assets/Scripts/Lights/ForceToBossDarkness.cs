using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ForceToBossDarkness : MonoBehaviour
{
    [SerializeField] GameObject bossRoomLighting;
    [SerializeField] Timer timerScript;
    CircleCollider2D circleOfDeath;
    Light2D lightScript;
    public bool ability;
    float speed = 4f;


    void Start()
    {
        circleOfDeath = GetComponent<CircleCollider2D>();
        lightScript = GetComponent<Light2D>();
    }

    void Update()
    {
        if(!ability && lightScript.pointLightOuterRadius < 13.4f)
        {
            lightScript.pointLightOuterRadius = 13.45f;
            circleOfDeath.radius = 13.45f;
            circleOfDeath.enabled = true;
            lightScript.enabled = true;
        }

        if(timerScript.timeOut && !ability && lightScript.pointLightOuterRadius > 13.5f)
        {
            lightScript.pointLightOuterRadius -= Time.deltaTime * speed;
            circleOfDeath.radius -= Time.deltaTime * speed;
        }

        if(ability && lightScript.pointLightOuterRadius > 0.1f)
        {
            lightScript.pointLightOuterRadius -= Time.deltaTime * (speed * 2);
            circleOfDeath.radius -= Time.deltaTime * (speed * 2);
        } else if(ability && lightScript.pointLightOuterRadius < 0.2f) {
            circleOfDeath.enabled = false;
            lightScript.enabled = false;
        }

        //if(lightScript.pointLightOuterRadius < 13.6f)
        //{
        //    circleOfDeath.enabled = false;
        //    lightScript.enabled = false;
        //    bossRoomLighting.SetActive(true);
        //}
    }
}
