using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
	[SerializeField] GameObject leafPrefab;
    MinionSpawning activateMinionSpawning;
    BossAnimationManager animationScript;
    DifficultyManager dificultyScript;
    GameObject bossPositionOffset;
    RootSnare snareActive;

	int numOfAttacks, attackRandomizer;
    public bool bossAwake, wokenUp;
	bool attackCooldown;
	
	public enum State {NoAttacks, Leafs, BranchSweep, Minion, RootSnare, Death, Idle};
    public System.Object current;

    void Start()
    {
        activateMinionSpawning = GameObject.FindObjectOfType(typeof(MinionSpawning)) as MinionSpawning;
        dificultyScript = GameObject.FindObjectOfType(typeof(DifficultyManager)) as DifficultyManager;
        //numOfAttacks = System.Enum.GetNames(typeof(State)).Length;
        animationScript = GetComponent<BossAnimationManager>();
        bossPositionOffset = GameObject.Find("BossOffset");
        snareActive = GetComponent<RootSnare>();
        current = State.Idle;
    }

    void Update()
    {
        if(wokenUp && !activateMinionSpawning.bossInvicible)
        {
            switch(DifficultyManager.difficultyLevel)
            {
                case 0:
                    numOfAttacks = 0;
                    break;
                case 1:
                    numOfAttacks = 1;
                    break;
                case 2: 
                    numOfAttacks = 2;
                    break;
				case 3: 
                    numOfAttacks = 3;
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
                current = (State)System.Enum.ToObject(typeof(State), attackRandomizer);
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
                    break;
                case State.RootSnare:
                    RootSnareAttack();
                    break;
				case State.NoAttacks:
				    break;
				case State.Death:
					Shoot(false);
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
        activateMinionSpawning.spawnActive = true;
        Invoke(nameof(SwitchAttack), 5f);
    }

    void FireLeaf()
	{
        animationScript.idle = false;
        Invoke(nameof(SwitchAttack), 5f);
        InvokeRepeating(nameof(Shoot), 0, 1);
    }

    void SwitchAttack()
	{
        activateMinionSpawning.spawnActive = false;
        snareActive.snareActivated = false;
        animationScript.idle = true;
        attackCooldown = false;
        current = State.Idle;
        CancelInvoke();
	}

	void Shoot(bool noShoot)
    {
		int numOfLeafs = 6;
		float leafLiveTime = 3f;
		for (float i = 0; i < numOfLeafs; i++)
		{
			GameObject leafinstance = Instantiate(leafPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),  Quaternion.identity);
			if (!noShoot)
			{	
				Destroy(leafinstance, leafLiveTime);
			} else {
				Destroy(leafinstance, 0f);
			}
		} 
			
			
    }
	
}


