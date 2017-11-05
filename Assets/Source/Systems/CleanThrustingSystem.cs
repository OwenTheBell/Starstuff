using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class CleanThrustingSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _thrusting;

    public CleanThrustingSystem(Contexts contexts) {
        _thrusting = contexts.game.GetGroup(GameMatcher.Thrusting);
    }

    public void Execute() {
        foreach (var e in _thrusting.GetEntities()) {
            e.RemoveThrusting();
        }
    }
}
