using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour
{
	[SerializeField] Text tutorialTextInput;
	public string typeOfItem = "";
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (typeOfItem == "Tutorial Vine")
		{
			public string textTest;
			textTest = "dfsgvsroykytöbodftyjhm";
			tutorialTextInput.text = textTest;
		}
    }
}
