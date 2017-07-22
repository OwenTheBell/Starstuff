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
        _context.isLeftMouse = true;
        _leftMouseEntity = _context.leftMouseEntity;
        _context.isRightMouse = true;
        _rightMouseEntity = _context.rightMouseEntity;
    }

    public void Execute() {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            _leftMouseEntity.ReplaceMouseDown(mousePosition);
        }
        if (Input.GetMouseButton(0)) {
            _leftMouseEntity.ReplaceMousePosition(mousePosition);
        }
        if (Input.GetMouseButtonUp(0)) {
            _leftMouseEntity.ReplaceMouseUp(mousePosition);
        }

        if (Input.GetMouseButtonDown(1)) {
            _rightMouseEntity.ReplaceMouseDown(mousePosition);
        }
        if (Input.GetMouseButton(1)) {
            _rightMouseEntity.ReplaceMousePosition(mousePosition);
        }
        if (Input.GetMouseButtonUp(1)) {
            _rightMouseEntity.ReplaceMouseUp(mousePosition);
        }

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

