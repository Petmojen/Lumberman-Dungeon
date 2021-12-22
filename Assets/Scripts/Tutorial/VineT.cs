using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class VineT : MonoBehaviour
{
	TutorialTextScript textScript;
	Light2D lightSource;
	
    Animator animator;
    public bool taken;

    void Start()
    {
        animator = GetComponent<Animator>();
		lightSource = gameObject.GetComponentInChildren<Light2D>();
		textScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
    }

    public void ChangeSprite()
    {
        animator.Play("taken");
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