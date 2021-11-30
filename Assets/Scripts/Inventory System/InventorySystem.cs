using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem:MonoBehaviour
{
    [SerializeField] Text seedText, vineText, torchText;
    bool seedBool, vineBool, torchBool, logBool;
    int seedInt, vineInt, torchInt;
    PlayerHpSystem playerHpScript;
    GameObject holdResource;

    void Start()
    {
        playerHpScript = GetComponent<PlayerHpSystem>();
    }

    void Update()
    {
        if(seedBool && Input.GetKeyDown(KeyCode.E))
        {
            AddSeed();
        } else if(vineBool && Input.GetKeyDown(KeyCode.E)) {
            AddVine();
        } else if(torchBool && Input.GetKeyDown(KeyCode.E)) {
            AddTorch();
        } else if(logBool && Input.GetKeyDown(KeyCode.E)) {
            AddArmor();
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
        seedText.text = seedInt.ToString();
        Destroy(holdResource);
    }

    void AddVine()
    {
        vineInt++;
        vineText.text = vineInt.ToString();
        Destroy(holdResource);
    }

    void AddTorch()
    {
        torchInt++;
        torchText.text = torchInt.ToString();
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
}
