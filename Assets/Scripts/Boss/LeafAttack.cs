using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafAttack : MonoBehaviour
{
	[SerializeField] GameObject leafPrefab;
	float leafLimeTime;
	int noofLeafs;
int angle;
	bool firedLeaf = true;
    // Start is called before the first frame update
    void Start()
    {
		noofLeafs = 3;
		leafLimeTime = 2f;
		angle = 25;
    }

    // Update is called once per frame
    void Update()
    {
		if (firedLeaf)
        {
            InvokeRepeating(nameof(Shoot), 0, 1);
            firedLeaf = false;
        }
    }
	void Shoot()
    {
		for (float i = 0; i < noofLeafs; i++)
		{
			GameObject leafinstance = Instantiate(leafPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),  Quaternion.identity);
			Destroy(leafinstance, leafLimeTime);
		}
    }
}

