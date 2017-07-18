using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System;

// rendering
[Game]
public class ViewComponent : IComponent {
    public GameObject gameObject;

    public Transform transform { get { return gameObject.transform; } }
}

[Game, Unique]
public class PlayerComponent : IComponent { }

[Game]
public class StarComponent : IComponent { }

[Game]
public class FollowingPlayerComponent : IComponent { }

[Game, System.Serializable]
public class ThrustPerFollowerComponent : IComponent {
    public float BaseSpeed;
    public float SpeedPerFollower;
    public float BaseThrust;
    public float ThrustPerFollower;
}

[Game]
public class MatchMotionComponent : IComponent {
    public GameObject gameObject;
    public float Scale;
    public Vector2 _lastPosition;
}

[Game, System.Serializable]
public class ThrusterComponent : IComponent {
    public float Force;
    public float Dampening;
}

[Game]
public class MaxVelocityComponent : IComponent {
    public float MaxVelocity;
}

[Game, System.Serializable]
public class SpinComponent : IComponent {
    public float Torque;
    public float Dampening;
}

[Game]
public class BackgroundLayer : IComponent {
    public BackgroundTileSetup TileSetup;
    public List<GameEntity> _Tiles;
}

[Game]
public class BackgroundTile : IComponent { }

// input
[Input, Unique]
public class LeftMouseComponent : IComponent { }

[Input, Unique]
public class RightMouseComponent : IComponent { }

[Input]
public class MouseDownComponent : IComponent {
    public Vector2 position;
}

[Input]
public class MousePositionComponent : IComponent {
    public Vector2 position;
}

[Input]
public class MouseUpComponent : IComponent {
    public Vector2 position;
}

[Input]
public class KeyDownComponent : IComponent { }

[Input]
public class KeyComponent : IComponent {
    public KeyCode key;
    public string name;
}

[Input]
public class KeyUpComponent : IComponent { }