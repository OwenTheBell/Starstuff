using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class ApplyTorqueSystem : IExecuteSystem {

    readonly IGroup<MessageEntity> _applyTorqueMessages;

    public ApplyTorqueSystem(Contexts contexts) {
        _applyTorqueMessages = contexts.message.GetGroup(MessageMatcher.ApplyTorqueMessage);
    }

    public void Execute() {
        var entities = _applyTorqueMessages.GetEntities();
        for (var i = entities.Length - 1; i >= 0; i--) {
            var message = entities[i].applyTorqueMessage;
            message.Target.AddTorque(message.Torque);
            if (entities[i].isDestroyOnConsume) {
                entities[i].Destroy();
            }
        }
    }
}
