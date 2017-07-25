using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var direction = player.view.transform.up;
            var m = MessageGenerator.Message(true);
            m.AddTriggerThrust(direction);
            m.AddMessageTarget(player.id.value);
        }
        if (spinLeft && !spinRight) {
            var m = MessageGenerator.Message(true);
            m.AddTriggerSpin(1);
            m.AddMessageTarget(player.id.value);
        }
        else if (!spinLeft && spinRight) {
            var m = MessageGenerator.Message(true);
            m.AddTriggerSpin(-1);
            m.AddMessageTarget(player.id.value);
        }
    }
}
