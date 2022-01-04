using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFX : MonoBehaviour
{

    [SerializeField] AudioClip BranchSound;
    AudioClip DarknessSound, LeafSound, SpiritMinionSound, RootSnareSound;
    AudioSource audioSrc;

    void Start()
    {
        //BranchSound = Resources.Load<AudioClip>("BranchSwipe");
        //Debug.Log(Resources.Load<AudioClip>("Music_SoundFX/BranchSwipe"));
        DarknessSound = Resources.Load<AudioClip>("DarknessAbility");
        LeafSound = Resources.Load<AudioClip>("LeafAttack");
        SpiritMinionSound = Resources.Load<AudioClip>("SpiritMinion");
        RootSnareSound = Resources.Load<AudioClip>("RootSnare");

        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "BranchSound":
                audioSrc.PlayOneShot(BranchSound);
                Debug.Log(BranchSound);
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
