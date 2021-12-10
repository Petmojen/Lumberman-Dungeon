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
    // Start is called before the first frame update
    void Start()
    {
        debuggerScript = GameObject.FindObjectOfType(typeof(Debugger)) as Debugger;
		text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 || debuggerScript.timerZero)
		{
            timeLeft = 0;
			timeOut = true;
			debuggerScript.timerZero = false;
		}
		string secwithTwoDigits;
		float seconds = Mathf.Floor(timeLeft % 60);
		secwithTwoDigits = seconds.ToString("00");
        text.text = "Timer: " + Mathf.Floor(timeLeft / 60f) + ":" + secwithTwoDigits;
        
    }
}
