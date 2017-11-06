using Entitas;
using UnityEngine;

public class MaxSpinSystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _entities;

    public MaxSpinSystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.MaxSpin,
            GameMatcher.Rigidbody2D
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _entities.GetEntities()) {
            var velocity = e.rigidbody2D.body2D.angularVelocity;
            if (Mathf.Abs(velocity) > e.maxSpin.value) {
                velocity = Mathf.Clamp(velocity, -e.maxSpin.value, e.maxSpin.value);
                e.rigidbody2D.body2D.angularVelocity = velocity;
            }
        }
    }
}
