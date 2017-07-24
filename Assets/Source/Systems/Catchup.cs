using Entitas;
using UnityEngine;

public class Catchup : IExecuteSystem {

    readonly IGroup<GameEntity> _catchers;

    public Catchup(Contexts contexts) {
        _catchers = contexts.game.GetGroup(GameMatcher.CatchingUp);
    }

    public void Execute() {
        foreach (var e in _catchers.GetEntities()) {
            var myPos = e.view.transform.position;
            var targetPos = e.trackedTransform.Transform.position;
            var distance = Vector2.Distance(myPos, targetPos);
            if (distance < e.catchup.Range) {
                e.isCatchingUp = false;
                e.isFollowing = true;
                continue;
            }

            var myBody = e.view.gameObject.GetComponent<Rigidbody2D>();
            var myVelocity = myBody.velocity;
            var radians = Mathf.Atan2(targetPos.y - myPos.y, targetPos.x - myPos.x);
            var targetBody = e.trackedTransform.Transform.GetComponent<Rigidbody2D>();
            // calculate how much faster than the target's velocity to go
            var magnitude = targetBody.velocity.magnitude * e.catchup.Factor;
            var minMagnitude = e.catchup.MinimumMagnitude;

            magnitude = Mathf.Clamp(magnitude, minMagnitude, Mathf.Infinity);
            var force = new Vector2(
                Mathf.Cos(radians) * magnitude,
                Mathf.Sin(radians) * magnitude
            );

            var body = e.view.gameObject.GetComponent<Rigidbody2D>();
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            buffer.AddToBuffer(this, (Rigidbody2D r) => r.AddForce(force * r.mass));

            var angle1 = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            var angle2 = Mathf.Atan2(myVelocity.y, myVelocity.x) * Mathf.Rad2Deg;
            if (Mathf.Abs(angle1 - angle2) > 20f) {
                e.AddDampenInertia(0.9f);
            }
        }
    }
}
