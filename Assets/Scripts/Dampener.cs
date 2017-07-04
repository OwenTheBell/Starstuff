using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dampener : MonoBehaviour
{

	[RangeAttribute(0, 1)]
	public float VelocityDampening;
	public float MaxCounterVelocity;
	[RangeAttribute(0, 1)]
	public float AngularDampening;
	public float MaxCounterAngular;

	void Start ()
	{
		
	}
	
	void FixedUpdate ()
	{
		var rigidbody = GetComponent<Rigidbody2D>();
		var velocity = rigidbody.velocity;	
		var angularVelocity = rigidbody.angularVelocity;

		velocity = (-velocity * VelocityDampening);
		velocity = Vector2.ClampMagnitude(velocity, MaxCounterVelocity);
		rigidbody.AddForce(velocity * VelocityDampening, ForceMode2D.Force);
		angularVelocity = -angularVelocity * AngularDampening;
		angularVelocity = Mathf.Clamp(angularVelocity, -MaxCounterAngular, MaxCounterAngular);
		rigidbody.AddTorque(angularVelocity);
	}
}
