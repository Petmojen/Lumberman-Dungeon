using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    [SerializeField] Sprite spriteRight, spriteLeft, spriteUp, spriteDown;
    float movementSpeed = 5, angle;
    Vector2 playerPosition;
    bool attacking = false;
    Rigidbody2D rgbd2D;

    //temp until we gen animations
    SpriteRenderer changeSprite;

    void Start()
    {
        changeSprite = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            LookAtMouse();
        }
        if(!attacking)
        {
            playerPosition.x = Input.GetAxis("Horizontal");
            playerPosition.y = Input.GetAxis("Vertical");

            if(playerPosition.x > 0)
            {
                changeSprite.sprite = spriteRight;
            } else if(playerPosition.x < 0) {
                changeSprite.sprite = spriteLeft;
            }

            if(playerPosition.y > 0)
            {
                changeSprite.sprite = spriteUp;
            } else if(playerPosition.y < 0) {
                changeSprite.sprite = spriteDown;
            }
            
            rgbd2D.velocity = new Vector2(playerPosition.x * movementSpeed, playerPosition.y * movementSpeed);
        }
    }

    void LookAtMouse()
    {
        attacking = true;
        rgbd2D.velocity = new Vector2(0, 0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        if(lookDirection.x < 0)
        {
            changeSprite.sprite = spriteLeft;
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 180;
        } else
        {
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            changeSprite.sprite = spriteRight;
        }
        rgbd2D.rotation = angle;
        Invoke(nameof(DeactivateAttack), 0.2f);
    }

    void DeactivateAttack()
    {
        attacking = false;
        rgbd2D.rotation = 0;
        CancelInvoke();
    }
}
