using System;
using System.Collections.Generic;
using Entitas;

public interface IDestroyedEntity : IEntity, IDestroyed { }

public partial class GameEntity : IDestroyedEntity { }
public partial class InputEntity : IDestroyedEntity { }
public partial class MessageEntity : IDestroyedEntity { }

public class DestroySystem : MultiReactiveSystem<IDestroyedEntity, Contexts> {

    public DestroySystem(Contexts contexts) : base(contexts) { }

    protected override ICollector[] GetTrigger(Contexts contexts) {
        return new ICollector[] {
            contexts.game.CreateCollector(GameMatcher.Destroyed),
            contexts.input.CreateCollector(InputMatcher.Destroyed),
            contexts.message.CreateCollector(MessageMatcher.Destroyed),
        };
    }

    protected override bool Filter(IDestroyedEntity entity) {
        return entity.isDestroyed;
    }

    protected override void Execute(List<IDestroyedEntity> entities) {
        foreach (var e in entities) {
            e.Destroy();
        }
    }
}
