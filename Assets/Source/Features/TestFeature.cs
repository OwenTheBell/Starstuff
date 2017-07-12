using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class TestFeature : Feature {

    public TestFeature(Contexts contexts) : base("Test Systems") {
        Add(new HelloWorldSystem(contexts));
        Add(new LogMouseClickSystem(contexts));
        Add(new DebugMessageSystem(contexts));
        Add(new CleanupDebugMessageSystem(contexts));
    }

}
