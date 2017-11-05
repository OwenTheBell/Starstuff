using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SpinSystem : IFixedUpdateSystem {

    readonly GameContext _game;
    readonly IGroup<GameEntity> _spinning;

    public SpinSystem(Contexts contexts) {
        _game = contexts.game;
        _spinning = _game.GetGroup(GameMatcher.Spinning);
        var allOf = GameMatcher.AllOf(
            GameMatcher.Spin,
            GameMatcher.Spinning,
            GameMatcher.Rigidbody2D
        );
        _spinning = _game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _spinning.GetEntities()) {
            var torque = e.spin.Torque * e.spinning.direction;
            e.rigidbody2D.body2D.AddTorque(torque);
        }
    }

    //protected override ICollector<MessageEntity> GetTrigger(IContext<MessageEntity> c) {
    //    return c.CreateCollector(MessageMatcher.AllOf(
    //                                    MessageMatcher.TriggerSpin,
    //                                    MessageMatcher.CanBeProcessed,
    //                                    MessageMatcher.MessageTarget
    //                                )
    //                            );
    //}

    //protected override bool Filter(MessageEntity e) {
    //    var g = _game.GetEntityWithId(e.messageTarget.id);
    //    if (g == null) {
    //        return false;
    //    }
    //    return g.hasUpdateBuffer && g.hasSpin;
    //}

    //protected override void Execute(List<MessageEntity> entities) {
    //    foreach (var e in entities) {
    //        var gameEntity = _game.GetEntityWithId(e.messageTarget.id);
    //        var torque = gameEntity.spin.Torque * e.triggerSpin.value;
    //        var m = MessageGenerator.Message(true);
    //        m.AddBuffer2DAction(this, (Rigidbody2D r) => r.AddTorque(torque));
    //        m.AddMessageTarget(gameEntity.id.value);
    //        gameEntity.isSpinning = true;
    //    }
    //}

    //public void Cleanup() {
    //    foreach (var e in _spinning.GetEntities()) {
    //        e.isSpinning = false;
    //    }
    //}
}
