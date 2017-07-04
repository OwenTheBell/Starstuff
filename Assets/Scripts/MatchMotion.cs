using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMotion : MonoBehaviour {

    public GameObject Target;
    public float Scale;

    private Vector2 _LastPosition;

	void Start () {
        _LastPosition = Target.transform.position;
	}
	
	void Update () {
        var delta = (Vector2)Target.transform.position - _LastPosition;
        transform.position += (Vector3)delta * Scale;
        _LastPosition = Target.transform.position;
	}
}
