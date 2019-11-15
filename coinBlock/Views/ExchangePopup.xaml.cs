using coinBlock.Utility;
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
    /// Interaction logic for ExchangePopup.xaml
    /// </summary>
    public partial class ExchangePopup : Window
    {
        IniFileLib ini = new IniFileLib();

        public ExchangePopup()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chx1.IsChecked == false)
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_25);
                    alert.ShowDialog();
                    return;
                }
                else
                {
                    if (chx2.IsChecked == true)
                    {                        
                        ini.SetCheckID(DateTime.Now.ToString("yyyyMMdd"), "Popup", "Day");
                    }

                    this.DialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
