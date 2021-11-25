using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour
{
	
	[SerializeField] Transform rotateSprite2;
	float rotateSpeed = 2f;
	int rotateSteps = 200;
	public int i = 0;
	int rotateDirection = 1;
	bool attackStart = true;
    Rigidbody2D rgbd2D;
	
    // Start is called before the first frame update
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (i < rotateSteps && attackStart)
		{			
			rotateSprite2.Rotate(0, 0, -rotateSpeed * rotateDirection);
			i++;
		}

		if (i == rotateSteps / 2)
		{
			rotateDirection = -rotateDirection;
		}
		if (i == rotateSteps)
		{
			Destroy (rotateSprite2, 0.1f);
		}
		
    }
}
