using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

	public KeyCode ThrustKey;
	public float Force;
    public bool HasMaxVelcity;
    public float MaxVelocity;
	
	void FixedUpdate () {
		if (gameObject.HasComponentInChildren<ParticleSystem>()) {
			if (Input.GetKeyDown(ThrustKey)) {
				GetComponentInChildren<ParticleSystem>().Play();
			}
			else if (Input.GetKeyUp(ThrustKey)) {
				GetComponentInChildren<ParticleSystem>().Stop();
			}
		}

        var rigidbody = GetComponent<Rigidbody2D>();
        if (HasMaxVelcity && rigidbody.velocity.magnitude >= MaxVelocity) {
            return;
        }
		if (Input.GetKey(ThrustKey)) {
			var force = transform.up * Force;
			rigidbody.AddForce(force, ForceMode2D.Force);
		}

	}
}