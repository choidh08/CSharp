using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace coinBlock.Views
{
    /// <summary>
    /// Interaction logic for CoinDayPirceTrading.xaml
    /// </summary>
    public partial class CoinDayPirceTrading : Window
    {
        public CoinDayPirceTrading()
        {
            InitializeComponent();

            dpFrom.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
            dpTo.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
        }

        public void uiExit_Click(object sender, RoutedEventArgs e)
        {         
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount.Equals(1))
                {
                    base.OnMouseLeftButtonDown(e);
                    this.DragMove();
                }          
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
