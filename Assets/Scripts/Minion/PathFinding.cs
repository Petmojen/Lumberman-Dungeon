using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GameObject playerPosition, currentGridPoint, newGridPoint;
    GameObject[] holdConnectedGridPointData;
    Rigidbody2D rgbd2D;

    float angele, speed = 5;
    bool attackingPlayer, findGridPoint = true;
    CircleCollider2D detectGridPoint;
    
    GridPointData gridPointDataScript;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        detectGridPoint = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Debug.DrawLine(transform.position, playerPosition.transform.position, Color.blue);

        detectGridPoint.enabled = findGridPoint;

        if(Vector3.Distance(transform.position, playerPosition.transform.position) < 9f)
        {
            attackingPlayer = true;
        }

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
        if(Vector3.Distance(transform.position, playerPosition.transform.position) < 9f)
        {
            Vector2 lookDirection = (Vector2)playerPosition.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rgbd2D.velocity = transform.right * speed;
        } else {
            findGridPoint = true;
            attackingPlayer = false;
        }
    }

    void ChangeTarget()
    {
        holdConnectedGridPointData = gridPointDataScript.connectedGridPoints;
        float[] distance = new float[holdConnectedGridPointData.Length];
        for(int i = 0; i < holdConnectedGridPointData.Length; i++)
        {
            distance[i] = Vector2.Distance(playerPosition.transform.position, holdConnectedGridPointData[i].transform.position);
        }

        newGridPoint = holdConnectedGridPointData[0];
        float holdDistance = distance[0];

        for(int x = 1; x < distance.Length; x++)
        {
            if(holdDistance > distance[x])
            {
                holdDistance = distance[x];
                newGridPoint = holdConnectedGridPointData[x];
            }
        }

        currentGridPoint = newGridPoint;
        gridPointDataScript = currentGridPoint.GetComponent<GridPointData>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("GridPoint") && findGridPoint)
        {
            currentGridPoint = collision.gameObject;
            gridPointDataScript = currentGridPoint.GetComponent<GridPointData>();
            holdConnectedGridPointData = gridPointDataScript.connectedGridPoints;
            findGridPoint = false;
        }
    }
}
