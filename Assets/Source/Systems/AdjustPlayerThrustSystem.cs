using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class AdjustPlayerThrustSystem : ReactiveSystem<GameEntity>, IInitializeSystem {

    readonly GameContext _context;
    private GameEntity _player;

    public AdjustPlayerThrustSystem(
        Contexts contexts
    ) : base(contexts.game) {
        _context = contexts.game;
    }

    public void Initialize() {
        _player = _context.playerEntity;
        var thruster = _player.thruster;
        var perFollower = _player.thrustPerFollower;
        thruster.HasMaxVelocity = true;
        thruster.MaxVelocity = perFollower.BaseSpeed;
        thruster.Force = perFollower.BaseThrust;
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
        thruster.HasMaxVelocity = true;
        thruster.MaxVelocity = entities.Count * perFollower.SpeedPerFollower + perFollower.BaseSpeed;
        thruster.Force = entities.Count * perFollower.ThrustPerFollower + perFollower.BaseThrust;
    }
}
