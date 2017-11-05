using Entitas;
using UnityEngine;

public class AdjustPlayerThrustSystem : IExecuteSystem, IInitializeSystem {

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _followers;
    private GameEntity _player;

    public AdjustPlayerThrustSystem(Contexts contexts) {
        _gameContext = contexts.game;
        _followers = _gameContext.GetGroup(GameMatcher.FollowingPlayer);
    }

    public void Initialize() {
        _player = _gameContext.playerEntity;
        var thruster = _player.thruster;
        var perFollower = _player.thrustPerFollower;
        thruster.Force = perFollower.BaseThrust;
        _player.AddMaxVelocity(perFollower.BaseSpeed);
    }

    public void Execute() {
        if (!_player.hasThruster || !_player.hasThrustPerFollower) {
            return;
        }
        var thruster = _player.thruster;
        var perFollower = _player.thrustPerFollower;
        var rigidbody = _player.view.gameObject.GetComponent<Rigidbody2D>();
        thruster.Force = _followers.count * perFollower.ThrustPerFollower + perFollower.BaseThrust;
        var maxVelocity = _followers.count * perFollower.SpeedPerFollower + perFollower.BaseSpeed;
        if (!_player.hasMaxVelocity)
            _player.AddMaxVelocity(maxVelocity);
        else {
            _player.ReplaceMaxVelocity(maxVelocity);
        }
    }
}
