using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
	public Image image;
	public bool startOfScene = true;

    void Start()
    {
		image = GetComponent<Image>();
    }

    void Update()
    {
		if (startOfScene && image.color.a > 0f)
		{
			Fade(true);
		} else {
			startOfScene = false;
		}
    }
	public void Fade(bool FadeInOrOut)
	{
		float alphaBlend = image.color.a;
		
		if (!FadeInOrOut && alphaBlend < 1f)
		{
			alphaBlend += Time.deltaTime * 0.5f;
		}
		
		if (FadeInOrOut && alphaBlend > 0f)
		{
			alphaBlend -= Time.deltaTime * 0.5f;
		}
			
		image.color = new Color(0f, 0f, 0f, alphaBlend);
	}
}
