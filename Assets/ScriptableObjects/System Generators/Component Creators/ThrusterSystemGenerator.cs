using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Thruster", menuName = "SuperMash/Systems/Thruster")]
public class ThrusterSystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new ThrusterSystem(contexts);
    }
}
