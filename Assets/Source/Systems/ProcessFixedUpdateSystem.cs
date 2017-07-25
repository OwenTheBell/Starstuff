using Entitas;

public class ProcessFixedUpdateSystem : IExecuteSystem {

    readonly IGroup<GameEntity> _buffers;

    public ProcessFixedUpdateSystem(Contexts contexts) {
        _buffers = contexts.game.GetGroup(GameMatcher.UpdateBuffer);
    }

    public void Execute() {
        foreach (var e in _buffers.GetEntities()) {
            e.updateBuffer.buffer.CycleBuffers();
        }
    }
}
