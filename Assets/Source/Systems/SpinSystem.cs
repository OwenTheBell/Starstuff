using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SpinSystem : ReactiveSystem<MessageEntity>, ICleanupSystem {

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _spinning;

    public SpinSystem(Contexts contexts) : base (contexts.message) {
        _gameContext = contexts.game;
        _spinning = _gameContext.GetGroup(GameMatcher.Spinning);
    }

    protected override ICollector<MessageEntity> GetTrigger(IContext<MessageEntity> c) {
        return c.CreateCollector(MessageMatcher.AllOf(
                                        MessageMatcher.TriggerSpin,
                                        MessageMatcher.CanBeProcessed,
                                        MessageMatcher.MessageTarget
                                    )
                                );
    }

    protected override bool Filter(MessageEntity e) {
        var g = _gameContext.GetEntityWithId(e.messageTarget.id);
        if (g == null) {
            return false;
        }
        return g.hasUpdateBuffer && g.hasSpin;
    }

    protected override void Execute(List<MessageEntity> entities) {
        foreach (var e in entities) {
            var gameEntity = _gameContext.GetEntityWithId(e.messageTarget.id);
            var torque = gameEntity.spin.Torque * e.triggerSpin.value;
            var m = MessageGenerator.Message(true);
            m.AddBuffer2DAction(this, (Rigidbody2D r) => r.AddTorque(torque));
            m.AddMessageTarget(gameEntity.id.value);
            gameEntity.isSpinning = true;
        }
    }

    public void Cleanup() {
        foreach (var e in _spinning.GetEntities()) {
            e.isSpinning = false;
        }
    }
}
