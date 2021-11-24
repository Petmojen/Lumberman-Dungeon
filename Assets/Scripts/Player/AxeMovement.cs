using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement:MonoBehaviour
{
    [SerializeField] string[] antiCollision;
    float flyingSpeed = 10;
    Rigidbody2D rgbd2D;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Rotate(0, 0, -flyingSpeed / 2);
    }

    void bounceBack()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < antiCollision.Length; i++)
        {
            if(collision.gameObject.tag != antiCollision[i])
            {
                bounceBack();
            }
        }
    }

}
