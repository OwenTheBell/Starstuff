using Entitas;
using UnityEngine;

public class EmitInputSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem {

    readonly InputContext _context;
    private InputEntity _leftMouseEntity;
    private InputEntity _rightMouseEntity;
    readonly IGroup<InputEntity> _keys;
    private FlightActionSet _actionSet;

    public EmitInputSystem(Contexts contexts) {
        _context = contexts.input;
        _keys = _context.GetGroup(InputMatcher.Key);
        _actionSet = FlightActionSet.CreateWithDefaultBindings();
    }

    public void Initialize() {
    }

    public void Execute() {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (var action in _actionSet.Actions) {
            if (action.WasPressed) {
                var e = _context.CreateEntity();
                e.AddKey(KeyCode.Space, action.Name);
                e.isKeyDown = true;
            }
            else if (action.WasReleased) {
                foreach (var e in _keys.GetEntities()) {
                    if (e.key.name == action.Name) {
                        e.isKeyUp = true;
                    }
                }
            }
        }
    }

    public void Cleanup() {
        foreach (var e in _keys.GetEntities()) {
            if (e.isKeyDown) {
                e.isKeyDown = false;
            }
            if (e.isKeyUp) {
                e.Destroy();
            }
        }
    }
}

