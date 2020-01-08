using substitution_decipherer.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace substitution_decipherer
{
    class TabViewModelMemento : IMemento<TabViewModel>
    {
        public TabViewModel Memo { get; }

        public TabViewModelMemento(TabViewModel Memo)
        {
            this.Memo = Memo;
        }

        public IMemento<TabViewModel> Restore(TabViewModel target)
        {
            var previous = target.DeepCopy();

            target.IsMemoizingLock = false;
            target.CipherText = Memo.CipherText;
            target.IsCharDelimiterChecked = Memo.IsCharDelimiterChecked;
            target.CharDelimiter = Memo.CharDelimiter;
            target.IsWordDelimiterChecked = Memo.IsCharDelimiterChecked;
            target.WordDelimiter = Memo.WordDelimiter;
            target.Substitutions = Memo.Substitutions;
            target.DecryptedText = Memo.DecryptedText;
            target.IsMemoizingLock = true;

            return new TabViewModelMemento(previous);
        }
    }
}
