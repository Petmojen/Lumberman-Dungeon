using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    [SerializeField] Rigidbody2D rb;

    private ShootingObject shootingScript; 

    void Start()
    {
        rb.velocity = transform.right * -1 * speed;

        //ShootingObject shootingScript = gameObject.getComponent
    }

    void Update()
    {

    }
}