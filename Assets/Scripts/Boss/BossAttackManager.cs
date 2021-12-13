using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
	//Leaf attack stuff
	[SerializeField] GameObject leafPrefab;

    MinionSpawning activateMinionSpawning;

    ForceToBossDarkness darknessScript;

	Timer timerScript;
	int noofAttacks, attackRandomizer;
	public string attackType;
	bool attackCooldown = false;
	
	public enum Attacks {Leafs, Minion, Darkness, Dummy3};

    void Start()
    {
		noofAttacks = System.Enum.GetNames(typeof(Attacks)).Length;
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
		activateMinionSpawning = GameObject.FindObjectOfType(typeof(MinionSpawning)) as MinionSpawning;
        darknessScript = GameObject.FindObjectOfType(typeof(ForceToBossDarkness)) as ForceToBossDarkness;
    }

    void Update()
    {
		if (timerScript.timeOut)
		{
			AttackManager();
		}
    }
	void AttackManager()
	{
		if (!attackCooldown)
		{
			attackCooldown = true;
            attackRandomizer = Random.Range(0, noofAttacks);
            attackType = System.Enum.GetName(typeof(Attacks), attackRandomizer);
        
			switch (attackType)
			{
				case "Minion":
                    MinionAttack();
				    break;
				case "Darkness":
                    Darkness();
                    break;
				case "Dummy3":
                    
                    break;
				case "Leafs":
				    FireLeaf();
				    break;
			}
			Invoke(nameof(SwitchAttack), 4f);
		}
	}

    void MinionAttack()
    {
        activateMinionSpawning.spawnActivated = true;
    }

    void Darkness()
    {
        darknessScript.ability = true;
    }

	void SwitchAttack()
	{
        activateMinionSpawning.spawnActivated = false;
        darknessScript.ability = false;
        attackCooldown = false;
		CancelInvoke();
	}

	// Leaf attack
    void FireLeaf()
	{
            InvokeRepeating(nameof(Shoot), 0, 1);
	}


	void Shoot()
    {
		int noofLeafs = 3;
		float leafLimeTime = 4f;
		for (float i = 0; i < noofLeafs; i++)
		{
			GameObject leafinstance = Instantiate(leafPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),  Quaternion.identity);
			Destroy(leafinstance, leafLimeTime);
		}
    }
	
}


