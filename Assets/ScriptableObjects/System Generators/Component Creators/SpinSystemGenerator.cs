using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

[UnityEngine.CreateAssetMenu(fileName = "Spin", menuName = "SuperMash/Systems/Spin")]
public class SpinSystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new SpinSystem(contexts);
    }
}
