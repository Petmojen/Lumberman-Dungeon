using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] Sprite[] spriteLevels;
    [SerializeField] Image changeLevelImage;
    [SerializeField] GameObject WinText;
	public GameObject FadeToBlackForMenuUI;
    BossHP bossHPScript;
	FadeToBlack fadeToBlack;
	FadeToBlackForMenu fadeToBlackForMenu;
    DifficultyManager dificultyScript;

    void Start()
    {
        bossHPScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHP>();
		fadeToBlack = GameObject.FindObjectOfType(typeof(FadeToBlack)) as FadeToBlack;
        dificultyScript = GameObject.FindObjectOfType(typeof(DifficultyManager)) as DifficultyManager;
        fadeToBlackForMenu = GameObject.FindObjectOfType(typeof(FadeToBlackForMenu)) as FadeToBlackForMenu;
        changeLevelImage.sprite = spriteLevels[DifficultyManager.difficultyLevel];
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
