using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafAttack : MonoBehaviour
{
	[SerializeField] Rigidbody2D rbd;
	float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
		int randomDirection = Random.Range(-3, 4);
		rbd.velocity = new Vector3((randomDirection * speed) / 3, -speed, 0);
		BossSFX.PlaySound("LeafAttack");
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}
}
