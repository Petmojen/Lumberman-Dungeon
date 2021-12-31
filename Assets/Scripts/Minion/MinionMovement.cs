using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement:MonoBehaviour
{
    MinionAnimation animationScript;
    public Vector2 punchDirection;
    MinionHpSystem healthScript;
    GameObject playerPosition;
    float speed = 3, angle;
    public bool punching;
    Rigidbody2D rgbd2D;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        animationScript = GetComponent<MinionAnimation>();
        healthScript = GetComponent<MinionHpSystem>();
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!animationScript.spawning && !healthScript.isDead && !healthScript.knockBack)
        {
            Vector3 forcedDirection = playerPosition.transform.position - transform.position;
            angle = Mathf.Atan2(forcedDirection.y, forcedDirection.x) * Mathf.Rad2Deg;

            if(Vector2.Distance(transform.position, playerPosition.transform.position) > 3f && !punching)
            {
                forcedDirection = forcedDirection.normalized;
                rgbd2D.velocity = forcedDirection * speed;
            } else if(!punching) {
                punchDirection = forcedDirection;
                rgbd2D.velocity = Vector2.zero;
                punching = true;
                Invoke(nameof(Punch), 0.4f);
            }
        } else if(healthScript.knockBack || healthScript.isDead) {
            rgbd2D.velocity = Vector2.zero;
            rgbd2D.velocity = healthScript.knockDirection.normalized * 5;
        }
    }

    void Punch()
    {
        rgbd2D.velocity = punchDirection * 5;
        Invoke(nameof(Idle), 0.4f);
    }

    void Idle()
    {
        punching = false;
        CancelInvoke();
    }
}
