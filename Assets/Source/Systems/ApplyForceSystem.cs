using System.Collections.Generic;
using Entitas;

public class ApplyForceSystem : ReactiveSystem<MessageEntity> {

    public ApplyForceSystem(Contexts contexts) : base(contexts.message) { }

    protected override ICollector<MessageEntity> GetTrigger(IContext<MessageEntity> context) {
        return context.CreateCollector<MessageEntity>(MessageMatcher.ApplyForceMessage);
    }

    protected override bool Filter(MessageEntity entity) {
        return entity.isCanBeProcessed;
    }

    protected override void Execute(List<MessageEntity> entities) {
        foreach (var e in entities) {
            var message = e.applyForceMessage;
            message.Target.AddForce(message.Force, message.Mode);
            e.isPersistUntilConsumed = false;
        }
    }
}
