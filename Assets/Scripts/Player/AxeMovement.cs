using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement:MonoBehaviour
{
    [SerializeField] string[] antiCollision;
    [SerializeField] Transform rotateSprite;
    bool backToPlayer = false;
    float flyingSpeed = 10;
    Rigidbody2D rgbd2D;


    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if(backToPlayer)
        {
            rotateSprite.Rotate(0, 0, flyingSpeed / 2f);
            GameObject playerPos = GameObject.FindGameObjectWithTag("Player");
            Vector2 lookDirection = (Vector2)playerPos.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rgbd2D.velocity = transform.right * flyingSpeed;
        } else {
            rotateSprite.Rotate(0, 0, -flyingSpeed / 1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            backToPlayer = true;
        }
    }

}
