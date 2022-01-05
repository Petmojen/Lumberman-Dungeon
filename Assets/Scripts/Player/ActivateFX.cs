using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFX : MonoBehaviour
{
	ParticleSystem pS;
	PlayerHpSystem playerHPSystemScript;
	float checkForDamage;

    void Start()
    {
		playerHPSystemScript = GameObject.FindObjectOfType(typeof(PlayerHpSystem)) as PlayerHpSystem;
		pS = GetComponent<ParticleSystem>();
        pS.Play();
		var main = pS.emission;
		main.enabled = false;
		checkForDamage = playerHPSystemScript.health;
    }

    void Update()
    {
        if (playerHPSystemScript.health < checkForDamage)
		{
			var main = pS.emission;
			main.enabled = true;
			Invoke(nameof(fxTimer), 0.5f);
		}
    }
	void fxTimer()
	{
		var main = pS.emission;
		main.enabled = false;
		checkForDamage = playerHPSystemScript.health;
	}
}
