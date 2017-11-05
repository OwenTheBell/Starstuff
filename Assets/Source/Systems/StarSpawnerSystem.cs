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
            info.Range *= info.GapIncrease;
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
            var buffer = star.AddComponent<FixedUpdateBuffer>();
            var e = _context.CreateEntity();
            e.AddView(star);
            e.isStar = true;
            e.AddTrackedTransform(_player.view.transform);
            foreach (var c in _starComponents) {
                AddComponentToEntity(e, c);
            }
            e.isWaiting = true;
            e.AddThruster(0f, 0f);
            e.AddDampenInertia(1.1f);
            e.AddDampenSpin(0.0001f);
            e.AddUpdateBuffer(buffer);
            e.AddBody2D(star.GetComponent<Rigidbody2D>());
            e.AddRepulser(0f, 10f, 3f);
            e.AddMaxVelocity(100f);
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
            // use reflection to copy the component so that alterations to it
            // do not change the value of the original component
            var newComponent = entity.CreateComponent(index, types[index]);
            foreach (var field in types[index].GetFields()) {
                var value = field.GetValue(component);
                field.SetValue(newComponent, value);
            }
            entity.AddComponent(index, newComponent);
        }
    }
}
