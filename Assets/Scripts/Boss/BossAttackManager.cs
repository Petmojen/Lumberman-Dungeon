using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
	[SerializeField] GameObject leafPrefab, branchPrefab;
    MinionSpawning activateMinionSpawning;
    BossAnimationManager animationScript;
    DificultyManager dificultyScript;
    GameObject bossPositionOffset;
    RootSnare snareActive;

	int numOfAttacks, attackRandomizer;
    public bool bossAwake, wokenUp;
	bool attackCooldown;
	
	public enum State {Leafs, BranchSweep, Minion, RootSnare, Death};
    public System.Object current;

    void Start()
    {
        activateMinionSpawning = GameObject.FindObjectOfType(typeof(MinionSpawning)) as MinionSpawning;
        dificultyScript = GameObject.FindObjectOfType(typeof(DificultyManager)) as DificultyManager;
        //numOfAttacks = System.Enum.GetNames(typeof(State)).Length;
        animationScript = GetComponent<BossAnimationManager>();
        bossPositionOffset = GameObject.Find("BossOffset");
        snareActive = GetComponent<RootSnare>();
    }

    void Update()
    {
        if(wokenUp)
        {
            switch(dificultyScript.dificultyLevel)
            {
                case 0:
                    numOfAttacks = 1;
                    break;
                case 1:
                    numOfAttacks = 2;
                    break;
                case 2: 
                    numOfAttacks = 3;
                    break;
            }


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
                current = (State)System.Enum.ToObject(typeof(State), numOfAttacks);
                
            }
        
			switch (current)
			{
				case State.Minion:
                    MinionAttack();
				    break;
				case State.BranchSweep:
                    BranchSweepAttack();
                    break;
				case State.Leafs:
				    FireLeaf();
                    Invoke(nameof(SwitchAttack), 5f);
                    break;
                case State.RootSnare:
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


