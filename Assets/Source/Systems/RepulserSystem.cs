using System;
using UnityEngine;
using Entitas;

public class RepulserSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _repulsers;

    public RepulserSystem(Contexts contexts) {
        _repulsers = contexts.game.GetGroup(GameMatcher.AllOf(
										            GameMatcher.Repulser,
										            GameMatcher.Body2D,
										            GameMatcher.UpdateBuffer
										        )
                                           );
    }

    public void Execute() {
        foreach (var e in _repulsers.GetEntities()) {
            if (e.isImmuneToRepulsion) continue;
            foreach (var otherE in _repulsers.GetEntities()) {
                if (e == otherE) continue;

                var myPos = e.view.transform.position;
                var theirPos = otherE.view.transform.position;
                var distance = Vector2.Distance(myPos, theirPos);
                if (distance > e.repulser.range) continue;

                var angle = Mathf.Atan2(myPos.y - theirPos.y, myPos.x - theirPos.x);
                var force = new Vector2(
						                    e.repulser.force * Mathf.Cos(angle),
	                                        e.repulser.force * Mathf.Sin(angle)
                                        );
                var m = MessageGenerator.Message(true);
                m.AddBuffer2DAction(this, (Rigidbody2D r) => r.AddForce(force));
                m.AddMessageTarget(e.id.value);
            }
        }   
    }
}
