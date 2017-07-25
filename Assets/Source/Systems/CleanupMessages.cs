using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class CleanupMessages : ICleanupSystem {

    //readonly ICollector<MessageEntity> _Processable;
    readonly IGroup<MessageEntity> _Processable;
    readonly IGroup<MessageEntity> _JustIssued;

    //List<MessageEntity> _buffer;

    public CleanupMessages(Contexts contexts) {
        _Processable = contexts.message.GetGroup(MessageMatcher.CanBeProcessed);
        _JustIssued = contexts.message.GetGroup(MessageMatcher.JustIssued);
        //_buffer = new List<MessageEntity>();
    }

    public void Cleanup() {
        foreach (var e in _Processable.GetEntities()) {
            if (!e.isPersistUntilConsumed) e.Destroy();
        }
        foreach (var e in _JustIssued.GetEntities()) {
            e.isJustIssued = false;
            e.isCanBeProcessed = true;
            e.isDestroyed = true; // the destroy system will clear this up
        }
        //foreach (var e in _Processable.GetEntities()) {
        //    if (!e.isPersistUntilConsumed && !e.isJustIssued) {
        //        e.Destroy();
        //        //e.Retain(this);
        //        //_buffer.Add(e);
        //    }
        //}

        //foreach (var e in _buffer) {
        //    e.Release(this);
        //    e.Destroy();
        //}
        //_buffer.Clear();
    }
}
