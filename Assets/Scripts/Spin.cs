using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

	public KeyCode LeftKey;
	public KeyCode RightKey;
	public float Torque;

    [Range(0f, 0.1f)]
    public float Dampening;

	void Start ()
	{
		
	}
	
	void FixedUpdate ()
	{
		var adjust = 0;
        var keyDown = false;
		if (Input.GetKey(LeftKey) && !Input.GetKey(RightKey))
		{
            keyDown = true;
			adjust = 1;
		}
		if (!Input.GetKey(LeftKey) && Input.GetKey(RightKey))
		{
            keyDown = true;
			adjust = -1;
		}
        var rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.AddTorque(Torque * adjust);
        var angularVelocity = rigidbody.angularVelocity;
        if (!keyDown || angularVelocity/Mathf.Abs(angularVelocity) != adjust/Mathf.Abs(adjust)) {
            angularVelocity = -angularVelocity * Dampening;
            rigidbody.AddTorque(angularVelocity);
        }
	}
}
