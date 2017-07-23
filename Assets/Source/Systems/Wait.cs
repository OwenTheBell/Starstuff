using Entitas;
using UnityEngine;

public class Wait : IExecuteSystem {

    readonly IGroup<GameEntity> _waiters;

    public Wait(Contexts contexts) {
        _waiters = contexts.game.GetGroup(GameMatcher.Waiting);
    }

    public void Execute() {
        foreach (var e in _waiters.GetEntities()) {
            var myPos = e.view.transform.position;
            var targetPos = e.trackedTransform.Transform.position;
            var distance = Vector2.Distance(myPos, targetPos);
            var renderer = e.view.gameObject.GetComponentInChildren<Renderer>();

            if ((distance < e.wait.Range && e.wait._RemainingDelay >= 0) &&
                (!e.wait.BeVisible || (e.wait.BeVisible && renderer.isVisible))
            ) {
                e.wait._RemainingDelay = e.wait.Delay; 
            }
            if (e.wait._RemainingDelay >= 0f) {
                e.wait._RemainingDelay -= Time.deltaTime;
                if (e.wait._RemainingDelay <= 0f) {
                    if (e.view.gameObject.HasComponent<WakeupEffect>()) {
                        e.view.gameObject.GetComponent<WakeupEffect>().Play();
                    }
                    e.isWaiting = false;
                    e.isFollowing = true;
                    var state = e.changingMovementStateComponent;
                    state._Remaining = state.Time;
                }
            }
        }
    }
}
