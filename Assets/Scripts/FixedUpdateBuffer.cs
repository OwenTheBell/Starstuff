using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FixedUpdateBuffer : MonoBehaviour {

    abstract class Command {
        public readonly ISystem Issuer;
        public Command(ISystem issuer) {
            Issuer = issuer;
        }
    }

    class GameObjectCommand : Command {
        public readonly Action<GameObject> Act;
        public GameObjectCommand(ISystem issuer, Action<GameObject> act) : base(issuer) {
            Act = act;
        }
    }

    class RigidbodyCommand : Command {
        public readonly Action<Rigidbody> Act;
        public RigidbodyCommand(ISystem issuer, Action<Rigidbody> act) : base(issuer) {
            Act = act;
        }
    }

    class Rigidbody2DCommand : Command {
        public readonly Action<Rigidbody2D> Act;
        public Rigidbody2DCommand(ISystem issuer, Action<Rigidbody2D> act) : base(issuer) {
            Act = act;
        }
    }

    public bool Is2D { get; private set; }

    private List<Command> _persistantBuffer = new List<Command>();
    private List<Command> _buffer = new List<Command>();
    private List<Command> _tempPersistant = new List<Command>();
    private List<Command> _tempBuffer = new List<Command>();

    private void Awake() {
        Is2D = gameObject.HasComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        var go = gameObject;
        var body = GetComponent<Rigidbody>();
        var body2D = GetComponent<Rigidbody2D>();
        var processCommand = new Action<Command>(c => {
            if (c.GetType() == typeof(RigidbodyCommand) && !Is2D) {
                (c as RigidbodyCommand).Act(body);
            }
            else if (c.GetType() == typeof(Rigidbody2DCommand) && Is2D) {
                (c as Rigidbody2DCommand).Act(body2D);
            }
            else {
                (c as GameObjectCommand).Act(go);
            }
        });
        foreach (var command in _buffer) {
            processCommand(command);
        }
        foreach (var command in _persistantBuffer) {
            processCommand(command);
        }
        Purge();
    }

    void GenericAdd(Command c, bool persist) {
        if (persist) {
            _tempPersistant.Add(c);
        }
        else {
            _tempBuffer.Add(c);
        }
    }

    public void AddToBuffer(ISystem issuer, Action<GameObject> act, bool persist = false) {
        GenericAdd(new GameObjectCommand(issuer, act), persist);
    }

    public void AddToBuffer(ISystem issuer, Action<Rigidbody2D> act, bool persist = false) {
        if (!Is2D) {
            Debug.Log(CreateMessage(issuer, "TRIED TO BUFFER 3D ACTION ON 2D BODY"));
        }
        GenericAdd(new Rigidbody2DCommand(issuer, act), persist);
    }

    public void AddToBuffer(ISystem issuer, Action<Rigidbody> act, bool persist = false) {
        if (Is2D) {
            Debug.Log(CreateMessage(issuer, "TRIED TO BUFFER 2D ACTION ON 3D BODY"));
        }
        GenericAdd(new RigidbodyCommand(issuer, act), persist);
    }

    public void RemoveAll(ISystem issuer) {
        _buffer.FilterInPlace<Command>(c => { return c.Issuer != issuer; });
    }

    public void Clear() {
        _buffer.Clear();
    }

    public void Purge() {
        Clear();
        _persistantBuffer.Clear();
    }

    public void CycleBuffers() {
        _persistantBuffer.AddRange(_tempPersistant);
        _tempPersistant.Clear();
        _buffer.Clear();
        _buffer.AddRange(_tempBuffer);
        _tempBuffer.Clear();
    }

    string CreateMessage(ISystem issuer, string alert) {
        return "<color='red'>" + issuer + " " + alert + "</color>";
    }
}
