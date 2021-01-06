using LedgerConsolid.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LedgerConsolid.CustomControls
{
    /// <summary>
    /// Interaction logic for LedgerBookBox.xaml
    /// </summary>
    /// 
    public partial class LedgerBookBox : UserControl
    {
        public LedgerBook CurrentBook   
        {
            get { return (LedgerBook)GetValue(CurrentBookProperty); }
            set { SetValue(CurrentBookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentBookProperty =
            DependencyProperty.Register("CurrentBook", typeof(LedgerBook), typeof(LedgerBookBox), new PropertyMetadata(null));


        public LedgerBookBox()
        {
            InitializeComponent();
        }

        private void CanExecuteAddLedgerCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            Control target = e.Source as Control;
            if (target != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void ExecutedAddLedgerCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentBook == null)
            {
                CurrentBook = new LedgerBook("testbook");
            }
            Debug.Print(CurrentBook.ToString());
            CurrentBook.Add(LedgerItem.Create(new DateTime(1993, 3, 14), "test Ledger Entry Item", LedgerItemCreateMode.Credit, 3667.43));
            Guid guid = Guid.NewGuid();
            CurrentBook.Title = guid.ToString();
            Debug.Print(CurrentBook.ToString());
            Debug.Print("Current FontSize is:" + ucLedgerBookBox.FontSize);
            Debug.Print("Current Count is: " + CurrentBook.Count.ToString());
        }
    }
}
