using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack:MonoBehaviour
{
    [SerializeField] GameObject axePrefab, axeOffset;
	[SerializeField] GameObject axeAttackPrefab;
    public bool axeInAir = false;
    public bool axeinAttack = false;
    float axeThrowForce = 20;
    Rigidbody2D rgbd2D;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !axeInAir && !axeinAttack)
        {
            axeinAttack = true;
            GetAngle4Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        } else if(Input.GetMouseButtonDown(1) && !axeInAir) {
            axeInAir = true;
            GetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void GetAngle(Vector2 mousePos)
    {
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        axeOffset.transform.rotation = Quaternion.Euler(0, 0, angle);
        ThrowingAxe(angle);
    }

    void ThrowingAxe(float angle)
    {
        GameObject axe = Instantiate(axePrefab, axeOffset.transform.position, axeOffset.transform.rotation);
        rgbd2D = axe.GetComponent<Rigidbody2D>();
        rgbd2D.AddForce(axe.transform.right * axeThrowForce, ForceMode2D.Impulse);
    }
	void GetAngle4Attack(Vector2 mousePos)
    {
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        axeOffset.transform.rotation = Quaternion.Euler(0, 0, angle);
        Attack(angle);
    }
	void Attack(float angle)
	{
        GameObject axeAttack = Instantiate(axeAttackPrefab, axeOffset.transform.position + new Vector3(0.8f, 0, 0), Quaternion.identity);
        rgbd2D = axeAttack.GetComponent<Rigidbody2D>();
        axeAttack.transform.RotateAround(axeOffset.transform.position, Vector3.forward, angle);
    }
}
