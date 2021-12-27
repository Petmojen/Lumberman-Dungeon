using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovementT:MonoBehaviour
{
    public bool backToPlayer = false, flipSpriteBool = false;
    [SerializeField] string[] antiCollision;
	PlayerMovementT playerMovementScriptT;
    GameObject playerPosition;
    SpriteRenderer flipSprite;
    float flyingSpeed = 10;
    Rigidbody2D rgbd2D;

    void Start()
    {

        rgbd2D = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
		playerMovementScriptT = GameObject.FindObjectOfType(typeof(PlayerMovementT)) as PlayerMovementT; 
    }

    void Update()
    {

        if(Vector2.Distance(playerPosition.transform.position, transform.position) > 5)
        {
            backToPlayer = true;
			playerMovementScriptT.axeAttack = PlayerMovementT.Attack.AxeReturning;
        } 

        if(backToPlayer && Vector2.Distance(playerPosition.transform.position, transform.position) > 1.5f)
        {
            Vector2 lookDirection = (Vector2)playerPosition.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rgbd2D.velocity = transform.right * flyingSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall") || collision.CompareTag("Boss"))
        {
            backToPlayer = true;
			playerMovementScriptT.axeAttack = PlayerMovementT.Attack.AxeReturning;
        }
    }
}
