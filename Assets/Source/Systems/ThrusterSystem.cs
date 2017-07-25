using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class ThrusterSystem : ReactiveSystem<GameEntity> {

    readonly IGroup<GameEntity> _thrustTriggers;

    public ThrusterSystem(Contexts contexts) : base(contexts.game) {
        _thrustTriggers = contexts.game.GetGroup(GameMatcher.TriggerThrust);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> c) {
        return c.CreateCollector(GameMatcher.AllOf(
                                        GameMatcher.TriggerThrust,
                                        GameMatcher.Thruster,
                                        GameMatcher.UpdateBuffer
                                    )
                                );
    }

    protected override bool Filter(GameEntity e) { return true; }

    protected override void Execute(List<GameEntity> entities) {
        foreach(var e in entities) {
            var buffer = e.updateBuffer.buffer;
            var force = e.triggerThrust.Direction * e.thruster.Force;
            buffer.AddToBuffer(this, (Rigidbody2D r) => r.AddForce(force) );
        }
    }
}
