using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class ApplyForceSystem : IExecuteSystem {

    readonly IGroup<MessageEntity> _applyForceMessages;

    public ApplyForceSystem(Contexts contexts) {
        _applyForceMessages = contexts.message.GetGroup(MessageMatcher.ApplyForceMessage);
    }

    public void Execute() {
        var entities = _applyForceMessages.GetEntities();
        for (var i = entities.Length - 1; i >= 0; i--) {
            var message = entities[i].applyForceMessage;
            message.Target.AddForce(message.Force, message.Mode);
            if (entities[i].isDestroyOnConsume) {
                entities[i].Destroy();
            }
        }
    }
}
