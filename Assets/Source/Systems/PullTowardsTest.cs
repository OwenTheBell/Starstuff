using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class PullTowardsTest : IInitializeSystem {

    readonly GameContext _gameContext;

    public PullTowardsTest(Contexts contexts) {
        _gameContext = contexts.game;
    }

    public void Initialize() {
        var pullers = new List<GameEntity>();
        foreach (var o in Object.FindObjectsOfType<TestTag>()) {
            var buffer = o.gameObject.AddComponent<FixedUpdateBuffer>();
            var e = _gameContext.CreateEntity();
            e.AddView(o.gameObject);
            e.AddUpdateBuffer(buffer);
            o.gameObject.Link(e, _gameContext);
            pullers.Add(e);
        }
        while (pullers.Count > 1) {
            var e = pullers[0];
            var other = pullers[Random.Range(1, pullers.Count)];
            var force = 2f;
            e.AddPullTowards(other.id.value, force);
            other.AddPullTowards(e.id.value, force);
            pullers.Remove(e);
            pullers.Remove(other);
        }
    }
}
