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
            DependencyProperty.Register("MyProperty", typeof(LedgerBook), typeof(LedgerBookBox), new PropertyMetadata(null));


        public LedgerBookBox()
        {
            InitializeComponent();
        }
    }
}
