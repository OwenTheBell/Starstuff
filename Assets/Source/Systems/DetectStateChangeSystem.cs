﻿using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class DetectStateChangeSystem : ReactiveSystem<GameEntity> {

    public DetectStateChangeSystem(Contexts contexts) : base(contexts.game) {}

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(
                    GameMatcher.AnyOf(
                            GameMatcher.Following,
                            GameMatcher.Waiting,
                            GameMatcher.CatchingUp
                        ).AddedOrRemoved()
                );
    }

    protected override bool Filter(GameEntity entity) {
        return true;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
        }
    }
}
