using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace substitution_decipherer
{
    class TabItemCustomControl : TabItem
    {
        static TabItemCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TabItemCustomControl),
                new FrameworkPropertyMetadata(typeof(TabItemCustomControl)));
        }

        public static readonly DependencyProperty RemoveProperty =
            DependencyProperty.Register("Remove", typeof(ICommand), typeof(TabItemCustomControl));

        public static readonly DependencyProperty UndoProperty =
            DependencyProperty.Register("Undo", typeof(ICommand), typeof(TabItemCustomControl));

        public static readonly DependencyProperty RedoProperty =
            DependencyProperty.Register("Redo", typeof(ICommand), typeof(TabItemCustomControl));

        public static readonly DependencyProperty CipherProperty =
            DependencyProperty.Register("Cipher", typeof(string), typeof(TabItemCustomControl));

        public static readonly DependencyProperty IsCharDelimCheckedProperty =
            DependencyProperty.Register("IsCharDelimChecked", typeof(bool), typeof(TabItemCustomControl));

        public static readonly DependencyProperty CharDelimProperty =
            DependencyProperty.Register("CharDelim", typeof(string), typeof(TabItemCustomControl));

        public static readonly DependencyProperty IsWordDelimCheckedProperty =
            DependencyProperty.Register("IsWordDelimChecked", typeof(bool), typeof(TabItemCustomControl));

        public static readonly DependencyProperty WordDelimProperty =
            DependencyProperty.Register("WordDelim", typeof(string), typeof(TabItemCustomControl));

        public static readonly DependencyProperty LettersProperty =
            DependencyProperty.Register("Letters", typeof(ObservableCollection<LetterViewModel>), typeof(TabItemCustomControl));

        public static readonly DependencyProperty DecipheredProperty =
            DependencyProperty.Register("Deciphered", typeof(string), typeof(TabItemCustomControl));

        public ICommand Remove
        {
            get => (ICommand)GetValue(RemoveProperty);
            set { SetValue(RemoveProperty, value); }
        }

        public ICommand Undo
        {
            get => (ICommand)GetValue(UndoProperty);
            set { SetValue(UndoProperty, value); }
        }

        public ICommand Redo
        {
            get => (ICommand)GetValue(RedoProperty);
            set { SetValue(RedoProperty, value); }
        }

        public string Cipher
        {
            get => (string)GetValue(CipherProperty);
            set { SetValue(CipherProperty, value); }
        }

        public bool IsCharDelimChecked
        {
            get => (bool)GetValue(IsCharDelimCheckedProperty);
            set { SetValue(IsCharDelimCheckedProperty, value); }
        }

        public string CharDelim
        {
            get => (string)GetValue(CharDelimProperty);
            set { SetValue(CharDelimProperty, value); }
        }

        public bool IsWordDelimChecked
        {
            get => (bool)GetValue(IsCharDelimCheckedProperty);
            set { SetValue(IsCharDelimCheckedProperty, value); }
        }

        public string WordDelim
        {
            get => (string)GetValue(CharDelimProperty);
            set { SetValue(CharDelimProperty, value); }
        }

        public ObservableCollection<LetterViewModel> Letters
        {
            get => (ObservableCollection<LetterViewModel>)GetValue(LettersProperty);
            set { SetValue(LettersProperty, value); }
        }

        public string Deciphered
        {
            get => (string)GetValue(DecipheredProperty);
            set { SetValue(DecipheredProperty, value); }
        }
    }
}
