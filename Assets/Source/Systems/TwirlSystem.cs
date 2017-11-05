using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class TwirlSystem : BehaviorSystem {

    const float DISTANCE = 1f;
    const float TIME = 6f;
    const float FORCE = 40f;

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _followers;

    public TwirlSystem(Contexts contexts) : base(
                                                contexts,
                                                GameMatcher.Twirl,
                                                typeof(TwirlComponent)
    ) {
        _gameContext = contexts.game;
        _followers = contexts.game.GetGroup(GameMatcher.Following);
    }

    protected override void AddNewEntities(List<GameEntity> entities) {
        foreach (var e in entities) {
            // need at least one other entity to twirl with
            if (_followers.count == 0) {
                e.isCatchingUp = true;
                continue;
            }
            var r = Random.Range(0, _followers.count);
            var other = _followers.GetEntities()[r];
            var pullForce = (e.thruster.Force > FORCE/2f) ? e.thruster.Force : FORCE/2f;
            var clockwise = (Random.value > 0.5f) ? true : false;
            other.isFollowing = false;
            e.AddTwirl(TIME, other.id.value, DISTANCE, FORCE, clockwise);
            e.AddPullTowards(other.id.value, pullForce);
            e.isImmuneToRepulsion = true;
            pullForce = (other.thruster.Force > FORCE/2f) ? other.thruster.Force : FORCE/2f;
            other.AddPullTowards(e.id.value, other.thruster.Force);
            other.isImmuneToRepulsion = true;
            other.RemoveBehaviorDelay();
            other.AddTwirl(TIME, e.id.value, DISTANCE, FORCE, clockwise);
        }
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
            var other = _gameContext.GetEntityWithId(e.twirl.partnerId);
            if (other == null) {
                Clear(e);
                continue;
            }

            var myPos = e.view.transform.position;
            var theirPos = other.view.transform.position;
            var distance = Vector2.Distance(myPos, theirPos);
            //if (distance <= e.twirl.distance && e.hasPullTowards) {
            //    e.RemovePullTowards();
            //    e.isImmuneToRepulsion = false;
            //}

            var radians = ((Vector2)myPos).AngleToPoint(theirPos);
            if (e.twirl.clockwise) radians += Mathf.PI / 2;
            else radians -= Mathf.PI / 2;

            var force = new Vector2(
                Mathf.Cos(radians) * e.twirl.force,
                Mathf.Sin(radians) * e.twirl.force
            );

            e.thruster.Force = force.magnitude;
            e.AddThrusting(force.normalized);

            e.twirl.duration -= Time.deltaTime;
            if (e.twirl.duration <= 0f) {
                Clear(e);
            }
        }
    }

    void Clear(GameEntity e) {
        if (e.hasPullTowards) {
            e.RemovePullTowards();
            e.isImmuneToRepulsion = false;
        }
        e.RemoveTwirl();
        e.isCatchingUp = true;
    }
}
