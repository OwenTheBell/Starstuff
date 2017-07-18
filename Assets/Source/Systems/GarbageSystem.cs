using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class GarbageSystem : IInitializeSystem, IExecuteSystem {

    readonly GameContext _context;
    readonly IGroup<GameEntity> _stars;

    readonly int _childCount = 2000;

    public GarbageSystem(Contexts contexts) {
        _context = contexts.game;
        _stars = _context.GetGroup(GameMatcher.Star);
    }

    public void Initialize() {
        var e = _context.CreateEntity();
        var go = new GameObject("Garbage");
        e.AddView(go);
        e.isStar = true;
        go.Link(e, _context);

        for (var i = 0; i < _childCount; i++) {
            var child = new GameObject("Garbage Child");
            child.transform.parent = go.transform;
            child.AddComponent<Quit>();
        }
    }

    public void Execute() {
        var iterationCount = 0;
        var entity = _stars.GetSingleEntity();
        var children = entity.view.gameObject.ListOfChildrenWithComponent<Quit>();
        foreach (var child in children) {
            iterationCount++;
        }
    }
}
