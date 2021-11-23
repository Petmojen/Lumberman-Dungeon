using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject getAxe;
    Rigidbody2D rgbd2D;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {

        } else if(Input.GetMouseButton(1)) {
            Invoke(nameof(ThrowingAxe), 0f);
        }    
    }

    void ThrowingAxe()
    {
        CancelInvoke();
    }
}
