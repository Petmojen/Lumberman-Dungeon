using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] GameObject itemButton;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                Debug.Log("Hej1");
                if(inventory.slotFull[i] == false)
                {
                    //item can be added to inventory
                    inventory.slotFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Debug.Log("Hej2");
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
