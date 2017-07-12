using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

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
}

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
[Input]
public class LeftMouseComponent : IComponent { }

[Input]
public class RightMouseComponent : IComponent { }

[Input]
public class MouseDownComponet : IComponent {
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
