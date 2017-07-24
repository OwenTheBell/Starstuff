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
    readonly IGroup<GameEntity> _thrustTriggers;

    public ThrusterSystem(Contexts contexts) {
        _messageContext = contexts.message;
        _thrusters = contexts.game.GetGroup(GameMatcher.Thruster);
        _keys = contexts.input.GetGroup(InputMatcher.Key);
        _thrustTriggers = contexts.game.GetGroup(GameMatcher.TriggerThrust);
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

        var applyDampening = new Action<float, Rigidbody2D>((dampening, r) => {
            var velocity = -dampening * r.velocity;
            applyForce(velocity, r);
        });

        //foreach(var e in _thrusters.GetEntities()) {
        foreach(var e in _thrustTriggers.GetEntities()) {
            var go = e.view.gameObject;
            //var trigger = e.triggerThrust.Direction;
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            var body = go.GetComponent<Rigidbody2D>();

            var force = e.triggerThrust.Direction * e.thruster.Force;
            buffer.AddToBuffer(this, (Rigidbody2D r) => r.AddForce(force) );

            //if (!e.hasView) {
            //    continue;
            //}

            //var dampening = e.thruster.Dampening;
            //if (thrustKey) {
            //    var force = go.transform.up * e.thruster.Force;
            //    buffer.AddToBuffer(this, (Rigidbody2D r) => r.AddForce(force) );
            //    var velocity = body.velocity;
            //    // use two seperate Atan2 here to calculate the angle as being around a
            //    // central origin
            //    var angle1 = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            //    var angle2 = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            //    if (Mathf.Abs(angle1 - angle2) > 20f) {
            //        e.AddDampenInertia(e.thruster.Dampening);
            //    }
            //}
            //else {
            //    e.AddDampenInertia(e.thruster.Dampening);
            //}
        }
    }
}
