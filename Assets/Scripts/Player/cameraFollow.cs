using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool followPlayerX = true, followPlayerY = true;
	public LayerMask mask;
	public Vector2 bossRoomCenter;
	public float cameraSpeedX = 150f, cameraSpeedY = 150f;
	public float cameraSize;
	
	
	void Start()
	{
		cameraSize = Camera.main.orthographicSize;
		mask = LayerMask.GetMask("WallOutline");
		transform.position = player.transform.position;
	}
	
	

    void Update()
    {
		float edgeDistanceX = 12.8f;
		float edgeDistanceY = 7.5f;
		float stepX =  cameraSpeedX * Time.deltaTime;
		float stepY =  cameraSpeedY * Time.deltaTime;
		
		if(followPlayerX)
        {
			//Vector3 tempVector3X = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position =  Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), stepX);
        }
		
		if (transform.position.x > player.transform.position.x + 0.1f || transform.position.x < player.transform.position.x - 0.1f)
		{
			cameraSpeedX = 15f;
		} else {
			cameraSpeedX = 150f;
		}
		
		if(followPlayerY)
		{
			//Vector3 tempVector3Y = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            transform.position =  Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y, transform.position.z), stepY);
        }
		
		if (transform.position.y > player.transform.position.y + 0.1f || transform.position.y < player.transform.position.y - 0.1f)
		{
			cameraSpeedY = 15f;
		} else {
			cameraSpeedY = 150f;
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
		
		if (Vector2.Distance(new Vector2(player.transform.position.x, transform.position.y), bossRoomCenter) < 12 && Vector2.Distance(new Vector2(transform.position.x, player.transform.position.y), bossRoomCenter) < 12)
		{
			cameraSpeedY = 15f;
			cameraSpeedX = 15f;
			followPlayerX = false;
			followPlayerY = false;
			stepX =  cameraSpeedX * Time.deltaTime;
			transform.position =  Vector3.MoveTowards(transform.position, bossRoomCenter + new Vector2(0f, -2.5f), stepX);
			Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 10.5f, 5f * Time.deltaTime);
		} else {
			Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, cameraSize, 5f * Time.deltaTime);
		}
    }
}
