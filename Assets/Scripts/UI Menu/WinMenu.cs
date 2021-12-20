using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    [SerializeField] GameObject WinText;
    BossHP bossHPScript;

    void Start()
    {
        bossHPScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHP>(); 
    }

    void Update()
    {
        if(bossHPScript.bossDead) {
            WinText.SetActive(true);
            Time.timeScale = 0f;
        }        
    }
}
