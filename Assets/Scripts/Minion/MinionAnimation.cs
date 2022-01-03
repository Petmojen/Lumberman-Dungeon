using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimation:MonoBehaviour
{
    MinionMovement movementScript;
    MinionHpSystem healthScript;
    GameObject playerPosition;
    SpriteRenderer flipSprite;
    Rigidbody2D rgbd2D;
    Animator animator;

    string currentState, holdFightState, walkingState, holdDeadState, holdKnockState;
    public bool spawning;
    float angle;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        movementScript = GetComponent<MinionMovement>();
        healthScript = GetComponent<MinionHpSystem>();
        flipSprite = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawning = true;

        Vector3 playerAngle = playerPosition.transform.position - transform.position;
        angle = Mathf.Atan2(playerAngle.y, playerAngle.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        if(!spawning && !healthScript.isDead && !healthScript.knockBack)
        {
            if(!movementScript.punching)
            {
                Vector3 playerAngle = playerPosition.transform.position - transform.position;
                angle = Mathf.Atan2(playerAngle.y, playerAngle.x) * Mathf.Rad2Deg;
                if(angle < 45 && angle > -45)
                {
                    // Right
                    walkingState = "walk_Side";
                    holdFightState = "fight_Side";
                    holdDeadState = "dead_Side";
                    holdKnockState = "hit_Side";
                    flipSprite.flipX = false;
                } else if(angle > 45 && angle < 135) {
                    //Up
                    walkingState = "walk_Up";
                    holdFightState = "fight_Up";
                    holdDeadState = "dead_Up";
                    holdKnockState = "hit_Up";
                    flipSprite.flipX = false;
                } else if(angle < -45 && angle > -135) {
                    //Down
                    walkingState = "walk_Down";
                    holdFightState = "fight_Down";
                    holdDeadState = "dead_Down";
                    holdKnockState = "hit_Down";
                    flipSprite.flipX = false;
                } else if(angle > 135 || angle < -135) {
                    //Left
                    walkingState = "walk_Side";
                    holdFightState = "fight_Side";
                    holdDeadState = "dead_Side";
                    holdKnockState = "hit_Side";
                    flipSprite.flipX = true;
                }
            }

            if(!movementScript.punching)
            {
                ChangeAnimationState("Walking");
            } else if(movementScript.punching)
            {
                ChangeAnimationState("Fight");
            }

        } else if(spawning && !healthScript.isDead && !healthScript.knockBack) {
            if(angle > 135 || angle < -135)
            {
                flipSprite.flipX = true;
            }

            ChangeAnimationState("Spawn");
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                spawning = false;
            }

        } else if(healthScript.knockBack && !healthScript.isDead) {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                healthScript.knockBack = false;
            }
            ChangeAnimationState("Knock");

        } else if(healthScript.isDead) {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && healthScript.isDead)
            {
                Destroy(gameObject);
            }
            ChangeAnimationState("dead");
        }
    }

    void ChangeAnimationState(string newState)
    {
        if(newState == "Walking")
        {
            animator.Play(walkingState);
        } else if(newState == "Fight") {
            animator.Play(holdFightState);
        } else if(newState == "Spawn") {
            animator.Play("spawn");
        } else if(newState == "Knock") {
            animator.Play(holdKnockState);
        } else if(newState == "dead") {
            animator.Play(holdDeadState);
        }
        currentState = newState;
    }
}
