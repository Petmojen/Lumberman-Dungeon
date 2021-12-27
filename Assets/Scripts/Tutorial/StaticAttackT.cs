using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttackT : MonoBehaviour
{
	PlayerMovementT playerMovementScriptT;
	float rotateSpeed = 3f;
    Rigidbody2D rgbd2D;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, 45);
		playerMovementScriptT = GameObject.FindObjectOfType(typeof(PlayerMovementT)) as PlayerMovementT;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -rotateSpeed);
        Invoke(nameof(DestroyAxe), 0.4f);
    }

    void DestroyAxe()
    {
        playerMovementScriptT.axeAttack = PlayerMovementT.Attack.Idle;
        Destroy(gameObject);
    }
}
