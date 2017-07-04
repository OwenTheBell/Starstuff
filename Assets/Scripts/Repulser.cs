using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulser : MonoBehaviour {

	public float Force;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void FixedUpdate() {
		if (gameObject.HasComponentInChildren<Detector>()) {
			var bodies = GetComponentInChildren<Detector>().NearbyBodies;
			foreach (var body in bodies) {
				var selfPos = transform.position;
				var targetPos = body.position;
				var angle = Mathf.Atan2(selfPos.y - targetPos.y, selfPos.x - targetPos.x);
				var force = new Vector2(Force * Mathf.Cos(angle), Force * Mathf.Sin(angle));
				GetComponent<Rigidbody2D>().AddForce(force);
			}
		}
	}
}
