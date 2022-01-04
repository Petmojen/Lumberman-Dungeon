using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour
{
	[SerializeField] Text tutorialTextInput;
	InventorySystem inventorySystemScript;
	InventorySystemT inventorySystemScriptT;
	PlayerHpSystem playerHPSystemscript;
	Timer timerScript;
	Image textImage;
	
	public string typeOfItem = "";
	public int tutorialStep = 0, textStacker = 0;
	public bool roomFive, bossRoom;
	bool itemTutorialActive, pickedUpSeed, pickedUpTorch, pickedUpLog, pickedUpVine, pickedUp4Vines;
	public Sprite TutorialStart, Torch, TorchPickup, NextRoom, Vine, VinePickup, FourVines, EarthMound, FoundSeed, Log, Armour, Minion, Boss;
	
	void Start()
	{
		inventorySystemScript = GameObject.FindObjectOfType(typeof(InventorySystem)) as InventorySystem;
		inventorySystemScriptT = GameObject.FindObjectOfType(typeof(InventorySystemT)) as InventorySystemT;
		playerHPSystemscript = GameObject.FindObjectOfType(typeof(PlayerHpSystem)) as PlayerHpSystem;
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
		textImage = GetComponent<Image>();
		textImage.sprite = TutorialStart;
		
	}
	
    void Update()
    {
		if (itemTutorialActive)
		{	
			switch(tutorialStep)
			{	
				case 1:
				textImage.sprite = NextRoom;
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
					
					case 3:
					textImage.sprite = NextRoom;
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
					
					case 5:
					textImage.sprite = NextRoom;
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					tutorialStep++;
					break;
					
					case 7:
					textImage.sprite = NextRoom;
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					break;
					
					case 9:
					textImage.sprite = NextRoom;
					this.gameObject.GetComponent<Image>().enabled = true;
					itemTutorialActive = false;
					break;
			}
		
			switch(typeOfItem)
			{
				case "Seed":
					textImage.sprite = EarthMound;
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
					
				case "Log":
					textImage.sprite = Log;
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
					
				case "Tourch":
					textImage.sprite = Torch;
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
					
				case "Vine":
					textImage.sprite = Vine;
					this.gameObject.GetComponent<Image>().enabled = true;
					break;
			}
			
			if (inventorySystemScriptT.seedInt >= 1 && !pickedUpSeed)
			{
				textImage.sprite = FoundSeed;
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpSeed = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			if (playerHPSystemscript.armor >= 1 && !pickedUpLog)
			{
				textImage.sprite = Armour;
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpLog = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			
			if (inventorySystemScript.torchInt >= 1 && !pickedUpTorch)
			{
				textImage.sprite = TorchPickup;
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpTorch = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			
			if (inventorySystemScript.vineInt >= 1 && !pickedUpVine)
			{
				textImage.sprite = VinePickup;
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUpVine = true;
				itemTutorialActive = false;
				textStacker++;
			}
			
			if (inventorySystemScript.vineInt >= 4 && !pickedUp4Vines)
			{
				textImage.sprite = FourVines;
				this.gameObject.GetComponent<Image>().enabled = true;
				pickedUp4Vines = true;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker++;
			}
			
			if (roomFive && tutorialStep == 7)
			{
				textImage.sprite = Minion;
				this.gameObject.GetComponent<Image>().enabled = true;
				roomFive = false;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker = 0;
			}
			
			if (bossRoom && tutorialStep >= 10)
			{
				textImage.sprite = Boss;
				this.gameObject.GetComponent<Image>().enabled = true;
				bossRoom = false;
				itemTutorialActive = false;
				tutorialStep++;
				textStacker = 0;
				timerScript.timeLeft = 10f;
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
