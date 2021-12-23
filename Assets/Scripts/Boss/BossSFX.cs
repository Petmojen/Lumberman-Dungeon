using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFX : MonoBehaviour
{

    public static AudioClip BranchSound, DarknessSound, LeafSound;
    static AudioSource audioSrc;

    void Start()
    {
        BranchSound = Resources.Load<AudioClip>("BranchSwipe");
        DarknessSound = Resources.Load<AudioClip>("DarknessAbility");
        LeafSound = Resources.Load<AudioClip>("LeafAttack");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "BranchSound":
                audioSrc.PlayOneShot(BranchSound);
                break;
            case "DarknessSound":
                audioSrc.PlayOneShot(DarknessSound);
                break;
            case "LeafSound":
                audioSrc.PlayOneShot(LeafSound);
                break;
        }
    }
}
