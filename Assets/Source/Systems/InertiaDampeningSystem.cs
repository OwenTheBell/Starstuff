using Entitas;
using UnityEngine;

public class InertiaDampeningSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _dampeners;
    readonly IGroup<MessageEntity> _triggerThrust;

    public InertiaDampeningSystem(Contexts contexts) {
        _dampeners = contexts.game.GetGroup(GameMatcher.AllOf(
                                                    GameMatcher.DampenInertia,
                                                    GameMatcher.UpdateBuffer,
                                                    GameMatcher.Body2D,
                                                    GameMatcher.Thruster
                                                )
                                            );
        _triggerThrust = contexts.message.GetGroup(MessageMatcher.AllOf(
                                                        MessageMatcher.TriggerThrust,
                                                        MessageMatcher.CanBeProcessed,
                                                        MessageMatcher.MessageTarget
                                                    )
                                                );
    }

    public void Execute() {
        foreach (var e in _dampeners.GetEntities()) {
            var buffer = e.updateBuffer.buffer;
            var dampening = e.thruster.Dampening;

            if (e.isThrustring) {
                // get the average direction that the thrust is being applied
                var direction = Vector2.zero;
                var totalThrusts = 0;
                foreach (var m in _triggerThrust.GetEntities()) {
                    if (m.messageTarget.id != e.id.value) {
                        continue;
                    }
                    direction += (Vector2)m.triggerThrust.direction;
                    totalThrusts++;
                }
                // just ensure no divide by zeroes, although in theory that should
                // never happen
                totalThrusts = (int)Mathf.Clamp(totalThrusts, 1f, Mathf.Infinity);
                direction /= totalThrusts;

                var velocity = e.body2D.value.velocity;
                var angle1 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                var angle2 = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                if (Mathf.Abs(angle1 - angle2) > 20f) {
                    Debug.Log("angle calculation is wrong");
                    buffer.AddToBuffer(this, (Rigidbody2D r) => DampenIntertia(r, dampening));
                }
            }
            else {
                buffer.AddToBuffer(this, (Rigidbody2D r) => DampenIntertia(r, dampening));
            }
        }
    }

    void DampenIntertia(Rigidbody2D r, float dampening) {
        var force = -(dampening * r.mass * r.velocity);
        r.AddForce(force, ForceMode2D.Force);
    }
}
