using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    /*The SFX List is as follows
     * 0. PlayerFootsteps
     * 1. AxeMelee
     * 2. AxeThrow
     */
    [SerializeField]
    AudioClip[] playerSFX;
    AudioSource audioSource;

    Rigidbody2D rb2d;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb2d.velocity.magnitude > 0f)
        {
            Invoke(nameof(PlayFootstepSFX), 0.3f);
        }
    }
    public void PlayFootstepSFX()
    {
        audioSource.clip = playerSFX[0];
        audioSource.pitch = Random.Range(0.7f, 1.4f);
        audioSource.Play();
        CancelInvoke();
    }
    public void AxeMelee()
    {
        audioSource.clip = playerSFX[1];
        audioSource.Play();
    }
    public void AxeThrow()
    {
        audioSource.clip = playerSFX[2];
        audioSource.Play();
    }
}
