using Entitas;
using Entitas.Unity;
using UnityEngine;

public class StarSpawnerSystem : IInitializeSystem, IExecuteSystem {

    readonly GameContext _context;
    readonly IGroup<GameEntity> _stars;
    readonly StarSpawnInfo _info;
    readonly IComponent[] _starComponents;

    private GameEntity _player;
    private GameEntity _starInfo;

    public StarSpawnerSystem(Contexts contexts, StarSpawnInfo info, IComponent[] components) {
        _context = contexts.game;
        _stars = _context.GetGroup(GameMatcher.Star);
        _info = info;
        _starComponents = components;
    }

    public void Initialize() {
        _starInfo = _context.CreateEntity();
        AddComponentToEntity(_starInfo, _info);
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
            e.AddTrackedTransform(_player.view.transform);
            foreach (var c in _starComponents) {
                AddComponentToEntity(e, c);
            }
            e.isWaiting = true;
            star.Link(e, _context);
        }
    }

    private void AddComponentToEntity(IEntity entity, IComponent component) {
        var index = -1;
        var types = entity.contextInfo.componentTypes;
        for (var i = 0; i < types.Length; i++) {
            if (types[i] == component.GetType()) {
                index = i;
                break;
            }
        }
        if (index >= 0) {
            entity.AddComponent(index, component);
        }
    }
}
