using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
	bool hitCooldown = false;
    int bossHp = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !hitCooldown)
        {
			bossHp--;
			hitCooldown = true;
			Debug.Log(bossHp);
			if (bossHp == 0)
			{
				Destroy(gameObject);
			}
        }
		Invoke(nameof(BossHitCooldown), 0.5f);
    }
	void BossHitCooldown()
	{
		hitCooldown = false;
	}
	
}
