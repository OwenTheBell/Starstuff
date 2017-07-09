using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupEffect : MonoBehaviour {

    public ParticleSystem Particles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play() {
        Particles.Play();
    }
}
