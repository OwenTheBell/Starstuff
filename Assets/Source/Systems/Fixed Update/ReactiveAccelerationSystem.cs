using Entitas;
using UnityEngine;

public class ReactiveAccelerationSystem : IFixedUpdateSystem {

	readonly IGroup<GameEntity> _entities;

	public ReactiveAccelerationSystem(Contexts contexts) {
		var allOf = GameMatcher.AllOf(
			GameMatcher.ReactiveAcceleration,
			GameMatcher.AppliedThrust,
			GameMatcher.Rigidbody2D
		);
		_entities = contexts.game.GetGroup(allOf);
	}

	public void FixedUpdate() {
		foreach (var e in _entities.GetEntities()) {
			var dot = Vector2.Dot(e.rigidbody2D.body2D.velocity.normalized, e.appliedThrust.value.normalized);
			var percent = 1f - (dot + 1 / 2f);
			var force = e.appliedThrust.value * percent;
		}
	}
}