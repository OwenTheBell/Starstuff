using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Render Sprite", menuName = "SuperMash/Systems/Render Sprite")]
public class RenderSpriteGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new RenderSpriteSystem(contexts);
    }
}
