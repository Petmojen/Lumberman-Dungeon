using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour
{
	[SerializeField] Text tutorialTextInput;
	TutorialInventorySystem inventorySystemScript;
	PlayerHpSystem playerHPSystemscript;
	
	public string typeOfItem = "";
	public int tutorialStep = 0;
	bool itemTutorialActive, pickedUpSeedYet, pickedUpTorchYet, pickedUpLogYet, pickedUpVineYet, pickedUp4VinesYet;
	float textActiveTimer = 5f;
	
	void Start()
	{
		inventorySystemScript = GameObject.FindObjectOfType(typeof(TutorialInventorySystem)) as TutorialInventorySystem;
		playerHPSystemscript = GameObject.FindObjectOfType(typeof(PlayerHpSystem)) as PlayerHpSystem;
	}
	
    void Update()
    {
	
		if (itemTutorialActive)
		{	
			switch(tutorialStep)
			{
				case 1:
					tutorialTextInput.text = "Time to move on to the next room, beware of the darkness";
					this.gameObject.GetComponent<Image>().enabled = true;
					textActiveTimer = 5f;
					pickedUpSeedYet = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
					case 3:
					tutorialTextInput.text = "Time to move on to the next room, beware of the darkness";
					this.gameObject.GetComponent<Image>().enabled = true;
					textActiveTimer = 5f;
					pickedUpSeedYet = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
			}
		
			switch(typeOfItem)
			{
				case "Seed":
					tutorialTextInput.text = "This is earth mound, press E or LB to search for seeds";
					this.gameObject.GetComponent<Image>().enabled = true;
					textActiveTimer = 2f;
					break;
					
				case "Log":
					tutorialTextInput.text = "This is a log, press E or LB to pick up for armour";
					this.gameObject.GetComponent<Image>().enabled = true;
					textActiveTimer = 2f;
					break;
					
				case "Tourch":
					tutorialTextInput.text = "This is a torch, press E or LB to pick up";
					this.gameObject.GetComponent<Image>().enabled = true;
					textActiveTimer = 2f;
					break;
					
				case "Vine":
					tutorialTextInput.text = "This is a vine, press E or LB to pick up";
					this.gameObject.GetComponent<Image>().enabled = true;
					textActiveTimer = 2f;
					break;
			}
			
			if (inventorySystemScript.seedInt >= 1 && !pickedUpSeedYet)
			{
				tutorialTextInput.text = "You found a seed, can be planted pressing 2 or controller button A to grow a tree";
				this.gameObject.GetComponent<Image>().enabled = true;
				textActiveTimer = 5f;
				pickedUpSeedYet = true;
				itemTutorialActive = false;
			}
			if (playerHPSystemscript.armor >= 1 && !pickedUpLogYet)
			{
				tutorialTextInput.text = "You got some armour, a total of 3 armour pieces can be equipped for extra protection";
				this.gameObject.GetComponent<Image>().enabled = true;
				textActiveTimer = 5f;
				pickedUpLogYet = true;
				itemTutorialActive = false;
			}
			if (inventorySystemScript.torchInt >= 1 && !pickedUpTorchYet)
			{
				tutorialTextInput.text = "You picked up a torch, press 1 or controller button X to use to prevent taking damage from the darkness. The torch burns for a limited time";
				this.gameObject.GetComponent<Image>().enabled = true;
				textActiveTimer = 5f;
				pickedUpTorchYet = true;
				itemTutorialActive = false;
				tutorialStep++;
			}
			if (inventorySystemScript.vineInt >= 1 && !pickedUpVineYet)
			{
				tutorialTextInput.text = "You picked up a vine, collect a total of 4 vines to make a bonfire to heal yourself by pressing 3 or controller button Y";
				this.gameObject.GetComponent<Image>().enabled = true;
				textActiveTimer = 5f;
				pickedUpVineYet = true;
				itemTutorialActive = false;
			}
			if (inventorySystemScript.vineInt >= 4 && !pickedUp4VinesYet)
			{
				tutorialTextInput.text = "You now have 4 vines, make a bonfire to heal yourself by pressing 3 or controller button Y. The bonfire burns for a limited time";
				this.gameObject.GetComponent<Image>().enabled = true;
				textActiveTimer = 5f;
				pickedUp4VinesYet = true;
				itemTutorialActive = false;
				tutorialStep++;
			}
			
		}
		TutorialTextTimer(textActiveTimer);
		textActiveTimer -= Time.deltaTime;
    }
	
	void TutorialTextTimer(float stopTime)
	{
		if (textActiveTimer <= 0)
		{
			itemTutorialActive = true;
			tutorialTextInput.text = "";
			this.gameObject.GetComponent<Image>().enabled = false;
		}
	}

}
