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
            var myBody = e.view.gameObject.GetComponent<Rigidbody2D>();
            var targetPos = e.trackedTransform.Transform.position;
            var radians = Mathf.Atan2(targetPos.y - myPos.y, targetPos.x - myPos.x);
            var targetBody = e.trackedTransform.Transform.GetComponent<Rigidbody2D>();
            var targetVelocity = targetBody.velocity;
            var magnitude = targetVelocity.magnitude * e.catchup.Factor;
            var minMagnitude = e.catchup.MinimumMagnitude;

            magnitude = Mathf.Clamp(magnitude, minMagnitude, Mathf.Infinity);
            var velocity = new Vector2(
                Mathf.Cos(radians) * magnitude,
                Mathf.Sin(radians) * magnitude
            );

            var relativeVelocity = velocity - targetVelocity;
            var vel = e.view.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (relativeVelocity.magnitude > magnitude) {
                var diff = relativeVelocity.magnitude - magnitude;
                magnitude -= diff;
                magnitude = Mathf.Clamp(magnitude, minMagnitude, Mathf.Infinity);
                velocity.Normalize();
                velocity *= magnitude;
            }

            //var message = MessageGenerator.Message();
            //message.AddSetVelocityMessage(velocity, myBody);
            //message.isPersistUntilConsumed = true;

            var body = e.view.gameObject.GetComponent<Rigidbody2D>();
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            buffer.AddToBuffer(this, b => b.velocity = velocity);

            var distance = Vector2.Distance(myPos, targetPos);
            if (distance < e.catchup.Range) {
                e.isCatchingUp = false;
                e.isFollowing = true;
                var state = e.changingMovementStateComponent;
                state._Remaining = state.Time;
            }
        }
    }
}
