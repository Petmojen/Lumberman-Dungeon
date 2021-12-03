using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionHpSystem : MonoBehaviour
{
    int minionHp = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe"))
        {
            minionHp--;
            if (minionHp == 0)
                Destroy(gameObject);
        }
    }
}
