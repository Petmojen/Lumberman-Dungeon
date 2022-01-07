using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuT : MonoBehaviour
{
    [SerializeField] GameObject WinText;
	public GameObject FadeToBlackForMenuUI;
    BossHPT bossHPScript;
	FadeToBlack fadeToBlack;
	FadeToBlackForMenu fadeToBlackForMenu;

    void Start()
    {
        bossHPScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHPT>();
		fadeToBlack = GameObject.FindObjectOfType(typeof(FadeToBlack)) as FadeToBlack;
		fadeToBlackForMenu = GameObject.FindObjectOfType(typeof(FadeToBlackForMenu)) as FadeToBlackForMenu;			
    }

    void Update()
    {
        if(bossHPScript.bossDead)
		{
			Invoke(nameof(FadeOut), 3f);
			Invoke(nameof(FadeIn), 6f);
        }        
    }

	void FadeIn()
	{
		if (FadeToBlackForMenuUI.activeSelf)
		{
			fadeToBlackForMenu.Fade(true);
			
			if (fadeToBlackForMenu.image.color.a <= 0f)
			{
				WinText.SetActive(true);
				FadeToBlackForMenuUI.SetActive(false);
				DifficultyManager.difficultyLevel++;
				Invoke(nameof(NextStage), 3f);
			}
		}
	}
	
	void FadeOut()
	{
		fadeToBlack.Fade(false);
	}
	
	void NextStage()
	{
		SceneManager.LoadScene("Map");
		CancelInvoke();
	}
}
