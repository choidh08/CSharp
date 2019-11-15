using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.ViewModels;
using System;
using System.Windows.Controls;

namespace coinBlock.Views.AdditionalService
{
    /// <summary>
    /// Interaction logic for AlarmAdditionalService.xaml
    /// </summary>
    public partial class AlarmAdditionalService : UserControl
    {
        public AlarmAdditionalService()
        {
            InitializeComponent();
            this.Loaded += AlarmAdditionalService_Loaded;            
        }

        private void AlarmAdditionalService_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            UrlCall();
        }

        async void UrlCall()
        {
            try
            {
                using (RequestGetTimeModel req = new RequestGetTimeModel())
                {
                    using (ResponseGetTimeModel res = await WebApiLib.AsyncCall<ResponseGetTimeModel, RequestGetTimeModel>(req))
                    {
                        ResponseGetTimeDataModel dataTime = res.data;

                        string userEmail = ViewModels.MainViewModel.LoginDataModel.userEmail;
                        long lSha256 = Utility.EncodingLib.HmacSha256Encrypt(dataTime.sysTime);
                        string lang = string.Empty;
                        if (LoginViewModel.LanguagePack.IndexOf("ko-KR") > 0) lang = "ko";
                        else if (LoginViewModel.LanguagePack.IndexOf("en-US") > 0) lang = "en";
                        else if (LoginViewModel.LanguagePack.IndexOf("ru-RU") > 0) lang = "kg";

                        string postData = "param1=Y&param2=" + userEmail + "&param3=" + lSha256 + "&param4=" + dataTime.sysTime + "&lang=" + lang;
                        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                        byte[] bytes = encoding.GetBytes(postData);
                        string Header = "Content-Type: application/x-www-form-urlencoded";

                        string Url = ConfigLib.WebUrl + "bt.additional.alarm.dp/proc.go";
                        //string Url = MainViewModel.DomainUrl + "bitkrx.additional.alarm.dp/proc.go?param1=Y&param2=" + userEmail + "&param3=" + lSha256 + "&param4=" + dataTime.sysTime;

                        web.Navigate(Url, string.Empty, bytes, Header);                       
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            web.Document.Body.Style = "zoom:100%; overflow-x:hidden; overflow-y:auto";
        }

        private void web_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F5)
            {
                e.IsInputKey = true;
            }
        }
    }
}
