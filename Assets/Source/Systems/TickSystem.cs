using Entitas;
using UnityEngine;

public class TickSystem : IExecuteSystem {

    readonly GameContext _gameContext;

    public TickSystem(Contexts contexts) {
        _gameContext = contexts.game;
        var e = _gameContext.CreateEntity();
        e.AddTickTracker(Time.time, 0f, 1f);
    }

    public void Execute() {
        if (!_gameContext.isPaused) {
            _gameContext.tickTracker.Tick = Time.deltaTime * _gameContext.tickTracker.Scale;
        }
        else {
            _gameContext.tickTracker.Tick = 0f;
        }
        _gameContext.tickTracker.Time += _gameContext.tickTracker.Tick;
    }
}
