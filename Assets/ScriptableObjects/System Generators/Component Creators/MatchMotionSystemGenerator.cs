using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Match Motion", menuName = "SuperMash/Systems/Match Motion")]
public class MatchMotionSystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new MatchMotionSystem(contexts);
    }
}
