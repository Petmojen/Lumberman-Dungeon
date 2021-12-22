using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenuT : MonoBehaviour
{
    GameObject player;
    public GameObject deathMenuUI;
    PlayerHpSystemT getHealth;
	BossHP bossHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        getHealth = player.GetComponent<PlayerHpSystemT>();
		bossHealth = GameObject.FindObjectOfType(typeof(BossHP)) as BossHP;
    }
    // Update is called once per frame
    void Update()
    {
        if (getHealth.isDead)
        {
            Time.timeScale = 0f;
            deathMenuUI.SetActive(true);
        }
		if (bossHealth.bossHp <= 0)
		{
			SceneManager.LoadScene("Map");
		}
    }

    public void Retry()
    {
        SceneManager.LoadScene("Tutorial");
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
