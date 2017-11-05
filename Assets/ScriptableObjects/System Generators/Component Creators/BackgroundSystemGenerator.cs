using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "Background", menuName = "SuperMash/Systems/Background")]
public class BackgroundSystemGenerator : SystemGenerator {
    public GameObject Tile;
    public BackgroundTileSetup[] Setups;
    public float HideDistance;
    public override ISystem Generate(Contexts contexts) {
        return new BackgroundSystem(contexts, Tile, Setups, HideDistance);
    }
}
