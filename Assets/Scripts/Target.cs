using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	void Update()
	{
		Vector2 mousePosition = Input.mousePosition;
		Vector2 position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

		transform.position = position;
	}
}
