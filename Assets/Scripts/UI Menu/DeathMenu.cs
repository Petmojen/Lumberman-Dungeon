using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour
{
    GameObject player;
    public GameObject deathMenuUI, FadeToBlackForMenuUI;
    PlayerHpSystem getHealth;
	BossHP bossHealth;
	FadeToBlack fadeToBlack;
	FadeToBlackForMenu fadeToBlackForMenu;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        getHealth = player.GetComponent<PlayerHpSystem>();
		bossHealth = GameObject.FindObjectOfType(typeof(BossHP)) as BossHP;
		fadeToBlack = GameObject.FindObjectOfType(typeof(FadeToBlack)) as FadeToBlack;
		fadeToBlackForMenu = GameObject.FindObjectOfType(typeof(FadeToBlackForMenu)) as FadeToBlackForMenu;
    }
    // Update is called once per frame
    void Update()
    {
        if (getHealth.isDead)
        {
			Invoke(nameof(FadeOut), 2f);
			Invoke(nameof(FadeIn), 4f);
            //Time.timeScale = 0.75f;
        }
		if (bossHealth.bossHp <= 0)
		{
			SceneManager.LoadScene("MainMenu");
		}
    }

    public void Retry()
    {
		fadeToBlack.startOfScene = true;
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
	void FadeIn()
	{
		if (FadeToBlackForMenuUI.activeSelf)
		{
			fadeToBlackForMenu.Fade(true);
			
			if (fadeToBlackForMenu.image.color.a <= 0f)
			{
				deathMenuUI.SetActive(true);
				FadeToBlackForMenuUI.SetActive(false);
			}
		}
	}
	void FadeOut()
	{
		fadeToBlack.Fade(false);
	}
}
