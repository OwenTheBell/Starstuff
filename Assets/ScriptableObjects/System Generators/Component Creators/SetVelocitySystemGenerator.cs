using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Set Velocity", menuName = "SuperMash/Systems/Set Velocity")]
public class SetVelocitySystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new SetVelocitySystem(contexts);
    }
}
