using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class CheckForGridCorrectnessSystem : IExecuteSystem {
    readonly IGroup<GameEntity> _tiles;

    public CheckForGridCorrectnessSystem(Contexts contexts) {
        _tiles = contexts.game.GetGroup(GameMatcher.BackgroundTile);
    }

    public void Execute() {
        //foreach (var tile in _tiles.GetEntities()) {
        //    var dimensions = tile.backgroundTile.TileSetup.Dimensions;
        //    var localPosition = tile.view.transform.localPosition;
        //    var x = localPosition.x / dimensions.x;
        //    var y = localPosition.y / dimensions.y;
        //    if (x != Mathf.Floor(x) || y != Mathf.Floor(y)) {
        //        Debug.Log("not aligned");
        //    }
        //}
    }
}
