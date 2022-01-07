using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinShield : MonoBehaviour
{
	RectTransform rectTransform;
	public float rotateTimer = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
		if (rotateTimer < 1f)
		{
			rectTransform.Rotate(0f, 2f, 0.0f, Space.Self);
			rotateTimer += Time.deltaTime;
		} else if (rectTransform.rotation.y >= -0.5f && rectTransform.rotation.y < 0.5f)
		{
			rectTransform.rotation = Quaternion.Euler(0, 0, 0);
		}
    }
	    void OnDisable()
    {
        rotateTimer = 0f;
    }
}
