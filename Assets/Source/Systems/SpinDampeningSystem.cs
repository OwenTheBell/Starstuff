using Entitas;
using UnityEngine;

public class SpinDampeningSystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _entities;

    public SpinDampeningSystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.Spin,
            GameMatcher.DampenSpin,
            GameMatcher.Rigidbody2D
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _entities.GetEntities()) {
            if (e.hasSpinning) continue;
            if (e.rigidbody2D.body2D.angularVelocity == 0f) continue;

            var angularVelocity = e.rigidbody2D.body2D.angularVelocity;
            e.rigidbody2D.body2D.AddTorque(-angularVelocity * e.dampenSpin.value);
        }
    }
}