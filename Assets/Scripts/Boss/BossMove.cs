using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
	float movementSpeed = 0.5f;
	int i, xDir, yDir;
	Rigidbody2D rgbd2D;
	public Timer timerScript;	
	
    // Start is called before the first frame update
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
		i = 0;
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
    }

    // Update is called once per frame
    void Update()
    {
		if (timerScript.timeOut)
		{	
			if (i < 20)
			{
				xDir = 1; yDir = 1;
			}
			if (i >= 20 && i < 40)
			{
				xDir = -1; yDir = -1;
			}
			if (i >= 40 && i < 60)
			{
				xDir = -1; yDir = 1;
			}
			if (i >= 60 && i < 80)
			{
				xDir = 1; yDir = -1;
			}
			if (i >= 80)
			{
				i = 0;
			}
		
			rgbd2D.velocity = new Vector2(movementSpeed * xDir, movementSpeed * yDir);
			i++;
		}
    }
}
