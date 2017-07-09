using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedControl : MonoBehaviour {

    public float BaseSpeed;
    public float SpeedPerFollower;

    private List<GameObject> _Followers;

    private void Awake() {
        _Followers = new List<GameObject>();
    }

    void Start () {
		
	}
	
	void Update () {
        GetComponent<Thruster>().MaxVelocity = _Followers.Count * SpeedPerFollower + BaseSpeed;
	}

    public void AddNewFollower(GameObject follower) {
        if (!_Followers.Contains(follower)) {
            _Followers.Add(follower);
        }
    }

    public void RemoveFollower(GameObject follower) {
        _Followers.Remove(follower);
    }
}
