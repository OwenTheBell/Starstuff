using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class ThrusterSystem : IExecuteSystem {

    readonly MessageContext _messageContext;
    readonly IGroup<GameEntity> _thrusters;
    readonly IGroup<InputEntity> _keys;

    public ThrusterSystem(Contexts contexts) {
        _messageContext = contexts.message;
        _thrusters = contexts.game.GetGroup(GameMatcher.Thruster);
        _keys = contexts.input.GetGroup(InputMatcher.Key);
    }

    public void Execute() {
        var thrustKey = false;
        foreach (var e in _keys.GetEntities()) {
            if (e.hasKey && !e.isKeyUp && e.key.name == "Thrust") {
                thrustKey = true;
                break;
            }
        }

        var applyForce = new Action<Vector2, Rigidbody2D>((f, r) => {
            r.AddForce(f);
        });

        foreach(var e in _thrusters.GetEntities()) {
            var go = e.view.gameObject;
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            var body = go.GetComponent<Rigidbody2D>();

            if (!e.hasView) {
                continue;
            }

            var applyDampening = new Action(() => {
                var velocity = -e.thruster.Dampening * body.velocity;
                applyForce(velocity, body);
            });

            if (thrustKey) {
                var force = go.transform.up * e.thruster.Force;
                buffer.AddToBuffer(this, () => applyForce(force, body));
                var velocity = body.velocity;
                var angle1 = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
                var angle2 = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                if (Mathf.Abs(angle1 - angle2) > 20f) {
                    buffer.AddToBuffer(this, () => applyDampening());
                }
            }
            else {
                buffer.AddToBuffer(this, () => applyDampening());
            }
        }
    }
}
