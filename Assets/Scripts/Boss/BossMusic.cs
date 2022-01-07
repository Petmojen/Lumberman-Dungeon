using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    BossAnimationManager animScript;
    AudioClip bossMs;
    AudioSource bossSrc;
    bool playOnce;

    void Start()
    {
        animScript = GameObject.FindObjectOfType(typeof(BossAnimationManager)) as BossAnimationManager;
        bossMs = Resources.Load<AudioClip>("BossMusic");
        bossSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(animScript.wakeOnce && !playOnce)
        {
            bossSrc.Stop();
            bossSrc.PlayOneShot(bossMs);
            playOnce = true;
        }
    }
}
