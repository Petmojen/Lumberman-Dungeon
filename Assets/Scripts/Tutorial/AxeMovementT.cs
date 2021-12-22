using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovementT:MonoBehaviour
{
    public bool backToPlayer = false, flipSpriteBool = false;
    [SerializeField] string[] antiCollision;
    [SerializeField] Transform rotateSprite;
	PlayerMovementT playerMovementScript;
    GameObject playerPosition;
    SpriteRenderer flipSprite;
    float flyingSpeed = 10;
    Rigidbody2D rgbd2D;

    void Start()
    {
        flipSprite = rotateSprite.GetComponent<SpriteRenderer>();
        flipSprite.flipY = flipSpriteBool;

        playerPosition = GameObject.FindGameObjectWithTag("Player");
        rgbd2D = GetComponent<Rigidbody2D>();
		playerMovementScript = GameObject.FindObjectOfType(typeof(PlayerMovementT)) as PlayerMovementT; 
    }

    void Update()
    {

        if(Vector2.Distance(playerPosition.transform.position, transform.position) > 5)
        {
            backToPlayer = true;
			playerMovementScript.axeAttack = PlayerMovementT.Attack.AxeReturning;
        } 

        if(backToPlayer && Vector2.Distance(playerPosition.transform.position, transform.position) > 1.5f)
        {
            if(flipSpriteBool)
            {
                rotateSprite.Rotate(0, 0, flyingSpeed / 2f);
            } else {
                rotateSprite.Rotate(0, 0, -flyingSpeed / 2f);
            }
            Vector2 lookDirection = (Vector2)playerPosition.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rgbd2D.velocity = transform.right * flyingSpeed;
        } else {
            if(flipSpriteBool)
            {
                rotateSprite.Rotate(0, 0, flyingSpeed / 2f);
            } else {
                rotateSprite.Rotate(0, 0, -flyingSpeed / 2f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall") || collision.CompareTag("Boss"))
        {
            backToPlayer = true;
			playerMovementScript.axeAttack = PlayerMovementT.Attack.AxeReturning;
        }
    }
}