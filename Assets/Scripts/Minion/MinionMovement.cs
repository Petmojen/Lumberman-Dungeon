using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    GameObject playerPosition;
    Rigidbody2D rgbd2D;
    float speed = 3;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 forcedDirection = playerPosition.transform.position - transform.position;
        forcedDirection = forcedDirection.normalized;

        rgbd2D.velocity = forcedDirection * speed;
    }
}
