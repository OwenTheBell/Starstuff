using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class BackgroundSystem : IExecuteSystem {

    readonly GameContext _context;
    readonly IGroup<GameEntity> _tiles;
    readonly IGroup<GameEntity> _onScreenTiles;
    readonly IGroup<GameEntity> _hiddenTiles;

    readonly GameObject _tilePrefab;
    readonly BackgroundTileSetup[] _tileSetups;
    readonly float _hideDistance;

    public BackgroundSystem(Contexts contexts, GameObject tile, BackgroundTileSetup[] setups, float distance) {
        _context = contexts.game;
        _tiles = _context.GetGroup(GameMatcher.BackgroundTile);
        _onScreenTiles = _context.GetGroup(GameMatcher.OnScreenTile);
        _hiddenTiles = _context.GetGroup(GameMatcher.HiddenTile);
        _tilePrefab = tile;
        _tileSetups = setups;
        _hideDistance = distance;
    }

    public void Execute() {
        var camera = Camera.main;
        var height = 2f * camera.orthographicSize;
        var width = camera.aspect * height;
        var widthBound = width * 2f;
        var heightBound = height * 2f;

        var tiles = new List<GameEntity>(_tiles.GetEntities());
        var hiddenTiles = new List<GameEntity>(_hiddenTiles.GetEntities());
        var onScreenTiles = new List<GameEntity>(_onScreenTiles.GetEntities());
        for (var i = onScreenTiles.Count - 1; i >= 0; i--) {
            var tile = onScreenTiles[i];
            var setup = tile.backgroundTile.TileSetup;
            var local = camera.transform.InverseTransformPoint(tile.view.gameObject.transform.position);
            if (Mathf.Abs(local.x + setup.Dimensions.x / 2f) >= widthBound / 2f ||
                Mathf.Abs(local.y + setup.Dimensions.y / 2f) >= heightBound / 2f) {
                onScreenTiles.RemoveAt(i);
                tile.isHiddenTile = true;
            }
        }

        var newOnScreenTiles = new List<GameEntity>();
        foreach (var setup in _tileSetups) {
            Predicate<GameEntity> tilesForLayer = (e) => {
                return e.backgroundTile.TileSetup == setup;
            };
            var myTiles = tiles.Filter<GameEntity>(tilesForLayer);
            var myOnScreenTiles = onScreenTiles.Filter<GameEntity>(tilesForLayer);
            var myHiddenTiles = hiddenTiles.Filter<GameEntity>(tilesForLayer);

            for (var x = -widthBound / 2f; x <= widthBound/2f; x += setup.Dimensions.x) {
                for (var y = -heightBound / 2f; y <= heightBound/2f; y += setup.Dimensions.y) {
                    var point = camera.transform.TransformPoint(new Vector2(x, y));
                    var tilePos = TilePositionForPointOnGrid(point, setup.Dimensions);
                    Predicate<GameEntity> comparePosition = (e) => {
                        return (Vector2)e.view.gameObject.transform.position == tilePos;
                    };
                    if (myOnScreenTiles.Filter<GameEntity>(comparePosition).Count == 0) {
                        var filtered = myTiles.Filter<GameEntity>(comparePosition);
                        if (filtered.Count > 0) {
                            newOnScreenTiles.Add(filtered[0]);
                        }
                        else {
                            if (myHiddenTiles.Count > 0) {
                                var tile = myHiddenTiles[0];
                                var go = tile.view.gameObject;
                                go.transform.position = tilePos;
                                newOnScreenTiles.Add(tile);
                            }
                            else {
                                var myPos = new Vector3(tilePos.x, tilePos.y, setup.ZDepth);
                                var tileObject = GameObject.Instantiate(
                                                                _tilePrefab,
                                                                myPos,
                                                                Quaternion.identity
                                                            );
                                tileObject.GetComponent<Tile>().Dimensions = setup.Dimensions;
                                tileObject.GetComponent<Tile>().Density = setup.Density;
                                tileObject.GetComponent<Tile>().Scale = setup.Scale;
                                tileObject.GetComponent<Tile>().Alpha = setup.Alpha;
                                var e = _context.CreateEntity();
                                e.AddView(tileObject);
                                e.AddMatchMotion(camera.gameObject, setup.ParalaxScale, Vector2.zero);
                                e.AddBackgroundTile(setup);
                                tileObject.Link(e, _context);
                                newOnScreenTiles.Add(e);
                            }
                        }
                    }
                }
            }
        }

        foreach (var newTile in newOnScreenTiles) {
            newTile.isHiddenTile = false;
            newTile.isOnScreenTile = true;
        }

        foreach (var tile in tiles) {
            var position = tile.view.gameObject.transform.position;
            var distance = Vector3.Distance(position, camera.transform.position);
            if (distance > _hideDistance) {
                tile.isHiddenTile = true;
            }
        }
    }

    Vector2 TilePositionForPointOnGrid(Vector2 point, Vector2 dimensions) {
        var x = Mathf.Floor(point.x / dimensions.x) * dimensions.x;
        var y = Mathf.Floor(point.y / dimensions.y) * dimensions.y;
        return new Vector2(x, y);
    }
}
