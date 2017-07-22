using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class SpinSystem : IExecuteSystem {

    readonly MessageContext _messageContext;

    readonly IGroup<GameEntity> _spinners;
    readonly IGroup<InputEntity> _keys;
    readonly IGroup<MessageEntity> _senderMessages;

    List<MessageEntity> _messagesISent;

    public SpinSystem(Contexts contexts) {
        _messageContext = contexts.message;
        _spinners = contexts.game.GetGroup(GameMatcher.Spin);
        _keys = contexts.input.GetGroup(InputMatcher.Key);
        _senderMessages = contexts.message.GetGroup(MessageMatcher.MessageSender);

        _messagesISent = new List<MessageEntity>();
    }

    public void Execute() {
        var leftKey = false;
        var rightKey = false;
        foreach (var e in _keys.GetEntities()) {
            if (e.hasKey && !e.isKeyUp) {
                if (e.key.name == "Left") {
                    leftKey = true;
                }
                else if (e.key.name == "Right") {
                    rightKey = true;
                }
            }
        }
        var adjust = 0;
        if (leftKey && !rightKey) {
            adjust = 1;
        }
        else if (!leftKey && rightKey) {
            adjust = -1;
        }

        // gather all unconsumed messages so they can be replaced with new info
        foreach (var m in _senderMessages.GetEntities()) {
            if (m.messageSender.Sender == this) {
                _messagesISent.Add(m);
            }
        }
        var index = 0;
        
        var generateMessage = new Action<float, Rigidbody2D>((t, r) => {
            if (index < _messagesISent.Count) {
                _messagesISent[index++].ReplaceApplyTorqueMessage(t, r);
            }
            else {
                var newMessage = MessageGenerator.OwnedMessage(this, true);
                newMessage.AddApplyTorqueMessage(t, r);
                newMessage.isPersistUntilConsumed = true;
            }
        });

        foreach (var e in _spinners.GetEntities()) {
            if (!e.hasView) {
                continue;
            }

            var go = e.view.gameObject;
            var rigidbody = go.GetComponent<Rigidbody2D>();

            if (adjust != 0) {
                var torque = e.spin.Torque * adjust;
                generateMessage(torque, rigidbody);
            }

            var angularVelocity = rigidbody.angularVelocity;
            var angularDirection = (int)(angularVelocity / Mathf.Abs(angularVelocity));
            if (adjust == 0 || adjust != angularDirection) {
                var counterTorque = -angularVelocity * e.spin.Dampening;
                generateMessage(counterTorque, rigidbody);
            }
        }
        while (index < _messagesISent.Count) {
            _messagesISent[index++].Destroy();
        }
        _messagesISent.Clear();
    }

}
