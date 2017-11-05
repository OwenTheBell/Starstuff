using Entitas;
using UnityEngine;

public class Catchup : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _catchers;

    public Catchup(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.Rigidbody2D,
            GameMatcher.CatchingUp
        );
        _catchers = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
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
            var targetBody = e.trackedTransform.Transform.GetComponent<Rigidbody2D>();
            var maxMagnitude = targetBody.velocity.magnitude * e.catchup.Factor;
            var minMagnitude = e.catchup.MinimumMagnitude;
            var radians = Mathf.Atan2(targetPos.y - myPos.y, targetPos.x - myPos.x);
            var force = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            var magnitude = Mathf.Clamp(maxMagnitude, minMagnitude, Mathf.Infinity);
            e.thruster.Force = magnitude * myBody.mass;
            e.maxVelocity.MaxVelocity = maxMagnitude;
            //var m = MessageGenerator.Message(true);
            //m.AddTriggerThrust(force);
            //m.AddMessageTarget(e.id.value);
        }
    }
}
