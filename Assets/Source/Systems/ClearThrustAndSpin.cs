using Entitas;

public class ClearThrustAndSpin : ICleanupSystem {

    readonly IGroup<GameEntity> _triggers;

    public ClearThrustAndSpin(Contexts contexts) {
        //_triggers = contexts.game.GetGroup(GameMatcher.AnyOf(GameMatcher.TriggerThrust, GameMatcher.TriggerSpin));
    }

    public void Cleanup() {
        //foreach (var e in _triggers.GetEntities()) {
            //if (e.hasTriggerSpin) e.RemoveTriggerSpin();
            //if (e.hasTriggerThrust) e.RemoveTriggerThrust();
        //}
    }
}
