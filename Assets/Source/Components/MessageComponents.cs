using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[Message]
public class MessageSender : IComponent {
    public ISystem Sender;
}

[Message]
public class DestroyOnConsume : IComponent { }

[Message]
public class PersistUntilConsumed : IComponent { }

[Message]
public class JustIssued : IComponent { }

[Message]
public class CanBeProcessed : IComponent { }

[Message]
public class SetVelocityMessage : IComponent {
    public Vector2 Velocity;
    public Rigidbody2D Target;
}

[Message]
public class SetAngularVelocityMessage : IComponent {
    public float Velocity;
    public Rigidbody2D Target;
}

[Message]
public class ApplyForceMessage : IComponent {
    public Vector2 Force;
    public ForceMode2D Mode;
    public Rigidbody2D Target;
}

[Message]
public class ApplyTorqueMessage : IComponent {
    public float Torque;
    public Rigidbody2D Target;
}