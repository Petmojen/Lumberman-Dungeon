using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour
{
    GameObject player;
    public GameObject deathMenuUI;
    PlayerHpSystem getHealth;
	BossHP bossHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        getHealth = player.GetComponent<PlayerHpSystem>();
		bossHealth = GameObject.FindObjectOfType(typeof(BossHP)) as BossHP;
    }
    // Update is called once per frame
    void Update()
    {
        if (getHealth.isDead)
        {
            Time.timeScale = 0.75f;
            deathMenuUI.SetActive(true);
        }
		if (bossHealth.bossHp <= 0)
		{
			SceneManager.LoadScene("MainMenu");
		}
    }

    public void Retry()
    {
        SceneManager.LoadScene("Map");
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
