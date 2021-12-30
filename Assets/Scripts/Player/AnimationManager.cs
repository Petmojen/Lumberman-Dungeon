using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager:MonoBehaviour
{
    InventorySystem inventoryScript;
    PlayerMovement movementScript;
    PlayerHpSystem healthScript;
    PlayerAttack attackScript;

    string currentState, 
        holdIdleState = "idle_Side", 
        holdDashState = "dash_Side", 
        walkingState = "walking_Side", 
        meleeHoldState = "melee_Side", 
        holdDeathState = "death", 
        holdThrowState = "throwing_Axe_Side",
        holdTakeDamageState = "taking_Damage_Side";
    SpriteRenderer flipSprite;
    Animator animator;

    bool noAxe, melee, throwing;

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
        } else if(healthScript.knockback) {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                healthScript.knockback = false;
            }
            KnockbackAngle();
        } else {

            if(movementScript.axeAttack == PlayerMovement.Attack.Throw || movementScript.axeAttack == PlayerMovement.Attack.AxeReturning && !throwing && !noAxe)
            {
                noAxe = true;
            } else if(movementScript.axeAttack == PlayerMovement.Attack.Melee || movementScript.axeAttack == PlayerMovement.Attack.Idle) {
                noAxe = false;
            }

            if(movementScript.axeAttack == PlayerMovement.Attack.Throw && Input.GetMouseButton(1) ||Input.GetAxisRaw("Throw") > 0f)
            {
                throwing = true;
                AttackAngle();
            }

            if(movementScript.axeAttack == PlayerMovement.Attack.Melee)
            {
                melee = true;
                AttackAngle();
            }

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && melee)
            {
                attackScript.EndMelee();
                melee = false;
            }

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && throwing)
            {
                throwing = false;
            }

            if(movementScript.playerPosition.x > 0 && !melee && !throwing)
            {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                holdDashState = "dash_Side";
                flipSprite.flipX = false;
            } else if(movementScript.playerPosition.x < 0 && !movementScript.rootSnared && !melee && !throwing) {
                walkingState = "walking_Side";
                holdIdleState = "idle_Side";
                holdDashState = "dash_Side";
                flipSprite.flipX = true;
            } else if(movementScript.playerPosition.y > 0 && movementScript.playerPosition.x == 0 && !melee && !throwing) {
                walkingState = "walking_Up";
                holdIdleState = "idle_Up";
                holdDashState = "dash_Up";
                flipSprite.flipX = false;
            } else if(movementScript.playerPosition.y < 0 && movementScript.playerPosition.x == 0 && !melee && !throwing) {
                walkingState = "walking_Down";
                holdIdleState = "idle_Down";
                holdDashState = "dash_Down";
                flipSprite.flipX = false;
            }


            if(movementScript.playerPosition.magnitude > 0 && !movementScript.rootSnared && !movementScript.dashing && !throwing)
            {
                ChangeAnimationState("Walking");
            } else if(movementScript.playerPosition.magnitude == 0 && !movementScript.rootSnared && !melee && !throwing) {
                ChangeAnimationState("Idle");
            } else if(movementScript.dashing && !movementScript.rootSnared && !melee && !throwing) {
                ChangeAnimationState("Dash");
            } else if(movementScript.rootSnared) {
                ChangeAnimationState("Snared");
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        string undoWalkState = walkingState, 
            undoIdleState = holdIdleState, 
            undoDashState = holdDashState,
            undoDeathState = holdDeathState,
            undoKnockState = holdTakeDamageState;

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
            holdTakeDamageState += addToState;
        } else if(noAxe && inventoryScript.torchUsing) {
            //No axe & Torch
            string addToState = "_Torch_Noaxe";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
            holdTakeDamageState += addToState;
        } else if(noAxe && !inventoryScript.torchUsing) {
            //No axe & no Torch
            string addToState = "_Noaxe_Notorch";
            walkingState += addToState;
            holdIdleState += addToState;
            holdDashState += addToState;
            holdTakeDamageState += addToState;
        }

        if(newState == "Walking" && !movementScript.dashing && !melee)
        {
            animator.Play(walkingState);
        } else if(newState == "Idle" && !movementScript.dashing && !melee) {
            animator.Play(holdIdleState);
        } else if(newState == "Dash" && !melee) {
            animator.Play(holdDashState);
        } else if(newState == "Melee") {
            animator.Play(meleeHoldState);
        } else if(newState == "Throw" && throwing) {
            animator.Play(holdThrowState);
        } else if(newState == "Snared") {
            animator.Play("snare_Start");
            Invoke(nameof(SnareIdle), 1f);
        } else if(newState == "Knock") {
            animator.Play(holdTakeDamageState);
        } else if(newState == "Death") {
            animator.Play(holdDeathState);
        }

        currentState = newState;

        walkingState = undoWalkState;
        holdIdleState = undoIdleState;
        holdDashState = undoDashState;
        holdDeathState = undoDeathState;
        holdTakeDamageState = undoKnockState;
    }

    void KnockbackAngle()
    {
        if(movementScript.knockAngle < 45 && movementScript.knockAngle > -45)
        {
            //Right
            flipSprite.flipX = false;
            holdTakeDamageState = "taking_Damage_Side";
        } else if(movementScript.knockAngle > 45 && movementScript.knockAngle < 135) {
            //Up
            flipSprite.flipX = false;
            holdTakeDamageState = "taking_Damage_Up";
        } else if(movementScript.knockAngle < -45 && movementScript.knockAngle > -135) {
            //Down
            flipSprite.flipX = false;
            holdTakeDamageState = "taking_Damage_Down";
        } else if(movementScript.angle > 135 || movementScript.angle < -135) {
            //Left
            flipSprite.flipX = true;
            holdTakeDamageState = "taking_Damage_Side";
        }
        ChangeAnimationState("Knock");
    }

    void AttackAngle()
    {
        if(movementScript.angle < 45 && movementScript.angle > -45)
        {
            //Right
            flipSprite.flipX = false;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Side_Torch";
                holdThrowState = "throwing_Axe_Side_Torch";
            } else {
                meleeHoldState = "melee_Side";
                holdThrowState = "throwing_Axe_Side";
            }
        } else if(movementScript.angle > 45 && movementScript.angle < 135) {
            //Up
            flipSprite.flipX = false;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Up_Torch";
                holdThrowState = "throwing_Axe_Up_Torch";
            } else {
                meleeHoldState = "melee_Up";
                holdThrowState = "throwing_Axe_Up";
            }
        } else if(movementScript.angle < -45 && movementScript.angle > -135) {
            //Down
            flipSprite.flipX = false;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Down_Torch";
                holdThrowState = "throwing_Axe_Down_Torch";
            } else {
                meleeHoldState = "melee_Down";
                holdThrowState = "throwing_Axe_Down";
            }
        } else if(movementScript.angle > 135 || movementScript.angle < -135) {
            //Left
            flipSprite.flipX = true;
            if(inventoryScript.torchUsing)
            {
                meleeHoldState = "melee_Side_Torch";
                holdThrowState = "throwing_Axe_Side_Torch";
            } else {
                meleeHoldState = "melee_Side";
                holdThrowState = "throwing_Axe_Side";
            }
        }

        if(noAxe && !melee)
        {
            ChangeAnimationState("Throw");
        } else if(!noAxe && melee) {
            ChangeAnimationState("Melee");
        }
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
