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
    /// Interaction logic for CoinExchangePopUpTrading.xaml
    /// </summary>
    public partial class CoinExchangePopUpTrading : Window
    {
        public CoinExchangePopUpTrading()
        {
            InitializeComponent();
        }

        private void uiConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
