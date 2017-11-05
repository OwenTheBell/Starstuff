using Entitas;

public class ApplyThrustSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _entities;
    readonly IGroup<GameEntity> _appliedThrust;

    public ApplyThrustSystem(Contexts contexts) {
        _appliedThrust = contexts.game.GetGroup(GameMatcher.AppliedThrust);
        var allOf = GameMatcher.AllOf(
            GameMatcher.Rigidbody2D,
            GameMatcher.Thruster,
            GameMatcher.Thrusting
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void Execute() {
        foreach (var e in _appliedThrust.GetEntities()) {
            e.RemoveAppliedThrust();
        }
        foreach (var e in _entities.GetEntities()) {
            var force = e.thrusting.direction * e.thruster.Force;
            e.AddAppliedThrust(force);
        }
    }
}