using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackT:MonoBehaviour
{
    [SerializeField] GameObject axePrefab, axeOffset, axeAttackPrefab;
    PlayerMovementT playerMovementScriptT;
    float axeThrowForce = 20;
    GameObject axeAttack;
    Rigidbody2D rgbd2D;
	
	void Start()
	{
		playerMovementScriptT = GameObject.FindObjectOfType(typeof(PlayerMovementT)) as PlayerMovementT;
	}

    void Update()
    {

        if(Input.GetMouseButtonDown(0) && playerMovementScriptT.axeAttack == PlayerMovementT.Attack.Idle)
        {
            GetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0);

        } else if(Input.GetMouseButtonDown(1) && playerMovementScriptT.axeAttack == PlayerMovementT.Attack.Idle) {
            GetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        }

		if(Input.GetAxisRaw("Melee") > 0f && playerMovementScriptT.axeAttack == PlayerMovementT.Attack.Idle)
        {
			Vector2 cAim = transform.position + new Vector3(Input.GetAxisRaw("HorizontalAim"), Input.GetAxisRaw("VerticalAim"), 0);
            GetAngle(cAim, 0);
        } else if(Input.GetAxisRaw("Throw") > 0f && playerMovementScriptT.axeAttack == PlayerMovementT.Attack.Idle) {
			Vector2 cAim = transform.position + new Vector3(Input.GetAxisRaw("HorizontalAim"), Input.GetAxisRaw("VerticalAim"), 0);
            GetAngle(cAim, 1);
        }
    }

    void GetAngle(Vector2 mousePos, int mouseInput)
    {
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        axeOffset.transform.rotation = Quaternion.Euler(0, 0, angle);
        switch(mouseInput)
        {
            case 0:
                MeeleAxe(angle);
                break;
            case 1:
                ThrowingAxe(angle);
                break;
        }
    }

    void ThrowingAxe(float angle)
    {
		playerMovementScriptT.axeAttack = PlayerMovementT.Attack.Throw;
        GameObject axe = Instantiate(axePrefab, axeOffset.transform.position, axeOffset.transform.rotation);
        if(angle > 90 || angle < -90)
        {
            axe.GetComponent<AxeMovement>().flipSpriteBool = true;
        }
        rgbd2D = axe.GetComponent<Rigidbody2D>();
        rgbd2D.AddForce(axe.transform.right * axeThrowForce, ForceMode2D.Impulse);
    }

	void MeeleAxe(float angle)
	{
		playerMovementScriptT.axeAttack = PlayerMovementT.Attack.Melee;
        axeAttack = Instantiate(axeAttackPrefab, axeOffset.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        axeAttack.transform.RotateAround(axeOffset.transform.position, Vector3.forward, angle);
    }

    public void EndMelee()
    {
        Destroy(axeAttack);
        playerMovementScriptT.axeAttack = PlayerMovementT.Attack.Idle;
        CancelInvoke();
    }
}
