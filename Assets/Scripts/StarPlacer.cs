using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlacer : MonoBehaviour {

    public GameObject StarPrefab;
    public float Arc;
    public float SpawnDistance;
    public Vector2 SpawnRange;
    public int MaxStars;

    private float _RemainingDistance;
    private Vector3 _LastPosition;
    private List<GameObject> _Stars;

    private void Awake() {
        _Stars = new List<GameObject>();
    }

    void Start () {
        _RemainingDistance = Random.Range(SpawnRange.x, SpawnRange.y);
        _LastPosition = transform.position;
	}
	
	void Update () {
        _RemainingDistance -= (transform.position - _LastPosition).magnitude;
        _LastPosition = transform.position;
        if (_RemainingDistance <= 0f && _Stars.Count < MaxStars) {
            _RemainingDistance = Random.Range(SpawnRange.x, SpawnRange.y);
            var halfarc = Arc * Mathf.Deg2Rad / 2f;
            var angle = Random.Range(-halfarc, halfarc) + (Mathf.PI / 2f);
            var point = new Vector2(Mathf.Cos(angle) * SpawnDistance, Mathf.Sin(angle) * SpawnDistance);
            point = transform.TransformPoint(point);
            var euler = new Vector3(0, 0, Random.value * 360f);
            var star = Instantiate(StarPrefab, point, Quaternion.Euler(euler));
            star.GetComponent<ChaseTransform>().Target = transform;
            _Stars.Add(star);
        }
	}
}
