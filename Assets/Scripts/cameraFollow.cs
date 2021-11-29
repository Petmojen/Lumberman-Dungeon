using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool followPlayerX = true, followPlayerY = true;
	public LayerMask mask;
	
	void Start()
	{
		mask = LayerMask.GetMask("WallOutline");
	}
	
	

    void Update()
    {
		float edgeDistanceX = 8.4f;
		float edgeDistanceY = 5.7f;
		
		if(followPlayerX)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
		if(followPlayerY)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
		
		RaycastHit2D hitRight = Physics2D.Raycast(player.transform.position, player.transform.right, edgeDistanceX, mask);
		RaycastHit2D hitUp = Physics2D.Raycast(player.transform.position, player.transform.up, edgeDistanceY, mask);
		RaycastHit2D hitLeft = Physics2D.Raycast(player.transform.position, -player.transform.right, edgeDistanceX, mask);
		RaycastHit2D hitDown = Physics2D.Raycast(player.transform.position, -player.transform.up, edgeDistanceY, mask);
		
		if (hitRight.collider != null || hitLeft.collider != null)
		{
			followPlayerX = false;
		}
		if (hitUp.collider != null || hitDown.collider != null)
		{
			followPlayerY = false;
		}
		if (hitRight.collider == null && hitLeft.collider == null)
		{
			followPlayerX = true;
		}
		if (hitUp.collider == null && hitDown.collider == null)
		{
			followPlayerY = true;
		}
    }
}
