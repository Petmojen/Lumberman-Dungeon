using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGridPoint : MonoBehaviour
{
    public GameObject sendGridPoint;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("GridPoint") && sendGridPoint == null)
        {
            sendGridPoint = collision.gameObject;
        }
    }
}
