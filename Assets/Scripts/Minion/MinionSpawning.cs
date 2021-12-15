using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawning:MonoBehaviour
{
    [SerializeField] GameObject topLeft, bottomRight, minionPrefab;
    float minionSpawnPadding = 1, randomPointX, randomPointY;
    bool spawnMinionCycle, spawnPointCleared ;
    GameObject[] holdAliveMinions;
    public bool spawnActivated;
    GameObject playerPosition;
    int numberOfMinions, numberOfDead;

    void Start()
    {
        spawnMinionCycle = true;
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Debug.DrawRay(topLeft.transform.position, transform.right * 19, Color.blue);
        //Debug.DrawRay(topLeft.transform.position, -transform.up * 11, Color.blue);
        //Debug.DrawRay(bottomRight.transform.position, -transform.right * 19, Color.blue);
        //Debug.DrawRay(bottomRight.transform.position, transform.up * 11, Color.blue);


        if(spawnActivated)
        {
            if(spawnMinionCycle && numberOfDead == numberOfMinions)
            {
                numberOfMinions = Random.Range(3, 5);
                holdAliveMinions = new GameObject[numberOfMinions];
                for(int i = 0; i <= numberOfMinions; i++)
                {
                    spawnPointCleared = false;
                    CreateSpawnPoint();
                }
                spawnMinionCycle = false;
                Invoke(nameof(SpawnCycle), 5);
            }
        }
    }

    void CreateSpawnPoint()
    {
        while(!spawnPointCleared)
        {
            for(int i = 0; i < numberOfMinions; i++)
            {
                randomPointY = Random.Range(bottomRight.transform.position.y + minionSpawnPadding, topLeft.transform.position.y - minionSpawnPadding);
                randomPointX = Random.Range(bottomRight.transform.position.x - minionSpawnPadding, topLeft.transform.position.x + minionSpawnPadding);

                Vector3 checkSpawnPos = new Vector3(randomPointX, randomPointY, 0);

                if(Vector3.Distance(checkSpawnPos, playerPosition.transform.position) > 3)
                {
                    holdAliveMinions[i] = Instantiate(minionPrefab, new Vector3(randomPointX, randomPointY, 0), Quaternion.identity);
                    spawnPointCleared = true;
                }
            }
        }
    }

    void SpawnCycle()
    {
        spawnMinionCycle = true;
        CancelInvoke(nameof(SpawnCycle));
    }
}
