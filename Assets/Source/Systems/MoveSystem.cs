using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class MoveSystem : IExecuteSystem, ICleanupSystem {

    readonly IGroup<GameEntity> _moves;
    readonly IGroup<GameEntity> _moveCompletes;
    const float SPEED = 4f;

    public MoveSystem(Contexts contexts) {
        _moves = contexts.game.GetGroup(GameMatcher.Move);
        _moveCompletes = contexts.game.GetGroup(GameMatcher.MoveComplete);
    }

    public void Execute() {
        foreach(var e in _moves.GetEntities()) {
            var direction = e.move.target - e.position.value;
            var newPosition = e.position.value + direction.normalized * SPEED * Time.deltaTime;
            e.ReplacePosition(newPosition);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            e.ReplaceDirection(angle);

            var distance = direction.magnitude;
            if (distance <= 0.5f) {
                e.RemoveMove();
                e.isMoveComplete = true;
            }
        }
    }

    public void Cleanup() {
        foreach(var e in _moveCompletes.GetEntities()) {
            e.isMoveComplete = false;
        }
    }

}

[CreateAssetMenu(fileName = "Move", menuName = "SuperMash/Systems/Move")]
public class MoveGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new MoveSystem(contexts);
    }
}
