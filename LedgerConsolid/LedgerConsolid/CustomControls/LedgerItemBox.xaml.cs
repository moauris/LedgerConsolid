using LedgerConsolid.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LedgerItemBox.xaml
    /// </summary>
    public partial class LedgerItemBox : UserControl
    {


        public LedgerItem CurrentItem
        {
            get { return (LedgerItem)GetValue(CurrentItemProperty); }
            set { SetValue(CurrentItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentItemProperty =
            DependencyProperty.Register("CurrentItem", typeof(LedgerItem), typeof(LedgerItemBox), new PropertyMetadata(null));


        public LedgerItemBox()
        {
            InitializeComponent();
        }
    }
}
