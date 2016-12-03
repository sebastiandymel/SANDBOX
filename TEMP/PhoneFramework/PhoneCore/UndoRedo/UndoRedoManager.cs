using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDY.PhoneCore.UndoRedo
{
    public class UndoRedoManager : IUndoRedoManager
    {
        private readonly Stack<IUndoRedoState> statesToUndo = new Stack<IUndoRedoState>();
        private readonly Stack<IUndoRedoState> statesToRedo = new Stack<IUndoRedoState>();

        public void Add(IUndoRedoState state)
        {
            this.statesToUndo.Push(state);
            this.statesToRedo.Clear();
        }

        public bool CanRedo
        {
            get { return this.statesToRedo != null && statesToRedo.Any(); }
        }

        public bool CanUndo
        {
            get { return this.statesToUndo != null && statesToUndo.Any(); }
        }

        public void Undo()
        {
            if (!CanUndo)
            {
                throw new InvalidOperationException("No undo actions");
            }

            var undo = this.statesToUndo.Pop();
            this.statesToRedo.Push(undo);
            undo.Execute();
            RaiseUndo();
        }

        public void Redo()
        {
            if (!CanRedo)
            {
                throw new InvalidOperationException("No redo actions");
            }
            var redo = statesToRedo.Pop();
            redo.Execute();
            RaiseRedo();
        }

        public event EventHandler UndoExecuted;

        protected virtual void RaiseUndo()
        {
            var handler = UndoExecuted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler RedoExecuted;

        protected virtual void RaiseRedo()
        {
            var handler = RedoExecuted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
