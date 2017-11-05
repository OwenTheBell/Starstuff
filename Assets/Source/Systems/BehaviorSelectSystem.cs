using Entitas;
using UnityEngine;

public class BehaviorSelectSystem : IExecuteSystem {

    const float LOWER_DELAY = 5f;
    const float UPPER_DELAY = 5f;

    readonly System.Type[] _behaviorTypes = new System.Type[]{
        //typeof(CirclePlayerComponent),
        //typeof(CircleStarComponent),
        typeof(TwirlComponent),
        //typeof(SlamStarComponent),
        //typeof(CatapultComponent)
    };
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _followers;

    public BehaviorSelectSystem(Contexts contexts) {
        _gameContext = contexts.game;
        _followers = _gameContext.GetGroup(GameMatcher.Following);
    }

    public void Execute() {
        foreach (var e in _followers.GetEntities()) {
            if (!e.hasBehaviorDelay) {
                e.AddBehaviorDelay(Random.Range(LOWER_DELAY, UPPER_DELAY));
            }
            e.behaviorDelay.value -= _gameContext.tickTracker.Tick;
            if (e.behaviorDelay.value <= 0f) {
                e.isFollowing = false;
                var r = Random.Range(0, _behaviorTypes.Length);
                var randomType = _behaviorTypes[r];
                var m = MessageGenerator.Message();
                m.AddChangeBehavior(randomType, e.id.value);
                e.RemoveBehaviorDelay();
            }
        }
    }
}
