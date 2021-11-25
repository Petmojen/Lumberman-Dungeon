using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttack : MonoBehaviour
{
	PlayerAttack playerAttackScript;
	float rotateSpeed = 3f;
	GameObject findPlayer;

    void Start()
    {
		findPlayer = GameObject.FindGameObjectWithTag("Player");
		playerAttackScript = findPlayer.GetComponent<PlayerAttack>();
        transform.Rotate(0, 0, 45);
    }

    void Update()
    {
        transform.Rotate(0, 0, -rotateSpeed);
        Invoke(nameof(DestroyAxe), 0.25f);
    }

    void DestroyAxe()
    {
        playerAttackScript.usingAxe = false;
        Destroy(gameObject);
    }
}
