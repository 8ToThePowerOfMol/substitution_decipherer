using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace substitution_decipherer
{
    class LetterCustomControl : ListBoxItem
    {
        static LetterCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LetterCustomControl),
                new FrameworkPropertyMetadata(typeof(LetterCustomControl)));
        }

        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(string), typeof(LetterCustomControl));

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(string), typeof(LetterCustomControl));

        public string From
        {
            get => (string)GetValue(FromProperty);
            set { SetValue(FromProperty, value); }
        }

        public string To
        {
            get => (string)GetValue(ToProperty);
            set { SetValue(ToProperty, value); }
        }
    }
}
