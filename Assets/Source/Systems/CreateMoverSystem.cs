using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CreateMoverSystem : ReactiveSystem<InputEntity> {

    readonly GameContext _context;

    public CreateMoverSystem(Contexts contexts) : base(contexts.input) {
        _context = contexts.game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.AllOf(
                                                        InputMatcher.RightMouse,
                                                        InputMatcher.MouseDown
                                                    ));
    }

    protected override bool Filter(InputEntity entity) {
        return entity.hasMouseDown;
    }

    protected override void Execute(List<InputEntity> entities) {
        foreach (var e in entities) {
            var mover = _context.CreateEntity();
            mover.isMover = true;
            mover.AddPosition(e.mouseDown.position);
            mover.AddDirection(Random.Range(0, 360));
            mover.AddSprite("Planet");
        }
    }
}
