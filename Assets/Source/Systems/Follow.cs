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

            if (distance > catchup.Range) {
                e.isFollowing = false;
                e.isCatchingUp = true;
                var state = e.changingMovementStateComponent;
                state._Remaining = state.Time;
                continue;
            }

            var targetVel = target.GetComponent<Rigidbody2D>().velocity;
            var velocity = myBody.velocity;
            var angle = Mathf.Atan2(targetVel.y - velocity.y, targetVel.x - velocity.x);
            var time = angle / 180 * follow.RetargetSpeed;
            var percent = Time.deltaTime / time;
            var newVelocity = Vector2.Lerp(velocity, targetVel, percent);

            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            buffer.AddToBuffer(this, b => b.velocity = newVelocity );
            //var m = MessageGenerator.Message();
            //m.AddSetVelocityMessage(velocity, myBody);
            //m.isPersistUntilConsumed = true;
        }
    }
}
