using System.Collections.Generic;
using Entitas;

public abstract class BehaviorSystem : IExecuteSystem {

    readonly System.Type _myType;
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _myEntites;
    readonly IGroup<MessageEntity> _behaviorChanges;

    private List<GameEntity> _entitiesHolder = new List<GameEntity>();

    public BehaviorSystem(Contexts contexts, IMatcher<GameEntity> matcher, System.Type type) {
        _gameContext = contexts.game;
        _behaviorChanges = contexts.message.GetGroup(MessageMatcher.AllOf(
                                                        MessageMatcher.ChangeBehavior,
                                                        MessageMatcher.CanBeProcessed
                                                        )
                                                    );
        _myEntites = contexts.game.GetGroup(matcher);
        _myType = type;
    }

    public void Execute() {
        foreach (var m in _behaviorChanges.GetEntities()) {
            if (m.changeBehavior.behaviorType != _myType) continue;

            var e = _gameContext.GetEntityWithId(m.changeBehavior.id);
            if (e == null) continue;

            _entitiesHolder.Add(e);
        }
        if (_entitiesHolder.Count > 0) {
            AddNewEntities(_entitiesHolder);
        }

        _entitiesHolder.Clear();
        _entitiesHolder.AddRange(_myEntites.GetEntities());
        Execute(_entitiesHolder);
        _entitiesHolder.Clear();
    }

    protected abstract void AddNewEntities(List<GameEntity> entities);
    protected abstract void Execute(List<GameEntity> entities);
}
