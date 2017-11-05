using Entitas;
using UnityEngine;

public class ReactiveTorqueSystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _entities;

    public ReactiveTorqueSystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.Spin,
            GameMatcher.Spinning,
            GameMatcher.ReactiveTorque,
            GameMatcher.Rigidbody2D
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _entities.GetEntities()) {
            var angularVelocity = e.rigidbody2D.body2D.angularVelocity;

            var angularDirection = Mathf.Abs(angularVelocity) / angularVelocity;

            if (angularDirection != e.spinning.direction) {
                var torque = e.spinning.direction * e.spin.Torque * e.reactiveTorque.percent;
                e.rigidbody2D.body2D.AddTorque(torque);
            }
        }
    }
}
