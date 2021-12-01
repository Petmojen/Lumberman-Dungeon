using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSweep : MonoBehaviour
{
    GameObject bossPositionOffset;
    float angle;
    bool maxAttackRange;
    Rigidbody2D rgbd2D;
    float speed = 2f;
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        bossPositionOffset = GameObject.Find("BossOffset");
        rgbd2D.transform.RotateAround(bossPositionOffset.transform.position, Vector3.forward, 55f);
    }

    void GetAngle(Vector2 mousePos)
    {
        Vector2 lookDirection = (Vector2)bossPositionOffset.transform.position - (Vector2)transform.position;
        float getAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        Vector2 lookDirection = (Vector2)bossPositionOffset.transform.position - (Vector2)transform.position;
        float getAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Debug.Log(getAngle);
        if (!maxAttackRange)
        {
            angle = -0.1f;
            if (getAngle <= 30)
            {
                maxAttackRange = true;
            }
        }else{
            angle = 0.1f;
            if(getAngle >= 145)
            {
                Destroy(gameObject);
            }
        }
        rgbd2D.transform.RotateAround(bossPositionOffset.transform.position, Vector3.forward, angle);
    }
}
