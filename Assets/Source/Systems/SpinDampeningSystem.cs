using Entitas;
using UnityEngine;

public class SpinDampeningSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _dampeners;

    public SpinDampeningSystem(Contexts contexts) {
        _dampeners = contexts.game.GetGroup(GameMatcher.AllOf(
                                                GameMatcher.DampenSpin,
                                                GameMatcher.UpdateBuffer
                                            )
                                        );
    }

    public void Execute() {
        foreach (var e in _dampeners.GetEntities()) {
            var body = e.view.gameObject.GetComponent<Rigidbody2D>();
            var angularVelocity = body.angularVelocity;
            var direction = (int)(angularVelocity / Mathf.Abs(angularVelocity));
            if ((e.hasTriggerSpin && direction != e.triggerSpin.value) ||
                !e.hasTriggerSpin
            ) {
                var buffer = e.updateBuffer.buffer;
                buffer.RemoveAll(this);
                buffer.AddToBuffer(this, (Rigidbody2D r) => {
                    r.AddTorque(-r.angularVelocity * e.dampenSpin.value);
                });
            }
        }
    }
}