using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace substitution_decipherer.Support
{
    interface IMemento<T>
    {
        /// <summary>
        /// Restores target to the state memorized by this memento.
        /// </summary>
        /// <returns>
        /// A memento of the state before restoring
        /// </returns>
        IMemento<T> Restore(T target);
    }
}
