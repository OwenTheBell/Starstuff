using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulser : MonoBehaviour {

    public float AngleVariance;
	public float Force;

    private float _AngleRandomizer;

    private void Awake() {
        _AngleRandomizer = Random.Range(-AngleVariance / 2f, AngleVariance / 2f) * Mathf.Deg2Rad;
    }

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
                angle += _AngleRandomizer;
				var force = new Vector2(Force * Mathf.Cos(angle), Force * Mathf.Sin(angle));
				GetComponent<Rigidbody2D>().AddForce(force);
			}
		}
	}
}
