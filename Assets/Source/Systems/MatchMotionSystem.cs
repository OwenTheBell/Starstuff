using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class MatchMotionSystem : IInitializeSystem, IExecuteSystem {

    readonly IGroup<GameEntity> _matchMotions;

    public MatchMotionSystem(Contexts contexts) {
        _matchMotions = contexts.game.GetGroup(GameMatcher.MatchMotion);
    }

    public void Initialize() {
        foreach (var e in _matchMotions.GetEntities()) {
            var lastPosition = e.matchMotion.gameObject.transform.position;
            e.matchMotion._lastPosition = lastPosition;
        }
    }

    public void Execute() {
        foreach (var e in _matchMotions.GetEntities()) {
            if (!e.hasView) {
                return;
            }
            var currentPosition = (Vector2)e.matchMotion.gameObject.transform.position;
            var lastPosition = e.matchMotion._lastPosition;
            var delta = currentPosition - lastPosition;
            var transform = e.view.gameObject.transform;
            transform.position += (Vector3)delta * e.matchMotion.Scale;
            e.matchMotion._lastPosition = currentPosition;
        }
    }
}
