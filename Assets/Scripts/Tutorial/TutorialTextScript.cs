using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour
{
	[SerializeField] Text tutorialTextInput;
	TutorialInventorySystem inventorySystemScript;
	PlayerHpSystemT playerHPSystemscript;
	Timer timerScript;
	
	public string typeOfItem = "";
	public int tutorialStep = 0, textStacker = 0;
	public bool roomFive, bossRoom;
	bool itemTutorialActive, pickedUpSeed, pickedUpTorch, pickedUpLog, pickedUpVine, pickedUp4Vines;
	
	void Start()
	{
		inventorySystemScript = GameObject.FindObjectOfType(typeof(TutorialInventorySystem)) as TutorialInventorySystem;
		playerHPSystemscript = GameObject.FindObjectOfType(typeof(PlayerHpSystemT)) as PlayerHpSystemT;
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
		
	}
	
    void Update()
    {
		if (itemTutorialActive)
		{	
			switch(tutorialStep)
			{
				case 1:
					tutorialTextInput.text = "Time to move on to the next room.\n\nBeware of the darkness\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
					
					case 3:
					tutorialTextInput.text = "Time to move on to the next room.\n\nBeware of the darkness\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
					
					case 5:
					tutorialTextInput.text = "Time to move on to the next room.\n\nBeware of the darkness\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
					
					case 7:
					tutorialTextInput.text = "Time to move on to the next room.\n\nBeware of the darkness\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					//tutorialStep++;
					break;
					
					case 9:
					tutorialTextInput.text = "Time to move on to the next room.\n\nBeware of the darkness\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					//tutorialStep++;
					break;
			}
		
			switch(typeOfItem)
			{
				case "Seed":
					tutorialTextInput.text = "This is earth mound.\n\npress E or LB to search for seeds\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
					
				case "Log":
					tutorialTextInput.text = "This is a log.\n\npress E or LB to pick up for armour\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
					
				case "Tourch":
					tutorialTextInput.text = "This is a torch.\n\npress E or LS to pick up\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
					
				case "Vine":
					tutorialTextInput.text = "This is a vine.\n\npress E or LS to pick up\n\nPress E/LB to continue";
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
			}
			
			if (inventorySystemScript.seedInt >= 1 && !pickedUpSeed)
			{
				tutorialTextInput.text = "You found a seed.\n\nCan be planted pressing 2 or controller button A to grow a tree\n\nPress E/LB to continue";
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpSeed = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			if (playerHPSystemscript.armor >= 1 && !pickedUpLog)
			{
				tutorialTextInput.text = "You got some armour.\n\nA total of 3 armour pieces can be equipped for extra protection\n\nPress E/LB to continue";
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpLog = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			
			if (inventorySystemScript.torchInt >= 1 && !pickedUpTorch)
			{
				tutorialTextInput.text = "You picked up a torch.\n\nPress 1 or controller button X to use to prevent taking damage from the darkness.\n\nThe torch burns for a limited time\n\nPress E/LB to continue";
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpTorch = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			
			if (inventorySystemScript.vineInt >= 1 && !pickedUpVine)
			{
				tutorialTextInput.text = "You picked up a vine.\n\nCollect a total of 4 vines to make a bonfire to heal yourself by pressing 3 or controller button Y\n\nPress E/LB to continue";
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpVine = true;
				itemTutorialActive = false;
				textStacker++;
			}
			
			if (inventorySystemScript.vineInt >= 4 && !pickedUp4Vines)
			{
				tutorialTextInput.text = "You now have 4 vines.\n\nMake a bonfire to heal yourself by pressing 3 or controller button Y.\n\nThe bonfire burns for a limited time\n\nPress E/LB to continue";
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUp4Vines = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			
			if (roomFive && tutorialStep == 7)
			{
				tutorialTextInput.text = "Please kill the monster in the next room.\n\nUse LMB/LT to Melee or RMB/RT to throw axe.\n\nAim with mouse or right analog stick.\n\nPPress E/LB to continue";
				this.gameObject.GetComponent<Image>().enabled = true;
				roomFive = false;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker = 0;
			}
			
			if (bossRoom && tutorialStep >= 12)
			{
				tutorialTextInput.text = "Are you ready for the boss?\n\nTry killing it but don't get too close...\n\nPress E/LB to continue";
				this.gameObject.GetComponent<Image>().enabled = true;
				bossRoom = false;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker = 0;
				timerScript.timeLeft = 0f;
			}
			
		}
		if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Pickup"))
		{	
			if (textStacker != 0)
			{
				textStacker--;
			} else {
				itemTutorialActive = true;
				typeOfItem = "";
				tutorialTextInput.text = "";
				this.gameObject.GetComponent<Image>().enabled = false;
			}
		}
    }


}
