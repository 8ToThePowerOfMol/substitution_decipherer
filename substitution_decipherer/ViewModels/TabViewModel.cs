using substitution_decipherer.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace substitution_decipherer
{
    public class TabViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModelReference;
        private UndoCatetaker<TabViewModel> _catetaker;
        public bool IsMemoizingLock { get; set; }

        public TabViewModel(MainViewModel mainViewModelReference)
        {
            _mainViewModelReference = mainViewModelReference;
            _catetaker = new UndoCatetaker<TabViewModel>(this);
            Title = String.Format("Cipher {0}", _mainViewModelReference.newTabCounter++);
            IsMemoizingLock = true;
        }

        public TabViewModel(MainViewModel mainViewModelReference,
                            string CipherText,
                            bool IsCharDelimiterChecked,
                            string CharDelimiter,
                            bool IsWordDelimiterChecked,
                            string WordDelimiter,
                            ObservableCollection<LetterViewModel> Substitutions,
                            string DecryptedText) : this(mainViewModelReference)
        {
            IsMemoizingLock = false;
            this.CipherText = CipherText;
            this.IsCharDelimiterChecked = IsCharDelimiterChecked;
            this.CharDelimiter = CharDelimiter;
            this.IsWordDelimiterChecked = IsWordDelimiterChecked;
            this.WordDelimiter = WordDelimiter;
            this.Substitutions = Substitutions;
            this.DecryptedText = DecryptedText;
            IsMemoizingLock = true;
        }

        public TabViewModel(MainViewModel mainViewModelReference,
                            string CipherText,
                            bool IsCharDelimiterChecked,
                            string CharDelimiter,
                            bool IsWordDelimiterChecked,
                            string WordDelimiter,
                            ObservableCollection<LetterViewModel> Substitutions,
                            string DecryptedText,
                            string parentTitle) : this(mainViewModelReference,
                                                       CipherText,
                                                       IsCharDelimiterChecked,
                                                       CharDelimiter,
                                                       IsWordDelimiterChecked,
                                                       WordDelimiter,
                                                       Substitutions,
                                                       DecryptedText)
        {
            Title = parentTitle;
        }

        public string Title { get; }

        private RelayCommand _removeTabCommand;
        public RelayCommand RemoveTabCommand
        {
            get => _removeTabCommand ?? (_removeTabCommand = new RelayCommand(RemoveTabItem, RemoveTabItemIsExecutable));
        }
        private void RemoveTabItem(object obj) => _mainViewModelReference.Tabs.Remove(this);
        private bool RemoveTabItemIsExecutable(object obj) => _mainViewModelReference.Tabs.Count() > 0;
        public List<TabViewModel> ChildrenBranches = new List<TabViewModel>();

        private RelayCommand _undoTabCommand;
        public RelayCommand UndoTabCommand
        {
            get => _undoTabCommand ?? (_undoTabCommand = new RelayCommand(Undo, UndoIsExecutable));
        }
        public void Undo(object obj) => _catetaker.Undo();
        public bool UndoIsExecutable(object obj) => _catetaker.CanUndo();

        public void Redo() => _catetaker.Redo();
        public bool RedoIsExecutable() => _catetaker.CanRedo();

        private string _cipherText = "";
        public string CipherText
        {
            get => _cipherText;
            set
            {
                Memoize();
                _cipherText = value;
                RefreshSubtitutionsSet();
                OnPropertyChanged();
            }
        }

        public TabViewModel DeepCopy()
        {
            var substitutionsCopy = new ObservableCollection<LetterViewModel>(
                _substitutions.Select(x => x.DeepCopy()).ToList());

            return new TabViewModel(_mainViewModelReference,
                CipherText,
                IsCharDelimiterChecked,
                CharDelimiter,
                IsWordDelimiterChecked,
                WordDelimiter,
                substitutionsCopy,
                DecryptedText);
        }

        public TabViewModel Duplicate(TabViewModel parent)
        {
            var substitutionsCopy = new ObservableCollection<LetterViewModel>(
                _substitutions.Select(x => x.DeepCopy()).ToList());

            // TODO: Fix when two branches create tab with the same name
            var parentTitle = parent.ChildrenBranches.Count > 0 ? parent.ChildrenBranches.Last().Title : parent.Title;
            var parentTitleWords = parentTitle.Split(' ');
            int parentNumber = Int32.Parse(parentTitleWords[1]);
            string childTitle;
            if (parentTitleWords.Length > 3)
            {
                int parentBranchNumber = Int32.Parse(parentTitleWords[3]);
                childTitle = String.Format("Cipher {0} Branch {1}", parentNumber, parentBranchNumber + 1);
            }
            else
                childTitle = String.Format("Cipher {0} Branch 1", parentNumber);

            var child = new TabViewModel(_mainViewModelReference,
                CipherText,
                IsCharDelimiterChecked,
                CharDelimiter,
                IsWordDelimiterChecked,
                WordDelimiter,
                substitutionsCopy,
                DecryptedText,
                childTitle);

            ChildrenBranches.Add(child);

            return child;
        }

        private bool _isCharDelimiterChecked = false;
        public bool IsCharDelimiterChecked
        {
            get => _isCharDelimiterChecked;
            set
            {
                Memoize();
                _isCharDelimiterChecked = value;
                RefreshSubtitutionsSet();
                OnPropertyChanged();
            }
        }

        private string _charDelimiter = "";
        public string CharDelimiter
        {
            get => _charDelimiter;
            set
            {
                Memoize();
                _charDelimiter = value;
                RefreshSubtitutionsSet();
                OnPropertyChanged();
            }
        }

        private bool _isWordDelimiterChecked = false;
        public bool IsWordDelimiterChecked
        {
            get => _isWordDelimiterChecked;
            set
            {
                Memoize();
                _isWordDelimiterChecked = value;
                RefreshSubtitutionsSet();
                OnPropertyChanged();
            }
        }

        private string _wordDelimiter = "";
        public string WordDelimiter
        {
            get => _wordDelimiter;
            set
            {
                Memoize();
                _wordDelimiter = value;
                RefreshSubtitutionsSet();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LetterViewModel> _substitutions = new ObservableCollection<LetterViewModel>() { };
        public ObservableCollection<LetterViewModel> Substitutions
        {
            get => _substitutions;
            set
            {
                _substitutions = value;
                OnPropertyChanged();
            }
        }

        private bool IsWordDelimApplied() => _isWordDelimiterChecked && !_wordDelimiter.Equals("");

        private bool IsCharDelimApplied() => _isCharDelimiterChecked && !_charDelimiter.Equals("");

        private ObservableCollection<LetterViewModel> CopyOldSubstitutions(
            ObservableCollection<LetterViewModel> oldSubstitutions,
            ObservableCollection<LetterViewModel> newSubstitutions)
        {
            foreach (var s in newSubstitutions)
            {
                var old = oldSubstitutions.FirstOrDefault(x => x.Key == s.Key);
                s.SetValueWithoutNotify(old == null ? "" : old.Value);
            }

            return newSubstitutions;
        }

        private void RefreshSubtitutionsSet()
        {
            if (IsCharDelimApplied() && IsWordDelimApplied())
                Substitutions = CopyOldSubstitutions(Substitutions, new ObservableCollection<LetterViewModel>(
                    _cipherText
                        .Split(new[] { " ", "\n", _charDelimiter, _wordDelimiter }, StringSplitOptions.RemoveEmptyEntries)
                        .Distinct()
                        .Select(x => new LetterViewModel(x, this))));
            else if(IsWordDelimApplied())
                Substitutions = CopyOldSubstitutions(Substitutions, new ObservableCollection<LetterViewModel>(
                    _cipherText
                        .Replace(_wordDelimiter, " ")
                        .ToCharArray()
                        .Distinct()
                        .Where(x => !char.IsWhiteSpace(x))
                        .Select(x => new LetterViewModel(x.ToString(), this))));
            else if(IsCharDelimApplied())
                Substitutions = CopyOldSubstitutions(Substitutions, new ObservableCollection<LetterViewModel>(
                    _cipherText
                        .Split(new[] {" ", "\n", _charDelimiter}, StringSplitOptions.RemoveEmptyEntries)
                        .Distinct()
                        .Select(x => new LetterViewModel(x, this))));
            else
                Substitutions = CopyOldSubstitutions(Substitutions, new ObservableCollection<LetterViewModel> (
                    _cipherText
                        .ToCharArray()
                        .Distinct()
                        .Where(x => !char.IsWhiteSpace(x))
                        .Select(x => new LetterViewModel(x.ToString(), this))));
        }

        private string _decryptedText = "";
        public string DecryptedText
        {
            get => _decryptedText;
            set
            {
                _decryptedText = value;
                OnPropertyChanged();
            }
        }

        private string GetSubstitutionValue(string key)
        {
            switch (key)
            {
                case " ":   return " ";
                case "\n":  return "\n";
                default:
                    var value = Substitutions
                                    .FirstOrDefault(x => x.Key.Equals(key))
                                    .Value;
                    return value.Equals("") ? " " : value;
            }
        }

        public void RefreshDecryptedText()
        {
            if(IsCharDelimApplied() && IsWordDelimApplied())
                DecryptedText = string.Join(" ",
                                    CipherText
                                        .Split(new[] { "\n", " ", _wordDelimiter }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(x => String.Join("",
                                            x.Split(new[] { _charDelimiter }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(y => GetSubstitutionValue(y)))));
            else if(IsCharDelimApplied())
                DecryptedText = string.Join(" ",
                                    CipherText
                                        .Split(new[] { '\n', ' ' })
                                        .Select(x => String.Join("",
                                            x.Split(new[] { _charDelimiter }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(y => GetSubstitutionValue(y)))));
            else if(IsWordDelimApplied())
                DecryptedText = string.Join("",
                                    CipherText
                                        .Replace(_wordDelimiter, " ")
                                        .ToCharArray()
                                        .Select(x => GetSubstitutionValue(x.ToString()))
                                        .ToArray());
            else
                DecryptedText = string.Join("",
                                    CipherText
                                        .ToCharArray()
                                        .Select(x => GetSubstitutionValue(x.ToString()))
                                        .ToArray());
        }

        public void Memoize()
        {
            if (IsMemoizingLock)
            {
                IsMemoizingLock = false;
                _catetaker.Memoize(new TabViewModelMemento(this.DeepCopy()));
                IsMemoizingLock = true;
            }
            
        }
    }
}
