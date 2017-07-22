﻿using System;
using System.Collections.Generic;
using Entitas;

public class ApplyTorqueSystem : ReactiveSystem<MessageEntity> {

    readonly IGroup<MessageEntity> _applyTorqueMessages;

    public ApplyTorqueSystem(Contexts contexts) : base(contexts.message) { }

    protected override ICollector<MessageEntity> GetTrigger(IContext<MessageEntity> context) {
        return context.CreateCollector(MessageMatcher.ApplyTorqueMessage);
    }

    protected override bool Filter(MessageEntity entity) {
        return entity.isCanBeProcessed;
    }

    protected override void Execute(List<MessageEntity> entities) {
        foreach (var e in entities) {
            var message = e.applyTorqueMessage;
            message.Target.AddTorque(message.Torque);
            e.isPersistUntilConsumed = false;
        }
    }
}
