using Entitas;
using UnityEngine;

public class SpinDampeningSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _dampeners;

    public SpinDampeningSystem(Contexts contexts) {
        _dampeners = contexts.game.GetGroup(GameMatcher.DampenSpin);
    }

    public void Execute() {
        foreach (var e in _dampeners.GetEntities()) {
            var dampening = e.dampenSpin.value;
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            buffer.AddToBuffer(this, (Rigidbody2D r) => {
                r.AddTorque(-r.angularVelocity * dampening);
            });
            e.RemoveDampenSpin();
        }
    }
}