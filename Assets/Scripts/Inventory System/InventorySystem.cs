using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem:MonoBehaviour
{
    [SerializeField] Text seedText, vineText, torchText;
	[SerializeField] GameObject bonFirePrefab, torchPrefab, treePrefab;
    bool seedBool, vineBool, torchBool, logBool;
    int seedInt, vineInt, torchInt;
	float bonFireTimer = 10, torchTimer = 5;
    PlayerHpSystem playerHpScript;
    GameObject holdResource;
	Debugger debuggerScript;

    void Start()
    {
        playerHpScript = GetComponent<PlayerHpSystem>();
		debuggerScript = GameObject.FindObjectOfType(typeof(Debugger)) as Debugger;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(seedBool)
            {
                AddSeed();
            } else if(vineBool) {
                AddVine();
            } else if(torchBool) {
                AddTorch();
            } else if(logBool) {
                AddArmor();
            }
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			PlaceBonFire();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			PlaceTorch();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			PlaceTree();
		}
		
		// Debug code
		if (debuggerScript.addInventorySeed)
		{
			AddSeed();
			debuggerScript.addInventorySeed = !debuggerScript.addInventorySeed;
		}
		if (debuggerScript.addInventoryVine)
		{
			AddVine();
			debuggerScript.addInventoryVine = !debuggerScript.addInventoryVine;
		}
		if (debuggerScript.addInventoryTorch)
		{
			AddTorch();
			debuggerScript.addInventoryTorch = !debuggerScript.addInventoryTorch;
		}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        holdResource = collision.gameObject;
        switch(collision.gameObject.tag)
        {
            case "Seed":
                seedBool = true;
                break;
            case "Vine":
                vineBool = true;
                break;
            case "Tourch":
                torchBool = true;
                break;
            case "Log":
                logBool = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        holdResource = null;
        seedBool = false;
        vineBool = false;
        torchBool = false;
        logBool = false;
    }

    void AddSeed()
    {
        seedInt++;
        seedText.text = string.Format("{0:0}", seedInt);
        Destroy(holdResource);
    }

    void AddVine()
    {
        vineInt++;
        vineText.text = string.Format("{0:0}", vineInt);
        Destroy(holdResource);
    }

    void AddTorch()
    {
        torchInt++;
        torchText.text = string.Format("{0:0}", torchInt);
        Destroy(holdResource);
    }

    void AddArmor()
    {
        if(playerHpScript.armor < 3)
        {
            playerHpScript.armor++;
            Destroy(holdResource);
            playerHpScript.UpdateArmor();
        }
    }
	
	void PlaceBonFire()
	{
		if (vineInt > 0)
		{
			vineInt--;
			vineText.text = vineInt.ToString();
			GameObject bonFireinstance = Instantiate(bonFirePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z),  Quaternion.identity);
			Destroy(bonFireinstance, bonFireTimer);
		}
	}
	
	void PlaceTorch()
	{
		if (torchInt > 0)
		{
			torchInt--;
			torchText.text = torchInt.ToString();
			GameObject torchinstance = Instantiate(torchPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z),  Quaternion.identity);
			Destroy(torchinstance, torchTimer);
		}
	}
	
		void PlaceTree()
	{
		if (seedInt > 0)
		{
			seedInt--;
			seedText.text = seedInt.ToString();
			GameObject treeinstance = Instantiate(treePrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),  Quaternion.identity);
			//Destroy(treeinstance, treeeTimer);
		}
	}
}
