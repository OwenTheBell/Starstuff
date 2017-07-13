using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class RenderSpriteSystem : ReactiveSystem<GameEntity> {

    public RenderSpriteSystem(Contexts contexts) : base(contexts.game) { }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.Sprite);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasSprite && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
            var go = e.view.gameObject;
            var renderer = go.GetComponent<SpriteRenderer>();
            if (renderer == null) {
                renderer = go.AddComponent<SpriteRenderer>();
            }
            renderer.sprite = Resources.Load<Sprite>(e.sprite.sprite);
        }
    }
}
