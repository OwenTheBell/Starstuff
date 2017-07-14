using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Render Position", menuName = "SuperMash/Systems/Render Position")]
public class RenderPositionGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new RenderPositionSystem(contexts);
    }
}
