using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "Adjust Player Thrust", menuName = "SuperMash/Systems/Adjust Player Thrust")]
public class AdjustPlayerThrustSystemGenerator : SystemGenerator {

    public override ISystem Generate(Contexts contexts) {
        return new AdjustPlayerThrustSystem(contexts);
    }
}
