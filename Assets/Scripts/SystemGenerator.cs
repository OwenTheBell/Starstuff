using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

public abstract class SystemGenerator : ScriptableObject {
    public abstract ISystem Generate(Contexts contexts);
}
