using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTiler : MonoBehaviour {

    public Vector2 Dimensions;
    public float Density;
    public float Scale;
    [Range(0, 1)]
    public float Alpha;
    public GameObject TilePrefab;

    private List<GameObject> _Tiles;
    private List<GameObject> _OnScreenTiles;

    private void Awake() {
        _Tiles = new List<GameObject>();
        _OnScreenTiles = new List<GameObject>();
    }

    void Start () {
	}
	
	void Update () {
        var camera = Camera.main;
        var height = 2f * camera.orthographicSize;
        var width = camera.aspect * height;

        Predicate<GameObject> inBounds = (g) => {
            var local = camera.transform.InverseTransformPoint(g.transform.position);
            if (Mathf.Abs(local.x + Dimensions.x/2f) <= width / 2f + Dimensions.x &&
                Mathf.Abs(local.y + Dimensions.y/2f) <= height / 2f + Dimensions.y) {
                return true;
            }
            return false;
        };
        _OnScreenTiles = _OnScreenTiles.Filter<GameObject>(inBounds);

        var localPositions = new List<Vector2>();
        foreach (var tile in _OnScreenTiles) {
            localPositions.Add(camera.transform.InverseTransformPoint(tile.transform.position));
        }
        // check for missing tiles in the range and add a new one
        for (var x = -width/2f - Dimensions.x; x <= width/2f + Dimensions.x; x += Dimensions.x) {
            for (var y = -height/2f - Dimensions.y; y <= height/2f + Dimensions.y; y += Dimensions.y) {
                var point = camera.transform.TransformPoint(new Vector2(x, y));
                var tilePos = TilePositionForPointOnGrid(transform.InverseTransformPoint(point));
                tilePos = transform.TransformPoint(tilePos);
                Predicate<GameObject> comparePosition = (g) => {
                    return (Vector2)g.transform.position == tilePos;
                };
                if (_OnScreenTiles.Filter<GameObject>(comparePosition).Count == 0) {
                    var filtered = _Tiles.Filter<GameObject>(comparePosition);
                    if (filtered.Count > 0) {
                        _OnScreenTiles.Add(filtered[0]);
                    }
                    else {
                        var tile = Instantiate(TilePrefab, tilePos, Quaternion.identity);
                        tile.transform.parent = transform;
                        tile.GetComponent<Tile>().Dimensions = Dimensions;
                        tile.GetComponent<Tile>().Density = Density;
                        tile.GetComponent<Tile>().Scale = Scale;
                        tile.GetComponent<Tile>().Alpha = Alpha;
                        _OnScreenTiles.Add(tile);
                        _Tiles.Add(tile);
                    }
                }
            }
        }
	}

    Vector2 TilePositionForPointOnGrid(Vector2 point) {
        var x = Mathf.Floor(point.x / Dimensions.x) * Dimensions.x;
        var y = Mathf.Floor(point.y / Dimensions.y) * Dimensions.y;
        return new Vector2(x, y);
    }
}
