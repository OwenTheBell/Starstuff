using UnityEngine;
using Entitas;

public class RepulserSystem : IFixedUpdateSystem {

    readonly IGroup<GameEntity> _repulsers;

    public RepulserSystem(Contexts contexts) {
        var allOf = GameMatcher.AllOf(
            GameMatcher.Repulser,
            GameMatcher.Rigidbody2D
        );
        _repulsers = contexts.game.GetGroup(allOf);
    }

    public void FixedUpdate() {
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
                e.rigidbody2D.body2D.AddForce(force);
            }
        }   
    }
}
