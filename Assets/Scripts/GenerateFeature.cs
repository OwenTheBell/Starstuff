using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

[CreateAssetMenu(fileName = "Generic Feature", menuName = "SuperMash/Features/Generic Feature")]
public class GenerateFeature : ScriptableObject {
    public SystemGenerator[] Systems;

    public Feature Generate(Contexts contexts) {
        List<ISystem> systems = new List<ISystem>();
        foreach (var generator in Systems) {
            systems.Add(generator.Generate(contexts));
        }
        return new GenericFeature(name, systems.ToArray());
    }
}
