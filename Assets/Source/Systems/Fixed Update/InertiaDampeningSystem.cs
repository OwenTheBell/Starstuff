using Entitas;
using UnityEngine;

public class InertiaDampeningSystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _entities;

    public InertiaDampeningSystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.DampenInertia,
            GameMatcher.Rigidbody2D
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _entities.GetEntities()) {
            if (e.hasAppliedThrust) continue;

            var velocity = e.rigidbody2D.body2D.velocity;
            var sqrMag = velocity.sqrMagnitude;
            velocity = -velocity.normalized * e.dampenInertia.value * sqrMag;
            e.rigidbody2D.body2D.AddForce(velocity);
        }
    }
}
