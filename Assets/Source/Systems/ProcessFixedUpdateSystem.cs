using Entitas;

public class ProcessFixedUpdateSystem : IExecuteSystem {

    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _buffers;
    readonly IGroup<MessageEntity> _bufferActions;
    readonly IGroup<MessageEntity> _buffer2DActions;

    public ProcessFixedUpdateSystem(Contexts contexts) {
        _gameContext = contexts.game;
        _buffers = contexts.game.GetGroup(GameMatcher.UpdateBuffer);
        _bufferActions = contexts.message.GetGroup(MessageMatcher.AllOf(
                                                            MessageMatcher.MessageTarget,
                                                            MessageMatcher.CanBeProcessed,
                                                            MessageMatcher.BufferAction
                                                        )
                                                    );
        _buffer2DActions = contexts.message.GetGroup(MessageMatcher.AllOf(
															MessageMatcher.MessageTarget,
															MessageMatcher.CanBeProcessed,
                                                            MessageMatcher.Buffer2DAction
                                                        )
                                                    );
    }

    public void Execute() {
        foreach (var e in _bufferActions.GetEntities()) {
            var buffer = _gameContext.GetEntityWithId(e.messageTarget.id).updateBuffer.buffer;
            buffer.AddToBuffer(e.bufferAction.issuer, e.bufferAction.act);
        }

        foreach (var e in _buffer2DActions.GetEntities()) {
            var buffer = _gameContext.GetEntityWithId(e.messageTarget.id).updateBuffer.buffer;
			buffer.AddToBuffer(e.buffer2DAction.issuer, e.buffer2DAction.act);
		}

		foreach (var e in _buffers.GetEntities()) {
            e.updateBuffer.buffer.CycleBuffers();
        }
    }
}
