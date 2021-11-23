using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour 
{
    float movementSpeed = 5;
    Vector2 position;
    Rigidbody2D rgbd2D;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        position.x = Input.GetAxis("Horizontal") * movementSpeed;
        position.y = Input.GetAxis("Vertical") * movementSpeed;

        rgbd.velocity = new Vector2(position.x, position.y);
    }
}
