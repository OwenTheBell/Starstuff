using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupEffect : MonoBehaviour {

    public ParticleSystem Particles;

    public void Play() {
        Particles.Play();
    }
}
