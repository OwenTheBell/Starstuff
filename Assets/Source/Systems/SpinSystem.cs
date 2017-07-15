using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class SpinSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _spinners;
    readonly IGroup<InputEntity> _keys;

    public SpinSystem(Contexts contexts) {
        _spinners = contexts.game.GetGroup(GameMatcher.Spin);
        _keys = contexts.input.GetGroup(InputMatcher.Key);
    }

    public void Execute() {
        var leftKey = false;
        var rightKey = false;
        foreach (var e in _keys.GetEntities()) {
            if (e.hasKey && !e.isKeyUp) {
                if (e.key.name == "Left") {
                    leftKey = true;
                }
                else if (e.key.name == "Right") {
                    rightKey = true;
                }
            }
        }
        var adjust = 0;
        if (leftKey && !rightKey) {
            adjust = 1;
        }
        else if (!leftKey && rightKey) {
            adjust = -1;
        }

        foreach (var e in _spinners.GetEntities()) {
            if (!e.hasView) {
                continue;
            }

            var go = e.view.gameObject;
            var rigidbody = go.GetComponent<Rigidbody2D>();

            rigidbody.AddTorque(e.spin.Torque * adjust);
            var angularVelocity = rigidbody.angularVelocity;
            var angularDirection = (int)(angularVelocity / Mathf.Abs(angularVelocity));
            if (adjust == 0 || adjust != angularDirection) {
                angularVelocity = -angularVelocity * e.spin.Dampening;
                rigidbody.AddTorque(angularVelocity);
            }
        }
    }

}
