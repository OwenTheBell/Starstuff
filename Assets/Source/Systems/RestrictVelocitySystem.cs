using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class RestrictVelocitySystem : IExecuteSystem {

    readonly IGroup<GameEntity> _maxVelocity;

    public RestrictVelocitySystem(Contexts contexts) {
        _maxVelocity = contexts.game.GetGroup(GameMatcher.MaxVelocity);
    }

    public void Execute() {
        var entities = _maxVelocity.GetEntities();
        foreach (var e in entities) {
            var maxVelocity = e.maxVelocity.MaxVelocity;
            var rigidbody = e.view.gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null && rigidbody.velocity.magnitude > maxVelocity) {
                var velocity = rigidbody.velocity;
                velocity.Normalize();
                rigidbody.velocity = velocity * maxVelocity;
            }
        }
    }
}
