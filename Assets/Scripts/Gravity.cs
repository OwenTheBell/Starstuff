using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

	public float Force;

	private Rigidbody2D _PlayerRigidbody;

	void Start ()
	{
		
	}
	
	void FixedUpdate ()
	{
		if (_PlayerRigidbody != null)
		{
			var selfPos = transform.position;
			var playerPos = _PlayerRigidbody.position;
			var distance = Vector2.Distance(selfPos, playerPos);
			var percent = distance / GetComponent<CircleCollider2D>().radius;
			var grav = new Vector2(Force * percent, 0);
			var angle = Mathf.Atan2(
										selfPos.y - playerPos.y,
									 	selfPos.x - playerPos.x
			);
			grav = new Vector2(
								grav.x * Mathf.Cos(angle) - grav.y * Mathf.Sin(angle),
								grav.x * Mathf.Sin(angle) + grav.y * Mathf.Cos(angle)
			);
			_PlayerRigidbody.AddForce(grav, 0);
		}	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		_PlayerRigidbody = other.GetComponent<Rigidbody2D>();
	}

	void OnTriggerExit2D(Collider2D other)
	{
		_PlayerRigidbody = null;
	}
}
