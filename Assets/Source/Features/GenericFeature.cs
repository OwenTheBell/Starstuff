using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class GenericFeature : Feature {
    public GenericFeature(string name, ISystem[] systems) : base(name) {
        foreach(var system in systems) {
            Add(system);
        }
    }
}
