using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMound:MonoBehaviour
{
	InventorySystem inventorySystemScript;
	SpriteRenderer earthMoundRenderer;
    Animator animator;
    public bool taken;

    void Start()
    {
		inventorySystemScript = GameObject.FindObjectOfType(typeof(InventorySystem)) as InventorySystem;
		earthMoundRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
	void Update()
	{
		if (inventorySystemScript.seedBool)
		{
			earthMoundRenderer.color = Color.yellow;
		} else {
			earthMoundRenderer.color = Color.white;
		}
	}

    public void ChangeSprite()
    {
        animator.Play("taken");
    }
}
