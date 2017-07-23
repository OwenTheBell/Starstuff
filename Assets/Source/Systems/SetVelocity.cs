using System.Collections.Generic;
using Entitas;

public class SetVelocitySystem : ReactiveSystem<MessageEntity> {

    public SetVelocitySystem(Contexts contexts) : base (contexts.message) { }

    protected override ICollector<MessageEntity> GetTrigger(IContext<MessageEntity> context) {
        return context.CreateCollector(MessageMatcher.SetVelocityMessage);
    }

    protected override bool Filter(MessageEntity entity) {
        return entity.isCanBeProcessed;
    }

    protected override void Execute(List<MessageEntity> entities) {
        foreach (var e in entities) {
            var message = e.setVelocityMessage;
            message.Target.velocity = message.Velocity;
            e.isPersistUntilConsumed = false;
        }
    }
}
