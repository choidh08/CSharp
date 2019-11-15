using coinBlock.Model.Common;
using coinBlock.Utility;
using coinBlock.ViewModels;
using System;
using System.Net;
using System.Windows;

namespace coinBlock.Views.Common
{
    /// <summary>
    /// DanalMobile.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DanalMobile : Window
    {
        static System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
        Alert alert = null;

        //public string sMobileCertYn = string.Empty;

        string _userEmail = string.Empty;
        string _userMobile = string.Empty;
        string _userNm = string.Empty;
        string _userBrithDay = string.Empty;

        public DanalMobile(string userEmail, string userMobile, string userNm, string userBrithDay)
        {
            InitializeComponent();

            web.ScriptErrorsSuppressed = true;
            wfh.Child = web;
            web.DocumentCompleted += web_DocumentCompleted;
            web.Disposed += Web_Disposed;

            _userEmail = userEmail;
            _userMobile = userMobile;
            _userNm = userNm;
            _userBrithDay = userBrithDay;

            this.Loaded += DanalMobile_Loaded;
            this.Closed += DanalMobile_Closed;
        }

        private void DanalMobile_Closed(object sender, EventArgs e)
        {
            System.Windows.Forms.HtmlDocument docment = web.Document;
            //sMobileCertYn = docment.GetElementById("successYn").GetAttribute("value");
            //using (RequestUserCertModel req = new RequestUserCertModel())
            //{
            //    req.userEmail = MainViewModel.LoginDataModel.userEmail;
            //    req.smsCertYn = sMobileCertYn;

            //    using (ResponseUserCertModel res = WebApiLib.SyncCall<ResponseUserCertModel, RequestUserCertModel>(req))
            //    {
            //        if (res.resultStrCode == "000")
            //        {
            //            if (sMobileCertYn.Equals("Y"))
            //            {
            //                alert = new Alert(Localization.Resource.MemberInfo_Pop_21);
            //                alert.ShowDialog();
            //            }
            //            else
            //            {
            //                alert = new Alert(Localization.Resource.MemberInfo_Pop_22);
            //                alert.ShowDialog();
            //            }
            //        }
            //    }
            //}
        }

        private void DanalMobile_Loaded(object sender, RoutedEventArgs e)
        {
            UrlCall(_userEmail, _userMobile, _userNm, _userBrithDay);
        }

        private void Web_Disposed(object sender, EventArgs e)
        {            
            this.Close();
        }

        public void UrlCall(string userEmail, string userMobile, string userNm, string userBrithDay)
        {
            try
            {
                int height = web.Height;
                int width = web.Width;
                string Url = ConfigLib.WebUrl + "bt.front.mypage.danalUserHts.ds/proc.go?user_email=" + WebUtility.UrlEncode(userEmail) + "&user_mobile=" + WebUtility.UrlEncode(userMobile) + "&birth_day="+ WebUtility.UrlEncode(userBrithDay) + "&isUptYn=" + WebUtility.UrlEncode("Y") + "&htsYn=" + WebUtility.UrlEncode("Y"); //+ "&height=" + height + "&width=" + width; //"&user_nm=" + WebUtility.UrlEncode(userNm) 
                MessageBox.Show(Url);
                web.Navigate(Url);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            web.Document.Body.Style = "zoom:100%; overflow-x:hidden; overflow-y:hidden;";
        }
    }
}
