using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerT:MonoBehaviour
{
    InventorySystem inventoryScript;
    PlayerMovement movementScript;
    PlayerHpSystem healthScript;
    PlayerAttack attackScript;

    string currentState, holdIdleState = "idle_Side", holdDashState = "dash_Side", walkingState = "walking_Side", meleeHoldState = "melee_Side", holdDeathState = "death";
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

        if(healthScript.isDead)
        {
            ChangeAnimationState("Death");
        } else
        {

            if(movementScript.axeAttack == PlayerMovement.Attack.Throw || movementScript.axeAttack == PlayerMovement.Attack.AxeReturning)
            {
                noAxe = true;
            } else
            {
                noAxe = false;
            }

            if(movementScript.axeAttack == PlayerMovement.Attack.Melee)
            {
                melee = true;
                MeleeAngle();
            }

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && melee)
            {
                attackScript.EndMelee();
                melee = false;
            }

            if(movementScript.playerPosition.x > 0 && !melee)
            {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                holdDashState = "dash_Side";
                flipSprite.flipX = false;
            } else if(movementScript.playerPosition.x < 0 && !movementScript.rootSnared && !melee)
            {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                holdDashState = "dash_Side";
                flipSprite.flipX = true;
            } else if(movementScript.playerPosition.y > 0 && movementScript.playerPosition.x == 0 && !melee)
            {
                walkingState = "walking_Up";
                holdIdleState = "idle_Up";
                holdDashState = "dash_Up";
                flipSprite.flipX = false;
            } else if(movementScript.playerPosition.y < 0 && movementScript.playerPosition.x == 0 && !melee)
            {
                walkingState = "walking_Down";
                holdIdleState = "idle_Down";
                holdDashState = "dash_Down";
                flipSprite.flipX = false;
            }


            if(movementScript.playerPosition.magnitude > 0 && !movementScript.rootSnared)
            {
                ChangeAnimationState("Walking");
            } else if(movementScript.playerPosition.magnitude == 0 && !movementScript.rootSnared && !melee)
            {
                ChangeAnimationState("Idle");
            } else if(movementScript.rootSnared)
            {
                ChangeAnimationState("Snared");
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        string undoWalkState = walkingState, undoIdleState = holdIdleState, undoDashState = holdDashState;

        if(currentState == newState && movementScript.rootSnared)
            return;

        //Check two factor authentication for valid animation
        //Debug.Log(noAxe + " | " + inventoryScript.torchUsing);

        if(!noAxe && inventoryScript.torchUsing)
        {
            //Axe & Torch
            string addToState = "_Torch";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
            holdDeathState += addToState;
        } else if(noAxe && inventoryScript.torchUsing)
        {
            //No axe & Torch
            string addToState = "_Torch_Noaxe";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
        } else if(noAxe && !inventoryScript.torchUsing)
        {
            //No axe & no Torch
            string addToState = "_Noaxe_Notorch";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
        }


        if(newState == "Walking" && !movementScript.dashing && !melee)
        {
            animator.Play(walkingState);
        } else if(newState == "Idle" && !movementScript.dashing && !melee)
        {
            animator.Play(holdIdleState);
        } else if(newState == "Dash" && !melee)
        {
            animator.Play(holdDashState);
        } else if(newState == "Melee")
        {
            animator.Play(meleeHoldState);
        } else if(newState == "Snared")
        {
            animator.Play("snare_Start");
            Invoke(nameof(SnareIdle), 1f);
        } else if(newState == "Death")
        {
            animator.Play(holdDeathState);
        }

        currentState = newState;

        walkingState = undoWalkState;
        holdIdleState = undoIdleState;
        holdDashState = undoDashState;
    }

    public void MeleeAngle()
    {
        if(movementScript.angle < 45 && movementScript.angle > -45)
        {
            //Right
            flipSprite.flipX = false;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Side_Torch";
            } else
            {
                meleeHoldState = "melee_Side";
            }
        } else if(movementScript.angle > 45 && movementScript.angle < 135)
        {
            //Up
            flipSprite.flipX = false;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Up_Torch";
            } else
            {
                meleeHoldState = "melee_Up";
            }
        } else if(movementScript.angle < -45 && movementScript.angle > -135)
        {
            //Down
            flipSprite.flipX = false;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Down_Torch";
            } else
            {
                meleeHoldState = "melee_Down";
            }
        } else if(movementScript.angle > 135 || movementScript.angle < -135)
        {
            //Left
            flipSprite.flipX = true;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Side_Torch";
            } else
            {
                meleeHoldState = "melee_Side";
            }
        }
        ChangeAnimationState("Melee");
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
