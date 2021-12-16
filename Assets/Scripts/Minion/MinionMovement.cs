using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    string currentState, holdFightState, walkingState;
    SpriteRenderer flipSprite;
    GameObject playerPosition;
    float speed = 3, angle;
    Rigidbody2D rgbd2D;
    Animator animator;

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
        if(angle < 45 && angle > -45)
        {
            // Right
            walkingState = "walk_Side";
            flipSprite.flipX = false;
        } else if(angle > 45 && angle < 135) {
            //Up
            walkingState = "walk_Up";
            flipSprite.flipX = false;
        } else if(angle < -45 && angle > -135) {
            //Down
            walkingState = "walk_Down";
            flipSprite.flipX = false;
        } else if(angle > 135 || angle < -135) {
            //Left
            walkingState = "walk_Side";
            flipSprite.flipX = true;
        }

        if(Vector2.Distance(transform.position, playerPosition.transform.position) > 2)
        {
            ChangeAnimationState("Walking");
        } else {
            ChangeAnimationState("Fight");
        }

        forcedDirection = forcedDirection.normalized;

        rgbd2D.velocity = forcedDirection * speed;
    }

    void ChangeAnimationState(string newState)
    {

        if(newState == "Walking")
        {
            animator.Play(walkingState);
        } else if(newState == "Fight") {
            animator.Play(holdFightState);
        }

        currentState = newState;
    }
}
