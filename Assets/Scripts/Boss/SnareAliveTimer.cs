using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnareAliveTimer : MonoBehaviour
{
    BoxCollider2D activateCollider;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        activateCollider = GetComponent<BoxCollider2D>();
        Invoke(nameof(ActivateCollider), 2f);
    }

    void ActivateCollider()
    {
        animator.Play("idle");
        activateCollider.enabled = true;
        CancelInvoke();
        Invoke(nameof(SnareDestroy), 4f);
    }

    void SnareDestroy()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
