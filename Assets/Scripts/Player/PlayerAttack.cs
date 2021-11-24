using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject getAxe, axeOffset;
    Rigidbody2D rgbd2D;

    void Start()
    {
        rgbd2D = getAxe.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Jacks kod
        } else if(Input.GetMouseButtonDown(1)) {
            ThrowingAxe(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }    
    }

    void ThrowingAxe(Vector2 mousePos)
    {
        
        Instantiate(getAxe, axeOffset.transform.position, transform.rotation);
        CancelInvoke();
    }
}
