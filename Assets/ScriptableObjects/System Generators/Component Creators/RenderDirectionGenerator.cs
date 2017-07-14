using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

[CreateAssetMenu(fileName = "Render Direction", menuName = "SuperMash/Systems/Render Direction")]
public class RenderDirectionGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new RenderDirectionSystem(contexts);
    }
}
