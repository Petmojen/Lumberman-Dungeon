using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManagerT : MonoBehaviour
{
	[SerializeField] GameObject leafPrefab;
    //MinionSpawning activateMinionSpawning;
    BossAnimationManagerT animationScript;
    DifficultyManager dificultyScript;
    GameObject bossPositionOffset;
    RootSnare snareActive;
    BossHPT healthScript;
	public int difficultyLevel;

	int numOfAttacks, attackRandomizer;
    public bool bossAwake, wokenUp;
	bool attackCooldown;
	
	public enum State {Leafs, BranchSweep, Minion, RootSnare, Death, Idle};
    public System.Object current;

    void Start()
    {
        //activateMinionSpawning = GameObject.FindObjectOfType(typeof(MinionSpawning)) as MinionSpawning;
        dificultyScript = GameObject.FindObjectOfType(typeof(DifficultyManager)) as DifficultyManager;
        animationScript = GetComponent<BossAnimationManagerT>();
        bossPositionOffset = GameObject.Find("BossOffset");
        snareActive = GetComponent<RootSnare>();
        healthScript = GetComponent<BossHPT>();
        current = State.Idle;
    }

    void Update()
    {
		difficultyLevel = DifficultyManager.difficultyLevel;
		
        if(wokenUp && !healthScript.bossDead)
        {
            switch(DifficultyManager.difficultyLevel)
            {
                case 0:
                    numOfAttacks = 0;
                    break;
                case 1:
                    numOfAttacks = 2;
                    break;
                case 2: 
                    numOfAttacks = 3;
                    break;
				case 3: 
                    numOfAttacks = 4;
                    break;
				case 4: 
                    numOfAttacks = 4;
                    break;
            }

            if((State)current == State.Idle)
            {
                Invoke(nameof(AttackManager), 3f);
            }

            attackRandomizer = Random.Range(0, numOfAttacks);
        }

    }

	void AttackManager()
	{
		if(!attackCooldown)
		{
            if(bossAwake)
            {
                attackCooldown = true;
				if (numOfAttacks >= 1)
				{
					current = (State)System.Enum.ToObject(typeof(State), attackRandomizer);
				} else {
					current = State.Idle;
				}
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
                    animationScript.idle = false;
                    Invoke(nameof(FireLeaf), 0.6f);
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
        animationScript.idle = false;
        Invoke(nameof(SwitchAttack), 3.1f);
    }

    void MinionAttack()
    {
        //activateMinionSpawning.spawnActive = true;
        Invoke(nameof(SwitchAttack), 5f);
    }

    void FireLeaf()
	{
        Invoke(nameof(SwitchAttack), 5f);
        InvokeRepeating(nameof(Shoot), 0, 1);
    }

    void SwitchAttack()
	{
        //activateMinionSpawning.spawnActive = false;
        snareActive.snareActivated = false;
        animationScript.idle = true;
        attackCooldown = false;
        current = State.Idle;
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


