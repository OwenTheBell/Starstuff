using Entitas;
using UnityEngine;

public class Follow : IExecuteSystem {

    readonly IGroup<GameEntity> _followers;

    public Follow(Contexts contexts) {
        _followers = contexts.game.GetGroup(GameMatcher.Following);
    }

    public void Execute() {
        foreach (var e in _followers.GetEntities()) {
            var myPos = e.view.transform.position;
            var myBody = e.view.gameObject.GetComponent<Rigidbody2D>();
            var target = e.trackedTransform.Transform;
            var targetPos = target.position;
            var distance = Vector2.Distance(myPos, targetPos);
            var follow = e.follow;
            var catchup = e.catchup;

            if (distance > catchup.Range * 2) {
                e.isFollowing = false;
                e.isCatchingUp = true;
                continue;
            }
            var targetVel = target.GetComponent<Rigidbody2D>().velocity;
            var myVelocity = myBody.velocity;
            var diff = targetVel - myVelocity;
            e.thruster.Force = diff.magnitude * myBody.mass;
            e.maxVelocity.MaxVelocity = targetVel.magnitude;
            diff.Normalize();
            var m = MessageGenerator.Message(true);
            m.AddTriggerThrust(diff);
            m.AddMessageTarget(e.id.value);
        }
    }
}
