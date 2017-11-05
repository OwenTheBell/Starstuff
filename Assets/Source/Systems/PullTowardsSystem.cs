using Entitas;
using UnityEngine;

public class PullTowardsSystem : IExecuteSystem {

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _pullers;

    public PullTowardsSystem(Contexts contexts) {
        _gameContext = contexts.game;
        _pullers = _gameContext.GetGroup(GameMatcher.PullTowards);
    }

    public void Execute() {
        foreach (var e in _pullers.GetEntities()) {
            var other = _gameContext.GetEntityWithId(e.pullTowards.id);
            if (other == null) {
                e.RemovePullTowards();
                continue;
            }
            var myTransform = e.view.transform;
            var theirTransform = other.view.transform;
            var myPos = (Vector2)myTransform.position;
            var radians = myPos.AngleToPoint(theirTransform.position);
            var force = new Vector2(
                Mathf.Cos(radians) * e.pullTowards.force,
                Mathf.Sin(radians) * e.pullTowards.force
            );
            e.thruster.Force = force.magnitude;
            e.AddThrusting(force.normalized);
        }
    }
}