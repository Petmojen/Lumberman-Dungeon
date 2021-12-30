using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSnare : MonoBehaviour
{
    float rootSpawnPadding = 1, randomPointX, randomPointY, snareChance = 10;
    [SerializeField] GameObject topLeft, bottomRight, rootPrefab;
    public bool snareActivated;
    Transform playerPosition;
    bool snareCycle;


    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(snareActivated && !snareCycle)
        {
            BossSFX.PlaySound("RootSnare");
            snareCycle = true;
            if(Random.Range(0, 100) > snareChance)
            {
                randomPointY = Random.Range(bottomRight.transform.position.y + rootSpawnPadding, topLeft.transform.position.y - rootSpawnPadding);
                randomPointX = Random.Range(bottomRight.transform.position.x - rootSpawnPadding, topLeft.transform.position.x + rootSpawnPadding);
            } else {
                randomPointX = playerPosition.position.x;
                randomPointY = playerPosition.position.y;
            }
            Instantiate(rootPrefab, new Vector2(randomPointX, randomPointY), Quaternion.identity);
            Invoke(nameof(WaitSpawn), 1f);
        }
    }

    void WaitSpawn()
    {
        snareCycle = false;
        CancelInvoke();
    }
}
