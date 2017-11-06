using Entitas;
using UnityEngine;

public class RestrictVelocitySystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _maxVelocity;

    public RestrictVelocitySystem(Contexts c) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.MaxVelocity,
            GameMatcher.Rigidbody2D
        );
        _maxVelocity = c.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _maxVelocity.GetEntities()) {
            var maxSpeed = e.maxVelocity.value;
            if (e.rigidbody2D.body2D.velocity.sqrMagnitude > maxSpeed * maxSpeed) {
                e.rigidbody2D.body2D.velocity = e.rigidbody2D.body2D.velocity.normalized * maxSpeed;
            }
        }
    }

    //public void Execute() {
    //    var entities = _maxVelocity.GetEntities();
    //    foreach (var e in entities) {
    //        var maxSpeed = e.maxVelocity.MaxVelocity;
    //        var buffer = e.updateBuffer.buffer;
    //        //buffer.RemoveAll(this);
    //        buffer.AddToBuffer(this, (Rigidbody2D r) => RestrictVelocity(r, maxSpeed));
    //    }
    //}

    //void RestrictVelocity(Rigidbody2D r, float maxSpeed) {
    //    var magDiff = r.velocity.magnitude - maxSpeed;
    //    if (magDiff > 0) {
    //        var inverse = -r.velocity;
    //        inverse.Normalize();
    //        r.AddForce(inverse * magDiff * r.mass, ForceMode2D.Impulse);
    //    }
    //}
}
