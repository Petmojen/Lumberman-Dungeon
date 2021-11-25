using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour
{
	PlayerAttack playerAttackScript;
	GameObject findPlayer;
	[SerializeField] Transform rotateSprite2;
	float rotateSpeed = 4f;
	int rotateSteps = 100;
	public int i = 0;
	int rotateDirection = 1;
    Rigidbody2D rgbd2D;

    // Start is called before the first frame update
    void Start()
    {
		findPlayer = GameObject.FindGameObjectWithTag("Player");
		playerAttackScript = findPlayer.GetComponent<PlayerAttack>();
		rgbd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (i < rotateSteps)
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
			playerAttackScript.axeinAttack = false;
			Destroy(gameObject);

		}

    }
}
