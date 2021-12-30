using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement:MonoBehaviour
{
    string currentState, holdFightState, walkingState;
    public Vector2 punchDirection;
    SpriteRenderer flipSprite;
    GameObject playerPosition;
    float speed = 3, angle;
    Rigidbody2D rgbd2D;
    Animator animator;
    bool punching;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        flipSprite = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 forcedDirection = playerPosition.transform.position - transform.position;
        angle = Mathf.Atan2(forcedDirection.y, forcedDirection.x) * Mathf.Rad2Deg;
        if(!punching)
        {
            if(angle < 45 && angle > -45)
            {
                // Right
                walkingState = "walk_Side";
                holdFightState = "fight_Side";
                flipSprite.flipX = false;
            } else if(angle > 45 && angle < 135) {
                //Up
                walkingState = "walk_Up";
                holdFightState = "fight_Up";
                flipSprite.flipX = false;
            } else if(angle < -45 && angle > -135) {
                //Down
                walkingState = "walk_Down";
                holdFightState = "fight_Down";
                flipSprite.flipX = false;
            } else if(angle > 135 || angle < -135) {
                //Left
                walkingState = "walk_Side";
                holdFightState = "fight_Side";
                flipSprite.flipX = true;
            }
        }

        if(Vector2.Distance(transform.position, playerPosition.transform.position) > 3f && !punching)
        {
            forcedDirection = forcedDirection.normalized;
            rgbd2D.velocity = forcedDirection * speed;
            ChangeAnimationState("Walking");
        } else {
            if(!punching)
            {
                punching = true;
                ChangeAnimationState("Fight");
            }
        }

    }

    void ChangeAnimationState(string newState)
    {

        if(newState == "Walking")
        {
            animator.Play(walkingState);
        } else if(newState == "Fight") {
            rgbd2D.velocity = Vector2.zero;
            animator.Play(holdFightState);
            punchDirection = playerPosition.transform.position - transform.position;
            Invoke(nameof(Punch), 0.25f);
        }

        currentState = newState;
    }

    void Punch()
    {
        rgbd2D.velocity = punchDirection * 5;
        Invoke(nameof(Idle), 0.5f);
    }

    void Idle()
    {
        animator.Play(walkingState);
        punching = false;
    }
}
