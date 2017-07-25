using Entitas;
using UnityEngine;

public class TickSystem : IExecuteSystem {

    readonly GameContext _gameContext;

    public TickSystem(Contexts contexts) {
        _gameContext = contexts.game;
        _gameContext.tickTracker.Time = Time.time;
        _gameContext.tickTracker.Tick = Time.deltaTime;
        _gameContext.tickTracker.Scale = 1f;
    }

    public void Execute() {
        _gameContext.tickTracker.Time = Time.time;
        _gameContext.tickTracker.Tick = Time.deltaTime * _gameContext.tickTracker.Scale;
    }
}
