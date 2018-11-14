using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrAim : MonoBehaviour {
	public int maxAngle = 90;
	public int minAngle = -90;
	private float angle = 0;

	private int direction = 1; // +/- indicates rotate up/down, constant indicates degrees to rotate

	void FixedUpdate() 
	{
		// Change direction of rotation when Max or Min is reached
		if (angle >= maxAngle || angle <= minAngle) 
		{
			direction *= -1;
		}
		// Rotate
		transform.RotateAround (transform.parent.position, Vector3.forward, direction);
		// Tracking rotation
		angle += direction;


		// The direction the player is aiming.
		Debug.DrawRay (transform.position, transform.right, Color.green);
	}
}
