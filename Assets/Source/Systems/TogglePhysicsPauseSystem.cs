using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class TogglePhysicsPauseSystem : ReactiveSystem<GameEntity> {

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _bodies;
    readonly IGroup<GameEntity> _2dBodies;
    readonly IGroup<GameEntity> _preservedBodies;
    readonly IGroup<GameEntity> _2dPreservedBodies;

    public TogglePhysicsPauseSystem(Contexts contexts) : base(contexts.game) {
        _gameContext = contexts.game;
        _bodies = _gameContext.GetGroup(GameMatcher.Body);
        _2dBodies = _gameContext.GetGroup(GameMatcher.Body2D);
        _preservedBodies = _gameContext.GetGroup(GameMatcher.PreservedBodyState);
        _2dPreservedBodies = _gameContext.GetGroup(GameMatcher.PreservedBody2DState);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Paused.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity) { return false; }

    protected override void Execute(List<GameEntity> entities) {
        if (_gameContext.isPaused) {
            foreach (var e in _bodies.GetEntities()) {
                var bodyState = _gameContext.CreateEntity();
                var body = e.body.value;
                bodyState.AddPreservedBodyState(e.id.value, body.velocity, body.angularVelocity);
                body.velocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
                body.isKinematic = true;
            }
            foreach (var e in _2dBodies.GetEntities()) {
                var bodyState = _gameContext.CreateEntity();
                var body = e.body2D.value;
                bodyState.AddPreservedBody2DState(e.id.value, body.velocity, body.angularVelocity);
                body.velocity = Vector3.zero;
                body.angularVelocity = 0f;
                body.isKinematic = true;
            }
        }
        else {
            foreach (var p in _preservedBodies.GetEntities()) {
                var bodyState = p.preservedBodyState;
                var e = _gameContext.GetEntityWithId(bodyState.Id);
                if (e != null) {
                    var body = e.body.value;
                    body.velocity = bodyState.velocity;
                    body.angularVelocity = bodyState.velocity;
                    body.isKinematic = false;
                }
                p.Destroy();
            }
            foreach (var p in _2dPreservedBodies.GetEntities()) {
                var bodyState = p.preservedBody2DState;
                var e = _gameContext.GetEntityWithId(bodyState.Id);
                if (e != null) {
                    var body = e.body.value;
                    body.velocity = bodyState.velocity;
                    body.angularVelocity = bodyState.velocity;
                    body.isKinematic = false;
                }
                p.Destroy();
            }
        }
    }
}
