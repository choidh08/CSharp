using coinBlock.Model;
using coinBlock.Model.MyPage;
using coinBlock.Utility;
using coinBlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace coinBlock.Views.MyPage.Popup
{
    /// <summary>
    /// PinNumberReset.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PinNumberReset : Window
    {
        Alert alert = null;// new Alert();        

        public PinNumberReset()
        {
            InitializeComponent();
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                using (RequestPublicKeyModel req2 = new RequestPublicKeyModel())
                {
                    using (ResponsePublicKeyModel res2 = WebApiLib.SyncCall<ResponsePublicKeyModel, RequestPublicKeyModel>(req2))
                    {
                        dict = EncodingLib.AesEncryptKey(res2.data.pubKeyModule, res2.data.pubKeyExponent);
                    }
                }

                using (RequestPinNumbrInitModel req = new RequestPinNumbrInitModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.userPwd = EncodingLib.AesEncrypt(userPwd.Text, dict["gid"]);
                    req.clientPe = dict["acekey"];

                    using (ResponsePinNumbrInitModel res = WebApiLib.SyncCall<ResponsePinNumbrInitModel, RequestPinNumbrInitModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd.Equals(""))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_19 + "\n" + Localization.Resource.MemberInfo_Pop_17, 330);
                                alert.ShowDialog();

                                this.DialogResult = true;
                                this.Close();
                            }
                            else if (res.data.failCd.Equals("998"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_18);
                                alert.ShowDialog();
                            }
                            else if (res.data.failCd.Equals("997"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_20);
                                alert.ShowDialog();
                            }
                            else if (res.data.failCd.Equals("996"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_16 + "\n" + Localization.Resource.MemberInfo_Pop_17, 330);
                                alert.ShowDialog();
                            }
                            else if (res.data.failCd.Equals("993"))
                            {
                                //비밀번호 미일치.
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_35 + "\n" + Localization.Resource.MemberInfo_Pop_36, 330);
                                alert.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }                
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
