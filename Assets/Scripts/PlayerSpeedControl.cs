using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedControl : MonoBehaviour {

    public float SpeedPerFollower;

    private List<GameObject> _Followers;

    private void Awake() {
        _Followers = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Thruster>().MaxVelocity = (_Followers.Count + 1) * SpeedPerFollower;
	}

    public void AddNewFollower(GameObject follower) {
        if (!_Followers.Contains(follower)) {
            _Followers.Add(follower);
            Debug.Log("I've got a new follower");
        }
    }

    public void RemoveFollower(GameObject follower) {
        _Followers.Remove(follower);
    }
}
