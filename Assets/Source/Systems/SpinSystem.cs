using Entitas;

public class SpinSystem : IFixedUpdateSystem {

    readonly GameContext _game;
    readonly IGroup<GameEntity> _spinning;

    public SpinSystem(Contexts contexts) {
        _game = contexts.game;
        _spinning = _game.GetGroup(GameMatcher.Spinning);
        var allOf = GameMatcher.AllOf(
            GameMatcher.Spin,
            GameMatcher.Spinning,
            GameMatcher.Rigidbody2D
        );
        _spinning = _game.GetGroup(allOf);
    }

    public void FixedUpdate() {
        foreach (var e in _spinning.GetEntities()) {
            var torque = e.spin.Torque * e.spinning.direction;
            e.rigidbody2D.body2D.AddTorque(torque);
        }
    }
}
