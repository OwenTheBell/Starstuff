using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class RenderDirectionSystem : ReactiveSystem<GameEntity> {

    readonly GameContext _context;

    public RenderDirectionSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Direction);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasDirection && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
            float angle = e.direction.value;
            e.view.gameObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

}

[CreateAssetMenu(fileName = "Render Direction", menuName = "SuperMash/Systems/Render Direction")]
public class RenderDirectionGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new RenderDirectionSystem(contexts);
    }
}
