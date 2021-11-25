using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool followPlayer = true;
	public Vector2 direction;
	public LayerMask mask;
	
	void Start()
	{
		mask = LayerMask.GetMask("Walls");
	}
	
	

    void Update()
    {
		if(followPlayer)
        {
            transform.position = player.transform.position;
        }
		
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 3, mask);
		if (hit.distance <= 1)
		{
			followPlayer = false;
			mask = LayerMask.GetMask("UI");
			Debug.Log("hit");
		}
		if (hit.distance > 1)
		{
			followPlayer = true;
			mask = LayerMask.GetMask("Walls");
			Debug.Log("No hit");
		}	
 
    }
}
