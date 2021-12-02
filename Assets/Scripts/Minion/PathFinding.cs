using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GameObject playerPosition, currentGridPoint, newGridPoint;
    [SerializeField] GameObject findGridPoint;
    GameObject[] holdConnectedGridPointData;
    Rigidbody2D rgbd2D;

    float angele, speed = 5;
    bool attackingPlayer;
    
    GridPointData gridPointDataScript;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        currentGridPoint = GameObject.FindGameObjectWithTag("GridPoint");
        gridPointDataScript = currentGridPoint.GetComponent<GridPointData>();
        holdConnectedGridPointData = gridPointDataScript.connectedGridPoints;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, playerPosition.transform.position) < 9f)
        {
            attackingPlayer = true;
        }

        Debug.DrawLine(transform.position, playerPosition.transform.position, Color.blue);
        Debug.Log(Vector3.Distance(transform.position, playerPosition.transform.position));

        if(Vector3.Distance(transform.position, currentGridPoint.transform.position) < 0.5f && !attackingPlayer)
        {
            ChangeTarget();
        } else if(!attackingPlayer) {
            for(int i = 0; i < holdConnectedGridPointData.Length; i++)
            {
                Debug.DrawLine(holdConnectedGridPointData[i].transform.position, playerPosition.transform.position, Color.green);
            }
            MoveTo(currentGridPoint);
        } else {
            AttackPlayer();
        }
        
    }

    void MoveTo(GameObject target)
    {
        Vector2 lookDirection = (Vector2)target.transform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rgbd2D.velocity = transform.right * speed;
    }

    void AttackPlayer()
    {
        Vector2 lookDirection = (Vector2)playerPosition.transform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rgbd2D.velocity = transform.right * speed;
    }

    void ChangeTarget()
    {
        holdConnectedGridPointData = gridPointDataScript.connectedGridPoints;
        float[] distance = new float[holdConnectedGridPointData.Length];
        for(int i = 0; i < holdConnectedGridPointData.Length; i++)
        {
            distance[i] = Vector2.Distance(playerPosition.transform.position, holdConnectedGridPointData[i].transform.position);
        }

        for(int x = 0; x < distance.Length - 1; x++)
        {
            if(distance[x] < distance[x + 1])
            {
                newGridPoint = holdConnectedGridPointData[x];
            } else {
                newGridPoint = holdConnectedGridPointData[x + 1];
            }
        }
        currentGridPoint = newGridPoint;
        gridPointDataScript = currentGridPoint.GetComponent<GridPointData>();
    }
}
