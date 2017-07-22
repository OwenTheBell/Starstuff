using Entitas;
using Entitas.Unity;
using UnityEngine;

public class StarSpawnerSystem : IInitializeSystem, IExecuteSystem {

    readonly GameContext _context;
    readonly IGroup<GameEntity> _stars;
    readonly StarSpawnInfo _info;
    private GameEntity _player;
    private GameEntity _starInfo;

    public StarSpawnerSystem(Contexts contexts, StarSpawnInfo info) {
        _context = contexts.game;
        _stars = _context.GetGroup(GameMatcher.Star);
        _info = info;
    }

    public void Initialize() {
        _starInfo = _context.CreateEntity();
        var index = 0;
        var types = GameComponentsLookup.componentTypes;
        for (var i = 0; i < types.Length; i++) {
            if (types[i] == typeof(StarSpawnInfo)) {
                index = i;
                break;
            }
        }
        _starInfo.AddComponent(index, _info);
        _player = _context.playerEntity;
        _starInfo.starSpawnInfo._RemainingDistance = Random.Range(_info.Range.x, _info.Range.y);
        _starInfo.starSpawnInfo._LastPosition = _player.view.transform.position;
    }

    public void Execute() {
        var playerPos = _player.view.transform.position;
        var info = _starInfo.starSpawnInfo;
        info._RemainingDistance -= (playerPos - info._LastPosition).magnitude;
        info._LastPosition = playerPos;
        if (info._RemainingDistance <= 0f && _stars.count < info.MaxStars) {
            info._RemainingDistance = Random.Range(info.Range.x, info.Range.y);
            var halfarc = info.Arc * Mathf.Deg2Rad / 2f;
            var angle = Random.Range(-halfarc, halfarc) + (Mathf.PI / 2f);
            var point = new Vector2(
                                Mathf.Cos(angle) * info.Distance,
                                Mathf.Sin(angle) * info.Distance
                            );
            point = _player.view.transform.TransformPoint(point);
            var euler = new Vector3(0, 0, Random.value * 360f);
            var star = GameObject.Instantiate(info.StarPrefab, point, Quaternion.Euler(euler));
            var e = _context.CreateEntity();
            e.AddView(star);
            e.isStar = true;
            star.Link(e, _context);
        }
    }
}
