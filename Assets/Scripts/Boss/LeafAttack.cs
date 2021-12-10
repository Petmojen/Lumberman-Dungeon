using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafAttack : MonoBehaviour
{
	[SerializeField] Rigidbody2D rbd;
	float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
		int randomDirection = Random.Range(-1, 2);
		rbd.velocity = new Vector3((randomDirection * speed) / 3, -1 * speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
