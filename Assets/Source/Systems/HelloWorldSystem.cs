using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class HelloWorldSystem : IInitializeSystem {

    readonly GameContext _context;

    public HelloWorldSystem(Contexts contexts) {
        _context = contexts.game;
    }

    public void Initialize() {
        _context.CreateEntity().AddDebugMessage("Hello World!");
        _context.CreateEntity().AddDebugMessage("This is Owen!");
    }

}
