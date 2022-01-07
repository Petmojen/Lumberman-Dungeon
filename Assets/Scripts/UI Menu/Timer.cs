using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 180f;
	public bool timeOut = false;
	Debugger debuggerScript;
    Text text;
	TutorialTextScript tutorialTextScript;

    void Start()
    {
        debuggerScript = GameObject.FindObjectOfType(typeof(Debugger)) as Debugger;
		text = GetComponent<Text>();
		tutorialTextScript = GameObject.FindObjectOfType(typeof(TutorialTextScript)) as TutorialTextScript;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 || debuggerScript.timerZero)
		{
            timeLeft = 0;
			timeOut = true;
			debuggerScript.timerZero = false;
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
