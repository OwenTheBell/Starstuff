using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Setup Camera", menuName = "SuperMash/Systems/Setup Camera")]
public class SetupCameraSystemGenerator : SystemGenerator {
    public float Scale;
    public override ISystem Generate(Contexts contexts) {
        return new SetupCameraSystem(contexts, Scale);
    }
}
