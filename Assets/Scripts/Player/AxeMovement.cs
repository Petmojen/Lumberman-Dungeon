using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement:MonoBehaviour
{
    [SerializeField] string[] antiCollision;
    Transform rotateSprite;
    float flyingSpeed = 10;
    Rigidbody2D rgbd2D;


    void Start()
    {
        rotateSprite = GetComponentInChildren<Transform>();
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rotateSprite.Rotate(0, 0, -flyingSpeed / 1.5f);
    }

    void bounceBack()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < antiCollision.Length; i++)
        {
            if(collision.CompareTag(antiCollision[i]))
            {
                bounceBack();
            }
        }
    }

}
