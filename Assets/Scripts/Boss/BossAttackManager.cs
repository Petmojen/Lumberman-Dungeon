using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
	//Leaf attack stuff
	[SerializeField] GameObject leafPrefab, branchPrefab;
    GameObject bossPositionOffset;

    [SerializeField] MinionSpawning activateMinionSpawning;

    RootSnare snareActive;
    ForceToBossDarkness darknessScript;

	int numOfAttacks, attackRandomizer;
    public bool bossAwake = false;
	bool attackCooldown = false;
	public string attackType;
	Timer timerScript;
	
	public enum Attacks {Leafs, Minion, BranchSweep, RootSnare};

    void Start()
    {
        snareActive = GetComponent<RootSnare>();
        bossPositionOffset = GameObject.Find("BossOffset");
        numOfAttacks = System.Enum.GetNames(typeof(Attacks)).Length;
		timerScript = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
        darknessScript = GameObject.FindObjectOfType(typeof(ForceToBossDarkness)) as ForceToBossDarkness;
    }

    void Update()
    {
        if(timerScript.timeOut && darknessScript.radiusOfLight < 13.51f && !activateMinionSpawning.bossInvicible)
        {
            AttackManager();
        }
    }

	void AttackManager()
	{
		if(!attackCooldown)
		{
            if(bossAwake)
            {
                attackCooldown = true;
                attackRandomizer = Random.Range(0, numOfAttacks);
                attackType = System.Enum.GetName(typeof(Attacks), attackRandomizer);
            } else {
                attackType = "";
            }
        
			switch (attackType)
			{
				case "Minion":
                    MinionAttack();
				    break;
				case "BranchSweep":
                    BranchSweepAttack();
                    break;
				case "Leafs":
				    FireLeaf();
                    Invoke(nameof(SwitchAttack), 5f);
                    break;
                case "RootSnare":
                    RootSnareAttack();
                    break;
			}
		}
	}

    void RootSnareAttack()
    {
        snareActive.snareActivated = true;
        Invoke(nameof(SwitchAttack), 10f);
    }

    void BranchSweepAttack()
    {
        Instantiate(branchPrefab, bossPositionOffset.transform.position + new Vector3(0, -8, 0), Quaternion.identity);
        Invoke(nameof(SwitchAttack), 5f);
    }

    void MinionAttack()
    {
        activateMinionSpawning.spawnActive = true;
        Invoke(nameof(SwitchAttack), 5f);
    }

    // Leaf attack
    void FireLeaf()
	{
        InvokeRepeating(nameof(Shoot), 0, 1);
    }

    void SwitchAttack()
	{
        activateMinionSpawning.spawnActive = false;
        snareActive.snareActivated = false;
        attackCooldown = false;
		CancelInvoke();
	}

	void Shoot()
    {
		int numOfLeafs = 6;
		float leafLiveTime = 3f;
		for (float i = 0; i < numOfLeafs; i++)
		{
			GameObject leafinstance = Instantiate(leafPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),  Quaternion.identity);
			Destroy(leafinstance, leafLiveTime);
		}
    }
	
}


