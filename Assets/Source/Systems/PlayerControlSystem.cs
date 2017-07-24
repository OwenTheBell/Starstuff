using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class PlayerControlSystem : IExecuteSystem, ICleanupSystem {

    readonly IGroup<InputEntity> _keys;
    private GameEntity _player;

    public PlayerControlSystem(Contexts contexts) {
        _keys = contexts.input.GetGroup(InputMatcher.Key);
        _player = contexts.game.playerEntity;
    }

    public void Execute() {
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
            var direction = _player.view.transform.up;
            _player.AddTriggerThrust(direction);
        }
        if (spinLeft && !spinRight) {
            _player.AddTriggerSpin(1);
        }
        else if (!spinLeft && spinRight) {
            _player.AddTriggerSpin(-1);
        }
    }

    public void Cleanup() {
        if (_player.hasTriggerThrust) _player.RemoveTriggerThrust();
        if (_player.hasTriggerSpin) _player.RemoveTriggerSpin();
    }
}
