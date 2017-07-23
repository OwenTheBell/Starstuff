using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

public class SmoothStarStateChange : IExecuteSystem {

    readonly IGroup<GameEntity> _changers;
    readonly IGroup<MessageEntity> _setMessages;

    public SmoothStarStateChange(Contexts contexts) {
        _changers = contexts.game.GetGroup(GameMatcher.ChangingMovementStateComponent);
        _setMessages = contexts.message.GetGroup(MessageMatcher.SetVelocityMessage);
    }

    public void Execute() {
        foreach (var e in _changers.GetEntities()) {
            var changing = e.changingMovementStateComponent;
            if (changing._Remaining < 0f) {
                continue;
            }
            changing._Remaining -= Time.deltaTime;

            var oldVelocity = changing._OldVelocity;
            var p = changing._Remaining / changing.Time;
            var body = e.view.gameObject.GetComponent<Rigidbody2D>();
            foreach (var m in _setMessages.GetEntities()) {
                if (m.setVelocityMessage.Target == body) {
                    var targetVelocity = m.setVelocityMessage.Velocity;
                    var adjustedVelocity = Vector2.Lerp(oldVelocity, targetVelocity, p);
                    m.ReplaceSetVelocityMessage(adjustedVelocity, body);
                    break;
                }
            }
        }
    }
}
