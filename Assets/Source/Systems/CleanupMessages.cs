using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class CleanupMessages : ICleanupSystem {

    readonly ICollector<MessageEntity> _Processable;

    List<MessageEntity> _buffer;

    public CleanupMessages(Contexts contexts) {
        _Processable = contexts.message.CreateCollector(MessageMatcher.CanBeProcessed);
        _buffer = new List<MessageEntity>();
    }

    public void Cleanup() {
        foreach (var e in _Processable.collectedEntities) {
            if (!e.isPersistUntilConsumed && !e.isJustIssued) {
                e.Retain(this);
                _buffer.Add(e);
            }
        }
        _Processable.ClearCollectedEntities();

        foreach (var e in _buffer) {
            e.Release(this);
            e.Destroy();
        }
        _buffer.Clear();
    }
}
