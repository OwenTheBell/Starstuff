using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class ThrusterSystem : IExecuteSystem {

    readonly MessageContext _messageContext;
    readonly IGroup<GameEntity> _thrusters;
    readonly IGroup<InputEntity> _keys;
    readonly IGroup<MessageEntity> _senderMessages;

    public ThrusterSystem(Contexts contexts) {
        _messageContext = contexts.message;
        _thrusters = contexts.game.GetGroup(GameMatcher.Thruster);
        _keys = contexts.input.GetGroup(InputMatcher.Key);
        _senderMessages = contexts.message.GetGroup(MessageMatcher.MessageSender);
    }

    public void Execute() {
        var thrustKey = false;
        foreach (var e in _keys.GetEntities()) {
            if (e.hasKey && !e.isKeyUp && e.key.name == "Thrust") {
                thrustKey = true;
                break;
            }
        }

        var messagesISent = new List<MessageEntity>(_senderMessages.GetEntities());
        messagesISent = messagesISent.Filter<MessageEntity>(e => {
            return e.messageSender.Sender == this;
        });
        var index = 0;

        var generateMessage = new Action<Vector2, ForceMode2D, Rigidbody2D>((f, m, r) => {
            if (index < messagesISent.Count) {
                messagesISent[index++].ReplaceApplyForceMessage(f, m, r);
            }
            else {
                var newMessage = _messageContext.CreateEntity();
                newMessage.AddMessageSender(this);
                newMessage.AddApplyForceMessage(f, m, r);
                newMessage.isPersistUntilConsumed = true;
                newMessage.isDestroyOnConsume = true;
            }
        });

        foreach(var e in _thrusters.GetEntities()) {
            var go = e.view.gameObject;
            var rigidbody = go.GetComponent<Rigidbody2D>();

            if (!e.hasView) {
                continue;
            }

            var applyDampening = new Action(() => {
                var velocity = -e.thruster.Dampening * rigidbody.velocity;
                generateMessage(velocity, ForceMode2D.Force, rigidbody);
            });

            if (thrustKey) {
                var force = go.transform.up * e.thruster.Force;
                generateMessage(force, ForceMode2D.Force, rigidbody);
                var velocity = rigidbody.velocity;
                var angle1 = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
                var angle2 = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                if (Mathf.Abs(angle1 - angle2) > 20f) {
                    applyDampening();
                }
            }
            else {
                applyDampening();
            }
        }
        while (index < messagesISent.Count) {
            messagesISent[index++].Destroy();
        }
    }
}
