using LedgerConsolid.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LedgerConsolid.ViewModels
{
    class MainWindowViewModels : INotifyPropertyChanged
    {
        public MainWindowViewModels()
        {
            SelectedLedgerBook = new LedgerBook("NewBook2");
        }

        private LedgerItem _selectedItem;

        public LedgerItem SelectedLedgerItem
        {
            get { return _selectedItem; }
            set 
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedLedgerItem");
            }
        }

        private LedgerBook _selectedBook;
        public LedgerBook SelectedLedgerBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged("SelectedLedgerBook");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
