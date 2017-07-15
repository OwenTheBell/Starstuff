using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System;

// location & orientation
[Game]
public class PositionComponent : IComponent {
    public Vector2 value;
}

[Game]
public class DirectionComponent : IComponent {
    public float value;
}

// rendering
[Game]
public class ViewComponent : IComponent {
    public GameObject gameObject;
}

[Game]
public class SpriteComponent : IComponent {
    public string name;
    public Sprite sprite;
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

// moving
[Game]
public class MoverComponent : IComponent { }

[Game]
public class MoveComponent : IComponent {
    public Vector2 target;
}

[Game, System.Serializable]
public class ThrusterComponent : IComponent {
    public float Force;
    public bool HasMaxVelocity;
    public float MaxVelocity;
    public float Dampening;
}

[Game, System.Serializable]
public class SpinComponent : IComponent {
    public float Torque;
    public float Dampening;
}


[Game]
public class MoveCompleteComponent : IComponent { }

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