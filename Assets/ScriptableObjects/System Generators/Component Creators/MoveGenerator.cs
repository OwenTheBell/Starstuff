using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "SuperMash/Systems/Move")]
public class MoveGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new MoveSystem(contexts);
    }
}
