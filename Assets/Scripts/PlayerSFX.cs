using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    /*The SFX List is as follows
     * 0. PlayerFootsteps
     */
    [SerializeField]
    AudioClip[] playerSFX;
    AudioSource audioSource;
    bool audioIsLooping = false;

    Rigidbody2D rb2d;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FootstepPlaySequence();
    }
    public void PlayFootstepSFX()
    {
        audioSource.clip = playerSFX[0];
        audioSource.pitch = Random.Range(0.7f,1.4f);
        audioSource.Play();
    }
    void FootstepPlaySequence()
    {
        if (rb2d.velocity.magnitude > 0.10f && !audioIsLooping)
        {
            InvokeRepeating(nameof(PlayFootstepSFX), 0, 0.5f);
            audioIsLooping = true;
        }
        else if (rb2d.velocity.magnitude <= 0.10f)
        {
            CancelInvoke(nameof(PlayFootstepSFX));
            audioIsLooping = false;
        }
    }
}
