using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem:MonoBehaviour
{
    bool seedBool, vineBool, tourchBool, logBool;
    int seedInt, vineInt, tourchInt;
    PlayerHpSystem playerHpScript;
    GameObject holdResource;

    void Start()
    {
        playerHpScript = GetComponent<PlayerHpSystem>();
    }

    void Update()
    {
        Debug.Log("Seeds:" + seedInt + " Vines:" + vineInt + " Tourches:" + tourchInt);

        if(seedBool && Input.GetKeyDown(KeyCode.E))
        {
            AddSeed();
        } else if(vineBool && Input.GetKeyDown(KeyCode.E))
        {
            AddVine();
        } else if(tourchBool && Input.GetKeyDown(KeyCode.E))
        {
            AddTourch();
        } else if(logBool && Input.GetKeyDown(KeyCode.E))
        {
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
            tourchBool = true;
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
            tourchBool = false;
        }

        if(collision.CompareTag("Log"))
        {
            logBool = false;
        }
    }

    void AddSeed()
    {
        seedInt++;
    }

    void AddVine()
    {
        vineInt++;
    }

    void AddTourch()
    {
        tourchInt++;
    }

    void AddArmor()
    {
        if(playerHpScript.armor < 3)
        {
            playerHpScript.armor++;
            Destroy(holdResource);
        }
    }
}
