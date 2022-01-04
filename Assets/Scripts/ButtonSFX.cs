using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip clickFx;

    public void ClickSound()
    {
        audioSrc.PlayOneShot(clickFx);
    }
}
