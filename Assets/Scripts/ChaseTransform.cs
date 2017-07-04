using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTransform : MonoBehaviour
{

	public Transform Target;
	public float Force;
	public AnimationCurve DistanceAmplification;
	public float AmplificationCutoff;

	void Start ()
	{
		
	}
	
	void FixedUpdate ()
	{
		var selfPos = transform.position;
		var targetPos = Target.position;
		var distance = Vector2.Distance(selfPos, targetPos);
		var percent = distance / AmplificationCutoff;
		var amp = Mathf.Clamp(percent, 1f, 2f) - 1f;
		var angle = Mathf.Atan2(selfPos.y - targetPos.y, selfPos.x - targetPos.x);
		var force = new Vector2(Force * Mathf.Cos(angle), Force * Mathf.Sin(angle));
		force *= DistanceAmplification.Evaluate(amp);

		var relativeAngle = Vector2.Angle(GetComponent<Rigidbody2D>().velocity, force);
		if (relativeAngle > 10 && GetComponent<Rigidbody2D>().velocity.magnitude > 2)
		{
			var rigidbody = GetComponent<Rigidbody2D>();
			var velocity = rigidbody.velocity;	
			velocity = -velocity * 2;
			rigidbody.AddForce(velocity, ForceMode2D.Force);
		}
		GetComponent<Rigidbody2D>().AddForce(-force, ForceMode2D.Force);
	}
}
