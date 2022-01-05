using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchLightFlicker:MonoBehaviour
{
    [SerializeField] GameObject sparks, smoke;
    ForceToBossDarkness darknessRadius;
    BoxCollider2D boxCollider2D;
    GameObject centerPoint;
    Light2D getLightScript;
    Animator animator;

    public float flickerValue = 1f, topTarget, floorTarget;
    public bool flick, isActive = true;

    void Start()
    {
        centerPoint = GameObject.FindGameObjectWithTag("BossRoom");
        darknessRadius = FindObjectOfType<ForceToBossDarkness>();
        boxCollider2D = GetComponentInParent<BoxCollider2D>();
        animator = GetComponentInParent<Animator>();
        getLightScript = GetComponent<Light2D>();
        topTarget = Random.Range(1.25f, 1.5f);
        floorTarget = Random.Range(0.75f, 1f);
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, centerPoint.transform.position) + 2.5f > darknessRadius.radiusOfLight) isActive = false;

        if(isActive)
        {
            if(flick)
            {
                flickerValue -= Time.deltaTime;
            } else {
                flickerValue += Time.deltaTime;
            }

            if(flickerValue >= topTarget)
            {
                floorTarget = Random.Range(0.75f, 1f);
                flick = true;
            } else if(flickerValue <= floorTarget) {
                topTarget = Random.Range(1.25f, 1.5f);
                flick = false;
            }
        } else {
            sparks.SetActive(false);
            smoke.SetActive(false);
            boxCollider2D.enabled = isActive;
            if(flickerValue > 0) flickerValue -= Time.deltaTime;
            if(flickerValue <= 0)
            {
                getLightScript.enabled = isActive;
                animator.Play("dead");
            }
        }

        getLightScript.intensity = flickerValue;
    }
}
