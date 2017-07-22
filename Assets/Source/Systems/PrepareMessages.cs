using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class PrepareMessages : ReactiveSystem<MessageEntity> {

    public PrepareMessages(Contexts contexts) : base(contexts.message) { }

    protected override ICollector<MessageEntity> GetTrigger(IContext<MessageEntity> context) {
        return context.CreateCollector(MessageMatcher.JustIssued);
    }

    protected override bool Filter(MessageEntity entity) {
        return entity.isJustIssued;
    }

    protected override void Execute(List<MessageEntity> entities) {
        entities.Act<MessageEntity>(e => {
            e.isJustIssued = false;
            e.isCanBeProcessed = true;
        });
    }
}
