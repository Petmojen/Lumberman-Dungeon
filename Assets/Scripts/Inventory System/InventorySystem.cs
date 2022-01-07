using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem:MonoBehaviour
{
    public bool seedBool, vineBool, torchBool, logBool, maxCapacity, torchUsing;
	[SerializeField] GameObject bonFirePrefab, treePrefab, itemPlacementOffset;
    [SerializeField] Text seedText, vineText, torchText;
	bool brightToDarkText, flashTextAtStart = true;
    public GameObject holdResource, miniMap;
	float bonFireTimer = 10, torchTimer = 5;
    public int seedInt, vineInt, torchInt;
	public float fadeOutTextColor = 0f;
    PlayerHpSystem playerHpScript;



    PlayerMovement movementScript;
    EarthMound earthMoundScript;
    Vine vineScript;
    Log logScript;


    void Start()
    {

        playerHpScript = GetComponent<PlayerHpSystem>();
        movementScript = GetComponent<PlayerMovement>();
		
		seedText.fontStyle = FontStyle.Bold;
		seedText.color = Color.white;
		vineText.fontStyle = FontStyle.Bold;
		vineText.color = Color.white;
		torchText.fontStyle = FontStyle.Bold;
		torchText.color = Color.white;
    }

    void Update()
    {
        if (flashTextAtStart)
		{
			FadeTextTimer();
		}
		
		if (miniMap.activeSelf)
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
		}

        if(movementScript.playerPosition.x > 0)
        {
            //Right
            itemPlacementOffset.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if(movementScript.playerPosition.x < 0) {
            //Left
            itemPlacementOffset.transform.rotation = Quaternion.Euler(0, 0, 180);
        } else if(movementScript.playerPosition.y > 0 && movementScript.playerPosition.x == 0) {
            //Up
            itemPlacementOffset.transform.rotation = Quaternion.Euler(0, 0, 90);
        } else if(movementScript.playerPosition.y < 0 && movementScript.playerPosition.x == 0) {
            //Down
            itemPlacementOffset.transform.rotation = Quaternion.Euler(0, 0, -90);
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
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
                holdResource = collision.gameObject;
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
		seedText.fontStyle = FontStyle.Bold;
		seedText.color = new Color(0.15f, 0.49f, 0.35f, 1f);
		Invoke(nameof(HighLightSeed), 0.5f);
    }

    void AddVine()
    {
        vineInt++;
        vineScript.taken = true;
        vineScript.ChangeSprite();
        vineText.text = string.Format("{0:0}", vineInt);
		vineText.fontStyle = FontStyle.Bold;
		vineText.color = new Color(0.15f, 0.49f, 0.35f, 1f);
		Invoke(nameof(HighLightVine), 0.5f);
    }

    void AddTorch()
    {
        torchInt++;
        torchText.text = string.Format("{0:0}", torchInt);
		torchText.fontStyle = FontStyle.Bold;
		torchText.color = new Color(0.15f, 0.49f, 0.35f, 1f);
		Invoke(nameof(HighLightTorch), 0.5f);
		if (holdResource.tag == "Tourch")
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
        RaycastHit2D hit = Physics2D.Raycast(itemPlacementOffset.transform.position, itemPlacementOffset.transform.right, Mathf.Infinity, LayerMask.GetMask("Raycast"));
		if (vineInt >= 4 && hit.distance > 2 && hit.transform.CompareTag("Wall"))
		{
            vineInt -= 4;
			vineText.text = vineInt.ToString();
			GameObject bonFireinstance = Instantiate(bonFirePrefab, itemPlacementOffset.transform.position + itemPlacementOffset.transform.right * 1.5f,  Quaternion.identity);
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
        RaycastHit2D hit = Physics2D.Raycast(itemPlacementOffset.transform.position, itemPlacementOffset.transform.right, Mathf.Infinity, LayerMask.GetMask("Raycast"));
        if(seedInt > 0 && hit.distance > 2 && hit.transform.CompareTag("Wall"))
        {
            seedInt--;
            seedText.text = seedInt.ToString();
            Instantiate(treePrefab, itemPlacementOffset.transform.position + itemPlacementOffset.transform.right * 2, Quaternion.identity);
        }
	}

	void FadeText()
	{
		if (fadeOutTextColor <= 1f && !brightToDarkText)
		{
			fadeOutTextColor += Time.deltaTime * 2;
			vineText.color = new Color(fadeOutTextColor, fadeOutTextColor, fadeOutTextColor, 1f);
			torchText.color = new Color(fadeOutTextColor, fadeOutTextColor, fadeOutTextColor, 1f);
			seedText.color = new Color(fadeOutTextColor, fadeOutTextColor, fadeOutTextColor, 1f);
		} else {
			brightToDarkText = true;
		}

		if (fadeOutTextColor > 0f && brightToDarkText) 
		{
			fadeOutTextColor -= Time.deltaTime * 2;
			vineText.color = new Color(fadeOutTextColor, fadeOutTextColor, fadeOutTextColor, 1f);
			torchText.color = new Color(fadeOutTextColor, fadeOutTextColor, fadeOutTextColor, 1f);
			seedText.color = new Color(fadeOutTextColor, fadeOutTextColor, fadeOutTextColor, 1f);
		}

		if (fadeOutTextColor <= 0f && brightToDarkText)
		{
			torchText.fontStyle = FontStyle.Normal;
			seedText.fontStyle = FontStyle.Normal;
			vineText.fontStyle = FontStyle.Normal;
			seedText.color = new Color(1f, 1f, 1f, 0.8f);
			vineText.color = new Color(1f, 1f, 1f, 0.8f);
			torchText.color = new Color(1f, 1f, 1f, 0.8f);
			flashTextAtStart = false;
		}
	}
	
	void FadeTextTimer()
	{
		Invoke(nameof(FadeText), 1f);
	}
	
	void HighLightTorch()
	{
		torchText.fontStyle = FontStyle.Normal;
		torchText.color = new Color(1f, 1f, 1f, 0.8f);
		CancelInvoke();
	}
	
    void HighLightSeed()
	{
		seedText.fontStyle = FontStyle.Normal;
		seedText.color = new Color(1f, 1f, 1f, 0.8f);
		CancelInvoke();
	}
	
    void HighLightVine()
	{
		vineText.fontStyle = FontStyle.Normal;
		vineText.color = new Color(1f, 1f, 1f, 0.8f);
		CancelInvoke();
	}
}
