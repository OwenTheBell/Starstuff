using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class BackgroundSystem : IInitializeSystem, IExecuteSystem {

    readonly GameContext _context;
    readonly IGroup<GameEntity> _layers;
    readonly IGroup<GameEntity> _tiles;
    readonly IGroup<GameEntity> _onScreenTiles;
    readonly IGroup<GameEntity> _hiddenTiles;

    readonly GameObject _tilePrefab;
    readonly BackgroundTileSetup[] _tileSetups;
    readonly float _hideDistance;

    bool _firstPass = true;

    public BackgroundSystem(Contexts contexts, GameObject tile, BackgroundTileSetup[] setups, float distance) {
        _context = contexts.game;
        _layers = _context.GetGroup(GameMatcher.BackgroundLayer);
        _tiles = _context.GetGroup(GameMatcher.BackgroundTile);
        _onScreenTiles = _context.GetGroup(GameMatcher.OnScreenTile);
        _hiddenTiles = _context.GetGroup(GameMatcher.HiddenTile);
        _tilePrefab = tile;
        _tileSetups = setups;
        _hideDistance = distance;
    }

    public void Initialize() {
        var camera = Camera.main;

        foreach (var setup in _tileSetups) {
            var e = _context.CreateEntity();
            var go = new GameObject();
            go.name = setup.name;
            go.transform.position = new Vector3(0f, 0f, setup.ZDepth);
            e.AddView(go);
            e.AddMatchMotion(camera.gameObject, setup.ParalaxScale, Vector2.zero);
            e.AddBackgroundLayer(setup);
            go.Link(e, _context);
        }
    }

    public void Execute() {
        //if (!_firstPass) return;

        var camera = Camera.main;
        var height = 2f * camera.orthographicSize;
        var width = camera.aspect * height;
        var widthBound = width * 2f;
        var heightBound = height * 2f;

        var tiles = new List<GameEntity>(_tiles.GetEntities());
        var hiddenTiles = new List<GameEntity>(_hiddenTiles.GetEntities());
        var onScreenTiles = new List<GameEntity>(_onScreenTiles.GetEntities());

        var newOnScreenTiles = new List<GameEntity>();
        foreach (var layer in _layers.GetEntities()) {
            var layerTransform = layer.view.gameObject.transform;
            var setup = layer.backgroundLayer.TileSetup;
            Predicate<GameEntity> tilesForLayer = (e) => {
                return e.view.gameObject.transform.parent == layerTransform;
            };
            var myTiles = tiles.Filter<GameEntity>(tilesForLayer);
            var myOnScreenTiles = onScreenTiles.Filter<GameEntity>(tilesForLayer);
            var myHiddenTiles = hiddenTiles.Filter<GameEntity>(tilesForLayer);

            for (var i = myOnScreenTiles.Count - 1; i >= 0; i--) {
                var tile = myOnScreenTiles[i];
                var local = camera.transform.InverseTransformPoint(tile.view.gameObject.transform.position);
                if (Mathf.Abs(local.x + setup.Dimensions.x / 2f) >= widthBound / 2f ||
                    Mathf.Abs(local.y + setup.Dimensions.y / 2f) >= heightBound / 2f) {
                    tile.isOnScreenTile = false;
                    myOnScreenTiles.RemoveAt(i);
                }
            }

            for (var x = -widthBound / 2f; x <= widthBound/2f; x += setup.Dimensions.x) {
                for (var y = -heightBound / 2f; y <= heightBound/2f; y += setup.Dimensions.y) {
                    var point = camera.transform.TransformPoint(new Vector2(x, y));
                    point = layerTransform.InverseTransformPoint(point);
                    point = TilePositionForPointOnGrid(point, setup.Dimensions);
                    point = layerTransform.TransformPoint(point);
                    Predicate<GameEntity> comparePosition = (e) => {
                        return (Vector2)e.view.gameObject.transform.position == (Vector2)point;
                    };
                    if (myOnScreenTiles.Filter<GameEntity>(comparePosition).Count == 0) {
                        var filtered = myTiles.Filter<GameEntity>(comparePosition);
                        if (filtered.Count > 0) {
                            newOnScreenTiles.Add(filtered[0]);
                        }
                        else {
                            if (myHiddenTiles.Count > 0) {
                                var furthestTile = myHiddenTiles[0];
                                var cameraPosition = camera.gameObject.transform.position;
                                var furthestDistance = Vector3.Distance(furthestTile.view.gameObject.transform.position, cameraPosition);
                                foreach (var tile in myHiddenTiles) {
                                    var tilePosition = tile.view.gameObject.transform.position;
                                    var testDistance = Vector3.Distance(tilePosition, cameraPosition);
                                    if (testDistance > furthestDistance) {
                                        furthestDistance = testDistance;
                                        furthestTile = tile;
                                    }
                                }
                                var go = furthestTile.view.gameObject;
                                go.transform.position = point;
                                newOnScreenTiles.Add(furthestTile);
                            }
                            else {
                                var tileObject = GameObject.Instantiate(
                                                                _tilePrefab,
                                                                point,
                                                                Quaternion.identity
                                                            );
                                tileObject.transform.parent = layerTransform;
                                tileObject.GetComponent<Tile>().Dimensions = setup.Dimensions;
                                tileObject.GetComponent<Tile>().Density = setup.Density;
                                tileObject.GetComponent<Tile>().Scale = setup.Scale;
                                tileObject.GetComponent<Tile>().Alpha = setup.Alpha;
                                var e = _context.CreateEntity();
                                e.AddView(tileObject);
                                e.isBackgroundTile = true;
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

        _firstPass = false;
    }

    Vector2 TilePositionForPointOnGrid(Vector2 point, Vector2 dimensions) {
        var x = Mathf.Floor(point.x / dimensions.x) * dimensions.x;
        var y = Mathf.Floor(point.y / dimensions.y) * dimensions.y;
        return new Vector2(x, y);
    }
}
