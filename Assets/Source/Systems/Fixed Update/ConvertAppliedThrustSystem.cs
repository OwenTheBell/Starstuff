using Entitas;

public class ConvertAppliedThrustSystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _entities;

    public ConvertAppliedThrustSystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.AppliedThrust,
            GameMatcher.Rigidbody2D
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _entities.GetEntities()) {
            e.rigidbody2D.body2D.AddForce(e.appliedThrust.value);
        }
    }
}
