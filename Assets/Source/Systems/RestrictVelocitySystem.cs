﻿using Entitas;
using UnityEngine;

public class RestrictVelocitySystem : IExecuteSystem {

    readonly IGroup<GameEntity> _maxVelocity;

    public RestrictVelocitySystem(Contexts contexts) {
        _maxVelocity = contexts.game.GetGroup(GameMatcher.MaxVelocity);
    }

    public void Execute() {
        var entities = _maxVelocity.GetEntities();
        foreach (var e in entities) {
            var maxSpeed = e.maxVelocity.MaxVelocity;
            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            buffer.AddToBuffer(this, (Rigidbody2D r) => RestrictVelocity(r, maxSpeed));
        }
    }

    void RestrictVelocity(Rigidbody2D r, float maxSpeed) {
        var magDiff = r.velocity.magnitude - maxSpeed;
        if (magDiff > 0) {
            var inverse = -r.velocity;
            inverse.Normalize();
            r.AddForce(inverse * magDiff * r.mass, ForceMode2D.Impulse);
        }
    }
}
