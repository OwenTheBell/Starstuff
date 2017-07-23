using Entitas;
using UnityEngine;

public class Follow : IExecuteSystem {

    readonly IGroup<GameEntity> _followers;

    public Follow(Contexts contexts) {
        _followers = contexts.game.GetGroup(GameMatcher.Following);
    }

    public void Execute() {

    }
}
