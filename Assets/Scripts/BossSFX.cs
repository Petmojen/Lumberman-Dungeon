using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFX : MonoBehaviour
{

    public static AudioClip BranchSound;
    static AudioSource auSrc;

    void Start()
    {
        BranchSound = Resources.Load<AudioClip>("BranchSwipe");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "BranchSound":
                auSrc.PlayOneShot(BranchSound);
                break;
        }
    }
}
