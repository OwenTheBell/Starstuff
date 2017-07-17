using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class ThrusterSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _thrusters;
    readonly IGroup<InputEntity> _keys;

    public ThrusterSystem(Contexts contexts) {
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
        foreach(var e in _thrusters.GetEntities()) {
            var go = e.view.gameObject;
            var rigidbody = go.GetComponent<Rigidbody2D>();

            if (!e.hasView) {
                continue;
            }

            var applyDampening = new Action(() => {
                var velocity = -e.thruster.Dampening * rigidbody.velocity;
                rigidbody.AddForce(velocity, ForceMode2D.Force);
            });

            if (thrustKey) {
                var force = go.transform.up * e.thruster.Force;
                rigidbody.AddForce(force, ForceMode2D.Force);
                var velocity = rigidbody.velocity;
                var angle1 = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
                var angle2 = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                if (Mathf.Abs(angle1 - angle2) > 20f) {
                    applyDampening();
                }
            }
            else {
                applyDampening();
            }

            if (e.hasMaxVelocity && rigidbody.velocity.magnitude > e.maxVelocity.MaxVelocity) {
                var velocity = rigidbody.velocity;
                velocity.Normalize();
                rigidbody.velocity = velocity * e.maxVelocity.MaxVelocity;
                e.RemoveMaxVelocity();
            }
        }
    }
}
