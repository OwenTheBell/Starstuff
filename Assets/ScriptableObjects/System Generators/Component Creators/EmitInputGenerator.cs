using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Emit Input", menuName = "SuperMash/Systems/Emit Input")]
public class EmitInputGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new EmitInputSystem(contexts);
    }
}
