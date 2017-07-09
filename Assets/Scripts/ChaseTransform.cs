using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct WaitingValues {
    public bool BeVisible;
    public float Range;
    public float Delay;
    [HideInInspector]
    public bool NotMoving;
    [HideInInspector]
    public float RemainingDelay;
}

[System.Serializable]
struct CatchupValues {
    public float Range;
    public float Factor;
    public float MinimumMagnitude;
}

[System.Serializable]
struct FollowValues {
    // degrees per second
    public float RetargetSpeed;
}

public class ChaseTransform : MonoBehaviour {

	public Transform Target;
    public float StateChangeTime;

    [SerializeField]
    private WaitingValues _Wait;
    [SerializeField]
    private CatchupValues _Catchup;
    [SerializeField]
    private FollowValues _Follow;

    private delegate Vector2 FixedUpdatedDelegate();
    private FixedUpdatedDelegate _UpdatedDelegate;
    private FixedUpdatedDelegate _NextUpdated;

    private Vector2 _oldVelocity;
    private float _remainingTransitionTime;

    private void Awake() {
        _UpdatedDelegate = WaitUpdate;
        _NextUpdated = WaitUpdate;
        _oldVelocity = Vector2.zero;
    }

    void Start () {
		
	}

    private void Update() {
        if (_NextUpdated != _UpdatedDelegate) {
            _UpdatedDelegate = _NextUpdated;
            _remainingTransitionTime = StateChangeTime;
        }
    }

    void FixedUpdate () {
        var velocity = _UpdatedDelegate();
        if (_remainingTransitionTime >= 0f) {
            _remainingTransitionTime -= Time.deltaTime;
            var percent = 1f - _remainingTransitionTime / StateChangeTime;
            velocity = Vector2.Lerp(_oldVelocity, velocity, percent);
        }
        GetComponent<Rigidbody2D>().velocity = velocity;
	}

    Vector2 WaitUpdate() {
        var distance = CalculateDistance();
        var renderer = GetComponentInChildren<Renderer>();
        if (!_Wait.NotMoving &&
            distance < _Wait.Range &&
            (!_Wait.BeVisible || (_Wait.BeVisible && renderer.isVisible))
        ) {
            _Wait.NotMoving = true;
            _Wait.RemainingDelay = _Wait.Delay;
        }
        if (_Wait.NotMoving) {
            _Wait.RemainingDelay -= Time.deltaTime;
            if (_Wait.RemainingDelay <= 0f) {
                if (gameObject.HasComponent<WakeupEffect>() &&
                    GetComponentInChildren<Renderer>().isVisible
                ) {
                    GetComponent<WakeupEffect>().Play();
                }
                Target.SendMessage("AddNewFollower", gameObject);
                _NextUpdated = CatchupUpdate;
                _oldVelocity = Vector2.zero;
            }
        }
        return Vector2.zero;
    }

    Vector2 CatchupUpdate() {
		var distance = CalculateDistance();
        if (distance < _Catchup.Range) {
            _NextUpdated = FollowUpdate;
            _oldVelocity = GetComponent<Rigidbody2D>().velocity;
            return _oldVelocity;
        }
       return CalculateChaseVelocity();
    }

    Vector2 FollowUpdate() {
        var distance = CalculateDistance();
        if (distance > _Catchup.Range) {
            _NextUpdated = CatchupUpdate;
            _oldVelocity = GetComponent<Rigidbody2D>().velocity;
            return _oldVelocity;
        }
        var targetVel = Target.GetComponent<Rigidbody2D>().velocity;
        var vel = GetComponent<Rigidbody2D>().velocity;
        var angle = Mathf.Atan2(targetVel.y - vel.y, targetVel.x - vel.x) * Mathf.Rad2Deg;
        var time = angle / 180 * _Follow.RetargetSpeed;
        var percent = Time.deltaTime / time;
        vel = Vector2.Lerp(vel, targetVel, percent);
        return vel;
    }

    float CalculateDistance() {
		var selfPos = transform.position;
		var targetPos = Target.position;
        return Vector2.Distance(selfPos, targetPos);
    }

    Vector3 CalculateChaseVelocity() {
		var selfPos = transform.position;
		var targetPos = Target.position;
        var radians = Mathf.Atan2(targetPos.y - selfPos.y, targetPos.x - selfPos.x);
        var targetVelocity = Target.GetComponent<Rigidbody2D>().velocity;
        var magnitude = targetVelocity.magnitude * _Catchup.Factor;
        if (magnitude < _Catchup.MinimumMagnitude) magnitude = _Catchup.MinimumMagnitude;
        var velocity = new Vector2(
            Mathf.Cos(radians) * magnitude,
            Mathf.Sin(radians) * magnitude
        );

        // if the star and player are moving roughly towards each other, then restrict
        // the magnitude of the velocity
        var relativeVelocity = velocity - targetVelocity;
        var targetVel = Target.GetComponent<Rigidbody2D>().velocity;
        var vel = GetComponent<Rigidbody2D>().velocity;
        var angle = Mathf.Atan2(targetVel.y - vel.y, targetVel.x - vel.x) * Mathf.Rad2Deg;
        // if relativeVelocity is greater than velocity, bodies are moving towards each other
        if (relativeVelocity.magnitude > magnitude) {
            var diff = relativeVelocity.magnitude - magnitude;
            magnitude -= diff;
            if (magnitude < _Catchup.MinimumMagnitude) magnitude = _Catchup.MinimumMagnitude;
            velocity.Normalize();
            velocity *= magnitude;
        }
        return velocity;
    }
}
