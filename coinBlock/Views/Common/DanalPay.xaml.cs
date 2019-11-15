using coinBlock.Utility;
using coinBlock.ViewModels;
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
using System.Web;
using DevExpress.XtraPrinting.HtmlExport.Native;
using System.Net;

namespace coinBlock.Views
{
    /// <summary>
    /// Interaction logic for DanalPay.xaml
    /// </summary>
    public partial class DanalPay : Window
    {
        static System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();

        string _orderId = string.Empty;
        string _userPhone = string.Empty;
        string _payMtd = string.Empty;
        
        

        public DanalPay(string orderId, string userPhone, string payMtd)
        {
            InitializeComponent();

            web.ScriptErrorsSuppressed = true;
            wfh.Child = web;
            web.DocumentCompleted += web_DocumentCompleted;
            web.Disposed += Web_Disposed;
            _orderId = orderId;
            _userPhone = userPhone;
            _payMtd = payMtd;

            this.Loaded += DanalPay_Loaded;

      
        }

        private void DanalPay_Loaded(object sender, RoutedEventArgs e)
        {
            UrlCall(_orderId, _userPhone, _payMtd);
        }

        private void Web_Disposed(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UrlCall(string orderId, string userPhone, string payMtd)
        {
            try
            {
                int height = web.Height;
                int width = web.Width;
                string Url = ConfigLib.WebUrl + "bt.danal.htsDanal.dp/proc.go?param1=Y&userEmail=" + WebUtility.UrlEncode(MainViewModel.LoginDataModel.userEmail) + "&userNm=" + WebUtility.UrlEncode(MainViewModel.LoginDataModel.userNm) + "&user_mobile="+ WebUtility.UrlEncode(userPhone) + "&inMthCd="+ WebUtility.UrlEncode(payMtd) + "&orderId="+ WebUtility.UrlEncode(orderId) + "&height="+ height +"&width="+width;

                web.Navigate(Url);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            web.Document.Body.Style = "zoom:120%; overflow-x:hidden; overflow-y:hidden;";
        }
    }
}
