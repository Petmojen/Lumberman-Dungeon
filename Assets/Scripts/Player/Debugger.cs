using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
	public bool immortal, instaDeath, addInventoryTorch, addInventoryVine, addInventorySeed, toggleMiniMap, timerZero;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			immortal = !immortal;
		}
		if(Input.GetKeyDown(KeyCode.Alpha5))
		{
			instaDeath = !instaDeath;
		}
		if(Input.GetKeyDown(KeyCode.Alpha6))
		{
			addInventoryTorch = !addInventoryTorch;
		}
		if(Input.GetKeyDown(KeyCode.Alpha7))
		{
			addInventoryVine = !addInventoryVine;
		}
		if(Input.GetKeyDown(KeyCode.Alpha8))
		{
			addInventorySeed = !addInventorySeed;
		}
				if(Input.GetKeyDown(KeyCode.Alpha9))
		{
			timerZero = !timerZero;
		}
    }
}
