using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

// all components needs to inherit from sm_Component so that they can be
// serialized in the inspector
[System.Serializable]
public abstract class sm_Component : System.Object, IComponent { };

[Game, Input, Message]
public class IdComponent : sm_Component {
    [PrimaryEntityIndex]
    public int value;
}

// rendering
[Game]
public class ViewComponent : sm_Component {
    public GameObject gameObject;
    public Transform transform { get { return gameObject.transform; } }
}

[Game]
public class Rigidbody2DComponent : sm_Component {
    public Rigidbody2D body2D;
}

[Game, Unique]
public class PlayerComponent : sm_Component { }

[Game]
public class StarComponent : sm_Component { }

[Game]
public class FollowingPlayerComponent : sm_Component { }

[Game, System.Serializable]
public class ThrustPerFollowerComponent : sm_Component {
    public float BaseSpeed;
    public float SpeedPerFollower;
    public float BaseThrust;
    public float ThrustPerFollower;
}

[Game]
public class MatchMotionComponent : sm_Component {
    public GameObject gameObject;
    public float Scale;
    public Vector2 _lastPosition;
}

[Game, System.Serializable]
public class ThrusterComponent : sm_Component {
    public float Force;
    public float Dampening;
}

[Game]
public class ThrustringComponent : sm_Component { }

public class AppliedThrustComponent : sm_Component {
    public Vector2 value;
}

[Game]
public class MaxVelocityComponent : sm_Component {
    public float MaxVelocity;
}

[Game, System.Serializable]
public class SpinComponent : sm_Component {
    public float Torque;
    public float Dampening;
}

[Game]
public class SpinningComponent : sm_Component { }

[Game]
public class BackgroundLayer : sm_Component {
    public BackgroundTileSetup TileSetup;
    public List<GameEntity> _Tiles;
}

[Game]
public class BackgroundTile : sm_Component { }

[Game, Unique, System.Serializable]
public class StarSpawnInfo : sm_Component {
    public GameObject StarPrefab;
    public float Arc;
    public float Distance;
    public Vector2 Range;
    public int MaxStars;
    public float GapIncrease;

    [HideInInspector]
    public float _RemainingDistance;
    [HideInInspector]
    public Vector3 _LastPosition;
}

[Game]
public class WaitingComponent : sm_Component { }
[Game]
public class CatchingUpComponent : sm_Component { }
[Game]
public class FollowingComponent : sm_Component { }

[Game, System.Serializable]
public class WaitComponent : sm_Component {
    public bool BeVisible;
    public float Range;
    public float Delay;
    [HideInInspector]
    public float _RemainingDelay = -1f;
}

[Game, System.Serializable]
public class CatchupComponent : sm_Component {
    public float Range;
    public float Factor;
    public float MinimumMagnitude;
}

[Game, System.Serializable]
public class FollowComponent : sm_Component {
    public float RetargetSpeed;
}

[Game, System.Serializable]
public class ChangingMovementStateComponent : sm_Component {
    public float Time;
    [HideInInspector]
    public float _Remaining = -1;
    [HideInInspector]
    public Vector2 _OldVelocity = Vector2.zero;
}

[Game]
public class RepulserComponent : sm_Component {
    public float angleVariance;
    public float force;
    public float range;
}

[Game]
public class ImmuneToRepulsionComponent : sm_Component { }

[Game]
public class TrackedTransformComponent : sm_Component {
    public Transform Transform;
}

[Game]
public class DampenInertiaComponent : sm_Component {
    public float value;
}

[Game]
public class DampenSpinComponent : sm_Component {
    public float value;
}

[Game]
public class UpdateBufferComponent : sm_Component {
    public FixedUpdateBuffer buffer;
}

[Game]
public class BodyComponent : sm_Component {
    public Rigidbody value;
}

[Game]
public class Body2DComponent : sm_Component {
    public Rigidbody2D value;
}

// input
[Input, Unique]
public class LeftMouseComponent : sm_Component { }

[Input, Unique]
public class RightMouseComponent : sm_Component { }

[Input]
public class MouseDownComponent : sm_Component {
    public Vector2 position;
}

[Input]
public class MousePositionComponent : sm_Component {
    public Vector2 position;
}

[Input]
public class MouseUpComponent : sm_Component {
    public Vector2 position;
}

[Input]
public class KeyDownComponent : sm_Component { }

[Input]
public class KeyComponent : sm_Component {
    public KeyCode key;
    public string name;
}

[Input]
public class KeyUpComponent : sm_Component { }

[Game, Input, Message]
public class Destroyed : sm_Component { }

[Game, Unique]
public class TickTracker : sm_Component {
    public float Tick;
    public float Time;
    public float Scale;
    public float Fixed;
}

[Game, Unique]
public class Paused : sm_Component { }

[Game]
public class PreservedBodyStateComponent : sm_Component {
    public int Id;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public bool isKinematic;
}

[Game]
public class PreservedBody2DStateComponent : sm_Component {
    public int Id;
    public Vector2 velocity;
    public float angularVelocity;
    public bool isKinematic;
}

[Game]
public class ThrustParticleComponent : sm_Component {
    public ParticleSystem system;
}

[Game]
public class BehaviorDelayComponent : sm_Component {
    public float value;
}

[Game]
public class CirclePlayerComponent : sm_Component { }

[Game]
public class CircleStarComponent : sm_Component { }

[Game]
public class TwirlComponent : sm_Component {
    public float duration;
    public int partnerId;
    public float distance;
    public float force;
    public bool clockwise; 
}

[Game]
public class SlamStarComponent : sm_Component { }

[Game]
public class CatapultComponent : sm_Component { }

[Game]
public class PullTowardsComponent : sm_Component {
    public int id;
    public float force;
}