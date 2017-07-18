using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Apply Force", menuName = "SuperMash/Systems/Apply Force")]
public class ApplyForceSystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new ApplyForceSystem(contexts);
    }
}
