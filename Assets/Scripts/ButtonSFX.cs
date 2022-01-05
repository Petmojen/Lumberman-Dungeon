using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip clickFx;
    public AudioClip subClickFx;

    public void ClickSound()
    {
        audioSrc.PlayOneShot(subClickFx);
    }

    public void ClickSoundChangeScene()
    {
        audioSrc.PlayOneShot(clickFx);
        DontDestroyOnLoad(gameObject);
        Destroy(gameObject, clickFx.length + 1);
    }
}
