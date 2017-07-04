using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{

	public KeyCode ThrustKey;
	public float Force;

	void Start ()
	{
		
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKey(ThrustKey))
		{
			var force = transform.up * Force;
			GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
		}

		if (gameObject.HasComponentInChildren<ParticleSystem>())
		{
			if (Input.GetKeyDown(ThrustKey))
			{
				GetComponentInChildren<ParticleSystem>().Play();
			}
			else if (Input.GetKeyUp(ThrustKey))
			{
				GetComponentInChildren<ParticleSystem>().Stop();
			}
		}
	}
}