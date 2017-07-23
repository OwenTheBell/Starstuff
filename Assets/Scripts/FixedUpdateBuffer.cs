using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FixedUpdateBuffer : MonoBehaviour {

    struct Command {
        public readonly ISystem Issuer;
        public readonly Action Act;

        public Command(ISystem issuer, Action act) {
            Issuer = issuer;
            Act = act;
        }
    }

    private List<Command> _buffer = new List<Command>();

    private void FixedUpdate() {
        foreach (var command in _buffer) {
            command.Act();
        }
        Clear();
    }

    public void AddToBuffer(ISystem issuer, Action act) {
        _buffer.Add(new Command(issuer, act));
    }

    public void RemoveAll(ISystem issuer) {
        _buffer.FilterInPlace<Command>(c => { return c.Issuer != issuer; });
    }

    public void Clear() {
        _buffer.Clear();
    }
}
