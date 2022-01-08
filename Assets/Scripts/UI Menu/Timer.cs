using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 180f;
	public bool timeOut = false;
    Text text;
	TutorialTextScript tutorialTextScript;

    void Start()
    {
		text = GetComponent<Text>();
		tutorialTextScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
		{
            timeLeft = 0;
			timeOut = true;
		}

		if (timeLeft <= 30)
		{
			text.color = Color.red;
			text.fontSize = 25;
		}
		string secwithTwoDigits;
		float seconds = Mathf.Floor(timeLeft % 60);
		secwithTwoDigits = seconds.ToString("00");
        text.text = Mathf.Floor(timeLeft / 60f) + ":" + secwithTwoDigits;
        
    }
}
