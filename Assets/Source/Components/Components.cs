using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

// all components needs to inherit from sm_Component so that they can be
// serialized in the inspector
[System.Serializable]
public abstract class sm_Component : System.Object, IComponent { };

// rendering
[Game]
public class ViewComponent : sm_Component {
    public GameObject gameObject;

    public Transform transform { get { return gameObject.transform; } }
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
public class MaxVelocityComponent : sm_Component {
    public float MaxVelocity;
}

[Game, System.Serializable]
public class SpinComponent : sm_Component {
    public float Torque;
    public float Dampening;
}

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
public class TrackedTransformComponent : sm_Component {
    public Transform Transform;
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