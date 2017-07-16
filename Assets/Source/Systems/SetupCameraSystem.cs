using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class SetupCameraSystem : IInitializeSystem {

    readonly GameContext _context;
    readonly float _scale;

    public SetupCameraSystem(Contexts contexts, float scale) {
        _context = contexts.game;
        _scale = scale;
    }

    public void Initialize() {
        var camera = Camera.main;
        var player = _context.playerEntity.view.gameObject;
        var e = _context.CreateEntity();
        e.AddMatchMotion(player, _scale, Vector2.zero);
        e.AddView(camera.gameObject);
        camera.gameObject.Link(e, _context);
    }
}
