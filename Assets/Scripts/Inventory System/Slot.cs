using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;

    public int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.slotFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpawnDropped>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
}
