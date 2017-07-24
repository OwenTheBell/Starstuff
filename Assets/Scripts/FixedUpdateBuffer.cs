using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FixedUpdateBuffer : MonoBehaviour {

    struct Command {
        public readonly ISystem Issuer;
        public readonly Action<Rigidbody2D> Act;

        public Command(ISystem issuer, Action<Rigidbody2D> act) {
            Issuer = issuer;
            Act = act;
        }
    }

    private List<Command> _buffer = new List<Command>();

    private void FixedUpdate() {
        var body = GetComponent<Rigidbody2D>();
        foreach (var command in _buffer) {
            command.Act(body);
        }
        Clear();
    }

    public void AddToBuffer(ISystem issuer, Action<Rigidbody2D> act) {
        _buffer.Add(new Command(issuer, act));
    }

    public void RemoveAll(ISystem issuer) {
        _buffer.FilterInPlace<Command>(c => { return c.Issuer != issuer; });
    }

    public void Clear() {
        _buffer.Clear();
    }
}
