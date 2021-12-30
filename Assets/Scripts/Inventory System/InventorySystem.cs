using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem:MonoBehaviour
{
	[SerializeField] GameObject bonFirePrefab, treePrefab, holdingTorch;
    [SerializeField] Text seedText, vineText, torchText;
    public bool seedBool, vineBool, torchBool, logBool;
	float bonFireTimer = 10, torchTimer = 5;
    public bool maxCapacity, torchUsing;
    int seedInt, vineInt, torchInt;
    PlayerHpSystem playerHpScript;
    GameObject holdResource;
	Debugger debuggerScript;

    EarthMound earthMoundScript;
    Vine vineScript;
    Log logScript;

    void Start()
    {
        playerHpScript = GetComponent<PlayerHpSystem>();
		debuggerScript = GameObject.FindObjectOfType(typeof(Debugger)) as Debugger;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetButton("Pickup"))
        {
            if(seedBool && !earthMoundScript.taken)
            {
                if(!earthMoundScript.seedFull)
                {
                    SeedChanse();
                } else {
                    AddSeed();
                }
            } else if(vineBool && !vineScript.taken) {
                AddVine();
            } else if(torchBool) {
                AddTorch();
            } else if(logBool) {
                AddArmor();
            }
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("UseTorch"))
		{
            UseTorch();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("Debug Validate"))
		{
            PlaceTree();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("Debug Reset"))
		{
            PlaceBonfire();
		}
		
		// Debug code
		if (debuggerScript.addInventorySeed)
		{
			seedInt++;
			seedText.text = string.Format("{0:0}", seedInt);
			debuggerScript.addInventorySeed = !debuggerScript.addInventorySeed;
		}
		if (debuggerScript.addInventoryVine)
		{
			vineInt++;
			vineText.text = string.Format("{0:0}", vineInt);
			debuggerScript.addInventoryVine = !debuggerScript.addInventoryVine;
		}
		if (debuggerScript.addInventoryTorch)
		{
			torchInt++;
			torchText.text = string.Format("{0:0}", torchInt);
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
                earthMoundScript = collision.GetComponent<EarthMound>();
                break;
            case "Vine":
                vineBool = true;
                vineScript = collision.GetComponent<Vine>();
                break;
            case "Tourch":
                torchBool = true;
                break;
            case "Log":
                logBool = true;
                logScript = collision.GetComponent<Log>();
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

    void SeedChanse()
    {
        if(Random.Range(0, 100) > 20)
        {
            earthMoundScript.ChangeSprite("seedFull");
            earthMoundScript.seedFull = true;
        } else {
            earthMoundScript.ChangeSprite("empty");
            earthMoundScript.taken = true;
        }
    }

    void AddSeed()
    {
        seedInt++;
        earthMoundScript.ChangeSprite("empty");
        earthMoundScript.seedFull = false;
        earthMoundScript.taken = true;
        seedText.text = string.Format("{0:0}", seedInt);
    }

    void AddVine()
    {
        vineInt++;
        vineScript.taken = true;
        vineScript.ChangeSprite();
        vineText.text = string.Format("{0:0}", vineInt);
    }

    void AddTorch()
    {
        torchInt++;
        torchText.text = string.Format("{0:0}", torchInt);
		if (holdResource != null)
		{
			Destroy(holdResource);
		}
        
    }

    void AddArmor()
    {
        if(playerHpScript.armor < 3 && !logScript.taken)
        {
            playerHpScript.armor++;
            logScript.taken = true;
            logScript.ChangeSprite();
            playerHpScript.UpdateArmor();
        }
    }
	
	void PlaceBonfire()
	{
		if (vineInt >= 4)
		{
			vineInt -= 4;
			vineText.text = vineInt.ToString();
			GameObject bonFireinstance = Instantiate(bonFirePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z),  Quaternion.identity);
			Destroy(bonFireinstance, bonFireTimer);
            Invoke(nameof(DestroyBon), bonFireTimer);
		}
	}
	
    void DestroyBon()
    {
        playerHpScript.bonfire = false;
        CancelInvoke(nameof(DestroyBon));
    }

	void UseTorch()
	{
		if (torchInt > 0)
		{
			torchInt--;
			torchText.text = torchInt.ToString();
            torchUsing = true;
            Invoke(nameof(TorchInactive), torchTimer);
		}
	}

    void TorchInactive()
    {
        torchUsing = false;
        CancelInvoke();
    }

    void PlaceTree()
	{
		if (seedInt > 0)
		{
			seedInt--;
			seedText.text = seedInt.ToString();
			GameObject treeinstance = Instantiate(treePrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),  Quaternion.identity);
		}
	}
}
