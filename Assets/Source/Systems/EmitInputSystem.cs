using System;
using Entitas;
using UnityEngine;

public class EmitInputSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem {
    readonly InputContext _context;
    private InputEntity _leftMouseEntity;
    private InputEntity _rightMouseEntity;

    //readonly IGroup<InputEntity> _keysDown;
    readonly IGroup<InputEntity> _keys;
    //readonly IGroup<InputEntity> _keysUp;

    private string _lastInputString;

    public EmitInputSystem(Contexts contexts) {
        _context = contexts.input;

        _keys = _context.GetGroup(InputMatcher.Key);
        //_keysDown = _context.GetGroup(InputMatcher.KeyDown);
        //_keysUp = _context.GetGroup(InputMatcher.KeyUp);
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

        if (Input.GetKeyDown(KeyCode.Space)) {
            var e = _context.CreateEntity();
            e.AddKey(KeyCode.Space);
            e.AddKeyDown();
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            foreach (var e in _keys.GetEntities()) {
                if (e.key.key == KeyCode.Space) {
                    e.AddKeyUp();
                }
            }
        }
    }

    public void Cleanup() {
        foreach (var e in _keys.GetEntities()) {
            if (e.hasKeyDown) {
                e.RemoveKeyDown();
            }
            if (e.hasKeyUp) {
                e.Destroy();
            }
        }
    }
}

