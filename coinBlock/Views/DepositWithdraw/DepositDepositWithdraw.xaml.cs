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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace coinBlock.Views.DepositWithdraw
{
    /// <summary>
    /// Interaction logic for DepositDepositWithdraw.xaml
    /// </summary>
    public partial class DepositDepositWithdraw : UserControl
    {
        public DepositDepositWithdraw()
        {
            InitializeComponent();
        }

        //private  void BitWallet_Click(object sender, RoutedEventArgs e)
        //{
        //    //Alert alert = new Alert("Closed Beta Test기간이므로 \r실제 거래 가능 전자지갑 주소가 아닙니다.", Alert.ButtonType.Ok, 320);
        //    //if(alert.ShowDialog()==true)
        //    //{
        //    DoubleAnimation db = new DoubleAnimation();
        //    db.From = 1;
        //    db.To = 0;
        //    db.Duration = TimeSpan.FromSeconds(1);
        //    db.Completed += Db_Completed;
        //    Bit0.BeginAnimation(OpacityProperty, db);
        //    //}
        //}

        //private void Db_Completed(object sender, EventArgs e)
        //{
        //    Bit0.Visibility = Visibility.Collapsed;
        //    Bit1.Visibility = Visibility.Visible;
        //}

        //private void EthWallet_Click(object sender, RoutedEventArgs e)
        //{
        //    //Alert alert = new Alert(Localization.Resource.DepositDepositWithdraw_Eth_1_4, Alert.ButtonType.Ok, 420);
        //    //if (alert.ShowDialog() == true)
        //    //{
        //    DoubleAnimation db1 = new DoubleAnimation();
        //    db1.From = 1;
        //    db1.To = 0;
        //    db1.Duration = TimeSpan.FromSeconds(1);
        //    db1.Completed += Db1_Completed;
        //    Eth0.BeginAnimation(OpacityProperty, db1);
        //    //}
        //}

        //private void Db1_Completed(object sender, EventArgs e)
        //{
        //    Eth0.Visibility = Visibility.Collapsed;
        //    Eth1.Visibility = Visibility.Visible;
        //}

        //private void BchWallet_Click(object sender, RoutedEventArgs e)
        //{
        //    //Alert alert = new Alert(Localization.Resource.DepositDepositWithdraw_Bch_1_4, Alert.ButtonType.Ok, 420);
        //    //if (alert.ShowDialog() == true)
        //    //{
        //    DoubleAnimation db2 = new DoubleAnimation();
        //    db2.From = 1;
        //    db2.To = 0;
        //    db2.Duration = TimeSpan.FromSeconds(1);
        //    db2.Completed += Db2_Completed;
        //    Bch0.BeginAnimation(OpacityProperty, db2);
        //    //}
        //}

        //private void Db2_Completed(object sender, EventArgs e)
        //{
        //    Bch0.Visibility = Visibility.Collapsed;
        //    Bch1.Visibility = Visibility.Visible;
        //}

        //private void XrpWallet_Click(object sender, RoutedEventArgs e)
        //{
        //    //Alert alert = new Alert(Localization.Resource.DepositDepositWithdraw_Xrp_1_3, Alert.ButtonType.Ok, 420);
        //    //if (alert.ShowDialog() == true)
        //    //{
        //    DoubleAnimation db3 = new DoubleAnimation();
        //    db3.From = 1;
        //    db3.To = 0;
        //    db3.Duration = TimeSpan.FromSeconds(1);
        //    db3.Completed += Db3_Completed;
        //    Xrp0.BeginAnimation(OpacityProperty, db3);
        //    //}
        //}

        //private void Db3_Completed(object sender, EventArgs e)
        //{
        //    Xrp0.Visibility = Visibility.Collapsed;
        //    Xrp1.Visibility = Visibility.Visible;
        //}
    }
}
