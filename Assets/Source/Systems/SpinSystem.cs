using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SpinSystem : ReactiveSystem<GameEntity> {

    public SpinSystem(Contexts contexts) : base (contexts.game) { }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> c) {
        return c.CreateCollector(GameMatcher.AllOf(
                                        GameMatcher.TriggerSpin,
                                        GameMatcher.Spin,
                                        GameMatcher.UpdateBuffer
                                    )
                                );
    }

    protected override bool Filter(GameEntity entity) { return true; }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
            var body = e.view.gameObject.GetComponent<Rigidbody2D>();
            var buffer = e.updateBuffer.buffer;
            var torque = e.spin.Torque * e.triggerSpin.value;
            buffer.AddToBuffer(this, (Rigidbody2D r) => {
                r.AddTorque(torque);
            });
        }
    }
}
