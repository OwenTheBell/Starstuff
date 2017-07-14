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

[Game]
public class PlayerComponent : IComponent { }

[Game]
public class StarComponent : IComponent { }

// moving
[Game]
public class MoverComponent : IComponent { }

[Game]
public class MoveComponent : IComponent {
    public Vector2 target;
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
}

[Input]
public class KeyUpComponent : IComponent { }