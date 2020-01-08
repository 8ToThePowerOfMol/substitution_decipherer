using substitution_decipherer.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

//using Xamarin.Forms;

namespace substitution_decipherer
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Tabs.Add(new TabViewModel(this));
        }

        public int newTabCounter = 0;

        public ObservableCollection<TabViewModel> Tabs { get; set; } = new ObservableCollection<TabViewModel>();

        private TabViewModel _selectedTab;
        public TabViewModel SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _addTabCommand;
        public RelayCommand AddTabCommand
        {
            get => _addTabCommand ?? (_addTabCommand = new RelayCommand(AddTabItem, AddTabItemIsExectutable));
        }
        private void AddTabItem(object obj) => Tabs.Add(new TabViewModel(this));
        private bool AddTabItemIsExectutable(object obj) => true;

        private RelayCommand _removeTabCommandFromMenu;
        public RelayCommand RemoveTabCommandFromMenu
        {
            get => _removeTabCommandFromMenu ?? (_removeTabCommandFromMenu = new RelayCommand(RemoveTabItem, RemoveTabItemIsExecutable));
        }
        private void RemoveTabItem(object obj) => Tabs.Remove(SelectedTab);
        private bool RemoveTabItemIsExecutable(object obj) => Tabs.Count() > 0 && SelectedTab != null;

        private RelayCommand _duplicateTabCommand;
        public RelayCommand DuplicateTabCommand
        {
            get => _duplicateTabCommand ?? (_duplicateTabCommand = new RelayCommand(DuplicateTabItem, DuplicateTabItemIsExecutable));
        }
        private void DuplicateTabItem(object obj) => Tabs.Add(SelectedTab.Duplicate(SelectedTab));
        private bool DuplicateTabItemIsExecutable(object obj) => Tabs.Count() > 0 && SelectedTab != null;

        private RelayCommand _undoCommand;
        public RelayCommand UndoCommand
        {
            get => _undoCommand ?? (_undoCommand = new RelayCommand(Undo, UndoIsExecutable));
        }
        private void Undo(object obj) => SelectedTab.Undo(null);
        private bool UndoIsExecutable(object obj) => Tabs.Count() > 0 && SelectedTab != null && SelectedTab.UndoIsExecutable(null);

        private RelayCommand _redoCommand;
        public RelayCommand RedoCommand
        {
            get => _redoCommand ?? (_redoCommand = new RelayCommand(Redo, RedoIsExecutable));
        }
        private void Redo(object obj) => SelectedTab.Redo();
        private bool RedoIsExecutable(object obj) => Tabs.Count() > 0 && SelectedTab != null && SelectedTab.RedoIsExecutable();

        private RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get => _closeCommand ?? (_closeCommand = new RelayCommand(param => this.OnClose()));
        }
        public void OnClose() => Application.Current.MainWindow.Close();
    }
}