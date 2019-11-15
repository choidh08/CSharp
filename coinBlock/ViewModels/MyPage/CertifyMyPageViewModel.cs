using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;
using coinBlock.Views;
using coinBlock.Model;
using coinBlock.Utility;
using System.Windows.Threading;
using coinBlock.Model.MyPage;
using coinBlock.Model.Common;
using System.Windows.Forms;
using System.IO;

namespace coinBlock.ViewModels.MyPage
{
    [POCOViewModel]
    public class CertifyMyPageViewModel
    {
        #region Valuable

        DateTime dt;
        Alert alert = null;//new Alert();
        DispatcherTimer RepeatTimer;
        string sLanguage = LoginViewModel.LanguagePack;

        public virtual string Message { get; set; }
        public virtual bool IsBusy { get; set; }        

        bool IsLoad { get; set; }
        public string otpYn;
        public virtual Visibility useYn { get; set; }

        #region SMS         
        public virtual Visibility SmsRequest { get; set; } = Visibility.Visible;
        public virtual Visibility SmsConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility SmsComplete { get; set; } = Visibility.Collapsed;

        public virtual bool SmsOverTime { get; set; } = false;
        public virtual bool SmsAuthCodeEnabled { get; set; } = false;        
        public virtual string SmsAuthCode { get; set; } = string.Empty;
        public virtual string SmsTime { get; set; } = "10 : 00";
        #endregion

        #region ImageButton
        public virtual string btn_cert_mobile { get; set; }
        public virtual string btn_cert_mobile_on { get; set; }
        public virtual string btn_cert_account { get; set; }
        public virtual string btn_cert_account_on { get; set; }
        public virtual string btn_cert_request { get; set; }
        public virtual string btn_cert_request_on { get; set; }
        public virtual string btn_cert_cancel { get; set; }
        public virtual string btn_cert_cancel_on { get; set; }
        public virtual string btn_cert_confirm { get; set; }
        public virtual string btn_cert_confirm_on { get; set; }
        public virtual string btn_cert_resend { get; set; }
        public virtual string btn_cert_resend_on { get; set; }
        public virtual string btn_regist_otp { get; set; }
        public virtual string btn_regist_otp_on { get; set; }
        public virtual string btn_certnum_send { get; set; }
        public virtual string btn_certnum_send_on { get; set; }
        public virtual string btn_cert_change { get; set; }
        public virtual string btn_cert_change_on { get; set; }
        public virtual string opt_img01 { get; set; }
        public virtual string opt_img02 { get; set; }
        public virtual string opt_img03 { get; set; }
        public virtual string opt_img04 { get; set; }
        public virtual string certified_img01 { get; set; }
        public virtual string certified_img02 { get; set; }
        public virtual string certified_img03 { get; set; }

        public virtual string btn_cert_confirm_color { get; set; }
        public virtual string btn_cert_confirm_color_on { get; set; }
        public virtual string btn_cert_request_color { get; set; }
        public virtual string btn_cert_request_color_on { get; set; }
        public virtual string btn_cert_resend_color { get; set; }
        public virtual string btn_cert_resend_color_on { get; set; }

        public virtual string btn_file_upload { get; set; }
        public virtual string btn_file_upload_on { get; set; }
        public virtual string btn_kyc_cert_status { get; set; }
        public virtual string btn_kyc_cert_status_on { get; set; }
        public virtual string btn_manager_checking { get; set; }
        public virtual string btn_kyc_cert_complete { get; set; }

        #endregion

        public virtual string userEmail { get; set; } = MainViewModel.LoginDataModel.userEmail;

        #region Step 2

        public virtual bool bQrCode { get; set; } = true;
        public virtual bool bDirectWrite { get; set; }
        public virtual string otpStatus { get; set; }
        public virtual string otpNumber { get; set; }
        public virtual string emailNumber { get; set; }
        public virtual string encodeKey { get; set; }
        public virtual string qrCodeUrl { get; set; }
        public virtual Visibility qrCodeVisible { get; set; } = Visibility.Visible;
        public virtual Visibility encodeKeyVisible { get; set; } = Visibility.Collapsed;

        public virtual Visibility certifyStep2Visible { get; set; } = Visibility.Visible;
        public virtual Visibility changeStep2Visible { get; set; } = Visibility.Collapsed;
        public virtual Visibility emailCertVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emailGoCertVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emailReCertVisible { get; set; } = Visibility.Collapsed;

        #endregion

        #region Step 3

        public virtual string sFileName1 { get; set; }
        public virtual string sFileName2 { get; set; }

        public virtual string kycStatus { get; set; }
        public virtual bool AuthEnabled { get; set; } = true;
        public virtual Visibility AuthVisible { get; set; } = Visibility.Visible;

        #endregion

        #endregion

        protected CertifyMyPageViewModel()
        {
            try
            {
                dt = new DateTime();
                dt = dt.AddMinutes(10);
                //타이머
                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 0, 1);

                Messenger.Default.Register<string>(this, kycUpdate);

                ImageSet();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static CertifyMyPageViewModel Create()
        {
            return ViewModelSource.Create(() => new CertifyMyPageViewModel());
        }

        public void Loaded()
        {
            try
            {
                IsLoad = true;

                getAuthStatus();
                //Step2
                CommonLib.GetOtpYn(ref otpYn);

                if (otpYn.Equals("N"))
                {
                    useYn = Visibility.Visible;

                    certifyStep2Visible = Visibility.Collapsed;
                    changeStep2Visible = Visibility.Collapsed;
                    emailCertVisible = Visibility.Collapsed;
                }
                else
                {
                    useYn = Visibility.Collapsed;

                    if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                    {
                        GetGoogleOTP();
                    }
                    else
                    {
                        certifyStep2Visible = Visibility.Collapsed;
                        emailCertVisible = Visibility.Collapsed;
                        changeStep2Visible = Visibility.Visible;
                    }
                }
                 
                getAuthStatus();
                if (MainViewModel.LoginDataModel.kycStatus.Equals("1"))
                {
                    btn_kyc_cert_status = sLanguage + "btn_kyc_cert_complete.png";
                    AuthEnabled = false;
                    AuthVisible = Visibility.Collapsed;
                }
                else if (MainViewModel.LoginDataModel.kycStatus.Equals("2") || MainViewModel.LoginDataModel.kycStatus.Equals(""))
                {
                    AuthEnabled = true;
                    AuthVisible = Visibility.Visible;
                    btn_kyc_cert_status = sLanguage + "btn_kyc_cert_request.png";
                }
                else if(MainViewModel.LoginDataModel.kycStatus.Equals("3"))
                {
                    AuthEnabled = false;
                    AuthVisible = Visibility.Collapsed;
                    btn_kyc_cert_status = sLanguage + "btn_manager_checking.png";
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void UnLoaded()
        {
            try
            {
                IsLoad = false;

                sFileName1 = string.Empty;
                sFileName2 = string.Empty;
                otpNumber = string.Empty;
                encodeKey = string.Empty;
                qrCodeUrl = null;
                bQrCode = true;
                bDirectWrite = false;
                qrCodeVisible = Visibility.Visible;
                encodeKeyVisible = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #region Method Step2

        public async void GetGoogleOTP()
        {
            try
            {
                using (RequestGetGoogleOtpModel req = new RequestGetGoogleOtpModel())
                {
                    using (ResponseGetGoogleOtpModel res = await WebApiLib.AsyncCall<ResponseGetGoogleOtpModel, RequestGetGoogleOtpModel>(req))
                    {
                        encodeKey = res.data.encodedKey;
                        qrCodeUrl = res.data.qrCodeUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void CmdSetGoogleOTP()
        {
            try
            {
                if (emailCertVisible == Visibility.Visible)
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_4, 320);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(otpNumber.Trim()))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_5);
                    alert.ShowDialog();
                    return;
                }

                IsBusy = true;

                using (RequestSetGoogleOtpModel req = new RequestSetGoogleOtpModel())
                {
                    req.userEmail = userEmail;
                    req.authCode = otpNumber;
                    req.encodedKey = encodeKey;
                    req.isUpt = "Y";

                    using (ResponseSetGoogleOtpModel res = await WebApiLib.AsyncCall<ResponseSetGoogleOtpModel, RequestSetGoogleOtpModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string sResultCd = res.data.failCd;

                            if (sResultCd.Equals(""))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_2);
                                alert.ShowDialog();

                                certifyStep2Visible = Visibility.Collapsed;
                                emailCertVisible = Visibility.Collapsed;
                                changeStep2Visible = Visibility.Visible;
                                qrCodeUrl = null;
                                otpNumber = string.Empty;

                                using (RequestUserCertModel req2 = new RequestUserCertModel())
                                {
                                    req2.userEmail = userEmail;
                                    req2.otpSerial = encodeKey;
                                    req2.otpCertYn = "N";

                                    using (ResponseUserCertModel res2 = await WebApiLib.AsyncCall<ResponseUserCertModel, RequestUserCertModel>(req2))
                                    {
                                        MainViewModel.LoginDataModel.otpNo = encodeKey;
                                        Messenger.Default.Send("CoinSetting");
                                    }
                                }
                            }
                            else
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_6);
                                alert.ShowDialog();
                            }
                        }
                       else
                        {
                            alert = new Alert(Localization.Resource.CertifyMyPage_Alert_9);
                            alert.ShowDialog();
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void CmdEditGoogleOtp()
        {
            try
            {
                getAuthStatus();

                alert = new Alert(Localization.Resource.CertifyMyPage_Alert_12, 320);
                alert.ShowDialog();

                if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                {
                    GetGoogleOTP();
                }
                else
                {
                    certifyStep2Visible = Visibility.Collapsed;
                    emailCertVisible = Visibility.Collapsed;
                    changeStep2Visible = Visibility.Visible;
                }
                return;

                IsBusy = true;

              
                if (!MainViewModel.LoginDataModel.kycStatus.Equals("1"))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_11);
                    alert.ShowDialog();
                    return;
                }
                else if (!otpStatus.Equals("Y"))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_12, 320);
                    alert.ShowDialog();
                    return;
                }
                else
                {
                    GetGoogleOTP();
                    otpNumber = string.Empty;
                    certifyStep2Visible = Visibility.Visible;
                    changeStep2Visible = Visibility.Collapsed;
                    emailCertVisible = Visibility.Visible;
                    emailGoCertVisible = Visibility.Visible;
                    emailReCertVisible = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void CmdSendEmail()
        {
            try
            {
                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_14, 320);
                            alert.ShowDialog();

                            IsBusy = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }


        }

        public async void CmdEditEmail()
        {
            try
            {
                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_19, 320);
                            alert.ShowDialog();

                            emailGoCertVisible = Visibility.Collapsed;
                            emailReCertVisible = Visibility.Visible;

                            IsBusy = false;
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void CmdCertEmail()
        {
            try
            {
                using (RequestSmsCodeVertifyModel req = new RequestSmsCodeVertifyModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.authCode = emailNumber;

                    using (ResponseSmsCodeVertifyModel res = await WebApiLib.AsyncCall<ResponseSmsCodeVertifyModel, RequestSmsCodeVertifyModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_13);
                            alert.ShowDialog();

                            emailCertVisible = Visibility.Collapsed;
                            emailGoCertVisible = Visibility.Visible;
                            emailReCertVisible = Visibility.Collapsed;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdChangeOtpCertify()
        {
            try
            {
                if (bQrCode)
                {
                    qrCodeVisible = Visibility.Visible;
                    encodeKeyVisible = Visibility.Collapsed;
                }
                else if (bDirectWrite)
                {
                    qrCodeVisible = Visibility.Collapsed;
                    encodeKeyVisible = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Method Step3        

        public void kycUpdate(string Message)
        {
            try
            {
                if (IsLoad)
                {
                    if (Message.Equals("kycStatusUpdate"))
                    {
                        getAuthStatus();
                        if (MainViewModel.LoginDataModel.kycStatus.Equals("1"))
                        {
                            btn_kyc_cert_status = sLanguage + "btn_kyc_cert_complete.png";
                            AuthEnabled = false;
                            AuthVisible = Visibility.Collapsed;
                        }
                        else if (MainViewModel.LoginDataModel.kycStatus.Equals("2") || MainViewModel.LoginDataModel.kycStatus.Equals(""))
                        {
                            AuthEnabled = true;
                            AuthVisible = Visibility.Visible;
                            btn_kyc_cert_status = sLanguage + "btn_kyc_cert_request.png";
                        }
                        else if (MainViewModel.LoginDataModel.kycStatus.Equals("3"))
                        {
                            AuthEnabled = false;
                            AuthVisible = Visibility.Collapsed;
                            btn_kyc_cert_status = sLanguage + "btn_manager_checking.png";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void getAuthStatus()
        {
            try
            {
                using (RequestCertifyInfoModel req = new RequestCertifyInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseCertifyInfoModel res = WebApiLib.SyncCall<ResponseCertifyInfoModel, RequestCertifyInfoModel>(req))
                    {
                        MainViewModel.LoginDataModel.kycStatus = res.data.kycCertYn;
                        MainViewModel.LoginDataModel.otpNo = res.data.otpNo;
                        otpStatus = res.data.otpCertYn;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdFileUpload(string sNum)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.RestoreDirectory = true;
                file.Multiselect = false;
                file.Filter = "Images Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";

                if (file.ShowDialog() == DialogResult.OK)
                {
                    long size = new FileInfo(file.FileName).Length;
                    long mb = size / 1024 / 1024;

                    if (mb >= 2)
                    {
                        Alert alert = new Alert(Localization.Resource.CertifyMyPage_6_3, 320);
                        alert.ShowDialog();
                        return;
                    }

                    if (sNum.Equals("1"))
                    {
                        sFileName1 = file.FileName;
                    }
                    else if (sNum.Equals("2"))
                    {
                        sFileName2 = file.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdKYCAuth()
        {
            try
            {
                if (otpYn.Equals("Y"))
                {
                    if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                    {
                        alert = new Alert(Localization.Resource.CertifyMyPage_Alert_8);
                        alert.ShowDialog();
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(sFileName1) || string.IsNullOrWhiteSpace(sFileName2))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_6_1);
                    alert.ShowDialog();
                    return;
                }
                else
                {
                    IsBusy = true;

                    using (RequestKYCAuthModel req = new RequestKYCAuthModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.fileSn = "1";

                        using (ResponseKYCAuthModel res = WebApiLib.UpLoad<ResponseKYCAuthModel, RequestKYCAuthModel>(req, sFileName1))
                        {
                            if (res.resultStrCode == "000")
                            {
                                using (RequestKYCAuthModel req2 = new RequestKYCAuthModel())
                                {
                                    if (res.resultStrCode == "000")
                                    {
                                        req2.userEmail = MainViewModel.LoginDataModel.userEmail;
                                        req2.fileSn = "2";

                                        using (ResponseKYCAuthModel res2 = WebApiLib.UpLoad<ResponseKYCAuthModel, RequestKYCAuthModel>(req2, sFileName2))
                                        {
                                            if (res2.resultStrCode == "000")
                                            {
                                                try
                                                {
                                                    using (RequestUserCertModel req3 = new RequestUserCertModel())
                                                    {
                                                        req3.userEmail = MainViewModel.LoginDataModel.userEmail;
                                                        req3.kycCertYn = "3";

                                                        using (ResponseUserCertModel res3 = WebApiLib.SyncCall<ResponseUserCertModel, RequestUserCertModel>(req3))
                                                        {
                                                            if (res3.resultStrCode == "000")
                                                            {
                                                                alert = new Alert(Localization.Resource.CertifyMyPage_6_2);
                                                                alert.ShowDialog();

                                                                AuthEnabled = false;
                                                                AuthVisible = Visibility.Collapsed;
                                                                btn_kyc_cert_status = sLanguage + "btn_manager_checking.png";
                                                                MainViewModel.LoginDataModel.kycStatus = "3";

                                                                IsBusy = false;
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);                                                    
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                alert = new Alert(Localization.Resource.CertifyMyPage_Alert_7, 300);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                dt = dt.AddSeconds(-1);
                SmsTime = dt.ToString("mm : ss");
                if (dt.Minute.Equals(0) && dt.Second.Equals(0))
                {
                    RepeatTimer.Stop();
                    SmsOverTime = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }        

        public void CmdPageMove()
        {
            try
            {
                Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_7"), Id = "CertificationDataSubmitHelpDesk", IconPath = "/Images/ico_nav_cert_doc.png" });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            btn_cert_mobile = sLanguage + "btn_cert_mobile.png";
            btn_cert_mobile_on = sLanguage + "btn_cert_mobile_on.png";
            btn_cert_account = sLanguage + "btn_cert_account.png";
            btn_cert_account_on = sLanguage + "btn_cert_account_on.png";
            btn_cert_request = sLanguage + "btn_cert_request.png";
            btn_cert_request_on = sLanguage + "btn_cert_request_on.png";
            btn_cert_cancel = sLanguage + "btn_cert_cancel.png";
            btn_cert_cancel_on = sLanguage + "btn_cert_cancel_on.png";
            btn_cert_confirm = sLanguage + "btn_cert_confirm.png";
            btn_cert_confirm_on = sLanguage + "btn_cert_confirm_on.png";
            btn_cert_resend = sLanguage + "btn_cert_resend.png";
            btn_cert_resend_on = sLanguage + "btn_cert_resend_on.png";
            btn_regist_otp = sLanguage + "btn_regist_otp.png";
            btn_regist_otp_on = sLanguage + "btn_regist_otp_on.png";
            btn_certnum_send = sLanguage + "btn_certnum_send.png";
            btn_certnum_send_on = sLanguage + "btn_certnum_send_on.png";
            btn_cert_change = sLanguage + "btn_cert_change.png";
            btn_cert_change_on = sLanguage + "btn_cert_change_on.png";
            opt_img01 = sLanguage + "opt_img01.jpg";
            opt_img02 = sLanguage + "opt_img02.jpg";
            opt_img03 = sLanguage + "opt_img03.jpg";
            opt_img04 = sLanguage + "opt_img04.jpg";
            certified_img01 = sLanguage + "certified_img01.png";
            certified_img02 = sLanguage + "certified_img02.png";
            certified_img03 = sLanguage + "certified_img03.png";

            btn_cert_confirm_color = sLanguage + "btn_cert_confirm_color.png";
            btn_cert_confirm_color_on = sLanguage + "btn_cert_confirm_color_on.png";
            btn_cert_request_color = sLanguage + "btn_cert_request_color.png";
            btn_cert_request_color_on = sLanguage + "btn_cert_request_color_on.png";
            btn_cert_resend_color = sLanguage + "btn_cert_resend_color.png";
            btn_cert_resend_color_on = sLanguage + "btn_cert_resend_color_on.png";

            btn_file_upload = sLanguage + "btn_file_upload.png";
            btn_file_upload_on = sLanguage + "btn_file_upload_on.png";
            btn_kyc_cert_status = sLanguage + "btn_kyc_cert_request.png";
            btn_kyc_cert_status_on = sLanguage + "btn_kyc_cert_request_on.png";
            btn_manager_checking = sLanguage + "btn_manager_checking.png";
            btn_kyc_cert_complete = sLanguage + "btn_kyc_cert_complete.png";
        }
    }
}