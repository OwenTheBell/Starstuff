using System;
using System.Collections.Generic;
using Entitas;

public class DestroySystem : ICleanupSystem {

    readonly ICollector[] _destroyed;

    public DestroySystem(Contexts contexts) {
        _destroyed = new ICollector[] {
            contexts.game.CreateCollector(GameMatcher.Destroyed),
            contexts.input.CreateCollector(InputMatcher.Destroyed),
            contexts.message.CreateCollector(MessageMatcher.Destroyed)
        };
    }
    public void Cleanup() {
        foreach(var group in _destroyed) {
            foreach (var e in group.GetCollectedEntities<IEntity>()) {
                e.Destroy();
            }
            group.ClearCollectedEntities();
        }
    }
}
