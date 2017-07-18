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

    public SpinSystem(Contexts contexts) {
        _messageContext = contexts.message;
        _spinners = contexts.game.GetGroup(GameMatcher.Spin);
        _keys = contexts.input.GetGroup(InputMatcher.Key);
        _senderMessages = contexts.message.GetGroup(MessageMatcher.MessageSender);
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
        var messagesISent = new List<MessageEntity>(_senderMessages.GetEntities());
        messagesISent = messagesISent.Filter<MessageEntity>(e => {
            return e.messageSender.Sender == this;
        });

        var index = 0;
        foreach (var e in _spinners.GetEntities()) {
            if (!e.hasView) {
                continue;
            }

            var go = e.view.gameObject;
            var rigidbody = go.GetComponent<Rigidbody2D>();

            if (adjust != 0) {
                var torque = e.spin.Torque * adjust;
                if (index < messagesISent.Count) {
                    messagesISent[index++].ReplaceApplyTorqueMessage(torque, rigidbody);
                }
                else {
                    var newMessage = _messageContext.CreateEntity();
                    newMessage.AddMessageSender(this);
                    newMessage.AddApplyTorqueMessage(torque, rigidbody);
                    newMessage.isPersistUntilConsumed = true;
                    newMessage.isDestroyOnConsume = true;
                }
            }

            var angularVelocity = rigidbody.angularVelocity;
            var angularDirection = (int)(angularVelocity / Mathf.Abs(angularVelocity));
            if (adjust == 0 || adjust != angularDirection) {
                var counterTorque = -angularVelocity * e.spin.Dampening;
                if (index < messagesISent.Count) {
                    messagesISent[index++].ReplaceApplyTorqueMessage(counterTorque, rigidbody);
                }
                else {
                    var newMessage = _messageContext.CreateEntity();
                    newMessage.AddMessageSender(this);
                    newMessage.AddApplyTorqueMessage(counterTorque, rigidbody);
                    newMessage.isPersistUntilConsumed = true;
                    newMessage.isDestroyOnConsume = true;
                }
            }
        }
        while (index < messagesISent.Count) {
            messagesISent[index++].Destroy();
        }
    }

}
