using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace coinBlock.Views
{
    /// <summary>
    /// Interaction logic for BookMark.xaml
    /// </summary>
    public partial class BookMark : Window
    {
        static bool Isload = false;

        public BookMark()
        {
            InitializeComponent();
            this.Loaded += BookMark_Loaded;
            this.Closing += BookMark_Closing;
        }

        private void BookMark_Loaded(object sender, RoutedEventArgs e)
        {
            Isload = true;
        }

        private void BookMark_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Isload = false;
        }

        public void uiExit_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send("QuickMenuRefresh");
            this.Close();
        }
    }
}
