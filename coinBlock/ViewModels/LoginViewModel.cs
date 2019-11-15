using coinBlock.Model;
using coinBlock.Utility;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;
using System.Windows;
using coinBlock.Views;
using System.Linq;
using coinBlock.Model.MyPage;
using System.Text;

namespace coinBlock.ViewModels
{
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class LoginViewModel
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        public virtual IMessageBoxService MessageService { get { return this.GetService<IMessageBoxService>(); } }
        protected virtual IWindowService DialogService { get { return null; } }

        public static string LanguagePack { get; set; }

        public virtual Visibility IsLoginView { get; set; }
        public virtual Visibility OTPVisible { get; set; }

        public virtual string Email { get; set; }
        public virtual string PassWord { get; set; }
        public virtual string LoginTitle { get; set; }
        public virtual string SaveID { get; set; }

        public virtual List<Lange> LangeList { get; set; }
        public virtual Lange SelectedLang { get; set; }
        public virtual bool IsBusy { get; set; }
        public virtual bool IsCheck { get; set; }

        [Required(ErrorMessage = "ID를 입력하세요")]
        public virtual string UserName { get; set; }
        [Required(ErrorMessage = "비밀번호를 입력하세요")]
        public virtual string Password { get; set; }
        [Required(ErrorMessage = "OTP인증번호를 입력하세요")]
        public virtual string OTP { get; set; }

        int LoginSubmitCount;
        bool LoginSubmitCheck = true;
        public virtual string TempUserName { get; set; }

        public virtual bool IsMaintenance { get; set; } = false;

        IniFileLib ini = new IniFileLib();

        public static LoginViewModel Create()
        {
            return ViewModelSource.Create(() => new LoginViewModel());
        }

        public LoginViewModel()
        {
            try
            {
                Messenger.Default.Register<string>(this, LogOutMessenger);

                LangeList = new List<Lange>();                
                LangeList.Add(new Lange() { Ln = "English", Lv = "en-US" });
                LangeList.Add(new Lange() { Ln = "한국어", Lv = "ko-KR" });
                //LangeList.Add(new Lange() { Ln = "日本語", Lv = "ja-JP" });
                //LangeList.Add(new Lange() { Ln = "简体中文", Lv = "zh-CN" }); //中國語
                //LangeList.Add(new Lange() { Ln = "Russian", Lv = "ru-RU" });
                //LangeList.Add(new Lange() { Ln = "Indonesian", Lv = "id-ID" });
                //LangeList.Add(new Lange() { Ln = "ภาษาไทย", Lv = "th-TH" });
                //LangeList.Add(new Lange() { Ln = "Vietnamese", Lv = "vi-VN" });

                OTP = string.Empty;
                OTPVisible = Visibility.Collapsed;                               

                #region ID 저장 확인

                UserName = ini.GetCheckID("Login", "ID");
                if (!UserName.Equals(string.Empty))
                {
                    IsCheck = true;
                }

                string Lv = ini.GetCheckID("Language", "Country");
                if (!string.IsNullOrWhiteSpace(Lv))
                {
                    Lange temp = LangeList.Where(x => x.Lv == Lv).FirstOrDefault();
                    if(temp != null)
                    {
                        SelectedLang = temp;
                    }                    
                }
                else
                {
                    SelectedLang = LangeList[0];
                }

                #endregion

                //Password = "q1w2e3r4t5!";
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void LogOutMessenger(string mType)
        {
            try
            {
                if (mType == "LogOut")
                {
                    IsLoginView = Visibility.Visible;
                    Password = "";
                }
                else if (mType == "Maintenance")
                {
                    IsMaintenance = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }       

        public void OnSelectedLangChanged()
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(SelectedLang.Lv);
                LanguagePack = "/Images/" + SelectedLang.Lv + "/";

                string AsmName = Assembly.GetExecutingAssembly().GetName().Name;
                Assembly Asm = Assembly.Load(AsmName);
                ResourceManager rm = new ResourceManager(AsmName + ".Localization.Resource", Asm);

                //국가별 폰트 변경
                var rDictionary = new ResourceDictionary();
                if (SelectedLang.Lv.Equals("ru-RU"))
                {                    
                    rDictionary.Source = new Uri(string.Format("/coinBlock;component/FontStyle/FontStyle.{0}.xaml", SelectedLang.Lv), UriKind.Relative);                    
                }
                else
                {                 
                    rDictionary.Source = new Uri(string.Format("/coinBlock;component/FontStyle/FontStyle.xaml", string.Empty), UriKind.Relative);                    
                }
                Application.Current.Resources.MergedDictionaries[0] = rDictionary;

                Email = LocalizationLib.GetLocalizaionString("Login_27");
                PassWord = LocalizationLib.GetLocalizaionString("Login_28");
                LoginTitle = LocalizationLib.GetLocalizaionString("Login_1");
                SaveID = LocalizationLib.GetLocalizaionString("Login_2");
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        //로그인 Command
        public async void Login()
        {
            try
            {
                if (IsMaintenance)
                {
                    return;
                }

                if (TempUserName != UserName)
                {
                    TempUserName = UserName;
                    LoginSubmitCount = 0;
                    LoginSubmitCheck = true;
                }

                if (LoginSubmitCount == 5 && LoginSubmitCheck)
                {
                    using (RequestLoginFailModel req = new RequestLoginFailModel())
                    {
                        req.userEmail = UserName;

                        using (ResponseLoginFailModel res = await WebApiLib.AsyncCall<ResponseLoginFailModel, RequestLoginFailModel>(req))
                        {
                            LoginSubmitCount = 0;
                            LoginSubmitCheck = false;
                            Alert alert = new Alert(Localization.Resource.Login_4, 400);
                            alert.ShowDialog();
                            return;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(UserName))
                {
                    Alert alert = new Alert(Localization.Resource.Login_5);
                    alert.ShowDialog();
                    return;
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    Alert alert = new Alert(Localization.Resource.Login_6);
                    alert.ShowDialog();
                    return;
                }           

                #region ID 저장 Check

                if (IsCheck)
                {
                    ini.SetCheckID(UserName.Trim(), "Login", "ID");
                }
                else
                {
                    ini.SetCheckID(string.Empty, "Login", "ID");
                }

                ini.SetCheckID(SelectedLang.Lv, "Language", "Country");

                #endregion

                IsBusy = true;

                Dictionary<string, string> dict = null;

                using (RequestPublicKeyModel req = new RequestPublicKeyModel())
                {
                    using (ResponsePublicKeyModel res = WebApiLib.SyncCall<ResponsePublicKeyModel, RequestPublicKeyModel>(req))
                    {                  
                        //dict = EncodingLib.AesEncrypt(Password.Trim(), res.data.pubKeyModule, res.data.pubKeyExponent);
                        dict = EncodingLib.AesEncryptKey(res.data.pubKeyModule, res.data.pubKeyExponent);
                    }
                }

                if (dict.Count == 2)
                {
                    using (RequestLoginModel req = new RequestLoginModel())
                    {
                        req.userEmail = UserName.Trim();
                        req.userPwd = EncodingLib.AesEncrypt(Password.Trim(), dict["gid"]);
                        req.clientPe = dict["acekey"];
                        req.langCd = SelectedLang.Lv.ToString().Split('-')[0];
                        req.regIp = CommonLib.Client_IP;
                        
                        using (ResponseLoginModel res = await WebApiLib.AsyncCall<ResponseLoginModel, RequestLoginModel>(req))
                        {
                            if (res.data.loginYn.Equals("Y"))
                            {
                                if (res.data.isIpFirst.Equals("Y"))
                                {
                                    Views.NoticePopup note = new Views.NoticePopup(NoticePopup.KindNotice.HTS_IP_CHECK_1);
                                    note.Title = Localization.Resource.NoticePopup_3;
                                    if (note.ShowDialog() == true)
                                    {
                                        note = new Views.NoticePopup(NoticePopup.KindNotice.HTS_IP_CHECK_2);
                                        note.Title = Localization.Resource.NoticePopup_3;
                                        note.ShowDialog();

                                        IsLoginView = Visibility.Collapsed;
                                        Messenger.Default.Send(res.data);
                                    }
                                    else
                                    {
                                        //IP삭제
                                        using (RequestIpRegModel req2 = new RequestIpRegModel())
                                        {
                                            req2.userEmail = UserName.Trim();
                                            req2.ip = res.data.regIp;
                                            req2.limtHr = 0;
                                            using (ResponseIpRegModel res2 = await WebApiLib.AsyncCall<ResponseIpRegModel, RequestIpRegModel>(req2))
                                            {
                                                //IP삭제 완료
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    IsLoginView = Visibility.Collapsed;
                                    Messenger.Default.Send(res.data);
                                }                               
                            }
                            else
                            {
                                IsBusy = false;
                                if (res.resultMsg != null)
                                {                
                                    if (res.data.lockYn == "Y")
                                    {
                                        Alert alert = new Alert("[" + Localization.Resource.Login_20 + "]" + "\n" + Localization.Resource.Login_18 + "\n" + Localization.Resource.Login_19, 460, 180);                                
                                        if (alert.ShowDialog() == true)
                                        {
                                            using (RequestPwdChangeModel req2 = new RequestPwdChangeModel())
                                            {
                                                req2.userEmail = UserName.Trim();

                                                using (ResponsePwdChagneModel res2 = WebApiLib.SyncCall<ResponsePwdChagneModel, RequestPwdChangeModel>(req2))
                                                {
                                                    if (res2.resultStrCode == "000")
                                                    {
                                                        alert = new Alert(Localization.Resource.Login_23, 330);
                                                        alert.ShowDialog();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.notExistPassword)))
                                        {
                                            LoginSubmitCount++;
                                            if (LoginSubmitCount == 5) LoginSubmitCheck = true;
                                            Alert alert = new Alert(string.Format(Localization.Resource.Login_24, LoginSubmitCount) + "\n" + Localization.Resource.Login_25, 300);
                                            alert.ShowDialog();
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.blockID)))
                                        {
                                            LoginSubmitCount = 0;
                                            LoginSubmitCheck = false;

                                            Alert alert = new Alert(Localization.Resource.Login_4, 400);
                                            alert.ShowDialog();
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.userEmailFail)))
                                        {
                                            Alert alert = new Alert(Localization.Resource.Login_11, 300);
                                            alert.ShowDialog();
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.userPasswordFail)))
                                        {
                                            Alert alert = new Alert(Localization.Resource.Login_12, 300);
                                            alert.ShowDialog();
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.notExistEmail)))
                                        {
                                            Alert alert = new Alert(Localization.Resource.Login_13, 300);
                                            alert.ShowDialog();
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.NotRegIP)))
                                        {
                                            NoticePopup note = new NoticePopup(NoticePopup.KindNotice.HTS_IP_NOT_CHECK_1);
                                            note.Title = Localization.Resource.NoticePopup_4;
                                            NoticePopupViewModel.UserName = UserName.Trim();
                                            if (note.ShowDialog() == true)
                                            {
                                                IpRegisterViewModel.regIp = res.data.regIp;
                                                IpRegisterViewModel.userEmail = UserName.Trim();
                                                IpRegister ip = new IpRegister();
                                                ip.ShowDialog();
                                            }
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.NotUseID))) 
                                        {
                                            Alert alert = new Alert(Localization.Resource.Login_16, 300);
                                            alert.ShowDialog();
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.loginFailCode)))
                                        {
                                            Alert alert = new Alert(Localization.Resource.Login_17, 350);
                                            alert.ShowDialog();
                                        }
                                        else if (res.data.failCd.Equals(StringEnum.GetStringValue(EnumLib.LoginFailCode.ReSettingPW)))
                                        {
                                            Alert alert = new Alert(Localization.Resource.Login_21, 300, 140, Localization.Resource.Login_22);
                                            if (alert.ShowDialog() == true)
                                            {
                                                using (RequestPwdChangeModel req2 = new RequestPwdChangeModel())
                                                {
                                                    req2.userEmail = UserName.Trim();

                                                    using (ResponsePwdChagneModel res2 = WebApiLib.SyncCall<ResponsePwdChagneModel, RequestPwdChangeModel>(req2))
                                                    {
                                                        if (res2.resultStrCode == "000")
                                                        {
                                                            alert = new Alert(Localization.Resource.Login_23, 330);
                                                            alert.ShowDialog();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Alert alert = new Alert(Localization.Resource.Login_17, 350);
                                            alert.ShowDialog();
                                        }   
                                    }
                                }
                                else
                                {
                                    Alert alert = new Alert(Localization.Resource.Login_7, 320);
                                    alert.ShowDialog();
                                }
                            }
                        }
                    }
                }
                else
                {
                    Alert alert = new Alert(Localization.Resource.Login_8, Alert.ButtonType.Ok);
                    alert.ShowDialog();
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                IsBusy = false;

                Alert alert = new Alert(Localization.Resource.Login_9, Alert.ButtonType.Ok);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public class Lange
        {
            public string Ln { get; set; }
            public string Lv { get; set; }
        }
    }
}