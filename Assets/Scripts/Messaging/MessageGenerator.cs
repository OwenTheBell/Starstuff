using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public static class MessageGenerator {

    public static MessageEntity Message(bool immediate = false) {
        var e = Contexts.sharedInstance.message.CreateEntity();
        if (immediate) {
            e.isCanBeProcessed = true;
        }
        else {
            e.isJustIssued = true;
        }
        return e;
    }

    public static MessageEntity OwnedMessage(ISystem sender, bool immediate = false) {
        var e = Message(immediate);
        e.AddMessageSender(sender);
        return e;
    }
}
