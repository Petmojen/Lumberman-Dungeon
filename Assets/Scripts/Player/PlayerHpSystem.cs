using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSystem : MonoBehaviour
{
	public bool hitCooldown = false;
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
        if ((collision.CompareTag("MinionShot") || collision.CompareTag("Boss") || collision.CompareTag("Leaf")) && !hitCooldown)
        {
            playerHP--;
            UpdatePlayerHP();
			hitCooldown = true;
        }
		if (playerHP <= 0)
		{
			playerHP = 3;
		}
		Invoke(nameof(PlayerHitCooldown), 2f);
    }
	void PlayerHitCooldown()
	{
		hitCooldown = false;
		CancelInvoke();
	}
}
