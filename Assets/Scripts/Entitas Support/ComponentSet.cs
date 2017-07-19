using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

[System.Serializable]
public class ComponentSet {
    public IComponent[] Components = new IComponent[0];

    public ComponentSet() { }
}
