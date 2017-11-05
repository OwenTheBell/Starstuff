using Entitas;
using UnityEngine;

public class ThrustParticleSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _particleEntities;

	public ThrustParticleSystem(Contexts contexts) {
		_particleEntities = contexts.game.GetGroup(GameMatcher.ThrustParticle);
	}

	public void Execute() {
		foreach (var e in _particleEntities.GetEntities()) {
			if (e.hasThrusting && !e.thrustParticle.system.isPlaying) {
				e.thrustParticle.system.Play();
			}
			else if (!e.hasThrusting && e.thrustParticle.system.isPlaying) {
				e.thrustParticle.system.Stop();
			}
		}
	}
}