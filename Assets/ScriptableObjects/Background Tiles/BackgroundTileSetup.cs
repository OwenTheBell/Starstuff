using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Background Tile", menuName = "Starstuff/Background Tile")]
public class BackgroundTileSetup : ScriptableObject {
    public GameObject StarPrefab;
    public Vector2 Dimensions;
    public float Scale;
    public float Density;
    [Range(0, 1)]
    public float Alpha;
    public int ZDepth;
    public float ParalaxScale;
}
