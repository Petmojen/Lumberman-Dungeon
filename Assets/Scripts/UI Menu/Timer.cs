using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timeLeft = 120f;
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
        text.text = "Timer: " + Mathf.Floor(timeLeft / 60f) + ":" + Mathf.Round(timeLeft % 60);
        
    }
}
