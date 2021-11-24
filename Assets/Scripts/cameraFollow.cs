using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    bool followPlayer = true;

    void Update()
    {
        if(followPlayer)
        {
            transform.position = player.transform.position;
        }
    }
}
