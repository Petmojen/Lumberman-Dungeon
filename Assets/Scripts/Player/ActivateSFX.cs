using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSFX : MonoBehaviour
{
	ParticleSystem particleSystem;
	PlayerHpSystem playerHPSystemScript;
	float checkForDamage;
    // Start is called before the first frame update
    void Start()
    {
		playerHPSystemScript = GameObject.FindObjectOfType(typeof(PlayerHpSystem)) as PlayerHpSystem;
		particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
		checkForDamage = playerHPSystemScript.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHPSystemScript.health < checkForDamage)
		{
			particleSystem.startLifetime = 1f;
			Invoke(nameof(sfxTimer), 0.5f);
		}
    }
	void sfxTimer()
	{
		particleSystem.startLifetime = 0f;
		checkForDamage = playerHPSystemScript.health;
	}
}
