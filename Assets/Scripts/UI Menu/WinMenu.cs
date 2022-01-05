using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    [SerializeField] GameObject WinText;
	public GameObject FadeToBlackForMenuUI;
    BossHP bossHPScript;
	FadeToBlack fadeToBlack;
	FadeToBlackForMenu fadeToBlackForMenu;

    void Start()
    {
        bossHPScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHP>();
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
			}
		}
	}
	
	void FadeOut()
	{
		fadeToBlack.Fade(false);
	}
}
