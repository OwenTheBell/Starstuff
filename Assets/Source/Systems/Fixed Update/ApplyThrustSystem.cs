using Entitas;

public class ApplyThrustSystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _entities;

    public ApplyThrustSystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.Rigidbody2D,
            GameMatcher.Thruster,
            GameMatcher.Thrustring
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _entities.GetEntities()) {
            var force = e.rigidbody2D.body2D.transform.up * e.thruster.Force;
            e.AddAppliedThrust(force);
        }
    }
}
