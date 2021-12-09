using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour
{
	PlayerMovement playerMovementScript;
	float rotateSpeed = 3f;
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
        transform.Rotate(0, 0, 45);
		playerMovementScript = GameObject.FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -rotateSpeed);
        Invoke(nameof(DestroyAxe), 0.4f);
    }

    void DestroyAxe()
    {
        playerMovementScript.axeAttack = PlayerMovement.Attack.Idle;
        Destroy(gameObject);
    }
}
