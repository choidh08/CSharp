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
    /// Interaction logic for IpRegister.xaml
    /// </summary>
    public partial class IpRegister : Window
    {
        public IpRegister()
        {
            InitializeComponent();       
        }

        public void uiExit_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }
    }
}
