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
using System.Collections.Generic;

namespace coinBlock.ViewModels.DepositWithdraw
{
    [POCOViewModel]
    public class RechargeDepositWithdrawViewModel 
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

        DispatcherTimer RepeatTimer;

        public virtual ObservableCollection<BankList> BankListAll { get; set; }
        public virtual ObservableCollection<CommonCombobox> RechargeBankList { get; set; }
        public virtual CommonCombobox SelectedRechargeBank { get; set; }
        public virtual ObservableCollection<ResponseCardReqContentListModel> CardReqList { get; set; }
        public virtual ObservableCollection<ComboBoxStrData> cardList { get; set; }
        public virtual ComboBoxStrData SelCard { get; set; }
        public virtual ObservableCollection<ResponseKrwRechargeListModel> KrwRechargeList { get; set; }
        public virtual ObservableCollection<ResponseKrwRechargeListModel> CardRechargeList { get; set; }
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;

        bool IsLoad = true;

        /// <summary>
        /// 입금은행
        /// </summary>
        public virtual string BankNm { get; set; }
        /// <summary>
        /// 계좌번호
        /// </summary>
        public virtual string AccNo { get; set; }
        /// <summary>
        /// 예금주
        /// </summary>
        public virtual string AccNm { get; set; }
        /// <summary>
        /// 입금자명(적요)
        /// </summary>
        public virtual string DepositNm { get; set; }     

        public virtual string cardActCode { get; set; }
        public virtual string actNo { get; set; }
        public virtual string bankNm { get; set; }
        public virtual decimal cardReqPrc { get; set; }
        public virtual decimal cardReqOriPrc { get; set; }

        public virtual string cardMonth { get; set; } = string.Empty;
        public virtual string cardYear { get; set; } = string.Empty;
        public virtual string cardCvc { get; set; } = string.Empty;
        public virtual string cardPw { get; set; } = string.Empty;
        public virtual decimal cardRemPrc { get; set; } 
        public virtual decimal RechargePrc { get; set; }        
        public virtual int fee { get; set; }
        public virtual decimal exRate { get; set; }
        public virtual string cryCode { get; set; }
        public virtual decimal ResultPrc { get; set; }
        public virtual string bankNo { get; set; }
        public virtual int idx { get; set; } = 0;

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

        public virtual Visibility langVisible { get; set; }

        public virtual bool IsBusy { get; set; }

        public virtual string rechargeHeader { get; set; }

        Alert alert = null;// new Alert();

        #region 선불카드

        public virtual string userNm { get; set; } = MainViewModel.LoginDataModel.userNm;
        public virtual string userEmail { get; set; } = MainViewModel.LoginDataModel.userEmail;
        public virtual string userPhoneNumber { get; set; }
        public virtual string userAccountNo { get; set; }

        public virtual string DepositMth { get; set; }
        public virtual Visibility DepositMth1Visible { get; set; } = Visibility.Visible;
        public virtual Visibility DepositMth2Visible { get; set; } = Visibility.Collapsed;

        public virtual bool bCardPayment { get; set; } = true;
        public virtual bool bAccountPayment { get; set; }

        public virtual bool bOnline { get; set; } = true;
        public virtual bool bOffline { get; set; }

        public virtual int hideHeight { get; set; } = 240;
        public virtual string hideRow { get; set; } = "1*;";

        #endregion

        public static RechargeDepositWithdrawViewModel Create()
        {
            return ViewModelSource.Create(() => new RechargeDepositWithdrawViewModel());
        }

        protected RechargeDepositWithdrawViewModel()
        {
            try
            {
                rechargeHeader = string.Format(Localization.Resource.RechargeDepositWithdraw_Krw_1_1, SC);

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 5, 0);

                Messenger.Default.Register<string>(this, CallCardList);

                ImageSet();
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
               
                GetKrwRecharge(EnumLib.PaymentWay.accountTransfer);
                GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
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
             
                //if (idx.Equals(0))
                //{
                //    GetCardList();
                //    GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
                //}
                //else
                //{                    
                GetAccount();
                GetKrwRecharge(EnumLib.PaymentWay.accountTransfer);

                GetUserInfo();

                if (string.IsNullOrEmpty(userAccountNo.Trim()) || string.IsNullOrEmpty(userPhoneNumber.Trim()))
                {
                    //idx = 0;
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_25, 350);
                    alert.ShowDialog();
                }
                else
                {
                    NoticePopup note = new Views.NoticePopup(NoticePopup.KindNotice.ReCharge);
                    note.Title = string.Format(Localization.Resource.NoticePopup_2, SC);
                    note.ShowDialog();
                }

                //}

                RepeatTimer.Start();

                if (LoginViewModel.LanguagePack.IndexOf("ko") > 0)
                {
                    langVisible = Visibility.Visible;
                }
                else
                {
                    langVisible = Visibility.Collapsed;
                }

                //if (string.IsNullOrEmpty(userAccountNo.Trim()) || string.IsNullOrEmpty(userPhoneNumber.Trim()))
                //{
                //    idx = 0;
                //}
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
                //SelCard = cardList[0];
                RechargePrc = 0;
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
                        GetCardList();
                        GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetAccount()
        {
            try
            {
                using (RequestGetAccountInfoModel req = new RequestGetAccountInfoModel())
                {                   
                    using (ResponseGetAccountInfoModel res = await WebApiLib.AsyncCall<ResponseGetAccountInfoModel, RequestGetAccountInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            RechargeBankList = new ObservableCollection<CommonCombobox>();
                            RechargeBankList.Add(new CommonCombobox { Name = Localization.Resource.Common_Alert_16, Value = "00" });
                            BankListAll = new ObservableCollection<DepositWithdraw.BankList>();
                            foreach (ResponseGetAccountInfoListModel item in res.data.list)
                            {
                                RechargeBankList.Add(new CommonCombobox { Name = item.bankNm, Value = item.bankCd });
                                BankListAll.Add(new BankList { bankNm = item.bankNm, bankCd = item.bankCd, accNo = item.accNo, accNm = item.accNm });                                
                            }      
                            
                            if(RechargeBankList.Count > 0)
                            {
                                SelectedRechargeBank = RechargeBankList[0];
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

        public void OnSelectedRechargeBankChanged()
        {
            try
            {
                //if (SelectedRechargeBank.Value != "")
                //{

                if (RechargeBankList.Count > 0)
                {
                    if (SelectedRechargeBank.Value != "")
                    {
                        BankList temp = BankListAll.Where(x => x.bankCd == SelectedRechargeBank.Value).FirstOrDefault();

                        if (temp != null)
                        {
                            BankNm = temp.bankNm;
                            AccNo = temp.accNo;
                            AccNm = temp.accNm;
                        }
                        else
                        {
                            BankNm = string.Empty;
                            AccNo = string.Empty;
                            AccNm = string.Empty;
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void OnSelCardChanged()
        {
            try
            {
                 if (SelCard != null)
                {
                    if (SelCard.Value.Equals("00"))
                    {
                        cardRemPrc = 0;
                        return;
                    }

                    Dictionary<string, string> dict = null;

                    IsBusy = true;

                    using (RequestPublicKeyModel req = new RequestPublicKeyModel())
                    {
                        using (ResponsePublicKeyModel res = WebApiLib.SyncCall<ResponsePublicKeyModel, RequestPublicKeyModel>(req))
                        {
                            //dict = EncodingLib.AesEncrypt(SelCard.Value, res.data.pubKeyModule, res.data.pubKeyExponent);
                            dict = EncodingLib.AesEncryptKey(res.data.pubKeyModule, res.data.pubKeyExponent);
                        }
                    }

                    using (RequestCardRemPrcModel req = new RequestCardRemPrcModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cardNum = EncodingLib.AesEncrypt(SelCard.Value, dict["gid"]);
                        req.clientPe = dict["acekey"];

                        using (ResponseCardRemPrcModel res = await WebApiLib.AsyncCall<ResponseCardRemPrcModel, RequestCardRemPrcModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string resultCd = res.data.failCd;
                                if (resultCd.Equals("999"))
                                {
                                    SelCard = cardList[0];
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_4);
                                    alert.ShowDialog();
                                    return;
                                }
                                else if (resultCd.Equals("998"))
                                {
                                    SelCard = cardList[0];
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_11);
                                    alert.ShowDialog();
                                    return;
                                }
                                else if (resultCd.Equals("997"))
                                {
                                    SelCard = cardList[0];
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_12);
                                    alert.ShowDialog();
                                    return;
                                }

                                cardRemPrc = res.data.remPrc;
                                //if (string.IsNullOrWhiteSpace(res.data.bankNo))
                                //{
                                //    bankNo = "-";
                                //}
                                //else
                                //{
                                //    string bankNm = string.Empty;    
                                //    if (LoginViewModel.LanguagePack.IndexOf("ko") > 0)
                                //    {
                                //        bankNm = "(신한은행)";
                                //    }
                                //    else
                                //    {
                                //        bankNm = "(ShinHan Bank)";
                                //    }

                                //    bankNo = res.data.bankNo + bankNm;
                                //}
                            }

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

        public void OnRechargePrcChanged()
        {
            try
            {
                //fee = (int)(RechargePrc * (decimal)0.05);
                //ResultPrc = Math.Truncate((RechargePrc - (decimal)fee));
                fee = 0;
                ResultPrc = RechargePrc;
                //ResultPrc = Math.Round((RechargePrc - (decimal)fee) / exRate, 2);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnidxChanged()
        {
            try
            {
                //if (idx.Equals(0))
                //{
                //    GetCardList();
                //    GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
                //}
                //else
                //{
                    GetAccount();
                    GetKrwRecharge(EnumLib.PaymentWay.accountTransfer);
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdTabSelected(DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e)
        {
            try
            {
                if(e.NewSelectedIndex.Equals(1))
                {
                    GetUserInfo();

                    if (string.IsNullOrEmpty(userAccountNo.Trim()) || string.IsNullOrEmpty(userPhoneNumber.Trim()))
                    {
                        //idx = 0;
                        alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_25, 350);
                        alert.ShowDialog();                        
                    }
                    else
                    {
                        NoticePopup note = new Views.NoticePopup(NoticePopup.KindNotice.ReCharge);
                        note.Title = string.Format(Localization.Resource.NoticePopup_2, SC);
                        note.ShowDialog();
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
                IsBusy = true;

                using (RequestAccInfoModel req = new RequestAccInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseAccInfoModel res = WebApiLib.SyncCall<ResponseAccInfoModel, RequestAccInfoModel>(req))
                    {
                        userAccountNo = res.data.BankAccNo;

                        userPhoneNumber = res.data.userMobile;

                        if (userPhoneNumber.Equals(string.Empty) || userPhoneNumber.Equals("00000000000"))
                        {
                            DepositNm = MainViewModel.LoginDataModel.userNm;
                        }
                        else
                        {
                            DepositNm = MainViewModel.LoginDataModel.userNm + userPhoneNumber.Substring(userPhoneNumber.Length - 4, 4);
                        }

                        if (DepositNm.Length >= 7)
                        {
                            DepositNm = DepositNm.Substring(0, 7);
                        }
                    }
                }

                IsBusy = false;
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

        public async void GetCardPayInfo()
        {
            try
            {

                using (RequestCardPayInfoModel req = new RequestCardPayInfoModel())
                {
                    using (ResponseCardPayInfoModel res = await WebApiLib.AsyncCall<ResponseCardPayInfoModel, RequestCardPayInfoModel>(req))
                    {
                        if(res.resultStrCode == "000")
                        {
                            cardActCode = res.data.cardActCode;
                            actNo = res.data.actNo;
                            bankNm = res.data.bankNm;
                            cardReqPrc = res.data.cardReqPrc;
                            cardReqOriPrc = res.data.cardReqPrc;

                            if (bOnline)
                            {
                                cardReqPrc = cardReqOriPrc + 1500;
                            }
                            else
                            {
                                cardReqPrc = cardReqOriPrc;
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

        public void CmdCardCompanyToPage()
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "iexplore.exe";
                info.UseShellExecute = true;
                info.WindowStyle = ProcessWindowStyle.Maximized;
                info.CreateNoWindow = true;
                info.Arguments = "http://moacard.joy365.kr/";

                Process process = new Process();
                Process.Start(info);
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
                        if (SelectCode.Equals(EnumLib.PaymentWay.accountTransfer))
                        {
                            KrwRechargeList = res.data.list;
                        }
                        else if (SelectCode.Equals(EnumLib.PaymentWay.prepaidCard))
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
                                SelCard = cardList[0];
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
        //선불카드 충전
        public async void CmdCardCharging()
        {
            try
            {
                //alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_15);
                //alert.ShowDialog();
                //return;

                IsBusy = true;

                if (SelCard.Value.Equals("00"))
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_10, 330);
                    alert.ShowDialog();
                    return;
                }
                else if(cardMonth.Length != 2)
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_3, 330);
                    alert.ShowDialog();
                    return;
                }
                else if (cardYear.Length != 2)
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_4, 330);
                    alert.ShowDialog();
                    return;
                }
                else if (cardCvc.Length != 3)
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_5, 330);
                    alert.ShowDialog();
                    return;
                }
                else if (cardPw.Length != 4)
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_6, 330);
                    alert.ShowDialog();
                    return;
                }
                else if (cardRemPrc < RechargePrc)
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_7, 330);
                    alert.ShowDialog();
                    return;
                }
                else if(RechargePrc < 5000)
                {
                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_22, 330);
                    alert.ShowDialog();
                    return;
                }
                else if (RechargePrc > 2000000)
                {
                    alert = new Alert(string.Format(Localization.Resource.RechargeDepositWithdraw_Common_29, decimal.Parse("2000000").ToString("#,#"), SC), 330);
                    alert.ShowDialog();
                    return;
                }

                //GetExRate();
                OnRechargePrcChanged();

                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_8, Alert.ButtonType.YesNo, 330);
                if(alert.ShowDialog() == true)
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();

                    using (RequestPublicKeyModel req2 = new RequestPublicKeyModel())
                    {
                        using (ResponsePublicKeyModel res2 = WebApiLib.SyncCall<ResponsePublicKeyModel, RequestPublicKeyModel>(req2))
                        {
                            dict = EncodingLib.AesEncryptKey(res2.data.pubKeyModule, res2.data.pubKeyExponent);
                        }
                    }

                    using (RequestDepositCardModel req = new RequestDepositCardModel())
                    {                  
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.crgPrc = RechargePrc;
                        req.cardNum = EncodingLib.AesEncrypt(SelCard.Value, dict["gid"]);
                        req.cardDate = EncodingLib.AesEncrypt(("20" + cardYear + cardMonth), dict["gid"]);
                        req.cardCvc = EncodingLib.AesEncrypt(cardCvc, dict["gid"]);
                        req.cardPwd = EncodingLib.AesEncrypt(cardPw, dict["gid"]);
                        req.clientPe = dict["acekey"];

                        using (ResponseDepositCardModel res = await WebApiLib.AsyncCall<ResponseDepositCardModel, RequestDepositCardModel>(req))
                        {
                            if(res.resultStrCode == "000")
                            {
                                string resultCd = res.data.failCd;
                                if (resultCd.Equals("999"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_12, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("998"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_13, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("997"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_14, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("996"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_15, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("995"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_16, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("994"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_17, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("993"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_18, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("992"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_12, 330);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("991"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_20, 330);
                                    alert.ShowDialog();
                                }
                                else
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_9, 330);
                                    alert.ShowDialog();

                                    SelCard = cardList[0];
                                    cardMonth = string.Empty;
                                    cardYear = string.Empty;
                                    cardCvc = string.Empty;
                                    cardPw = string.Empty;
                                    cardRemPrc = 0;
                                    RechargePrc = 0;
                                    fee = 0;
                                    ResultPrc = 0;
                                    
                                    GetCardList();
                                    GetKrwRecharge(EnumLib.PaymentWay.prepaidCard);
                                    //GetExRate();                       
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
        }
    }
}