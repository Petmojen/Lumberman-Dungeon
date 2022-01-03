using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafAttack : MonoBehaviour
{
	Rigidbody2D rgbd2D;
	float speed = 4f;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
		int randomDirection = Random.Range(-3, 4);
		rgbd2D.velocity = new Vector2((randomDirection * speed) / 3, -speed);
    }

	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall") || collision.CompareTag("PlantedTree"))
		{
			Destroy(gameObject);
		}
	}
}
