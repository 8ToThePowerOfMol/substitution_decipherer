using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace substitution_decipherer.Support
{
    class UndoCatetaker<T>
    {
        private readonly Stack<IMemento<T>> undoStack = new Stack<IMemento<T>>();
        private readonly Stack<IMemento<T>> redoStack = new Stack<IMemento<T>>();
        private bool isInUndoRedo = false;
        private readonly T subject;

        public UndoCatetaker(T subject)
        {
            this.subject = subject;
        }

        public void Undo()
        {
            isInUndoRedo = true;
            IMemento<T> top = undoStack.Pop();
            redoStack.Push(top.Restore(subject));
            isInUndoRedo = false;
        }

        public void Redo()
        {
            isInUndoRedo = true;
            IMemento<T> top = redoStack.Pop();
            undoStack.Push(top.Restore(subject));
            isInUndoRedo = false;
        }

        public void Memoize(IMemento<T> m)
        {
            if (isInUndoRedo)
                throw new InvalidOperationException("Cannot memoize inside undo/redo operation :-(");
            redoStack.Clear();
            undoStack.Push(m);
        }

        public bool CanUndo() => undoStack.Count > 0;

        public bool CanRedo() => redoStack.Count > 0;
    }
}
