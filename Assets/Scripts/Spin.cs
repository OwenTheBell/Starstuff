using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

	public KeyCode LeftKey;
	public KeyCode RightKey;
	public float Torque;

	void Start ()
	{
		
	}
	
	void FixedUpdate ()
	{
		var adjust = 0;
		if (Input.GetKey(LeftKey) && !Input.GetKey(RightKey))
		{
			adjust = 1;
		}
		if (!Input.GetKey(LeftKey) && Input.GetKey(RightKey))
		{
			adjust = -1;
		}
		GetComponent<Rigidbody2D>().AddTorque(Torque * adjust);
	}
}
