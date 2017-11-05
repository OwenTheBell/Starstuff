using UnityEngine;

public class FixedTickSystem : IFixedUpdateSystem {

    readonly GameContext _game;

    public FixedTickSystem(Contexts contexts) {
        _game = contexts.game;
    }

    public void FixedUpdate() {
        _game.tickTracker.Fixed = Time.fixedDeltaTime;
    }
}
