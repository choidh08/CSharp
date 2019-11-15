using coinBlock.Model;
using coinBlock.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace coinBlock.Views.Wallet
{
    /// <summary>
    /// Interaction logic for WebSocketTest.xaml
    /// </summary>
    public partial class WebSocketTest : UserControl
    {
     
        public WebSocketTest()
        {
            InitializeComponent();
            //web1.Url = new Uri("https://system.xlogic.co.kr");            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.HtmlDocument docment = (System.Windows.Forms.HtmlDocument)web1.Document;
            //string bbb = docment.GetElementById("_id").GetAttribute("value");
        }
    }
}