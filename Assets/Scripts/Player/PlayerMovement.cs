using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour 
{
    [SerializeField] GameObject offsetRotation;
    float movementSpeed = 5;
    Rigidbody2D rgbd2D;
    Vector2 position;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        position.x = Input.GetAxis("Horizontal") * movementSpeed;
        position.y = Input.GetAxis("Vertical") * movementSpeed;

        if(position.x > 0)
        {
            offsetRotation.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if(position.x < 0) {
            offsetRotation.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if(position.y > 0)
        {
            offsetRotation.transform.rotation = Quaternion.Euler(0, 0, 90);
        } else if(position.y < 0) {
            offsetRotation.transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        rgbd2D.velocity = new Vector2(position.x, position.y);
    }
}
