using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 150f;
	public bool timeOut = false;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
		{
            timeLeft = 0;
			timeOut = true;
		}
		string secwithTwoDigits;
		float seconds = Mathf.Floor(timeLeft % 60);
		secwithTwoDigits = seconds.ToString("00");
        text.text = "Timer: " + Mathf.Floor(timeLeft / 60f) + ":" + secwithTwoDigits;
        
    }
}
