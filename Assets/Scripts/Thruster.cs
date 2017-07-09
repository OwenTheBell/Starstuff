using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

	public KeyCode ThrustKey;
	public float Force;
    public bool HasMaxVelcity;
    public float MaxVelocity;

    [Range(0, 1)]
    public float Dampening;
	
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
		if (Input.GetKey(ThrustKey)) {
			var force = transform.up * Force;
			rigidbody.AddForce(force, ForceMode2D.Force);
            // if the added force is sufficiently far off from the current motion
            // add some dampening to help aid the turn
            var velocity = GetComponent<Rigidbody2D>().velocity;
            var angle1 = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            var angle2 = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            if (Mathf.Abs(angle1 - angle2) > 20f) {
                ApplyDampening();
            }
		}
        else {
            ApplyDampening();
        }
        if (HasMaxVelcity && rigidbody.velocity.magnitude >= MaxVelocity) {
            var velocity = rigidbody.velocity;
            velocity.Normalize();
            rigidbody.velocity = velocity * MaxVelocity;
        }
	}

    void ApplyDampening() {
        var rigidbody = GetComponent<Rigidbody2D>();
        var velocity = -Dampening * rigidbody.velocity;
        rigidbody.AddForce(velocity, ForceMode2D.Force);
    }
}