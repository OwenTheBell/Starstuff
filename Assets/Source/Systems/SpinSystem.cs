using System;
using Entitas;
using UnityEngine;

public class SpinSystem : IExecuteSystem {

    readonly MessageContext _messageContext;

    readonly IGroup<GameEntity> _spinners;
    readonly IGroup<InputEntity> _keys;

    public SpinSystem(Contexts contexts) {
        _messageContext = contexts.message;
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

        var applyTorque = new Action<float, Rigidbody2D>((f, r) => {
            r.AddTorque(f);
        });

        foreach (var e in _spinners.GetEntities()) {
            if (!e.hasView) {
                continue;
            }

            var go = e.view.gameObject;
            var body = go.GetComponent<Rigidbody2D>();
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);

            if (adjust != 0) {
                var torque = e.spin.Torque * adjust;
                buffer.AddToBuffer(this, b => applyTorque(torque, b));
            }

            var angularVel = body.angularVelocity;
            var angularDirection = (int)(angularVel / Mathf.Abs(angularVel));
            if (adjust == 0 || adjust != angularDirection) {
                var counterTorque = -angularVel * e.spin.Dampening;
                buffer.AddToBuffer(this, b => applyTorque(counterTorque, b));
            }
        }
    }

}
