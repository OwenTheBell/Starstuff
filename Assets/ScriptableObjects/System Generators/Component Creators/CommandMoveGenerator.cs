using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Command Move", menuName = "SuperMash/Systems/Command Move")]
public class CommandMoveGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new CommandMoveSystem(contexts);
    }
}
