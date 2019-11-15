using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.Views;
using coinBlock.Model.DepositWithdraw;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DevExpress.Mvvm.UI;
using System.Windows;
using coinBlock.ViewModels.MyPage;
using System.Linq;
using System.Windows.Threading;
using coinBlock.Model.MyPage;
using coinBlock.Model.Common;
using coinBlock.Views.Common;
using coinBlock.ViewModels.Common;
using System.Windows.Input;

namespace coinBlock.ViewModels.DepositWithdraw
{
    [POCOViewModel]
    public class PrepaidCardDepositWithdrawViewModel
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

        DispatcherTimer RepeatTimer;
                
        public virtual ObservableCollection<ResponseCardReqContentListModel> CardReqList { get; set; }        
        public virtual ObservableCollection<ResponseKrwRechargeListModel> CardRechargeList { get; set; }

        bool IsLoad = true;
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;
        public virtual string cardActCode { get; set; }
        public virtual string actNo { get; set; }
        public virtual string bankNm { get; set; }
        public virtual decimal cardReqPrc { get; set; }
        public virtual decimal cardReqPrcOri { get; set; }
        public virtual decimal deliveryFee { get; set; }

        public virtual string cardMonth { get; set; }
        public virtual string cardYear { get; set; }
        public virtual string cardCvc { get; set; }
        public virtual string cardPw { get; set; }
        public virtual decimal cardRemPrc { get; set; }
        public virtual int fee { get; set; }
        public virtual decimal exRate { get; set; }
        public virtual string cryCode { get; set; }
        public virtual decimal ResultPrc { get; set; }

        public virtual string img_text_usd_charge_notice { get; set; }
        public virtual string img_text_recent_exchange { get; set; }
        public virtual string btn_coupon_purchase { get; set; }
        public virtual string btn_coupon_purchase_on { get; set; }
        public virtual string img_text_coupon_use { get; set; }
        public virtual string btn_coupon_use { get; set; }
        public virtual string btn_coupon_use_on { get; set; }
        public virtual string img_text_coupon_record { get; set; }
        public virtual string btn_regist_card { get; set; }
        public virtual string btn_regist_card_on { get; set; }
        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }
        public virtual string btn_auto_request { get; set; }
        public virtual string btn_auto_request_on { get; set; }
        public virtual string btn_card_charging { get; set; }
        public virtual string btn_card_charging_on { get; set; }
        public virtual string img_process_card { get; set; }
        public virtual string btn_regist_atm { get; set; }
        public virtual string btn_regist_atm_on { get; set; }

        public virtual bool IsBusy { get; set; }

        Alert alert = null;// new Alert();

        #region 선불카드

        public virtual string userNm { get; set; } = MainViewModel.LoginDataModel.userNm;
        public virtual string userEmail { get; set; } = MainViewModel.LoginDataModel.userEmail;
        public virtual string userBirthDay { get; set; }
        public virtual string userPhoneNumber { get; set; }
        public virtual string userPhoneOriNumber { get; set; }
        public virtual string userPost { get; set; }
        public virtual string userAddr1 { get; set; }
        public virtual string userAddr2 { get; set; }
        public virtual string userCountry { get; set; }

        public virtual string DepositMth { get; set; } = Localization.Resource.RechargeDepositWithdraw_Common_11;   
        public virtual Visibility DepositMth1Visible { get; set; } = Visibility.Visible;
        public virtual Visibility DepositMth2Visible { get; set; } = Visibility.Collapsed;

        public virtual string deliveryAddr { get; set; }

        public virtual bool bCardPayment { get; set; } = true;
        public virtual bool bAccountPayment { get; set; }

        public virtual bool bOnline { get; set; } = true;
        public virtual bool bOffline { get; set; } 

        #endregion

        public static PrepaidCardDepositWithdrawViewModel Create()
        {
            return ViewModelSource.Create(() => new PrepaidCardDepositWithdrawViewModel());
        }

        protected PrepaidCardDepositWithdrawViewModel()
        {
            try
            {
                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 5, 0);

                Messenger.Default.Register<string>(this, CallCardList);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
        }

        public void Loaded()
        {
            try
            {
                IsLoad = true;

                ImageSet();
                GetUserInfo();
                GetCardPayInfo();
                GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
                GetCardRequestList();
                RepeatTimer.Start();
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

                cardMonth = string.Empty;
                cardYear = string.Empty;
                fee = 0;
                ResultPrc = 0;
                RepeatTimer.Stop();
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
                        GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetUserInfo()
        {
            try
            {
                using (RequestGetUserInfoModel req = new RequestGetUserInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetUserInfoModel res = await WebApiLib.AsyncCall<ResponseGetUserInfoModel, RequestGetUserInfoModel>(req))
                    {
                        userPhoneNumber = res.data.userMobile;
                        userPhoneOriNumber = res.data.userMobile;
                        userBirthDay = res.data.birthDay;
                        userCountry = res.data.countryCd;
                        userPost = res.data.postCd;
                        userAddr1 = res.data.adrs;
                        userAddr2 = res.data.dtlAdrs;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetCardPayInfo()
        {
            try
            {
                using (RequestCardPayInfoModel req = new RequestCardPayInfoModel())
                {
                    using (ResponseCardPayInfoModel res = await WebApiLib.AsyncCall<ResponseCardPayInfoModel, RequestCardPayInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            cardActCode = res.data.cardActCode;
                            actNo = res.data.actNo;
                            bankNm = res.data.bankNm;
                            cardReqPrc = res.data.cardReqPrc;
                            cardReqPrcOri = res.data.cardReqPrc;
                            deliveryFee = res.data.dlvr;

                            deliveryAddr = Localization.Resource.RechargeDepositWithdraw_Card_2_1_9 + string.Format(Localization.Resource.RechargeDepositWithdraw_Card_2_1_15, deliveryFee.ToString("#,#"), SC);
                            CmdSendCard();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 원화충전 리스트
        /// </summary>
        public async void GetKrwRecharge(EnumLib.PaymentWay SelectCode)
        {
            try
            {
                Messenger.Default.Send("AssetsRefresh");

                using (RequestKrwRechargeModel req = new RequestKrwRechargeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.payKndCd = StringEnum.GetStringValue(SelectCode);

                    using (ResponseKrwRechargeModel res = await WebApiLib.AsyncCall<ResponseKrwRechargeModel, RequestKrwRechargeModel>(req))
                    {
                        if (SelectCode.Equals(EnumLib.PaymentWay.prepaidCard))
                        {
                            for (int i = 0; i < res.data.list.Count; i++)
                            {
                                res.data.list[i].cardNum = CommonLib.CardNumChange(res.data.list[i].cardNum);
                            }
                            CardRechargeList = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
     
        public void CmdSearchAddr()
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //카드 등록 URL
        public void CmdCardRegedit()
        {
            try
            {
                //alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_15);
                //alert.ShowDialog();
                //return;

                if (CardReqList != null)
                {
                    if (CardReqList.Where(w => w.status == "01").Count() + CardReqList.Where(w => w.status == "02").Count() > 0)
                    {
                        alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_16);
                        alert.ShowDialog();
                        return;
                    }

                    //발송완료 상태가 1개라도 있으면
                    if (CardReqList.Where(w => w.status == "03").Count() > 0)
                    {
                        //등록완료가 1개 이상이면
                        if (CardReqList.Where(w => w.status == "04").Count() >= 1)
                        {
                            alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_9);
                            alert.ShowDialog();
                            return;
                        }
                    }

                    //발송완료 상태가 1개라도 있으면
                    if (CardReqList.Where(w => w.status == "03").Count() > 0)
                    {
                        //등록완료나 카드해재 기록이 있으면 카드추가등록
                        if (CardReqList.Where(w => w.status == "04").Count() > 0 || CardReqList.Where(w => w.status == "99").Count() > 0)
                        {
                            ProcessStartInfo info = new ProcessStartInfo();
                            info.FileName = "iexplore.exe";
                            info.UseShellExecute = true;
                            info.WindowStyle = ProcessWindowStyle.Maximized;
                            info.CreateNoWindow = true;
                            info.Arguments = "http://moacard.joy365.kr/card_addLogin.asp?uid=" + MainViewModel.LoginDataModel.userEmail;

                            Process process = new Process();
                            Process.Start(info);
                        }
                        //없으면 신규가입 및 카드등록
                        else
                        {
                            ProcessStartInfo info = new ProcessStartInfo();
                            info.FileName = "iexplore.exe";
                            info.UseShellExecute = true;
                            info.WindowStyle = ProcessWindowStyle.Maximized;
                            info.CreateNoWindow = true;
                            info.Arguments = "http://moacard.joy365.kr/Info.asp?Uid=" + MainViewModel.LoginDataModel.userEmail;

                            Process process = new Process();
                            Process.Start(info);
                        }
                    }
                    else
                    {
                        alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_14, 330);
                        alert.ShowDialog();
                    }
                }
                else
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_14, 330);
                    alert.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //PTM 비밀번호 등록
        public async void CmdAtmPwdSet()
        {
            try
            {
                using (RequestCardPwdCheckModel req = new RequestCardPwdCheckModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseCardPwdCheckModel res = await WebApiLib.AsyncCall<ResponseCardPwdCheckModel, RequestCardPwdCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd.Equals(""))
                            {
                                if (string.IsNullOrEmpty(res.data.atmPwd))
                                {
                                    AtmPwdSettingViewModel.CardNumStatic = res.data.cardNum == null ? "" : res.data.cardNum;
                                    AtmPwdSetting aps = new AtmPwdSetting();
                                    aps.ShowDialog();
                                }
                                else
                                {
                                    alert = new Alert(Localization.Resource.AtmPwdSetting_7 + "\n" + Localization.Resource.AtmPwdSetting_8, 330);
                                    alert.ShowDialog();
                                    return;
                                }
                            }
                            else if (res.data.failCd.Equals("998"))
                            {
                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_25, 330);
                                alert.ShowDialog();
                                return;
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

        public void CmdSendCard()
        {
            try
            {
                if (bOnline)
                {
                    DepositMth1Visible = Visibility.Visible;
                    DepositMth2Visible = Visibility.Collapsed;

                    cardReqPrc = cardReqPrcOri + deliveryFee;
                }
                else if (bOffline)
                {
                    DepositMth1Visible = Visibility.Collapsed;
                    DepositMth2Visible = Visibility.Visible;

                    cardReqPrc = cardReqPrcOri;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //선불카드 신청
        public async void CmdCardRequest()
        {
            try
            {
                alert = new Alert(Localization.Resource.Common_Alert_1);
                alert.ShowDialog();
                return;

                if (CardReqList.Where(w => w.status == "01").Count() + CardReqList.Where(w => w.status == "02").Count() + CardReqList.Where(w => w.status == "03").Count()  + CardReqList.Where(w => w.status == "04").Count()  + CardReqList.Where(w => w.status == "05").Count() >= 1)
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_9);
                    alert.ShowDialog();
                    return;
                }
                
                IsBusy = true;

                //타계정 선불카드 미리 체크.
                using (RequestCardCheckModel req = new RequestCardCheckModel())
                {
                    req.userMobile = userPhoneNumber;
                    req.userNm = userNm;

                    using (ResponseCardCheckModel res = await WebApiLib.AsyncCall<ResponseCardCheckModel, RequestCardCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd == "989")
                            {
                                alert = new Alert(Localization.Resource.CertifyMyPage_Alert_3);
                                alert.ShowDialog();
                                return;
                            }
                            else if (res.data.failCd == "987")
                            {
                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_17);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                }

                if (!userCountry.Equals("82"))
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_13);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(userPost))
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_1);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(userAddr1))
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_2);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(userAddr2))
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_3);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(userBirthDay.Trim()))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_1);
                    alert.ShowDialog();
                    return;
                }
                else if (!userBirthDay.Length.Equals(8))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_2);
                    alert.ShowDialog();
                    return;
                }
                else if (!BirthDayCheck())
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_2);
                    alert.ShowDialog();
                    return;
                }
                else if (!AdultCheck())
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_4);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(userPhoneNumber.Trim()))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_3);
                    alert.ShowDialog();
                    return;
                }

                else if (userPhoneOriNumber != userPhoneNumber)
                {
                    int phoneCheck = 0;

                    using (RequestPhoneNumberCheckModel req = new RequestPhoneNumberCheckModel())
                    {
                        req.userMobile = userPhoneNumber.Trim();

                        using (ResponsePhoneNumberCheckModel res = await WebApiLib.AsyncCall<ResponsePhoneNumberCheckModel, RequestPhoneNumberCheckModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                phoneCheck = res.data.result; // 0이면 중복아님, 1이면 중복

                                if (phoneCheck.Equals(1))
                                {
                                    alert = new Alert(Localization.Resource.MemberInfo_Pop_5);
                                    alert.ShowDialog();
                                    return;
                                }
                            }
                        }
                    }
                }

                string alertMsg = string.Empty;

                if (bOnline)
                {
                    alertMsg = Localization.Resource.RechargeDepositWithdraw_Card_Pop_18 + "\n" + Localization.Resource.RechargeDepositWithdraw_Card_Pop_21;                    
                }
                else if (bOffline)
                {
                    alertMsg = Localization.Resource.RechargeDepositWithdraw_Card_Pop_18 + "\n" + Localization.Resource.RechargeDepositWithdraw_Card_Pop_19 + "\n" + Localization.Resource.RechargeDepositWithdraw_Card_Pop_20;
                }

                alert = new Alert(alertMsg, Alert.ButtonType.YesNo, 400, 165);
                if (alert.ShowDialog() == true)
                {
                    //회원정보 저장
                    using (RequestUserInsUdtModel req = new RequestUserInsUdtModel())
                    {
                        req.userEmail = userEmail;
                        req.birthDay = userBirthDay;
                        req.userMobile = userPhoneNumber.Replace("-", "");
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseUserInsUdtModel res = await WebApiLib.AsyncCall<ResponseUserInsUdtModel, RequestUserInsUdtModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                //카드 신청
                                using (RequestCardReqModel req2 = new RequestCardReqModel())
                                {
                                    string payMtd = string.Empty;

                                    req2.userEmail = userEmail;
                                    req2.postCd = userPost;
                                    req2.adrs = userAddr1;
                                    req2.dtlAdrs = userAddr2;
                                    req2.amount = cardReqPrc;
                                    if (bOnline)
                                    {
                                        if (bCardPayment)
                                        {
                                            req2.inMthCd = "3";
                                        }
                                        else
                                        {
                                            req2.inMthCd = "4";
                                        }
                                        req2.sendMthCd = "2";
                                    }
                                    else if (bOffline)
                                    {
                                        req2.inMthCd = "2";
                                        req2.sendMthCd = "1";
                                    }

                                    req2.reqType = "1"; //온라인 고정
                                    req2.userMobile = userPhoneNumber;
                                    req2.userNm = userNm;                                    

                                    using (ResponseCardReqModel res2 = await WebApiLib.AsyncCall<ResponseCardReqModel, RequestCardReqModel>(req2))
                                    {
                                        if (res.resultStrCode == "000")
                                        {
                                            string resResult = res2.data.failCd;
                                            if (resResult.Equals("999"))
                                            {
                                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_4);
                                                alert.ShowDialog();
                                            }
                                            else if (resResult.Equals("998"))
                                            {
                                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_5);
                                                alert.ShowDialog();
                                            }
                                            else if (resResult.Equals("997"))
                                            {
                                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_6);
                                                alert.ShowDialog();
                                            }
                                            else if (resResult.Equals("996"))
                                            {
                                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_7);
                                                alert.ShowDialog();
                                            }
                                            else if (resResult.Equals("995"))
                                            {
                                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_8);
                                                alert.ShowDialog();
                                            }
                                            else if (resResult.Equals("993"))
                                            {
                                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_9);
                                                alert.ShowDialog();
                                            }
                                            else if (resResult.Equals("987"))
                                            {
                                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_17);
                                                alert.ShowDialog();
                                            }
                                            else
                                            {
                                                if (bOnline)
                                                {
                                                    if (bCardPayment)
                                                    {
                                                        payMtd = "3";
                                                    }
                                                    else
                                                    {
                                                        payMtd = "4";
                                                    }

                                                    DanalPay dp = new DanalPay(res2.data.orderId, userPhoneNumber.Replace("-", ""), payMtd);
                                                    dp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                                    if (dp.ShowDialog() == false)
                                                    {
                                                        using (RequestGetCardStatusModel req3 = new RequestGetCardStatusModel())
                                                        {
                                                            req3.orderId = res2.data.orderId;

                                                            using (ResponseGetCardStatusModel res3 = await WebApiLib.AsyncCall<ResponseGetCardStatusModel, RequestGetCardStatusModel>(req3))
                                                            {
                                                                if (res3.resultStrCode == "000")
                                                                {
                                                                    string status = res3.data.status;

                                                                    if (status.Equals("07"))
                                                                    {
                                                                        alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_4_3_8);
                                                                        alert.ShowDialog();
                                                                    }
                                                                    else
                                                                    {
                                                                        alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_10);
                                                                        alert.ShowDialog();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_10);
                                                    alert.ShowDialog();
                                                }

                                                GetUserInfo();
                                                bOnline = true;
                                                CmdSendCard();
                                                GetCardRequestList();
                                            }

                                            IsBusy = false;
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
            }
            finally
            {
                IsBusy = false;
            }
        }
        //선불카드 신청리스트
        public async void GetCardRequestList()
        {
            try
            {
                using (RequestCardReqContentModel req = new RequestCardReqContentModel())
                {
                    req.userEmail = userEmail;

                    using (ResponseCardReqContentModel res = await WebApiLib.AsyncCall<ResponseCardReqContentModel, RequestCardReqContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            for (int i = 1; i <= res.data.list.Count; i++)
                            {
                                ResponseCardReqContentListModel temp = res.data.list[i - 1];

                                temp.no = i;
                                temp.cardNum = CommonLib.CardNumChange(temp.cardNum);

                                if (temp.status == "01" || temp.status == "05")
                                {
                                    temp.cancelVisible = Visibility.Visible;
                                }
                                else
                                {
                                    temp.cancelVisible = Visibility.Collapsed;
                                }
                            }

                            CardReqList = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //선불카드 취소
        public async void CmdCardCancel(string orderId)
        {
            try
            {
                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_22, Alert.ButtonType.YesNo, 330);
                if (alert.ShowDialog() == true)
                {
                    using (RequestCardReqCancelModel req = new RequestCardReqCancelModel())
                    {
                        req.orderId = orderId;

                        using (ResponseCardReqCancelModel res = await WebApiLib.AsyncCall<ResponseCardReqCancelModel, RequestCardReqCancelModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                if (res.data.failCd == "997")
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_24);
                                    alert.ShowDialog();
                                }
                                else if (res.data.failCd == "998")
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_24);
                                    alert.ShowDialog();
                                }
                                else if (res.data.failCd == "999")
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_4);
                                    alert.ShowDialog();
                                }
                                else
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_23);
                                    alert.ShowDialog();
                                }

                                GetCardRequestList();
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
                            exRate = Math.Round(res.data.exRate, 2);
                            cryCode = res.data.cryCode;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public bool AdultCheck()
        {
            int sYear = int.Parse(userBirthDay.Substring(0, 4));
            int sMonth = int.Parse(userBirthDay.Substring(4, 2));
            int sDay = int.Parse(userBirthDay.Substring(6, 2));

            int cYear = DateTime.Now.Year;
            int cMonth = DateTime.Now.Month;
            int cDay = DateTime.Now.Day;

            int age = cYear - sYear;

            if (sMonth * 100 + sDay > cMonth * 100 + cDay)
            {
                age--;
            }

            if (age < 19)
            {
                return false;
            }

            return true;
        }

        public bool BirthDayCheck()
        {
            string sMonth = userBirthDay.Substring(4, 2);
            int iDay = int.Parse(userBirthDay.Substring(6, 2));

            int tempDay = 0;

            switch (sMonth)
            {
                case "01":
                case "03":
                case "05":
                case "07":
                case "08":
                case "10":
                case "12":
                    tempDay = 31;
                    break; ;
                case "04":
                case "06":
                case "09":
                case "11":
                    tempDay = 30;
                    break;
                case "02":
                    tempDay = 28;
                    break;
                default:
                    tempDay = 0;
                    break;
            }

            if (tempDay == 0)
            {
                return false;
            }
            if (tempDay < iDay)
            {
                return false;
            }

            return true;
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            img_text_usd_charge_notice = sLanguage + "img_text_usd_charge_notice.png";
            img_text_recent_exchange = sLanguage + "img_text_recent_exchange.png";
            btn_coupon_purchase = sLanguage + "btn_coupon_purchase.png";
            btn_coupon_purchase_on = sLanguage + "btn_coupon_purchase_on.png";
            img_text_coupon_use = sLanguage + "img_text_coupon_use.png";
            btn_coupon_use = sLanguage + "btn_coupon_use.png";
            btn_coupon_use_on = sLanguage + "btn_coupon_use_on.png";
            img_text_coupon_record = sLanguage + "img_text_coupon_record.png";
            btn_regist_card = sLanguage + "btn_regist_card.png";
            btn_regist_card_on = sLanguage + "btn_regist_card_on.png";
            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
            btn_auto_request = sLanguage + "btn_auto_request.png";
            btn_auto_request_on = sLanguage + "btn_auto_request_on.png";
            btn_card_charging = sLanguage + "btn_card_charging.png";
            btn_card_charging_on = sLanguage + "btn_card_charging_on.png";
            img_process_card = sLanguage + "img_process_card.png";
            btn_regist_atm = sLanguage + "btn_regist_atm.png";
            btn_regist_atm_on = sLanguage + "btn_regist_atm_on.png";
        }
    }

    public class BankList
    {
        public virtual string bankNm { get; set; }
        public virtual string bankCd { get; set; }
        public virtual string accNo { get; set; }
        public virtual string accNm { get; set; }
    }
}