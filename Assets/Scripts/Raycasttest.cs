using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasttest : MonoBehaviour
{
	 public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		LayerMask mask = LayerMask.GetMask("Walls");
		//Cast a ray in the direction specified in the inspector.
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, 2, mask);

        //If something was hit.
        if (hit.collider != null)
        {
            //Display the point in world space where the ray hit the collider's surface.
            Debug.Log(hit.point);
        }
    }

}
