using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFX : MonoBehaviour
{

    public static AudioClip BranchSound, DarknessSound, LeafSound, SpiritMinionSound, RootSnareSound;
    static AudioSource audioSrc;

    void Start()
    {
        BranchSound = Resources.Load<AudioClip>("BranchSwipe");
        DarknessSound = Resources.Load<AudioClip>("DarknessAbility");
        LeafSound = Resources.Load<AudioClip>("LeafAttack");
        SpiritMinionSound = Resources.Load<AudioClip>("SpiritMinion");
        RootSnareSound = Resources.Load<AudioClip>("RootSnare");

        audioSrc = GetComponent<AudioSource>();
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
            case "SpiritMinionSound":
                audioSrc.PlayOneShot(SpiritMinionSound);
                break;
            case "RootSnareSound":
                audioSrc.PlayOneShot(RootSnareSound);
                break;
        }
    }
}
