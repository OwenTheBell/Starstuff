using Entitas;

public class ProcessFixedUpdateSystem : ICleanupSystem {

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _buffers;
    readonly IGroup<MessageEntity> _bufferActions;
    readonly IGroup<MessageEntity> _buffer2DActions;

    public ProcessFixedUpdateSystem(Contexts contexts) {
        _gameContext = contexts.game;
        _buffers = contexts.game.GetGroup(GameMatcher.UpdateBuffer);
        _bufferActions = contexts.message.GetGroup(MessageMatcher.AllOf(
                                                            MessageMatcher.MessageTarget,
                                                            MessageMatcher.BufferAction
                                                        )
                                                    );
        _buffer2DActions = contexts.message.GetGroup(MessageMatcher.AllOf(
                                                            MessageMatcher.MessageTarget,
                                                            MessageMatcher.Buffer2DAction
                                                        )
                                                    );
    }

    public void Cleanup() {
        foreach (var e in _bufferActions.GetEntities()) {
        }

        
        foreach (var e in _buffers.GetEntities()) {
            e.updateBuffer.buffer.CycleBuffers();
        }
    }
}
