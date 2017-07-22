using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class MessagingFeature : Feature {

    public MessagingFeature(Contexts contexts) : base("Messsaging") {
        Add(new PrepareMessages(contexts));
        Add(new CleanupMessages(contexts));
    }

}
