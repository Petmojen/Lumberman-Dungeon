using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	Timer timerScript;
	void Start()
	{
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
	}
	
    public void PlayGame()
    {
        Time.timeScale = 1f;
		timerScript.timeOut = false;
        SceneManager.LoadScene("Map");
    }

    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
