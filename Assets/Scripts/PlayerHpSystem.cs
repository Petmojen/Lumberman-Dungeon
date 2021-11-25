using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSystem : MonoBehaviour
{
    int playerHP = 3;

    [SerializeField]
    Sprite[] hpBars;

    [SerializeField]
    Image hpBarUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void UpdatePlayerHP()
    {
        hpBarUI.sprite = hpBars[playerHP];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MinionShot"))
        {
            playerHP--;
            UpdatePlayerHP();
        }
    }
}
