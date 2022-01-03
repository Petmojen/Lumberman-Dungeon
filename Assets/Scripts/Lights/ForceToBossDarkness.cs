using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ForceToBossDarkness : MonoBehaviour
{
    //[SerializeField] GameObject minimapCircle;
    [SerializeField] Timer timerScript;

    Light2D lightScript;//, minimapLightScript;
    CircleCollider2D circleOfDeath;
    public float radiusOfLight = 92f;
    float speed = 50f;


    void Start()
    {
        circleOfDeath = GetComponent<CircleCollider2D>();
        lightScript = GetComponent<Light2D>();
        //minimapLightScript = minimapCircle.GetComponent<Light2D>();
    }

    void Update()
    {

        if(timerScript.timeOut && lightScript.pointLightOuterRadius > 13.5f)
        {
            radiusOfLight -= Time.deltaTime * speed;
            lightScript.pointLightOuterRadius = radiusOfLight;
            //minimapLightScript.pointLightOuterRadius = radiusOfLight;
            circleOfDeath.radius = radiusOfLight;
        }
    }
}
