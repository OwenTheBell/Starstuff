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
    readonly IGroup<GameEntity> _unusedTiles;

    readonly GameObject _tilePrefab;
    readonly BackgroundTileSetup[] _tileSetups;
    readonly float _hideDistance;

    Dictionary<GameEntity, List<GameObject>> _childTiles;

    bool _firstPass = true;

    public BackgroundSystem(
        Contexts contexts,
        GameObject tile,
        BackgroundTileSetup[] setups,
        float distance
    ) {
        _context = contexts.game;
        _layers = _context.GetGroup(GameMatcher.BackgroundLayer);
        _tiles = _context.GetGroup(GameMatcher.BackgroundTile);
        _onScreenTiles = _context.GetGroup(GameMatcher.OnScreenTile);
        _hiddenTiles = _context.GetGroup(GameMatcher.HiddenTile);
        _unusedTiles = _context.GetGroup(GameMatcher.UnusedTile);
        _tilePrefab = tile;
        _tileSetups = setups;
        _hideDistance = distance;
    }

    public void Initialize() {
        var camera = Camera.main;
        _childTiles = new Dictionary<GameEntity, List<GameObject>>();
        foreach (var setup in _tileSetups) {
            var e = _context.CreateEntity();
            var go = new GameObject(setup.name);
            go.transform.position = new Vector3(0f, 0f, setup.ZDepth);
            e.AddView(go);
            e.AddMatchMotion(camera.gameObject, setup.ParalaxScale, Vector2.zero);
            e.AddBackgroundLayer(setup);
            go.Link(e, _context);
            _childTiles.Add(e, new List<GameObject>());
        }
    }

    struct GridPosition {
        public readonly Vector2 point;
        public readonly BackgroundTileSetup layer;

        public GridPosition(Vector2 position, BackgroundTileSetup layer) {
            this.point = position;
            this.layer = layer;
        }
    }

    public void Execute() {
        //if (!_firstPass) return;
        var camera = Camera.main;
        var height = 2f * camera.orthographicSize;
        var width = camera.aspect * height;
        var widthBound = width * 2f;
        var heightBound = height * 2f;

        var layers = new List<GameEntity>(_layers.GetEntities());
        foreach (var layer in _layers.GetEntities()) {
            var slots = new List<Vector2>();
            var setup = layer.backgroundLayer.TileSetup;
            for (var x = -width; x <= width; x += setup.Dimensions.x) {
                for (var y = -height; y <= height; y += setup.Dimensions.y) {
                    var point = camera.transform.TransformPoint(new Vector2(x, y));
                    point = layer.view.transform.InverseTransformPoint(point);
                    point = TilePositionForPointOnGrid(point, setup.Dimensions);
                    slots.Add(point);
                }
            }
            //var childTiles = new List<Tile>(layer.view.gameObject.GetComponentsInChildren<Tile>());

            // make a new list so objects aren't being removed from the original
            var childTiles = new List<GameObject>(_childTiles[layer]);
            var camPos = camera.transform.position;
            var iterations = 0;
            foreach (var slot in slots) {
                GameObject selectedTile = null;
                var furthestDistance = 0f;
                foreach (var child in childTiles) {
                    iterations++;
                    if ((Vector2)child.transform.localPosition == slot) {
                        selectedTile = child;
                        break;
                    }
                    var distance = Vector2.Distance(child.transform.position, camPos);
                    if (distance > furthestDistance && distance > _hideDistance) {
                        furthestDistance = distance;
                        selectedTile = child;
                    }
                }
                if (selectedTile != null) {
                    selectedTile.transform.localPosition = slot;
                    //childTiles.Remove(selectedTile);
                    continue;
                }

                // no tile was found to fill the slot, so make one
                var tile = GameObject.Instantiate(_tilePrefab);
                tile.transform.parent = layer.view.transform;
                tile.transform.localPosition = slot;
                tile.GetComponent<Tile>().Dimensions = setup.Dimensions;
                tile.GetComponent<Tile>().Density = setup.Density;
                tile.GetComponent<Tile>().Scale = setup.Scale;
                tile.GetComponent<Tile>().Alpha = setup.Alpha;
                _childTiles[layer].Add(tile);

                //// check if there is a tile on screen that fits in this slot
                //var onScreen = childTiles.Filter<Tile>(t => {
                //    return (Vector2)t.transform.localPosition == missing.point;
                //});
                //if (onScreen.Count > 0) {
                //    continue;
                //}

                //// is there a tile far enough away that can be moved
                //var farAway = childTiles.Filter<Tile>(t => {
                //    var myPos = t.transform.position;
                //    var camPos = camera.transform.position;
                //    var distance = Vector2.Distance(myPos, camPos);
                //    return distance > _hideDistance;
                //});
                //if (farAway.Count > 0) {
                //    var farTile = farAway[0];
                //    farTile.transform.localPosition = missing.point;
                //    continue;
                //}

                //// make a tile to fill the gap
                //var tile = GameObject.Instantiate(_tilePrefab);
                //tile.transform.parent = layer.view.transform;
                //tile.transform.localPosition = missing.point;
                //tile.GetComponent<Tile>().Dimensions = setup.Dimensions;
                //tile.GetComponent<Tile>().Density = setup.Density;
                //tile.GetComponent<Tile>().Scale = setup.Scale;
                //tile.GetComponent<Tile>().Alpha = setup.Alpha;
            }
            Debug.Log("iterations this frame: " + iterations);
        }
       _firstPass = false;
    }

    Vector2 TilePositionForPointOnGrid(Vector2 point, Vector2 dimensions) {
        var x = Mathf.Floor(point.x / dimensions.x) * dimensions.x;
        var y = Mathf.Floor(point.y / dimensions.y) * dimensions.y;
        return new Vector2(x, y);
    }
}
