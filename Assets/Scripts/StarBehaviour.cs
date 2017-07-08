using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StarBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual Vector2 GetVelocity(Rigidbody2D targetBody) {
        return Vector2.zero; 
    }
}
