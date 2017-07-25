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