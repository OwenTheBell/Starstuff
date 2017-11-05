using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ThrusterSystem : ReactiveSystem<MessageEntity>, ICleanupSystem {

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _thrusting;

    public ThrusterSystem(Contexts contexts) : base(contexts.message) {
        _gameContext = contexts.game;
        _thrusting = _gameContext.GetGroup(GameMatcher.Thrustring);
    }

    protected override ICollector<MessageEntity> GetTrigger(IContext<MessageEntity> c) {
        return c.CreateCollector(MessageMatcher.AllOf(
                                                        MessageMatcher.TriggerThrust,
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
        return g.hasUpdateBuffer && g.hasThruster;
    }

    protected override void Execute(List<MessageEntity> entities) {
        foreach(var e in entities) {
            var gameEntity = _gameContext.GetEntityWithId(e.messageTarget.id);
            var force = e.triggerThrust.direction * gameEntity.thruster.Force;
            var m = MessageGenerator.Message(true);
            m.AddMessageTarget(e.messageTarget.id);
            m.AddBuffer2DAction(this, (Rigidbody2D r) => r.AddForce(force));
            gameEntity.isThrustring = true;
        }
    }

    public void Cleanup() {
        foreach (var e in _thrusting.GetEntities()) {
            e.isThrustring = false;
        }
    }
}
