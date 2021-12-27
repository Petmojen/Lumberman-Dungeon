using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerT:MonoBehaviour
{
    TutorialInventorySystem inventoryScriptT;
    PlayerMovementT movementScriptT;
    PlayerHpSystemT healthScriptT;
    PlayerAttackT attackScriptT;

    string currentState, holdIdleState = "idle_Side", holdDashState = "dash_Side", walkingState = "walking_Side", meleeHoldState = "melee_Side", holdDeathState = "death";
    SpriteRenderer flipSprite;
    Animator animator;

    bool noAxe, melee;

    void Start()
    {
        flipSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        inventoryScriptT = GetComponent<TutorialInventorySystem>();
        movementScriptT = GetComponent<PlayerMovementT>();
        healthScriptT = GetComponent<PlayerHpSystemT>();
        attackScriptT = GetComponent<PlayerAttackT>();
    }

    void Update()
    {

        if(healthScriptT.isDead)
        {
            ChangeAnimationState("Death");
        } else
        {

            if(movementScriptT.axeAttack == PlayerMovementT.Attack.Throw || movementScriptT.axeAttack == PlayerMovementT.Attack.AxeReturning)
            {
                noAxe = true;
            } else
            {
                noAxe = false;
            }

            if(movementScriptT.axeAttack == PlayerMovementT.Attack.Melee)
            {
                melee = true;
                MeleeAngle();
            }

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && melee)
            {
                attackScriptT.EndMelee();
                melee = false;
            }

            if(movementScriptT.playerPosition.x > 0 && !melee)
            {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                holdDashState = "dash_Side";
                flipSprite.flipX = false;
            } else if(movementScriptT.playerPosition.x < 0 && !movementScriptT.rootSnared && !melee)
            {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                holdDashState = "dash_Side";
                flipSprite.flipX = true;
            } else if(movementScriptT.playerPosition.y > 0 && movementScriptT.playerPosition.x == 0 && !melee)
            {
                walkingState = "walking_Up";
                holdIdleState = "idle_Up";
                holdDashState = "dash_Up";
                flipSprite.flipX = false;
            } else if(movementScriptT.playerPosition.y < 0 && movementScriptT.playerPosition.x == 0 && !melee)
            {
                walkingState = "walking_Down";
                holdIdleState = "idle_Down";
                holdDashState = "dash_Down";
                flipSprite.flipX = false;
            }


            if(movementScriptT.playerPosition.magnitude > 0 && !movementScriptT.rootSnared)
            {
                ChangeAnimationState("Walking");
            } else if(movementScriptT.playerPosition.magnitude == 0 && !movementScriptT.rootSnared && !melee)
            {
                ChangeAnimationState("Idle");
            } else if(movementScriptT.rootSnared)
            {
                ChangeAnimationState("Snared");
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        string undoWalkState = walkingState, undoIdleState = holdIdleState, undoDashState = holdDashState;

        if(currentState == newState && movementScriptT.rootSnared)
            return;

        //Check two factor authentication for valid animation
        //Debug.Log(noAxe + " | " + inventoryScriptT.torchUsing);

        if(!noAxe && inventoryScriptT.torchUsing)
        {
            //Axe & Torch
            string addToState = "_Torch";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
            holdDeathState += addToState;
        } else if(noAxe && inventoryScriptT.torchUsing)
        {
            //No axe & Torch
            string addToState = "_Torch_Noaxe";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
        } else if(noAxe && !inventoryScriptT.torchUsing)
        {
            //No axe & no Torch
            string addToState = "_Noaxe_Notorch";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
        }


        if(newState == "Walking" && !movementScriptT.dashing && !melee)
        {
            animator.Play(walkingState);
        } else if(newState == "Idle" && !movementScriptT.dashing && !melee)
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
        if(movementScriptT.angle < 45 && movementScriptT.angle > -45)
        {
            //Right
            flipSprite.flipX = false;
            if(inventoryScriptT.torchUsing)
            {
                meleeHoldState = "melee_Side_Torch";
            } else
            {
                meleeHoldState = "melee_Side";
            }
        } else if(movementScriptT.angle > 45 && movementScriptT.angle < 135)
        {
            //Up
            flipSprite.flipX = false;
            if(inventoryScriptT.torchUsing)
            {
                meleeHoldState = "melee_Up_Torch";
            } else
            {
                meleeHoldState = "melee_Up";
            }
        } else if(movementScriptT.angle < -45 && movementScriptT.angle > -135)
        {
            //Down
            flipSprite.flipX = false;
            if(inventoryScriptT.torchUsing)
            {
                meleeHoldState = "melee_Down_Torch";
            } else
            {
                meleeHoldState = "melee_Down";
            }
        } else if(movementScriptT.angle > 135 || movementScriptT.angle < -135)
        {
            //Left
            flipSprite.flipX = true;
            if(inventoryScriptT.torchUsing)
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
