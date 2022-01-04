using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationManagerT:MonoBehaviour
{
    //MinionSpawning activateMinionSpawning;
    TutorialDarkness darknessScript;
    BossAttackManager managerScript;
    SpriteRenderer changeColor;
    BossHP healthScript;
    Animator animator;
    Timer timerScript;
    BossSFX soundFX;

    public bool wakingUp, idle;
    bool wakeOnce;
    string currentState;

    void Start()
    {
        darknessScript = GameObject.FindObjectOfType(typeof(TutorialDarkness)) as TutorialDarkness;
        //activateMinionSpawning = GameObject.FindObjectOfType(typeof(MinionSpawning)) as MinionSpawning;
        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
        managerScript = GetComponent<BossAttackManager>();
        changeColor = GetComponent<SpriteRenderer>();
        healthScript = GetComponent<BossHP>();
        animator = GetComponent<Animator>();
        soundFX = GetComponent<BossSFX>();
    }

    void Update()
    {
        if(timerScript.timeOut && darknessScript.radiusOfLight < 13.51f && !wakeOnce)
        {
            wakingUp = true;
        }

        if(healthScript.takeHit)
        {
            changeColor.color = Color.red;
            Invoke(nameof(CancleRed), 0.05f);
        }

        if(healthScript.bossDead)
        {
            ChangeAnimation("dead");
        } else {
            if(wakingUp)
            {
                ChangeAnimation("waking_Up");
                Invoke(nameof(DeactivateWake), 1.4f);
            } else if(idle && !healthScript.healing) {
                ChangeAnimation("idle");
            } else if(healthScript.healing) {
                ChangeAnimation("healing");
            } else if((BossAttackManager.State)managerScript.current == BossAttackManager.State.Leafs) {
                ChangeAnimation("leaf");
            } else if((BossAttackManager.State)managerScript.current == BossAttackManager.State.BranchSweep) {
                ChangeAnimation("branch");
            }
        }
    }


    void ChangeAnimation(string newState)
    {
        if(newState == currentState) return;

        if(newState == "waking_Up")
        {
            animator.Play("wake_up");
        } else if(newState == "idle") {
            animator.Play("idle");
        } else if(newState == "leaf") {
            animator.Play("leaf");
        } else if(newState == "branch") {
            animator.Play("branch_swipe");
        } else if(newState == "dead") {
            animator.Play("death");
        } else if(newState == "healing") {
            animator.Play("healing");
        }

        currentState = newState;
    }

    void CancleRed()
    {
        changeColor.color = Color.white;
        healthScript.takeHit = false;
    }

    void DeactivateWake()
    {
        managerScript.wokenUp = true;
        wakingUp = false;
        idle = true;
        wakeOnce = true;
        CancelInvoke();
    }
}
