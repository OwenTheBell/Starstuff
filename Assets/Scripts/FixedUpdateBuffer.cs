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

    private List<Command> _buffer = new List<Command>();

    private void FixedUpdate() {
        var go = gameObject;
        var body = GetComponent<Rigidbody>();
        var body2D = GetComponent<Rigidbody2D>();
        foreach (var command in _buffer) {
            if (command.GetType() == typeof(RigidbodyCommand) && !Is2D) {
                (command as RigidbodyCommand).Act(body);
            }
            else if (command.GetType() == typeof(Rigidbody2DCommand) && Is2D) {
                (command as Rigidbody2DCommand).Act(body2D);
            }
            else {
                (command as GameObjectCommand).Act(go);
            }
        }
        Clear();
    }

    private void Awake() {
        Is2D = gameObject.HasComponent<Rigidbody2D>();
    }

    public void AddToBuffer(ISystem issuer, Action<GameObject> act) {
        _buffer.Add(new GameObjectCommand(issuer, act));
    }

    public void AddToBuffer(ISystem issuer, Action<Rigidbody2D> act) {
        if (!Is2D) {
            Debug.Log("<color='red'>TRYING TO BUFFER 3D ACTION ON 2D BODY</color>");
        }
        _buffer.Add(new Rigidbody2DCommand(issuer, act));
    }

    public void AddToBuffer(ISystem issuer, Action<Rigidbody> act) {
        if (Is2D) {
            Debug.Log("<color='red'>TRYING TO BUFFER 2D ACTION ON 3D BODY</color>");
        }
        _buffer.Add(new RigidbodyCommand(issuer, act));

    }

    public void RemoveAll(ISystem issuer) {
        _buffer.FilterInPlace<Command>(c => { return c.Issuer != issuer; });
    }

    public void Clear() {
        _buffer.Clear();
    }
}
