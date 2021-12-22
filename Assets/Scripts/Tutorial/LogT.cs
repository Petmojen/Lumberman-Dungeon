using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LogT : MonoBehaviour
{
	TutorialTextScript textScript;
	Light2D lightSource;
	
    [SerializeField] Sprite takenLog;
    SpriteRenderer changeSprite;
    public bool taken;

    void Start()
    {
        changeSprite = GetComponent<SpriteRenderer>();
		lightSource = gameObject.GetComponentInChildren<Light2D>();
		textScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
    }

    public void ChangeSprite()
    {
        changeSprite.sprite = takenLog;
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
