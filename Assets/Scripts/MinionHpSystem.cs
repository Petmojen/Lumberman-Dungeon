using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionHpSystem : MonoBehaviour
{
    //TODO: Should be tested if minion health will be 1 hp. If so optimize this code
    int minionHp = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

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
