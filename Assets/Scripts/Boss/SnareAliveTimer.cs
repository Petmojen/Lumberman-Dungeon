using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnareAliveTimer : MonoBehaviour
{
    BoxCollider2D activateCollider;

    void Start()
    {
        activateCollider = GetComponent<BoxCollider2D>();
        Invoke(nameof(ActivateCollider), 2f);
    }

    void ActivateCollider()
    {
        activateCollider.enabled = true;
        CancelInvoke();
        Invoke(nameof(SnareDestroy), 4f);
    }

    void SnareDestroy()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
