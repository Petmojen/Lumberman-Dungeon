using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ForceToBossDarkness : MonoBehaviour
{
    [SerializeField] GameObject bossRoomLighting;
    [SerializeField] Timer timerScript;
    public float radiusOfLight = 92f;
    CircleCollider2D circleOfDeath;
    Light2D lightScript;
    float speed = 4f;


    void Start()
    {
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

        //if(lightScript.pointLightOuterRadius < 13.6f)
        //{
        //    circleOfDeath.enabled = false;
        //    lightScript.enabled = false;
        //    bossRoomLighting.SetActive(true);
        //}
    }
}
