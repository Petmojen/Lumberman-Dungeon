using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
	//Leaf attack stuff
	[SerializeField] GameObject leafPrefab, branchPrefab;
    GameObject bossPositionOffset;

    [SerializeField] MinionSpawning activateMinionSpawning;

    ForceToBossDarkness darknessScript;

	int noofAttacks, attackRandomizer;
    public bool bossAwake = false;
	bool attackCooldown = false;
	public string attackType;
	Timer timerScript;
	
	public enum Attacks {Leafs, Minion, Darkness, BranchSweep};

    void Start()
    {
        bossPositionOffset = GameObject.Find("BossOffset");
        noofAttacks = System.Enum.GetNames(typeof(Attacks)).Length;
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
        darknessScript = GameObject.FindObjectOfType(typeof(ForceToBossDarkness)) as ForceToBossDarkness;
    }

    void Update()
    {
        if(timerScript.timeOut && darknessScript.radiusOfLight < 13.51f)
        {
            AttackManager();
        }
    }

	void AttackManager()
	{
		if (!attackCooldown)
		{
            if(bossAwake)
            {
                attackCooldown = true;
                attackRandomizer = Random.Range(0, noofAttacks);
                attackType = System.Enum.GetName(typeof(Attacks), attackRandomizer);
            } else {
                attackType = "";
            }
        
			switch (attackType)
			{
				case "Minion":
                    MinionAttack();
				    break;
				case "Darkness":
                    Darkness();
                    break;
				case "BranchSweep":
                    BranchSweepAttack();
                    break;
				case "Leafs":
				    FireLeaf();
                    Invoke(nameof(SwitchAttack), 5f);
                    break;
			}
		}
	}

    void BranchSweepAttack()
    {
        Instantiate(branchPrefab, bossPositionOffset.transform.position + new Vector3(0, -8, 0), Quaternion.identity);
        Invoke(nameof(SwitchAttack), 5f);
    }

    void MinionAttack()
    {
        activateMinionSpawning.spawnActivated = true;
        Invoke(nameof(SwitchAttack), 4f);
    }

    void Darkness()
    {
        darknessScript.ability = true;
        Invoke(nameof(SwitchAttack), 4f);
    }

	// Leaf attack
    void FireLeaf()
	{
        InvokeRepeating(nameof(Shoot), 0, 1);
    }

    void SwitchAttack()
	{
        activateMinionSpawning.spawnActivated = false;
        darknessScript.ability = false;
        attackCooldown = false;
		CancelInvoke();
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


