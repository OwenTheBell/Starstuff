using Entitas;

public class MaxVelocitySystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _entities;

    public MaxVelocitySystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.MaxVelocity,
            GameMatcher.Rigidbody2D
        );
        _entities = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _entities.GetEntities()) {
            if (e.rigidbody2D.body2D.velocity.magnitude > e.maxVelocity.value) {
                var velocity = e.rigidbody2D.body2D.velocity;
                velocity = velocity.normalized * e.maxVelocity.value;
                e.rigidbody2D.body2D.velocity = velocity;
            }
        }
    }
}