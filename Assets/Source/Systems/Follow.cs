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

            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);

            var targetVel = target.GetComponent<Rigidbody2D>().velocity;
            var myVelocity = myBody.velocity;
            var diff = targetVel - myVelocity;
            buffer.AddToBuffer(this, (Rigidbody2D r) => r.AddForce(diff * r.mass));

            var angle1 = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            var angle2 = Mathf.Atan2(myVelocity.y, myVelocity.x) * Mathf.Rad2Deg;
            if (Mathf.Abs(angle1 - angle2) > 20f) {
                e.AddDampenInertia(0.9f);
            }
        }
    }
}
