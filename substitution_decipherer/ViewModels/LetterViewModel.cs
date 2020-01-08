using substitution_decipherer.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace substitution_decipherer
{
    public class LetterViewModel : ViewModelBase
    {
        public LetterViewModel(string Key, TabViewModel TabReference)
        {
            this.Key = Key;
            this._tabReference = TabReference;
        }

        public LetterViewModel(string Key, string Value, TabViewModel TabReference) : this(Key, TabReference)
        {
            _value = Value;
        }

        private TabViewModel _tabReference;
        
        public string Key { get; }

        private string _value = "";
        public string Value
        {
            get => _value;
            set
            {
                _tabReference.Memoize();
                _value = value;
                _tabReference.RefreshDecryptedText();
                OnPropertyChanged();
            }
        }

        public void SetValueWithoutNotify(string value)
        {
            _value = value;
        }

        public LetterViewModel DeepCopy()
        {
            return new LetterViewModel(Key, Value, _tabReference);
        }
    }
}
