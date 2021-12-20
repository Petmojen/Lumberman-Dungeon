using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMound:MonoBehaviour
{
    Animator animator;
    public bool taken;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeSprite()
    {
        animator.Play("taken");
    }
}
