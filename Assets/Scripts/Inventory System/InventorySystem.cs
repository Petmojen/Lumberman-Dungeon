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
            AddTourch();
        } else if(logBool && Input.GetKeyDown(KeyCode.E)) {
            AddArmor();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        holdResource = collision.gameObject;
        if(collision.CompareTag("Seed"))
        {
            seedBool = true;
        }

        if(collision.CompareTag("Vine"))
        {
            vineBool = true;
        }

        if(collision.CompareTag("Tourch"))
        {
            torchBool = true;
        }

        if(collision.CompareTag("Log"))
        {
            logBool = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        holdResource = null;
        if(collision.CompareTag("Seed"))
        {
            seedBool = false;
        }

        if(collision.CompareTag("Vine"))
        {
            vineBool = false;
        }

        if(collision.CompareTag("Tourch"))
        {
            torchBool = false;
        }

        if(collision.CompareTag("Log"))
        {
            logBool = false;
        }
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

    void AddTourch()
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
