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

    readonly GameObject _tilePrefab;
    readonly BackgroundTileSetup[] _tileSetups;
    readonly float _hideDistance;

    // this list is maintained purely for garbage collection reasons
    // it should be cleared at the end of every Execute()
    List<GameEntity> _childTiles = new List<GameEntity>();

    public BackgroundSystem(
        Contexts contexts,
        GameObject tile,
        BackgroundTileSetup[] setups,
        float distance
    ) {
        _context = contexts.game;
        _layers = _context.GetGroup(GameMatcher.BackgroundLayer);;
        _tilePrefab = tile;
        _tileSetups = setups;
        _hideDistance = distance;
    }

    public void Initialize() {
        var camera = Camera.main;
        foreach (var setup in _tileSetups) {
            var e = _context.CreateEntity();
            var go = new GameObject(setup.name);
            go.transform.position = new Vector3(0f, 0f, setup.ZDepth);
            e.AddView(go);
            e.AddMatchMotion(camera.gameObject, setup.ParalaxScale, Vector2.zero);
            e.AddBackgroundLayer(setup, new List<GameEntity>());
            go.Link(e, _context);
        }
    }

    public void Execute() {
        var camera = Camera.main;
        var height = 2f * camera.orthographicSize;
        var width = camera.aspect * height;
        var widthBound = width * 2f;
        var heightBound = height * 2f;

        var iteration = 0;
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

            _childTiles.Clear();
            _childTiles = new List<GameEntity>(layer.backgroundLayer._Tiles);
            var camPos = camera.transform.position;
            foreach (var slot in slots) {
                GameEntity selectedTile = null;
                var furthestDistance = 0f;
                foreach (var child in _childTiles) {
                    iteration++;
                    if ((Vector2)child.view.transform.localPosition == slot) {
                        selectedTile = child;
                        break;
                    }
                    var distance = Vector2.Distance(child.view.transform.position, camPos);
                    if (distance > furthestDistance && distance > _hideDistance) {
                        furthestDistance = distance;
                        selectedTile = child;
                    }
                }
                if (selectedTile != null) {
                    selectedTile.view.transform.localPosition = slot;
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
                var e = _context.CreateEntity();
                e.AddView(tile);
                e.isBackgroundTile = true;
                tile.Link(e, _context);
                layer.backgroundLayer._Tiles.Add(e);
            }
        }
        _childTiles.Clear();
    }

    Vector2 TilePositionForPointOnGrid(Vector2 point, Vector2 dimensions) {
        var x = Mathf.Floor(point.x / dimensions.x) * dimensions.x;
        var y = Mathf.Floor(point.y / dimensions.y) * dimensions.y;
        return new Vector2(x, y);
    }
}
