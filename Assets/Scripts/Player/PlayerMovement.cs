using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    float movementSpeed = 5, angle;
    Vector2 playerPosition;
    Rigidbody2D rgbd2D;

    //temp until we gen animations
    SpriteRenderer spriteFlip;

    void Start()
    {
        spriteFlip = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerPosition.x = Input.GetAxis("Horizontal") * movementSpeed;
        playerPosition.y = Input.GetAxis("Vertical") * movementSpeed;

        if(playerPosition.x > 0)
        {
            spriteFlip.flipX = false;
        } else if(playerPosition.x < 0) {
            spriteFlip.flipX = true;
        }

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            rgbd2D.velocity = Vector2.zero;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDirection = mousePos - (Vector2)transform.position;
            if(lookDirection.x < 0)
            {
                spriteFlip.flipX = true;
                angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 180;
            } else {
                angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                spriteFlip.flipX = false;
            }
            rgbd2D.rotation = angle;
        }

        rgbd2D.velocity = new Vector2(playerPosition.x, playerPosition.y);
    }
}
