using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class StarSystems : Feature {
    public StarSystems(Contexts contexts) : base("Star Systems") {
        Add(new Wait(contexts));
        Add(new Catchup(contexts));
        Add(new Follow(contexts));
        Add(new DetectStateChangeSystem(contexts));
        Add(new SmoothStarStateChange(contexts));
    }
}
