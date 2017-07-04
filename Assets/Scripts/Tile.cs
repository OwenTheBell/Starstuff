using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public GameObject StarPrefab;
    public Vector2 Dimensions;
    public float Scale;
    public float Density;

    private List<GameObject> _Stars;

    private void Awake() {
        _Stars = new List<GameObject>();
    }

    void Start () {
        var count = (int)(Dimensions.x * Dimensions.y * Density);
        for (var i = 0; i < count; i++) {
            var x = Random.Range(0f, Dimensions.x) - Dimensions.x/2f;
            var y = Random.Range(0f, Dimensions.y) - Dimensions.y/2f;
            var position = transform.TransformPoint(new Vector2(x, y));
            var star = Instantiate(StarPrefab, position, Quaternion.identity);
            star.transform.localScale = Vector3.one * Scale;
            star.transform.parent = transform;
            _Stars.Add(star);
        }
	}
	
	void Update () {
		
	}

    private void OnDrawGizmos() {
        var upperLeft = transform.TransformPoint(new Vector2(-Dimensions.x / 2f, Dimensions.y / 2f));
        var lowerLeft = transform.TransformPoint(new Vector2(-Dimensions.x / 2f, -Dimensions.y / 2f));
        var upperRight = transform.TransformPoint(new Vector2(Dimensions.x / 2f, Dimensions.y / 2f));
        var lowerRight = transform.TransformPoint(new Vector2(Dimensions.x / 2f, -Dimensions.y / 2f));
        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(lowerRight, lowerLeft);
        Gizmos.DrawLine(lowerLeft, upperLeft);
    }
}
