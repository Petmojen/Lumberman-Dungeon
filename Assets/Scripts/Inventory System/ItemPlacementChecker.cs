using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacementChecker : MonoBehaviour
{
    InventorySystem inventoryScript;

    void Start()
    {
        inventoryScript = FindObjectOfType(typeof(InventorySystem)) as InventorySystem;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
