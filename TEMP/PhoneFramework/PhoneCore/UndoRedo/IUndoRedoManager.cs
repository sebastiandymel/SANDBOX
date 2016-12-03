using System;

namespace SEDY.PhoneCore.UndoRedo
{
    public interface IUndoRedoManager
    {
        void Add(IUndoRedoState state);
        bool CanRedo { get; }
        bool CanUndo { get; }
        void Undo();
        void Redo();
        event EventHandler UndoExecuted;
        event EventHandler RedoExecuted;
    }
}