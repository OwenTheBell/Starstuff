using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class MessagingFeature : Feature {

    public MessagingFeature(Contexts contexts) : base("Messaging") {
        Add(new CleanupMessages(contexts));
    }

}
