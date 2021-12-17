using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawning:MonoBehaviour
{
    [SerializeField] GameObject topLeft, bottomRight, minionPrefab;
    GameObject[] holdAliveMinions;
    GameObject playerPosition;

    float minionSpawnPadding = 1, randomPointX, randomPointY;
    int numberOfMinions, numberOfDead;

    public bool bossInvicible, spawnActive;
    bool spawnChecked;

    void Start()
    {
        holdAliveMinions = new GameObject[6];
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(spawnActive && !bossInvicible)
        {
            CreateSpawnPoint();
        }

        if(bossInvicible && numberOfDead != numberOfMinions)
        {
            for(int x = 0; x < numberOfMinions; x++)
            {
                if(holdAliveMinions[x] == null)
                {
                    numberOfDead++;
                } 
            }

            if(numberOfDead == numberOfMinions)
            {
                bossInvicible = false;
            }
            numberOfDead = 0;
        }
    }

    void CreateSpawnPoint()
    {
        bossInvicible = true;
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
    }
}
