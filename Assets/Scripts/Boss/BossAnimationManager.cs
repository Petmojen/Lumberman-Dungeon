using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationManager:MonoBehaviour
{
    MinionSpawning activateMinionSpawning;
    ForceToBossDarkness darknessScript;
    BossAttackManager managerScript;
    BossHP healthScript;
    Animator animator;
    Timer timerScript;

    public bool wakingUp, idle;
    string currentState;

    void Start()
    {
        darknessScript = GameObject.FindObjectOfType(typeof(ForceToBossDarkness)) as ForceToBossDarkness;
        activateMinionSpawning = GameObject.FindObjectOfType(typeof(MinionSpawning)) as MinionSpawning;
        timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
        managerScript = GetComponent<BossAttackManager>();
        healthScript = GetComponent<BossHP>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(timerScript.timeOut && darknessScript.radiusOfLight < 13.51f && !activateMinionSpawning.bossInvicible && !wakingUp && !idle)
        {
            wakingUp = true;
        }

        if(healthScript.bossDead)
        {
            ChangeAnimation("dead");
        } else {
            if(wakingUp)
            {
                ChangeAnimation("waking_Up");
                Invoke(nameof(DeactivateWake), 1.4f);
            } else if(idle) {
                ChangeAnimation("idle");
            } else if(managerScript.current == (System.Object)BossAttackManager.State.Leafs) {
                ChangeAnimation("leaf");
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
        }

        currentState = newState;
    }

    void DeactivateWake()
    {
        managerScript.bossAwake = true;
        wakingUp = false;
        idle = true;
    }
}
