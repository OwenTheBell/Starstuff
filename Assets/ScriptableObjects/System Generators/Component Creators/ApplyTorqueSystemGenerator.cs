using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Apply Torque", menuName = "SuperMash/Systems/Apply Torque")]
public class ApplyTorqueSystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new ApplyTorqueSystem(contexts);
    }
}
