using Entitas;
using UnityEngine;

public class SpinDampeningSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _dampeners;
    readonly IGroup<MessageEntity> _triggerSpin;
    readonly MessageContext _messageContext;

    bool _firstPass = true;

    public SpinDampeningSystem(Contexts contexts) {
        _messageContext = contexts.message;
        _dampeners = contexts.game.GetGroup(GameMatcher.AllOf(
                                                GameMatcher.DampenSpin,
                                                GameMatcher.UpdateBuffer,
                                                GameMatcher.Body2D
                                            )
                                        );
        _triggerSpin = contexts.message.GetGroup(MessageMatcher.AllOf(
                                                        MessageMatcher.TriggerSpin,
                                                        MessageMatcher.CanBeProcessed,
                                                        MessageMatcher.MessageTarget
                                                    )
                                                );
    }

    public void Execute() {
        foreach (var e in _dampeners.GetEntities()) {
            var buffer = e.updateBuffer.buffer;

            var shouldDampen = !e.isSpinning;
            if (e.isSpinning) {
                var direction = 0;
                foreach (var m in _triggerSpin.GetEntities()) {
                    if (m.messageTarget.id != e.id.value) {
                        continue;
                    }
                    direction += m.triggerSpin.value;
                }
                // normalize the value
                if (direction != 0) direction /= (int)Mathf.Abs(direction);

                var angularVelocity = e.body2D.value.angularVelocity;
                var currentDirection = (int)(angularVelocity / Mathf.Abs(angularVelocity));

                shouldDampen = direction != currentDirection;
            }
            if (shouldDampen) {
                Debug.Log("damping");
                var m = MessageGenerator.Message(true);
                m.AddBuffer2DAction(this, (Rigidbody2D r) => {
                    r.AddTorque(-r.angularVelocity * e.dampenSpin.value);
                });
                m.AddMessageTarget(e.id.value);
            }
            else {
                Debug.Log("not damping");
            }
        }
    }
}