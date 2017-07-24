﻿using Entitas;
using UnityEngine;

public class InertiaDampeningSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _dampeners;

    public InertiaDampeningSystem(Contexts contexts) {
        _dampeners = contexts.game.GetGroup(GameMatcher.DampenInertia);
    }

    public void Execute() {
        foreach (var e in _dampeners.GetEntities()) {
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            var dampening = e.thruster.Dampening;
            if (e.hasTriggerThrust) {
                var direction = e.triggerThrust.Direction;
                var velocity = e.view.gameObject.GetComponent<Rigidbody2D>().velocity;
                var angle1 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                var angle2 = Mathf.Atan2(velocity.y, velocity.y) * Mathf.Rad2Deg;
                if (Mathf.Abs(angle1 - angle2) > 20f) {
                    buffer.AddToBuffer(this, (Rigidbody2D r) => DampenIntertia(r, dampening));
                }
            }
            else {
                buffer.AddToBuffer(this, (Rigidbody2D r) => DampenIntertia(r, dampening));
            }
            //var dampening = e.dampenInertia.value;
            //var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            //buffer.RemoveAll(this);
            //buffer.AddToBuffer(this, (Rigidbody2D r) => {
            //    var force = -(dampening * r.mass * r.velocity);
            //    r.AddForce(force, ForceMode2D.Force);
            //});
            //e.RemoveDampenInertia();
        }
    }

    void DampenIntertia(Rigidbody2D r, float dampening) {
        var force = -(dampening * r.mass * r.velocity);
        r.AddForce(force, ForceMode2D.Force);
    }
}
