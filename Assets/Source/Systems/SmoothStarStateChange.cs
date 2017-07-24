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
            var p = 1f - changing._Remaining / changing.Time;
            var body = e.view.gameObject.GetComponent<Rigidbody2D>();

            var buffer = e.view.gameObject.GetComponent<FixedUpdateBuffer>();
            buffer.RemoveAll(this);
            buffer.AddToBuffer(this, () => {
                var currentVelocity = body.velocity;
                var velocity = Vector2.Lerp(oldVelocity, currentVelocity, p);
                body.velocity = velocity;
            });
        }
    }
}
