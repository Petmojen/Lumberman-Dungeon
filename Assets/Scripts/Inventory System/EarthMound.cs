using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMound:MonoBehaviour
{
	SpriteRenderer earthMoundRenderer;
    Animator animator;
    public bool taken, seedFull;

    void Start()
    {
		earthMoundRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void ChangeSprite(string animationName)
    {
        animator.Play(animationName);
    }
}
