using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows.Forms;
using coinBlock.Views;
using coinBlock.Model.DepositWithdraw;
using coinBlock.Utility;
using System.Windows;
using coinBlock.Model;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using coinBlock.Model.Common;
using System.Collections.Generic;
using System.Windows.Input;
using coinBlock.Model.MyPage;
using coinBlock.Views.DepositWithdraw.Popup;

namespace coinBlock.ViewModels.DepositWithdraw
{
    [POCOViewModel]
    public class WithdrawDepositWithdrawViewModel
    {

        DateTime dt;
        //DateTime dt_sms;
        Alert alert = null;//new Alert();
        DispatcherTimer RepeatTimer;
        DispatcherTimer RepeatTimer2;
        //DispatcherTimer RepeatTimer3;

        public virtual ObservableCollection<ResponseWithdrawContentListModel> WithdrawList { get; set; }
        public virtual ObservableCollection<ResponseWithdrawContentListModel> WithdrawCardList { get; set; }
        public virtual ObservableCollection<ResponseWithdrawContentListModel> WithdrawTransferList { get; set; }
        public virtual ObservableCollection<ComboBoxStrData> bankList { get; set; }
        public virtual ComboBoxStrData selBank { get; set; }
        public virtual ObservableCollection<ComboBoxStrData> cardList { get; set; }
        public virtual ComboBoxStrData selCard { get; set; }

        public virtual ObservableCollection<ResponseGetInfinityUserListModel> InfinityList { get; set; }

        bool IsLoad = true;

        public virtual string SC { get; } = CommonLib.StandardCurcyNm;

        public virtual decimal holdKrw { get; set; }
        public virtual decimal holdTransferKrw { get; set; }
        public virtual decimal holdKrwCard { get; set; }

        public virtual string bankCd { get; set; }
        public virtual string bankNm { get; set; }
        public virtual decimal exchangeRate { get; set; }
        public virtual decimal receiptPrice { get; set; }
        public virtual string exCode { get; set; }
        public virtual string cardPw { get; set; }
      
        public virtual string accountNo { get; set; }
        public virtual string accountHolder { get; set; }
        public virtual decimal receiveKrw { get; set; }

        public virtual Visibility EmailRequest { get; set; } = Visibility.Visible;
        public virtual Visibility EmailConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility EmailComplete { get; set; } = Visibility.Collapsed;

        public string otpYn;
        public virtual Visibility OtpRequest { get; set; } = Visibility.Visible;
        public virtual Visibility OtpConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility OtpComplete { get; set; } = Visibility.Collapsed;
        public virtual Visibility OtpVisible { get; set; } = Visibility.Visible;

        public virtual string IP { get; set; }
        public virtual bool EmailOverTime { get; set; } = false;
        public virtual bool EmailAuthCodeEnabled { get; set; } = false;
        public virtual string EmailAuthCode { get; set; } = string.Empty;
        public virtual string EmailTime { get; set; } = "10 : 00";

        //public virtual Visibility SmsAll { get; set; } = Visibility.Collapsed;
        //public virtual Visibility SmsRequest { get; set; } = Visibility.Visible;
        //public virtual Visibility SmsConfirm { get; set; } = Visibility.Collapsed;
        //public virtual Visibility SmsComplete { get; set; } = Visibility.Collapsed;

        //public virtual Visibility SmsMobileVisible { get; set; } = Visibility.Visible;

        //public virtual bool SmsOverTime { get; set; } = false;
        //public virtual bool SmsAuthCodeEnabled { get; set; } = true;
        //public virtual string SmsAuthCode { get; set; } = string.Empty;
        //public virtual string SmsTime { get; set; } = "03 : 00";

        public virtual string userMobile { get; set; }

        // 출금 신청 금액
        public virtual decimal WithdrawRequestPrice { get; set; }
        public virtual decimal TransferAmount { get; set; }
        public virtual string sTransferToSendId { get; set; }
        public virtual bool bEmail { get; set; } = true;
        public virtual bool bPhoneNumber { get; set; } = false;
        //OTP 인증번호
        public virtual string OtpSerialNumber { get; set; }

        public virtual string img_text_usd_withdraw_notice { get; set; }
        public virtual string btn_cert_request { get; set; }
        public virtual string btn_cert_request_on { get; set; }
        public virtual string btn_withdraw_request { get; set; }
        public virtual string btn_withdraw_request_on { get; set; }
        public virtual string btn_cert_cancel { get; set; }
        public virtual string btn_cert_cancel_on { get; set; }
        public virtual string btn_cert_confirm { get; set; }
        public virtual string btn_cert_confirm_on { get; set; }
        public virtual string btn_cert_resend { get; set; }
        public virtual string btn_cert_resend_on { get; set; }
        public virtual string btn_certnum_send { get; set; }
        public virtual string btn_certnum_send_on { get; set; }
        public virtual string btn_cost_all { get; set; }
        public virtual string btn_cost_all_on { get; set; }
        public virtual string btn_sms_cert { get; set; }
        public virtual string btn_sms_cert_on { get; set; }
        public virtual string btn_popup_confirm { get; set; }
        public virtual string btn_popup_confirm_on { get; set; }
        public virtual string btn_remittance_request { get; set; }
        public virtual string btn_remittance_request_on { get; set; }
        public virtual string btn_cert_change { get; set; }
        public virtual string btn_cert_change_on { get; set; }

        public virtual decimal Fee { get; set; }
        public virtual decimal WithdrawMinPrc { get; set; }
        public virtual decimal WithdrawMaxPrc { get; set; }
        public virtual decimal WithdrawDayPrc { get; set; }
        public virtual decimal WithdrawCardMaxPrc { get; set; }
        public virtual string TransferLevel3 { get; set; } = string.Empty;
        public virtual Visibility TransferMaxVisibile { get; set; } = Visibility.Visible;
        public virtual Visibility TransferIdCheck { get; set; } = Visibility.Visible;
        public virtual Visibility TransferIdChange { get; set; } = Visibility.Collapsed;
        public virtual bool TransferEnable { get; set; } = true;


        decimal CalFee { get; set; }

        public virtual int totalHeight { get; set; } = 180;
        public virtual string smsRowHeight { get; set; } = "1*";
        public virtual Visibility langVisible { get; set; }
        public virtual bool prcCheck { get; set; } = true;
        public virtual bool IsBusy { get; set; }

        public virtual string _sCheckedNm { get; set; }
        public virtual string _sCheckedId { get; set; }
        public virtual string withdrawHeader { get; set; } = string.Format(Localization.Resource.WithdrawDepositWithdraw_T_1, CommonLib.StandardCurcyNm);
        

        public virtual SmsInfo AccountSms { get; set; } = null;
        public virtual SmsInfo TransferSms { get; set; } = null;

        public static WithdrawDepositWithdrawViewModel Create()
        {
            return ViewModelSource.Create(() => new WithdrawDepositWithdrawViewModel());
        }

        protected WithdrawDepositWithdrawViewModel()
        {
            try
            {
                dt = new DateTime();
                dt = dt.AddMinutes(10);

                //dt_sms = new DateTime();
                //dt_sms = dt_sms.AddMinutes(3);

                //타이머
                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 0, 1);

                RepeatTimer2 = new DispatcherTimer();
                RepeatTimer2.Tick += RepeatTimer2_Tick;
                RepeatTimer2.Interval = new TimeSpan(0, 5, 0);

                //RepeatTimer3 = new DispatcherTimer();
                //RepeatTimer3.Tick += RepeatTimer3_Tick;
                //RepeatTimer3.Interval = new TimeSpan(0, 0, 1);

                ImageSet();

                Messenger.Default.Register<string>(this, CallCardList);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void RepeatTimer2_Tick(object sender, EventArgs e)
        {
            try
            {
                SearchWithdrawList("A");
                SearchWithdrawList("B", false);
                //SearchWithdrawList("C");
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Loaded()
        {
            try
            {
                IsLoad = true;
                OtpSerialNumber = string.Empty;

        
                //GetBank();
                GetFee();
                GetInfinity();
                //GetCardList();
                CommonLib.GetOtpYn(ref otpYn);
                SearchWithdrawList("A");
                //SearchWithdrawList("B", false);
                //SearchWithdrawList("C");

                AccountSms = ViewModelSource.Create(()=> new SmsInfo(userMobile));
                TransferSms = ViewModelSource.Create(() => new SmsInfo(userMobile));

                RepeatTimer2.Start();

                if (LoginViewModel.LanguagePack.IndexOf("ko") > 0)
                {
                    langVisible = Visibility.Visible;
                }
                else
                {
                    langVisible = Visibility.Collapsed;
                }

                if (MainViewModel.LoginDataModel.foreignIp.Equals("N"))
                {
                    totalHeight = 180;
                    smsRowHeight = "1*";
                    AccountSms.SmsAll = Visibility.Visible;
                    TransferSms.SmsAll = Visibility.Visible;
                }
                else
                {
                    totalHeight = 150;
                    smsRowHeight = "0";
                    AccountSms.SmsAll = Visibility.Collapsed;
                    TransferSms.SmsAll = Visibility.Collapsed;
                }

                if (otpYn.Equals("N"))
                {
                    OtpVisible = Visibility.Collapsed;
                }
                else
                {
                    OtpVisible = Visibility.Visible;

                    if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                    {
                        OtpRequest = Visibility.Visible;
                        OtpConfirm = Visibility.Collapsed;
                        OtpComplete = Visibility.Collapsed;
                    }
                    else
                    {
                        OtpRequest = Visibility.Collapsed;
                        OtpConfirm = Visibility.Visible;
                        OtpComplete = Visibility.Collapsed;
                    }
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

                RepeatTimer.Stop();
                RepeatTimer2.Stop();
                //RepeatTimer3.Stop();

                WithdrawRequestPrice = 0;
                EmailAuthCode = string.Empty;
                AccountSms.SmsAuthCode = string.Empty;
                TransferSms.SmsAuthCode = string.Empty;
                OtpSerialNumber = string.Empty;
                receiptPrice = 0;

                //selCard = cardList[0];

                EmailRequest = Visibility.Visible;
                EmailConfirm = Visibility.Collapsed;
                EmailComplete = Visibility.Collapsed;

                AccountSms.SmsRequest = Visibility.Visible;
                AccountSms.SmsConfirm = Visibility.Collapsed;
                AccountSms.SmsComplete = Visibility.Collapsed;

                TransferSms.SmsRequest = Visibility.Visible;
                TransferSms.SmsConfirm = Visibility.Collapsed;
                TransferSms.SmsComplete = Visibility.Collapsed;

                OtpRequest = Visibility.Visible;
                OtpConfirm = Visibility.Collapsed;
                OtpComplete = Visibility.Collapsed;

                TransferIdCheck = Visibility.Visible;
                TransferIdChange = Visibility.Collapsed;
                TransferEnable = true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CallCardList(string Message)
        {
            try
            {
                if (Message.Equals("CardListUpdate"))
                {
                    if (IsLoad)
                    {
                        GetCardList();
                        SearchWithdrawList("C");
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetUserInfo()
        {
            try
            {
                using (RequestUserWithdrawInfoModel req = new RequestUserWithdrawInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseUserWithdrawInfoModel res = WebApiLib.SyncCall<ResponseUserWithdrawInfoModel, RequestUserWithdrawInfoModel>(req))
                    {
                        holdKrw = Math.Truncate(res.data.krwPrc) < 0 ? 0 : Math.Truncate(res.data.krwPrc);
                        holdTransferKrw = Math.Truncate(res.data.krwPrc) < 0 ? 0 : Math.Truncate(res.data.krwPrc);
                        holdKrwCard = Math.Truncate(res.data.krwPrcCard) < 0 ? 0 : Math.Truncate(res.data.krwPrcCard);
                        bankCd = res.data.bankCd;
                        bankNm = res.data.bankNm;
                        accountNo = res.data.bankAccNo;
                        accountHolder = res.data.accntNm;
                        //exCode = res.data.cryCode;
                    }
                }
                using (RequestGetUserInfoModel req = new RequestGetUserInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetUserInfoModel res = WebApiLib.SyncCall<ResponseGetUserInfoModel, RequestGetUserInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            userMobile = res.data.userMobile;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetFee()
        {
            try
            {
                Fee = MainViewModel.CoinData.WithdrawFee;
                WithdrawMinPrc = MainViewModel.CoinData.WithdrawMinPrc;
                WithdrawMaxPrc = MainViewModel.CoinData.WithdrawMaxPrc;
                WithdrawDayPrc = MainViewModel.CoinData.WithdrawDayPrc;

                if (MainViewModel.CoinData.Lv.Equals(3))
                {
                    TransferMaxVisibile = Visibility.Collapsed;
                    TransferLevel3 = "무제한";
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetInfinity()
        {
            try
            {
                using (RequestGetInfinityUserModel req = new RequestGetInfinityUserModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetInfinityUserModel res = await WebApiLib.AsyncCall<ResponseGetInfinityUserModel, RequestGetInfinityUserModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd.Equals(""))
                            {
                                InfinityList = new ObservableCollection<ResponseGetInfinityUserListModel>();
                                InfinityList = res.data.list;
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

        public async void GetBank()
        {
            try
            {
                using (RequestBankModel req = new RequestBankModel())
                {
                    req.cmmCd = "CMMP00000000123";

                    using (ResponseBankModel res = await WebApiLib.AsyncCall<ResponseBankModel, RequestBankModel>(req))
                    {
                        bankList = new ObservableCollection<ComboBoxStrData>();

                        foreach (var item in res.data.list)
                        {
                            bankList.Add(new ComboBoxStrData { Name = item.bankNm, Value = item.bankCd });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetCardList()
        {
            try
            {
                using (RequestCardReqContentModel req = new RequestCardReqContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.status = "04";

                    using (ResponseCardReqContentModel res = await WebApiLib.AsyncCall<ResponseCardReqContentModel, RequestCardReqContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            cardList = new ObservableCollection<ComboBoxStrData>();
                            cardList.Add(new ComboBoxStrData { Name = Localization.Resource.Common_Alert_16, Value = "00" });
                            foreach (ResponseCardReqContentListModel item in res.data.list)
                            {
                                cardList.Add(new ComboBoxStrData() { Name = CommonLib.CardNumChange(item.cardNum), Value = item.cardNum });
                            }

                            if (cardList.Count > 0)
                            {
                                selCard = cardList[0];
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

        public void CmdTabChanged()
        {
            GetUserInfo();
            WithdrawRequestPrice = 0;
            EmailAuthCode = string.Empty;
            OtpSerialNumber = string.Empty;
            cardPw = string.Empty;
            receiptPrice = 0;

            EmailRequest = Visibility.Visible;
            EmailConfirm = Visibility.Collapsed;
            EmailComplete = Visibility.Collapsed;

            AccountSms.SmsRequest = Visibility.Visible;
            AccountSms.SmsConfirm = Visibility.Collapsed;
            AccountSms.SmsComplete = Visibility.Collapsed;

            TransferSms.SmsRequest = Visibility.Visible;
            TransferSms.SmsConfirm = Visibility.Collapsed;
            TransferSms.SmsComplete = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
            {
                OtpRequest = Visibility.Visible;
                OtpConfirm = Visibility.Collapsed;
                OtpComplete = Visibility.Collapsed;
            }
            else
            {
                OtpRequest = Visibility.Collapsed;
                OtpConfirm = Visibility.Visible;
                OtpComplete = Visibility.Collapsed;
            }
        }

        public void OnWithdrawRequestPriceChanged()
        {
            try
            {
                if (prcCheck)
                {
                    prcCheck = false;
                    WithdrawRequestPrice = (int)(WithdrawRequestPrice / 10000) * 10000;
                    receiveKrw = WithdrawRequestPrice - Fee < 0 ? 0 : WithdrawRequestPrice - Fee;
                    prcCheck = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        
        public void OnTransferAmountChanged()
        {
            try
            {
                if (prcCheck)
                {
                    prcCheck = false;
                    TransferAmount = (int)(TransferAmount / 10000) * 10000;                    
                    prcCheck = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }


        #region Email 인증
        /// <summary>
        /// 인증번호 요청
        /// </summary>
        public async void CmdEmailRequest()
        {
            try
            {
                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        //alert = new Alert(Localization.Resource.IP_Registration_4_12, 400);
                        //alert.ShowDialog();

                        EmailTime = dt.ToString("mm : ss");
                        RepeatTimer.Start();

                        EmailAuthCodeEnabled = true;
                        EmailRequest = Visibility.Collapsed;
                        EmailConfirm = Visibility.Visible;
                        EmailComplete = Visibility.Collapsed;
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
        /// <summary>
        /// 인증 확인
        /// </summary>
        public async void CmdEmailConfirm()
        {
            try
            {
                if (EmailOverTime)
                {
                    alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
                    alert.ShowDialog();
                    return;
                }

                using (RequestSmsCodeVertifyModel req = new RequestSmsCodeVertifyModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.authCode = EmailAuthCode;

                    using (ResponseSmsCodeVertifyModel res = await WebApiLib.AsyncCall<ResponseSmsCodeVertifyModel, RequestSmsCodeVertifyModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            //alert = new Alert(Localization.Resource.IP_Registration_4_13);
                            //alert.ShowDialog();

                            RepeatTimer.Stop();
                            EmailRequest = Visibility.Collapsed;
                            EmailConfirm = Visibility.Collapsed;
                            EmailComplete = Visibility.Visible;
                        }
                        else
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                            alert.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                alert.ShowDialog();
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 재발송
        /// </summary>
        public async void CmdEmailResend()
        {
            try
            {
                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        alert = new Alert(Localization.Resource.IP_Registration_4_14, 300);
                        alert.ShowDialog();
                        dt = new DateTime();
                        dt = dt.AddMinutes(10);
                        EmailTime = dt.ToString("mm : ss");
                        RepeatTimer.Start();

                        EmailAuthCode = string.Empty;
                        EmailOverTime = false;
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
        /// <summary>
        /// 취소
        /// </summary>
        public void CmdEmailCancel()
        {
            try
            {
                alert = new Alert(Localization.Resource.Common_Alert_6, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    RepeatTimer.Stop();
                    dt = new DateTime();
                    dt = dt.AddMinutes(10);
                    EmailTime = dt.ToString("mm : ss");

                    EmailOverTime = false;
                    EmailAuthCode = string.Empty;
                    EmailAuthCodeEnabled = false;
                    EmailRequest = Visibility.Visible;
                    EmailConfirm = Visibility.Collapsed;
                    EmailComplete = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                dt = dt.AddSeconds(-1);
                EmailTime = dt.ToString("mm : ss");
                if (dt.Minute.Equals(0) && dt.Second.Equals(0))
                {
                    RepeatTimer.Stop();
                    EmailOverTime = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Otp 인증
        public async void CmdOtpConfirm()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(OtpSerialNumber))
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_11);
                    alert.ShowDialog();
                    return;
                }

                IsBusy = true;

                using (RequestOtpCheckModel req = new RequestOtpCheckModel())
                {
                    req.encodedKey = MainViewModel.LoginDataModel.otpNo;
                    req.authCode = OtpSerialNumber;
                    using (ResponseOtpCheckModel res = await WebApiLib.AsyncCall<ResponseOtpCheckModel, RequestOtpCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_8, 320);
                            alert.ShowDialog();

                            OtpRequest = Visibility.Collapsed;
                            OtpConfirm = Visibility.Collapsed;
                            OtpComplete = Visibility.Visible;
                        }
                        else
                        {
                            alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_10, 320);
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
        #endregion

        //#region SMS 인증
        ///// <summary>
        ///// 인증번호 요청
        ///// </summary>
        //public async void CmdSmsRequest()
        //{
        //    try
        //    {
        //        if (userMobile.Equals("00000000000") || userMobile.Equals(string.Empty))
        //        {
        //            alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_1_44, 400);
        //            alert.ShowDialog();
        //            return;
        //        }

        //        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_1_45, Alert.ButtonType.YesNo, 330, 160);
        //        if (alert.ShowDialog() == true)
        //        {
        //            IsBusy = true;

        //            using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
        //            {
        //                req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //                req.userMobile = userMobile;

        //                using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
        //                {
        //                    if (res.resultStrCode == "000")
        //                    {
        //                        string resultCd = res.data.failCd;

        //                        if (resultCd.Equals(""))
        //                        {
        //                            alert = new Alert(Localization.Resource.MemberInfo_Pop_11, 400);
        //                            alert.ShowDialog();

        //                            dt_sms = new DateTime();
        //                            dt_sms = dt_sms.AddMinutes(3);
        //                            SmsTime = dt_sms.ToString("mm : ss");
        //                            RepeatTimer3.Start();

        //                            SmsOverTime = false;
        //                            SmsAuthCode = string.Empty;
        //                            SmsAuthCodeEnabled = true;
        //                            SmsRequest = Visibility.Collapsed;
        //                            SmsConfirm = Visibility.Visible;
        //                            SmsComplete = Visibility.Collapsed;
        //                            SmsMobileVisible = Visibility.Collapsed;
        //                        }
        //                        else if (resultCd.Equals("998"))
        //                        {
        //                            alert = new Alert(Localization.Resource.MemberInfo_Pop_12, 300);
        //                            alert.ShowDialog();
        //                        }
        //                        else if (resultCd.Equals("997"))
        //                        {
        //                            alert = new Alert(Localization.Resource.MemberInfo_Pop_13, 300);
        //                            alert.ShowDialog();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
        ///// <summary>
        ///// 인증 확인
        ///// </summary>
        //public async void CmdSmsConfirm()
        //{
        //    try
        //    {
        //        if (SmsOverTime)
        //        {
        //            alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
        //            alert.ShowDialog();
        //            return;
        //        }

        //        using (RequestUserCertSmsModel req = new RequestUserCertSmsModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //            req.code = SmsAuthCode;

        //            using (ResponseUserCertSmsModel res = await WebApiLib.AsyncCall<ResponseUserCertSmsModel, RequestUserCertSmsModel>(req))
        //            {
        //                if (res.resultStrCode == "000")
        //                {
        //                    string resultCd = res.data.failCd;

        //                    if (resultCd.Equals(""))
        //                    {
        //                        alert = new Alert(Localization.Resource.IP_Registration_4_13);
        //                        alert.ShowDialog();

        //                        RepeatTimer3.Stop();
        //                        SmsRequest = Visibility.Collapsed;
        //                        SmsConfirm = Visibility.Collapsed;
        //                        SmsComplete = Visibility.Visible;
        //                    }
        //                    else if (resultCd.Equals("999"))
        //                    {
        //                        alert = new Alert(Localization.Resource.Login_13, 300);
        //                        alert.ShowDialog();
        //                    }
        //                    else if (resultCd.Equals("998") || resultCd.Equals("996"))
        //                    {
        //                        alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
        //                        alert.ShowDialog();
        //                    }
        //                    else if (resultCd.Equals("997"))
        //                    {
        //                        alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
        //                        alert.ShowDialog();
        //                    }
        //                }
        //                else
        //                {
        //                    alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
        //                    alert.ShowDialog();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
        //        alert.ShowDialog();
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}
        ///// <summary>
        ///// 재발송
        ///// </summary>
        //public async void CmdSmsResend()
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //            req.userMobile = userMobile;

        //            using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
        //            {
        //                alert = new Alert(Localization.Resource.IP_Registration_4_14, 300);
        //                alert.ShowDialog();
        //                dt_sms = new DateTime();
        //                dt_sms = dt_sms.AddMinutes(3);
        //                SmsTime = dt_sms.ToString("mm : ss");

        //                SmsAuthCode = string.Empty;
        //                SmsOverTime = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
        ///// <summary>
        ///// 취소
        ///// </summary>
        //public void CmdSmsCancel()
        //{
        //    try
        //    {
        //        alert = new Alert(Localization.Resource.Common_Alert_6, Alert.ButtonType.YesNo);
        //        if (alert.ShowDialog() == true)
        //        {
        //            RepeatTimer3.Stop();
        //            dt_sms = new DateTime();
        //            dt_sms = dt_sms.AddMinutes(3);
        //            SmsTime = dt_sms.ToString("mm : ss");

        //            SmsOverTime = false;
        //            SmsAuthCode = string.Empty;
        //            SmsAuthCodeEnabled = true;

        //            SmsRequest = Visibility.Visible;
        //            SmsConfirm = Visibility.Collapsed;
        //            SmsComplete = Visibility.Collapsed;
        //            SmsMobileVisible = Visibility.Visible;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}

        //private void RepeatTimer3_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        dt_sms = dt_sms.AddSeconds(-1);
        //        SmsTime = dt_sms.ToString("mm : ss");
        //        if (dt_sms.Minute.Equals(0) && dt_sms.Second.Equals(0))
        //        {
        //            RepeatTimer3.Stop();
        //            SmsOverTime = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}
        //#endregion

        /// <summary>
        /// 출금요청
        /// </summary>
        /// <param name="payMthCd"></param>
        public void RequestWithdraw(string payMthCd)
        {
            try
            {
                //if (true)
                //{
                //    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                //    alert.ShowDialog();
                //    return;
                //}

                if (CommonLib.UserFuncCheck() == "Y")
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                if (payMthCd.Equals("C"))
                {
                    alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                //코인 출금 제한 확인
                using (RequestLmtWithdrowModel req = new RequestLmtWithdrowModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.curcyCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW);

                    using (ResponseLmtWithdrowModel res = WebApiLib.SyncCall<ResponseLmtWithdrowModel, RequestLmtWithdrowModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string withdrowType = res.data.withdrowType;

                            if (withdrowType.Equals("1"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_19);
                                alert.ShowDialog();
                                return;
                            }
                            else if (withdrowType.Equals("4"))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                }

                CommonLib.GetOtpYn(ref otpYn);

                if (otpYn.Equals("Y") && OtpVisible == Visibility.Collapsed)
                {
                    OtpVisible = Visibility.Visible;
                }
                else if (otpYn.Equals("N"))
                {
                    OtpVisible = Visibility.Collapsed;
                }

                IsBusy = true;

                if (payMthCd.Equals("C"))
                {
                    if (selCard.Value.Equals("00"))
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_17);
                        alert.ShowDialog();
                        return;
                    }
                    else if (holdKrwCard < WithdrawRequestPrice)
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_34, 400);
                        alert.ShowDialog();
                        return;
                    }
                }
                else if (payMthCd.Equals("A"))
                {
                    //계좌번호 체크
                    if (string.IsNullOrWhiteSpace(accountNo))
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_1, 320);
                        alert.ShowDialog();
                        return;
                    }
                }

                //출금 신청금액 체크
                if (WithdrawRequestPrice <= 0)
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_2, 330);
                    alert.ShowDialog();
                    return;
                }
                //보유 금액보다 출금신청 금액 입력 체크
                else if (holdKrw < WithdrawRequestPrice)
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_6, 400);
                    alert.ShowDialog();
                    return;
                }
                if (!MainViewModel.LoginDataModel.userEmail.Equals("kim0681@hanmail.net"))
                {
                    bool bCheck = true;

                    foreach (ResponseGetInfinityUserListModel item in InfinityList)
                    {
                        if (StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW) == item.cnKndCd)
                        {
                            bCheck = false;
                        }
                    }

                    if (bCheck)
                    {
                        if (receiveKrw > WithdrawMaxPrc)
                        {
                            alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_12, 400);
                            alert.ShowDialog();
                            return;
                        }
                        //수령금액 체크
                        else if (receiveKrw < WithdrawMinPrc - Fee)
                        {
                            alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_3, 330);
                            alert.ShowDialog();
                            return;
                        }
                    }
                }
                //Email 인증 체크
                if (EmailComplete != Visibility.Visible)
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_4, 330);
                    alert.ShowDialog();
                    return;
                }
                if (OtpVisible == Visibility.Visible)
                {
                    //OTP 인증번호 체크 
                    if (OtpComplete != Visibility.Visible)
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_5, 330);
                        alert.ShowDialog();
                        return;
                    }
                }
                if (MainViewModel.LoginDataModel.foreignIp.Equals("N"))
                {
                    if (AccountSms.SmsComplete != Visibility.Visible)
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_37, 330);
                        alert.ShowDialog();
                        return;
                    }
                }

                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_14, Alert.ButtonType.YesNo, 330);
                if (alert.ShowDialog() == true)
                {
                    //API로 데이터 보내기.
                    using (RequestWithdrawMoneyModel req = new RequestWithdrawMoneyModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.wdrPrc = WithdrawRequestPrice;
                        req.cnSndFee = Fee;
                        req.payMthCd = payMthCd;                     

                        if (payMthCd.Equals("C"))
                        {
                            Dictionary<string, string> dict = null;

                            using (RequestPublicKeyModel req2 = new RequestPublicKeyModel())
                            {
                                using (ResponsePublicKeyModel res2 = WebApiLib.SyncCall<ResponsePublicKeyModel, RequestPublicKeyModel>(req2))
                                {
                                    //dict = EncodingLib.AesEncrypt(selCard.Value, res2.data.pubKeyModule, res2.data.pubKeyExponent);
                                    dict = EncodingLib.AesEncryptKey(res2.data.pubKeyModule, res2.data.pubKeyExponent);

                                    req.cardNo = EncodingLib.AesEncrypt(selCard.Value, dict["gid"]);
                                    req.clientPe = dict["acekey"];
                                }
                            }
                        }
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseWithdrawMoneyModel res = WebApiLib.SyncCall<ResponseWithdrawMoneyModel, RequestWithdrawMoneyModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string resultCd = res.data.failCd;

                                if (resultCd.Equals(""))
                                {
                                    Clear();
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_7);
                                    alert.ShowDialog();
                                    Messenger.Default.Send("AssetsRefresh");
                                }
                                else if (resultCd.Equals("999"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_18, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("998"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_19, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("997"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_20, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("996"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_21, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("995"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_22, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("994"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_23, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("993"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_24, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("992"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_25, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("991"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_26, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("990"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_27, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("989"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_30, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("988"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_31, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("987"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_32, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("986"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_34, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("985"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_39, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("984"))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.WithdrawDepositWithdraw_T_Common_7 + "\n" + Localization.Resource.WithdrawDepositWithdraw_T_Common_8, CommonLib.StandardCurcyNm), 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("983"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_40 + "\n" + Localization.Resource.WithdrawDepositWithdraw_Common_41, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("982"))
                                {
                                    alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_11, 320);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("981"))
                                {
                                    alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_11, 320);
                                    alert.ShowDialog();
                                }
                                else if(resultCd.Equals("980"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_43, 320, 160);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("979"))
                                {
                                    alert = new Alert(Localization.Resource.Common_Alert_1);
                                    alert.ShowDialog();
                                }

                                IsBusy = false;
                            }
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

        public async void SearchWithdrawList(string payMthCd, bool doubleCheck = true)
        {
            //리스트조회
            try
            {
                if (doubleCheck)
                {
                    Messenger.Default.Send("AssetsRefresh");
                    GetUserInfo();
                }

                using (RequestWithdrawContentModel req = new RequestWithdrawContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.payMthCd = payMthCd;

                    using (ResponseWithdrawContentModel res = await WebApiLib.AsyncCall<ResponseWithdrawContentModel, RequestWithdrawContentModel>(req))
                    {
                        if (payMthCd.Equals("A"))
                        {
                            WithdrawList = res.data.list;
                        }
                        else if (payMthCd.Equals("C"))
                        {
                            for (int i = 0; i < res.data.list.Count; i++)
                            {
                                res.data.list[i].cardNo = CommonLib.CardNumChange(res.data.list[i].cardNo);
                            }
                            WithdrawCardList = res.data.list;
                        }
                        else if(payMthCd.Equals("B"))
                        {
                            WithdrawTransferList = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSetAllCash(string payMthCd)
        {
            if (payMthCd.Equals("A"))
            {
                //WithdrawRequestPrice = holdKrw;
                if (holdKrw > 0)
                {
                    WithdrawRequestPrice = holdKrw;
                    //receiveKrw = holdKrw - Fee < 0 ? 0 : holdKrw - Fee;
                }
            }
            else if (payMthCd.Equals("C"))
            {
                //WithdrawRequestPrice = holdKrwCard;
                if (holdKrwCard > 0)
                {
                    WithdrawRequestPrice = holdKrwCard;
                    //receiveKrw = holdKrwCard - Fee < 0 ? 0 : holdKrwCard - Fee;                
                }
            }
            else if(payMthCd.Equals("T"))
            {
                if (holdTransferKrw > 0)
                {
                    TransferAmount = holdTransferKrw;
                    //receiveKrw = holdKrw - Fee < 0 ? 0 : holdKrw - Fee;
                }
            }
        }

        public async void GetExRate()
        {
            try
            {
                using (RequestExRateModel req = new RequestExRateModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseExRateModel res = await WebApiLib.AsyncCall<ResponseExRateModel, RequestExRateModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            exchangeRate = Math.Round(res.data.exRate, 2);
                            receiptPrice = Math.Truncate(exchangeRate * receiveKrw);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Clear()
        {
            try
            {
                GetUserInfo();
                SearchWithdrawList("A");
                SearchWithdrawList("B", false);
                //SearchWithdrawList("C");
                CommonLib.GetOtpYn(ref otpYn);
                WithdrawRequestPrice = 0;
                receiveKrw = 0;

                RepeatTimer.Stop();
                dt = new DateTime();
                dt = dt.AddMinutes(10);
                EmailTime = dt.ToString("mm : ss");

                EmailOverTime = false;
                EmailAuthCode = string.Empty;
                EmailAuthCodeEnabled = false;
                EmailRequest = Visibility.Visible;
                EmailConfirm = Visibility.Collapsed;
                EmailComplete = Visibility.Collapsed;
                OtpSerialNumber = string.Empty;

                if (otpYn.Equals("N"))
                {
                    OtpVisible = Visibility.Collapsed;
                }
                else
                {
                    OtpVisible = Visibility.Visible;

                    if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                    {
                        OtpRequest = Visibility.Visible;
                        OtpConfirm = Visibility.Collapsed;
                        OtpComplete = Visibility.Collapsed;
                    }
                    else
                    {
                        OtpRequest = Visibility.Collapsed;
                        OtpConfirm = Visibility.Visible;
                        OtpComplete = Visibility.Collapsed;
                    }
                }

                if (MainViewModel.LoginDataModel.foreignIp.Equals("N"))
                {
                    totalHeight = 180;
                    smsRowHeight = "1*";
                    AccountSms.SmsAll = Visibility.Visible;
                }
                else
                {
                    totalHeight = 150;
                    smsRowHeight = "0";
                    AccountSms.SmsAll = Visibility.Collapsed;
                }

                AccountSms.SmsRequest = Visibility.Visible;
                AccountSms.SmsConfirm = Visibility.Collapsed;
                AccountSms.SmsComplete = Visibility.Collapsed;
                AccountSms.SmsMobileVisible = Visibility.Visible;

                TransferSms.SmsRequest = Visibility.Visible;
                TransferSms.SmsConfirm = Visibility.Collapsed;
                TransferSms.SmsComplete = Visibility.Collapsed;
                TransferSms.SmsMobileVisible = Visibility.Visible;

                TransferIdCheck = Visibility.Visible;
                TransferIdChange = Visibility.Collapsed;
                TransferEnable = true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdStrNonInput(TextCompositionEventArgs e)
        {
            try
            {
                int value = 0;
                if (e.Text == null) return;
                if (string.IsNullOrEmpty(e.Text.ToString())) return;

                if (!int.TryParse(e.Text.ToString(), out value))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            img_text_usd_withdraw_notice = sLanguage + "img_text_usd_withdraw_notice.png";
            btn_cert_request = sLanguage + "btn_cert_request.png";
            btn_cert_request_on = sLanguage + "btn_cert_request_on.png";
            btn_withdraw_request = sLanguage + "btn_withdraw_request.png";
            btn_withdraw_request_on = sLanguage + "btn_withdraw_request_on.png";
            btn_cert_cancel = sLanguage + "btn_cert_cancel.png";
            btn_cert_cancel_on = sLanguage + "btn_cert_cancel_on.png";
            btn_cert_confirm = sLanguage + "btn_cert_confirm.png";
            btn_cert_confirm_on = sLanguage + "btn_cert_confirm_on.png";
            btn_cert_resend = sLanguage + "btn_cert_resend.png";
            btn_cert_resend_on = sLanguage + "btn_cert_resend_on.png";
            btn_certnum_send = sLanguage + "btn_certnum_send.png";
            btn_certnum_send_on = sLanguage + "btn_certnum_send_on.png";
            btn_cost_all = sLanguage + "btn_cost_all.png";
            btn_cost_all_on = sLanguage + "btn_cost_all_on.png";
            btn_sms_cert = sLanguage + "btn_sms_cert.png";
            btn_sms_cert_on = sLanguage + "btn_sms_cert_on.png";
            btn_popup_confirm = sLanguage + "btn_popup_confirm.png";
            btn_popup_confirm_on = sLanguage + "btn_popup_confirm_on.png";
            btn_remittance_request = sLanguage + "btn_remittance_request.png";
            btn_remittance_request_on = sLanguage + "btn_remittance_request_on.png";
            btn_cert_change = sLanguage + "btn_cert_change.png";
            btn_cert_change_on = sLanguage + "btn_cert_change_on.png";
        }

        #region PTC 송금

        public void CmdCheckID(string sStatus)
        {
            try
            {
                if (sStatus.Equals("P"))
                {
                    if (string.IsNullOrWhiteSpace(sTransferToSendId))
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_T_Common_4);
                        alert.ShowDialog();
                        return;
                    }

                    using (RequestFindUserIdModel req = new RequestFindUserIdModel())
                    {
                        if (bEmail)
                        {
                            req.userEmail = sTransferToSendId;
                        }
                        else if (bPhoneNumber)
                        {
                            req.userMobile = sTransferToSendId;
                        }

                        using (ResponseFindUserIdModel res = WebApiLib.SyncCall<ResponseFindUserIdModel, RequestFindUserIdModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                if (res.data.failCd.Equals(""))
                                {
                                    if (res.data.findYn.Equals("Y"))
                                    {
                                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_T_Common_2);
                                        alert.ShowDialog();

                                        _sCheckedNm = res.data.userName;
                                        _sCheckedId = res.data.userEmail;

                                        TransferIdCheck = Visibility.Collapsed;
                                        TransferIdChange = Visibility.Visible;
                                        TransferEnable = false;
                                    }
                                    else
                                    {
                                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_T_Common_3);
                                        alert.ShowDialog();
                                    }
                                }
                                else if (res.data.failCd.Equals("997"))
                                {
                                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_27);
                                    alert.ShowDialog();
                                }
                            }
                        }
                    }
                }
                else if(sStatus.Equals("C"))
                {
                    TransferIdCheck = Visibility.Visible;
                    TransferIdChange = Visibility.Collapsed;
                    TransferEnable = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdTransferCurrency()
        {
            try
            {
                if (CommonLib.UserFuncCheck() == "Y")
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                //코인 출금 제한 확인
                using (RequestLmtWithdrowModel req = new RequestLmtWithdrowModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.curcyCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW);

                    using (ResponseLmtWithdrowModel res = WebApiLib.SyncCall<ResponseLmtWithdrowModel, RequestLmtWithdrowModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string withdrowType = res.data.withdrowType;

                            if (withdrowType.Equals("1"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_19);
                                alert.ShowDialog();
                                return;
                            }
                            else if (withdrowType.Equals("4"))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                }

                //받는 이 체크
                if (string.IsNullOrWhiteSpace(sTransferToSendId))
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_T_Common_4);
                    alert.ShowDialog();
                    return;
                }
                //받는 이 확인되었는지 체크
                else if(string.IsNullOrEmpty(_sCheckedId))
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_T_Common_9);
                    alert.ShowDialog();
                    return;
                }
                //송금 수량이 0일 때
                if (TransferAmount.Equals(0))
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_2);
                    alert.ShowDialog();
                    return;
                }
                //보유 수량보다 송금 수량이 많을 때
                if (TransferAmount > holdTransferKrw)
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_6);
                    alert.ShowDialog();
                    return;
                }
                //송금 수량이 1회 최소 송금보다 적을 때
                if (TransferAmount < WithdrawMinPrc)
                {
                    alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_14);
                    alert.ShowDialog();
                    return;
                }
                //레벨 3이 아닌데
                if (!MainViewModel.CoinData.Lv.Equals(3))
                {
                    //송금 수량이 1회 최고 송금액보다 많을 때
                    if (TransferAmount > WithdrawMaxPrc)
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_25);
                        alert.ShowDialog();
                        return;
                    }
                }
                //외국인이 아니거나 해외 IP가 아닌데
                if (MainViewModel.LoginDataModel.foreignIp.Equals("N"))
                {
                    //SMS 인증이 완료되지 않았을 때.
                    if (TransferSms.SmsComplete != Visibility.Visible)
                    {
                        alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_T_Common_5, 310);
                        alert.ShowDialog();
                        return;
                    }
                }

                CurrencyTransferPop pop = new CurrencyTransferPop(_sCheckedNm, _sCheckedId, TransferAmount);
                if (pop.ShowDialog() == true)
                {
                    SearchWithdrawList("B");
                    sTransferToSendId = string.Empty;
                    TransferAmount = 0;
                    TransferSms.SmsRequest = Visibility.Visible;
                    TransferSms.SmsConfirm = Visibility.Collapsed;
                    TransferSms.SmsComplete = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #endregion
    }

    public class SmsInfo
    {
        public virtual bool EmailOverTime { get; set; } = false;
        public virtual bool EmailAuthCodeEnabled { get; set; } = false;
        public virtual string EmailAuthCode { get; set; } = string.Empty;
        public virtual string EmailTime { get; set; } = "10 : 00";

        public virtual Visibility SmsAll { get; set; } = Visibility.Collapsed;
        public virtual Visibility SmsRequest { get; set; } = Visibility.Visible;
        public virtual Visibility SmsConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility SmsComplete { get; set; } = Visibility.Collapsed;

        public virtual Visibility SmsMobileVisible { get; set; } = Visibility.Visible;

        public virtual bool SmsOverTime { get; set; } = false;
        public virtual bool SmsAuthCodeEnabled { get; set; } = true;
        public virtual string SmsAuthCode { get; set; } = string.Empty;
        public virtual string SmsTime { get; set; } = "03 : 00";

        public virtual string userMobile { get; set; }

        public Alert alert = null;
        DateTime dt_sms;
        DispatcherTimer RepeatTimer3;

        public SmsInfo(string sUserMobile)
        {
            try
            {
                dt_sms = new DateTime();
                dt_sms = dt_sms.AddMinutes(3);
                userMobile = sUserMobile;

                RepeatTimer3 = new DispatcherTimer();
                RepeatTimer3.Tick += RepeatTimer3_Tick;
                RepeatTimer3.Interval = new TimeSpan(0, 0, 1);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #region SMS 인증
        /// <summary>z
        /// 인증번호 요청
        /// </summary>
        public async void CmdSmsRequest()
        {
            try
            {
                if (userMobile.Equals("00000000000") || userMobile.Equals(string.Empty))
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_1_44, 400);
                    alert.ShowDialog();
                    return;
                }

                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_1_45, Alert.ButtonType.YesNo, 330, 160);
                if (alert.ShowDialog() == true)
                {
                    //IsBusy = true;

                    using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.userMobile = userMobile;

                        using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string resultCd = res.data.failCd;

                                if (resultCd.Equals(""))
                                {
                                    //alert = new Alert(Localization.Resource.MemberInfo_Pop_11, 400);
                                    //alert.ShowDialog();

                                    dt_sms = new DateTime();
                                    dt_sms = dt_sms.AddMinutes(3);
                                    SmsTime = dt_sms.ToString("mm : ss");
                                    RepeatTimer3.Start();

                                    SmsOverTime = false;
                                    SmsAuthCode = string.Empty;
                                    SmsAuthCodeEnabled = true;
                                    SmsRequest = Visibility.Collapsed;
                                    SmsConfirm = Visibility.Visible;
                                    SmsComplete = Visibility.Collapsed;
                                    SmsMobileVisible = Visibility.Collapsed;
                                }
                                else if (resultCd.Equals("998"))
                                {
                                    alert = new Alert(Localization.Resource.MemberInfo_Pop_12, 300);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("997"))
                                {
                                    alert = new Alert(Localization.Resource.MemberInfo_Pop_13, 300);
                                    alert.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            //finally
            //{
            //    IsBusy = false;
            //}
        }
        /// <summary>
        /// 인증 확인
        /// </summary>
        public async void CmdSmsConfirm()
        {
            try
            {
                if (SmsOverTime)
                {
                    alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
                    alert.ShowDialog();
                    return;
                }

                using (RequestUserCertSmsModel req = new RequestUserCertSmsModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.code = SmsAuthCode;

                    using (ResponseUserCertSmsModel res = await WebApiLib.AsyncCall<ResponseUserCertSmsModel, RequestUserCertSmsModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string resultCd = res.data.failCd;

                            if (resultCd.Equals(""))
                            {
                                //alert = new Alert(Localization.Resource.IP_Registration_4_13);
                                //alert.ShowDialog();

                                RepeatTimer3.Stop();
                                SmsRequest = Visibility.Collapsed;
                                SmsConfirm = Visibility.Collapsed;
                                SmsComplete = Visibility.Visible;
                            }
                            else if (resultCd.Equals("999"))
                            {
                                alert = new Alert(Localization.Resource.Login_13, 300);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("998") || resultCd.Equals("996"))
                            {
                                alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("997"))
                            {
                                alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
                                alert.ShowDialog();
                            }
                        }
                        else
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                            alert.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                alert.ShowDialog();
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 재발송
        /// </summary>
        public async void CmdSmsResend()
        {
            try
            {
                //IsBusy = true;

                using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.userMobile = userMobile;

                    using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
                    {
                        alert = new Alert(Localization.Resource.IP_Registration_4_14, 300);
                        alert.ShowDialog();
                        dt_sms = new DateTime();
                        dt_sms = dt_sms.AddMinutes(3);
                        SmsTime = dt_sms.ToString("mm : ss");
                        RepeatTimer3.Start();

                        SmsAuthCode = string.Empty;
                        SmsOverTime = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            //finally
            //{
            //    IsBusy = false;
            //}
        }
        /// <summary>
        /// 취소
        /// </summary>
        public void CmdSmsCancel()
        {
            try
            {
                alert = new Alert(Localization.Resource.Common_Alert_6, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    RepeatTimer3.Stop();
                    dt_sms = new DateTime();
                    dt_sms = dt_sms.AddMinutes(3);
                    SmsTime = dt_sms.ToString("mm : ss");

                    SmsOverTime = false;
                    SmsAuthCode = string.Empty;
                    SmsAuthCodeEnabled = true;

                    SmsRequest = Visibility.Visible;
                    SmsConfirm = Visibility.Collapsed;
                    SmsComplete = Visibility.Collapsed;
                    SmsMobileVisible = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 10초 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepeatTimer3_Tick(object sender, EventArgs e)
        {
            try
            {
                dt_sms = dt_sms.AddSeconds(-1);
                SmsTime = dt_sms.ToString("mm : ss");
                if (dt_sms.Minute.Equals(0) && dt_sms.Second.Equals(0))
                {
                    RepeatTimer3.Stop();
                    SmsOverTime = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        #endregion
    }

    public class ComboBoxStrData
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
    }
}