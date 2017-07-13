using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class RenderPositionSystem : ReactiveSystem<GameEntity> {

    public RenderPositionSystem(Contexts contexts) : base(contexts.game) { }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
            e.view.gameObject.transform.position = e.position.value;
        }
    }
}
