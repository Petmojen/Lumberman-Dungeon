using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager:MonoBehaviour
{
    InventorySystem inventoryScript;
    PlayerMovement movementScript;
    PlayerHpSystem healthScript;
    PlayerAttack attackScript;

    string currentState, holdIdleState = "idle_Side", holdDashState = "dash_Side", walkingState = "walking_Side", meleeHoldState = "melee_Side";
    SpriteRenderer flipSprite;
    Animator animator;

    bool noAxe, melee;

    void Start()
    {
        flipSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        inventoryScript = GetComponent<InventorySystem>();
        movementScript = GetComponent<PlayerMovement>();
        healthScript = GetComponent<PlayerHpSystem>();
        attackScript = GetComponent<PlayerAttack>();
    }

    void Update()
    {

        if(movementScript.axeAttack == PlayerMovement.Attack.Throw || movementScript.axeAttack == PlayerMovement.Attack.AxeReturning)
        {
            noAxe = true;
        } else {
            noAxe = false;
        }

        if(movementScript.axeAttack == PlayerMovement.Attack.Melee)
        {
            melee = true;
            MeleeAngle();
        }

        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && melee)
        {
            Debug.Log("Animation ended");
            melee = false;
        }

        if(movementScript.playerPosition.x > 0)
        {
            walkingState = "walking_Side";
            holdIdleState = "idle_Side";
            holdDashState = "dash_Side";
            flipSprite.flipX = false;
        } else if(movementScript.playerPosition.x < 0 && !movementScript.rootSnared)
        {
            walkingState = "walking_Side";
            holdIdleState = "idle_Side";
            holdDashState = "dash_Side";
            flipSprite.flipX = true;
        } else if(movementScript.playerPosition.y > 0 && movementScript.playerPosition.x == 0)
        {
            walkingState = "walking_Up";
            holdIdleState = "idle_Up";
            holdDashState = "dash_Up";
            flipSprite.flipX = false;
        } else if(movementScript.playerPosition.y < 0 && movementScript.playerPosition.x == 0)
        {
            walkingState = "walking_Down";
            holdIdleState = "idle_Down";
            holdDashState = "dash_Down";
            flipSprite.flipX = false;
        }

        if(movementScript.playerPosition.magnitude > 0 && !movementScript.rootSnared && !melee)
        {
            ChangeAnimationState("Walking");
        } else if(movementScript.playerPosition.magnitude == 0 && !movementScript.rootSnared && !melee) {
            ChangeAnimationState("Idle");
        } else if(movementScript.rootSnared) {
            ChangeAnimationState("Snared");
        }
    }

    void ChangeAnimationState(string newState)
    {
        string undoWalkState = walkingState, undoIdleState = holdIdleState, undoDashState = holdDashState;

        if(currentState == newState && movementScript.rootSnared) return;

        //Check two factor authentication for valid animation
        //Debug.Log(noAxe + " | " + inventoryScript.torchUsing);

        //Axe & Torch
        if(!noAxe && inventoryScript.torchUsing)
        {
            string addToState = "_Torch";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
        }

        //No axe & Torch
        else if(noAxe && inventoryScript.torchUsing)
        {
            string addToState = "_Torch_Noaxe";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
        }

        //No axe & no Torch
        else if(noAxe && !inventoryScript.torchUsing)
        {
            string addToState = "_Noaxe_Notorch";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
        }


        if(newState == "Walking" && !movementScript.dashing)
        {
            animator.Play(walkingState);
        } else if(newState == "Idle" && !movementScript.dashing) {
            animator.Play(holdIdleState);
        } else if(newState == "Dash") {
            animator.Play(holdDashState);
        } else if(newState == "Snared") {
            animator.Play("snare_Start");
            Invoke(nameof(SnareIdle), 1f);
        }

        currentState = newState;

        walkingState = undoWalkState;
        holdIdleState = undoIdleState;
        holdDashState = undoDashState;
    }

    void MeleeAngle()
    {
        if(movementScript.angle < 45 && movementScript.angle > -45)
        {
            //Right
            animator.Play("melee_Side");
            flipSprite.flipX = false;
        } else if(movementScript.angle > 45 && movementScript.angle < 135)
        {
            //Up
            animator.Play("melee_Up");
            flipSprite.flipX = false;
        } else if(movementScript.angle < -45 && movementScript.angle > -135)
        {
            //Down
            animator.Play("melee_Down");
            flipSprite.flipX = false;
        } else if(movementScript.angle > 135 || movementScript.angle < -135)
        {
            //Left
            animator.Play("melee_Side");
            flipSprite.flipX = true;
        }
    }

    void CancelMelee()
    {
        melee = false;
        CancelInvoke();
    }

    void SnareIdle()
    {
        animator.Play("snare_Idle");
        CancelInvoke();
        Invoke(nameof(ReleaseSnare), 2f);
    }

    void ReleaseSnare()
    {
        animator.Play("snare_Release");
        CancelInvoke();
    }
}
