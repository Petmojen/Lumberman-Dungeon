using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
	//Leaf attack stuff
	[SerializeField] GameObject leafPrefab;
	//
	
	
	int noofAttacks, attackRandomizer;
	public string attackType;
	bool attackCooldown = false;
	
	enum Attacks {Leafs, Dummy, Dummy2, Dummy3};
	
    // Start is called before the first frame update
    void Start()
    {
		noofAttacks = System.Enum.GetNames(typeof(Attacks)).Length;
    }

    // Update is called once per frame
    void Update()
    {
		AttackManager();
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
				case "Dummy":
				break;
				
				case "Dummy2":
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
	
	void SwitchAttack()
	{
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
		float leafLimeTime = 2f;
		for (float i = 0; i < noofLeafs; i++)
		{
			GameObject leafinstance = Instantiate(leafPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),  Quaternion.identity);
			Destroy(leafinstance, leafLimeTime);
		}
    }
	//
}


