using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

	public LayerMask DetectLayers;

	public List<Rigidbody2D> NearbyBodies { get; private set; }


	void Awake() {
		NearbyBodies = new List<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((1 << other.gameObject.layer & DetectLayers) > 0) {
			NearbyBodies.Add(other.GetComponent<Rigidbody2D>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		NearbyBodies.Remove(other.GetComponent<Rigidbody2D>());
	}
}
