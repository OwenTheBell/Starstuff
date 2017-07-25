using Entitas;

public class ProcessFixedUpdateSystem : ICleanupSystem {

    readonly IGroup<GameEntity> _buffers;

    public ProcessFixedUpdateSystem(Contexts contexts) {
        _buffers = contexts.game.GetGroup(GameMatcher.UpdateBuffer);
    }

    public void Cleanup() {
        foreach (var e in _buffers.GetEntities()) {
            e.updateBuffer.buffer.CycleBuffers();
        }
    }
}
