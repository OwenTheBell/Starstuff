///////////////////////////////////////////////////////////////////////////////
/// Messages are for containing all impermanent state used to communicate between
/// systems. No values stored in these components should be expected to persist
/// for more than a few frames. Likewise, no data stored in these components
/// should be used to directly impact the game state or how it is interacted with
/// without first being transfered into components in other appropriate contexts.
///////////////////////////////////////////////////////////////////////////////

using System;
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
public class TriggerThrust : IComponent {
    public Vector3 direction;
}

[Message]
public class TriggerSpin : IComponent {
    public int value;
}

[Message]
public class MessageTarget : IComponent {
    public int id;
}

[Message]
public class BufferAction : IComponent {
    public ISystem issuer;
    public Action<Rigidbody> act;
}

[Message]
public class Buffer2DAction : IComponent {
    public ISystem issuer;
    public Action<Rigidbody2D> act;
}