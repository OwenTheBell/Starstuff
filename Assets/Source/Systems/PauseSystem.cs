using System;
using System.Collections.Generic;
using Entitas;

public class PauseSystem : ReactiveSystem<InputEntity> {

    readonly GameContext _gameContext;

    public PauseSystem(Contexts contexts) : base(contexts.input) {
        _gameContext = contexts.game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.KeyDown);
    }

    protected override bool Filter(InputEntity entity) {
        return entity.key.name == "Pause";
    }

    protected override void Execute(List<InputEntity> entities) {
        _gameContext.isPaused = !_gameContext.isPaused;
    }
}
