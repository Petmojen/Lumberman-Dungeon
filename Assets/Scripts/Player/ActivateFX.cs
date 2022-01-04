using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFX : MonoBehaviour
{
	ParticleSystem particleSystem;
	PlayerHpSystem playerHPSystemScript;
	float checkForDamage;

    void Start()
    {
		playerHPSystemScript = GameObject.FindObjectOfType(typeof(PlayerHpSystem)) as PlayerHpSystem;
		particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
		checkForDamage = playerHPSystemScript.health;
    }

    void Update()
    {
        if (playerHPSystemScript.health < checkForDamage)
		{
			var main = particleSystem.main;
			main.startLifetime = 1f;
			Invoke(nameof(fxTimer), 0.5f);
		}
    }
	void fxTimer()
	{
		var main = particleSystem.main;
		main.startLifetime = 0f;
		checkForDamage = playerHPSystemScript.health;
	}
}
