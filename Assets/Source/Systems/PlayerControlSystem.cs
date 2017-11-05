using Entitas;
using UnityEngine;

public class PlayerControlSystem : IExecuteSystem {

    readonly IGroup<InputEntity> _keys;
    readonly GameContext _gameContext;

    public PlayerControlSystem(Contexts contexts) {
        _keys = contexts.input.GetGroup(InputMatcher.Key);
        _gameContext = contexts.game;
    }

    public void Execute() {
        var player = _gameContext.playerEntity;
        var thrust = false;
        var spinLeft = false;
        var spinRight = false;
        foreach (var e in _keys.GetEntities()) {
            if (e.isKeyUp) {
                continue;
            }
            if (e.key.name == "Thrust") {
                thrust = true;
            }
            else if (e.key.name == "Left") {
                spinLeft = true;
            }
            else if (e.key.name == "Right") {
                spinRight = true;
            }
        }

        if (thrust) {
            player.AddThrusting(player.view.transform.up);
        }
        if (spinLeft && !spinRight) {
            SetSpinning(player, 1);
        }
        else if (!spinLeft && spinRight) {
            SetSpinning(player, -1);
        }
        else if (player.hasSpinning) {
            player.RemoveSpinning();
        }
    }

    void SetSpinning(GameEntity e, int direction) {
        if (e.hasSpinning) {
            e.ReplaceSpinning(direction);
        }
        else {
            e.AddSpinning(direction);
        }
    }
}
