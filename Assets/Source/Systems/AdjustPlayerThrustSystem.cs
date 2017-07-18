using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class AdjustPlayerThrustSystem : ReactiveSystem<GameEntity>, IInitializeSystem {

    readonly GameContext _gameContext;
    private GameEntity _player;

    public AdjustPlayerThrustSystem(Contexts contexts) : base(contexts.game) {
        _gameContext = contexts.game;
    }

    public void Initialize() {
        _player = _gameContext.playerEntity;
        var thruster = _player.thruster;
        var perFollower = _player.thrustPerFollower;
        thruster.Force = perFollower.BaseThrust;
        _player.AddMaxVelocity(perFollower.BaseSpeed);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.FollowingPlayer);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.isFollowingPlayer && entity.isStar;
    }

    protected override void Execute(List<GameEntity> entities) {
        if (!_player.hasThruster || !_player.hasThrustPerFollower) {
            return;
        }
        var thruster = _player.thruster;
        var perFollower = _player.thrustPerFollower;
        var rigidbody = _player.view.gameObject.GetComponent<Rigidbody2D>();
        thruster.Force = entities.Count * perFollower.ThrustPerFollower + perFollower.BaseThrust;
        var maxVelocity = entities.Count * perFollower.SpeedPerFollower + perFollower.BaseSpeed;
        if (!_player.hasMaxVelocity)
            _player.AddMaxVelocity(maxVelocity);
        else {
            _player.ReplaceMaxVelocity(maxVelocity);
        }
    }
}
