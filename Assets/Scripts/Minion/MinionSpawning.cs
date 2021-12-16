using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawning:MonoBehaviour
{
    [SerializeField] GameObject topLeft, bottomRight, minionPrefab;
    public GameObject[] holdAliveMinions = new GameObject[5];
    GameObject playerPosition;

    float minionSpawnPadding = 1, randomPointX, randomPointY;
    int numberOfMinions, numberOfDead;

    public bool bossInvicible, spawnActive;
    bool spawnChecked;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(spawnActive && !bossInvicible)
        {
            CreateSpawnPoint();
        }

        if(numberOfDead != numberOfMinions)
        {
            numberOfDead = 0;
            for(int i = 0; i < numberOfMinions; i++)
            {
                if(holdAliveMinions[i] == null)
                {
                    Debug.Log("Num:" + i + " is Empty");
                    holdAliveMinions[i] = null;
                    numberOfDead++;
                }
            }
        } else {
            bossInvicible = false;
        }
    }

    void CreateSpawnPoint()
    {
        numberOfMinions = Random.Range(3, 5);
        for(int i = 0; i < numberOfMinions; i++)
        {
            spawnChecked = false;
            while(!spawnChecked)
            {
                randomPointY = Random.Range(bottomRight.transform.position.y + minionSpawnPadding, topLeft.transform.position.y - minionSpawnPadding);
                randomPointX = Random.Range(bottomRight.transform.position.x - minionSpawnPadding, topLeft.transform.position.x + minionSpawnPadding);

                Vector3 checkSpawnPos = new Vector3(randomPointX, randomPointY, 0);

                if(Vector3.Distance(checkSpawnPos, playerPosition.transform.position) > 3)
                {
                    holdAliveMinions[i] = Instantiate(minionPrefab, new Vector3(randomPointX, randomPointY, 0), Quaternion.identity);
                    spawnChecked = true;
                }
            }
        }
        bossInvicible = true;
    }
}
