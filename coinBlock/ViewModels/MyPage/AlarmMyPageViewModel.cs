using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using coinBlock.Model.MyPage;
using coinBlock.Utility;
using coinBlock.Views;
using System.Linq;
using coinBlock.Model;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class AlarmMyPageViewModel
    {
        public virtual ObservableCollection<ResponseAlarmListModel> AlarmList { get; set; }
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

        public virtual string btcPrc { get; set; }
        public virtual string ethPrc { get; set; }
        public virtual string bchPrc { get; set; }
        public virtual string xrpPrc { get; set; }
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;

        public virtual bool basicCheck { get; set; } = true;
        public virtual bool basicEnabled { get; set; } = true;
        public virtual bool bLogin { get; set; }
        public virtual bool bNotice { get; set; }
        public virtual bool bAdditional { get; set; }
        public virtual bool bQna { get; set; }
        public virtual bool bBuyOrder { get; set; }
        public virtual bool bBuyComp { get; set; }
        public virtual bool bSellOrder { get; set; }
        public virtual bool bSellComp { get; set; }
        public virtual bool bCoinDeposit { get; set; }
        public virtual bool bCoinTrasnfer { get; set; }
        public virtual bool bRecharge { get; set; }
        public virtual bool bWithdrawl { get; set; }

        public virtual bool timeCheck { get; set; }
        public virtual bool timeEnabled { get; set; }
        public virtual bool tBTC { get; set; }
        public virtual bool tETH { get; set; }
        public virtual bool tBCH { get; set; }
        public virtual bool tXRP { get; set; }
        public virtual bool tERB { get; set; }
        //public virtual bool tSGC { get; set; }
        //public virtual bool tQTUM { get; set; }
        //public virtual bool tBTG { get; set; }
        //public virtual bool tDASH { get; set; }
        //public virtual bool tLTC { get; set; }        
        //public virtual bool tBMC { get; set; }
        //public virtual bool tSECRET { get; set; }
        //public virtual bool tHDAC { get; set; }
        //public virtual bool tVSTC { get; set; }
        //public virtual bool tADM { get; set; }
        //public virtual bool tBSV { get; set; }
        //public virtual bool tTRX { get; set; }
        //public virtual bool tXLM { get; set; }        
        //public virtual bool tWAVES { get; set; }
        //public virtual bool tPOLY { get; set; }
        //public virtual bool tOMG { get; set; }
        //public virtual bool tMITH { get; set; }
        //public virtual bool tICX { get; set; }
        //public virtual bool tXEM { get; set; }
        //public virtual bool tPAN { get; set; }
        //public virtual bool tXLAB { get; set; }
        //public virtual bool tHEC { get; set; }
        //public virtual bool tGIFO { get; set; }
        //public virtual bool tHAMA { get; set; }


        public virtual ObservableCollection<ComboBoxData> FromTime { get; set; }
        public virtual ObservableCollection<ComboBoxData> ToTime { get; set; }
        public virtual ObservableCollection<ComboBoxData> IntervalTime { get; set; }
        public virtual ComboBoxData SelFrom { get; set; }
        public virtual ComboBoxData SelTo { get; set; }
        public virtual ComboBoxData SelInterval { get; set; }

        public virtual string depositTxt { get; set; } = string.Format(Localization.Resource.AlramMyPage_2_4_3, CommonLib.StandardCurcyNm);
        public virtual string withdrawTxt { get; set; } = string.Format(Localization.Resource.AlramMyPage_2_4_4, CommonLib.StandardCurcyNm);

        //public virtual bool costCheck { get; set; }
        //public virtual bool costEnabled { get; set; }
        //public virtual string cBtcHighPrc { get; set; }
        //public virtual string cBtcLowPrc { get; set; }
        //public virtual string cEthHighPrc { get; set; }
        //public virtual string cEthLowPrc { get; set; }
        //public virtual string cBchHighPrc { get; set; }
        //public virtual string cBchLowPrc { get; set; }
        //public virtual string cXrpHighPrc { get; set; }
        //public virtual string cXrpLowPrc { get; set; }
        //public virtual bool cOneTime { get; set; }
        //public virtual bool cRepeatTime { get; set; } = true;
        //public virtual bool cHalfTime { get; set; } = true;
        //public virtual bool cHourTime { get; set; }

        public virtual string img_text_basic_alarm { get; set; }
        public virtual string img_text_time { get; set; }
        public virtual string img_text_cost { get; set; }
        public virtual string btn_alarm_save { get; set; }
        public virtual string btn_alarm_save_on { get; set; }
        public virtual string btn_more { get; set; }
        public virtual string btn_more_on { get; set; }

        int listCount { get; set; } = 15;
        public virtual bool IsBusy { get; set; }

        Alert alert = null;// new Alert();

        public static AlarmMyPageViewModel Create()
        {
            return ViewModelSource.Create(() => new AlarmMyPageViewModel());
        }

        protected AlarmMyPageViewModel()
        {

        }

        public void Loaded()
        {
            try
            {
                //foreach (var item in MainViewModel.WebSock.CoinInfoData)
                //{
                //    if (item.Value.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC)))
                //    {
                //        btcPrc = item.Value.nowPrc.ToString("###,###,###,##0");
                //    }
                //    else if (item.Value.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.ETH)))
                //    {
                //        ethPrc = item.Value.nowPrc.ToString("###,###,###,##0");
                //    }
                //    else if (item.Value.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BCH)))
                //    {
                //        bchPrc = item.Value.nowPrc.ToString("###,###,###,##0");
                //    }
                //    else if (item.Value.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.XRP)))
                //    {
                //        xrpPrc = item.Value.nowPrc.ToString("###,###,###,##0");
                //    }
                //}

                #region Combobox초기화

                FromTime = new ObservableCollection<ComboBoxData>();
                ToTime = new ObservableCollection<ComboBoxData>();
                IntervalTime = new ObservableCollection<ComboBoxData>();

                for (int i = 0; i <= 23; i++)
                {
                    FromTime.Add(new ComboBoxData() { Name = i.ToString(), Value = i });
                    ToTime.Add(new ComboBoxData() { Name = i.ToString(), Value = i });
                    if (i < 12)
                    {
                        IntervalTime.Add(new ComboBoxData() { Name = (i + 1).ToString(), Value = i + 1 });
                    }
                }

                SelFrom = FromTime[9];
                SelTo = ToTime[21];
                SelInterval = IntervalTime[0];

                #endregion
                listCount = 15;
                ImageSet();
                CmdAlarmGetting();
                SearchAlarmList();
            
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

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdMoreList()
        {
            try
            {
                listCount += 5;
                SearchAlarmList();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void SearchAlarmList()
        {
            try
            {
                using (RequestAlarmModel req = new RequestAlarmModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.pageUnit = listCount;

                    using (ResponseAlarmModel res = await WebApiLib.AsyncCall<ResponseAlarmModel, RequestAlarmModel>(req))
                    {

                        for (int i = 1; i <= res.data.list.Count; i++)
                        {
                            res.data.list[i - 1].rowNo = i.ToString();
                            if(res.data.list[i-1].pushType.Equals("CMMC00000000290") || res.data.list[i - 1].pushType.Equals("CMMC00000000291") || res.data.list[i - 1].pushType.Equals("CMMC00000001745"))
                            {
                                res.data.list[i - 1].contentVisible = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                res.data.list[i - 1].contentVisible = System.Windows.Visibility.Visible;
                            }
                        }
                        AlarmList = res.data.list;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdMyPageMove(string val)
        {
            try
            {
                //if (val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.Price)) || val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.Time))) //금액별, 시간대별
                //{
                //    Messenger.Default.Send(new MenuModel() {Name = LocalizationLib.GetLocalizaionString(item.CoinCode), Id = "CoinTrading", IconPath = "/Images/ico_nav_" + item.CoinName + ".png", Param = item,Certify = 1 });

                //    Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_2_1"), Id = "CoinTrading", IconPath = "/Images/ico_nav_btc.png", Param = EnumLib.ExchangeCurrencyCode.BTC, Certify = 1 });
                //}
                //else 
                if (val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.Question))) // 1:1문의
                {
                    Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_3"), Id = "QnaHelpDesk", IconPath = "/Images/ico_nav_qna.png" });
                }
                else if (val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.Addition)) || val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.DepositDrawWith)) || val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.BuySell))) //부가서비스, 입출금, 구매/판매
                {
                    Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_5"), Id = "TradingHistoryMyPage", IconPath = "/Images/ico_nav_exchage_list.png" });
                }
                else if (val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.Notice))) //공지사항
                {
                    Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_1"), Id = "NoticeHelpDesk", IconPath = "/Images/ico_nav_notice.png" });
                }
                else if (val.Equals(StringEnum.GetStringValue(EnumLib.PushCode.Login))) // 로그인
                {
                    Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_3"), Id = "LoginHistoryMyPage", IconPath = "/Images/ico_nav_connect_info.png" });
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #region 토글 체크
        //기본 알림
        public void CmdBasicCheck(bool bCheck)
        {
            basicCheck = bCheck;
            basicEnabled = bCheck;
        }
        //시간 알림
        public void CmdTimeCheck(bool bCheck)
        {
            timeCheck = bCheck;
            timeEnabled = bCheck;
        }
        //시세 알림
        //public void CmdCostCheck(bool bCheck)
        //{
        //    costCheck = bCheck;
        //    costEnabled = bCheck;
        //}

        #endregion

        private bool boolCheck(string sValue)
        {
            if (sValue.Equals("Y"))
                return true;
            else
                return false;
        }
        private string ynCheck(bool bValue)
        {
            if (bValue.Equals(true))
                return "Y";
            else
                return "N";
        }

        public async void CmdAlarmGetting()
        {
            //API 호출
            try
            {
                using (RequestAlarmContentModel req = new RequestAlarmContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseAlarmContentModel res = await WebApiLib.AsyncCall<ResponseAlarmContentModel, RequestAlarmContentModel>(req))
                    {
                        ResponseAlarmContentDataModel tempData = res.data;

                        basicCheck = boolCheck(tempData.alrmBsYn);
                        CmdBasicCheck(basicCheck);
                        bLogin = boolCheck(tempData.bsBs01);
                        bNotice = boolCheck(tempData.bsBs02);
                        bAdditional = boolCheck(tempData.bsBs03);
                        bQna = boolCheck(tempData.bsBs04);
                        bBuyOrder = boolCheck(tempData.bsSb01);
                        bBuyComp = boolCheck(tempData.bsSb02);
                        bSellOrder = boolCheck(tempData.bsSb03);
                        bSellComp = boolCheck(tempData.bsSb04);
                        bCoinDeposit = boolCheck(tempData.bsIo01);
                        bCoinTrasnfer = boolCheck(tempData.bsIo02);
                        bRecharge = boolCheck(tempData.bsIo03);
                        bWithdrawl = boolCheck(tempData.bsIo04);

                        timeCheck = boolCheck(tempData.alrmTmYn);
                        CmdTimeCheck(timeCheck);
                        tBTC = boolCheck(tempData.tmCoin01);
                        tETH = boolCheck(tempData.tmCoin02);
                        tBCH = boolCheck(tempData.tmCoin03);
                        tXRP = boolCheck(tempData.tmCoin04);
                        tERB = boolCheck(tempData.tmCoin05);
                        //tQTUM = boolCheck(tempData.tmCoin05);                        
                        //tBTG = boolCheck(tempData.tmCoin06);
                        //tDASH = boolCheck(tempData.tmCoin07);
                        //tLTC = boolCheck(tempData.tmCoin08);
                        //tSECRET = boolCheck(tempData.tmCoin09);
                        //tHDAC = boolCheck(tempData.tmCoin10);
                        //tSGC = boolCheck(tempData.tmCoin11);
                        //tVSTC = boolCheck(tempData.tmCoin12);
                        //tADM = boolCheck(tempData.tmCoin13);
                        //tBSV = boolCheck(tempData.tmCoin14);
                        //tTRX = boolCheck(tempData.tmCoin15);
                        //tXLM = boolCheck(tempData.tmCoin16);                        
                        //tWAVES = boolCheck(tempData.tmCoin17);
                        //tPOLY = boolCheck(tempData.tmCoin18);
                        //tOMG = boolCheck(tempData.tmCoin19);
                        //tMITH = boolCheck(tempData.tmCoin20);
                        //tICX = boolCheck(tempData.tmCoin21);
                        //tXEM = boolCheck(tempData.tmCoin22);
                        //tPAN = boolCheck(tempData.tmCoin23);
                        //tXLAB = boolCheck(tempData.tmCoin24);
                        //tHEC = boolCheck(tempData.tmCoin25);
                        //tGIFO = boolCheck(tempData.tmCoin26);
                        //tHAMA = boolCheck(tempData.tmCoin27);

                        if (tempData.sttHh.Equals(0) && tempData.endHh.Equals(0) && tempData.termHh.Equals(0))
                        {
                            SelFrom = FromTime[9];
                            SelTo = ToTime[21];
                            SelInterval = IntervalTime[0];
                        }
                        else
                        {
                            SelFrom = FromTime[tempData.sttHh];
                            SelTo = ToTime[tempData.endHh];
                            SelInterval = IntervalTime[tempData.termHh - 1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }

        }

        public async void CmdAlarmSetting()
        {
            try
            {
                if (timeCheck)
                {
                    if (SelFrom.Value > SelTo.Value)
                    {
                        alert = new Alert(Localization.Resource.AlramMyPage_Alert_1, 350);
                        alert.ShowDialog();
                        return;
                    }

                    if (SelInterval.Value > (SelTo.Value - SelFrom.Value))
                    {
                        alert = new Alert(Localization.Resource.AlramMyPage_Alert_2, 350);
                        alert.ShowDialog();
                        return;
                    }
                }

                IsBusy = true;

                using (RequestAlarmSetModel req = new RequestAlarmSetModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.alrmBsYn = ynCheck(basicCheck);
                    req.bsBs01 = ynCheck(bLogin);
                    req.bsBs02 = ynCheck(bNotice);
                    req.bsBs03 = ynCheck(bAdditional);
                    req.bsBs04 = ynCheck(bQna);
                    req.bsSb01 = ynCheck(bBuyOrder);
                    req.bsSb02 = ynCheck(bBuyComp);
                    req.bsSb03 = ynCheck(bSellOrder);
                    req.bsSb04 = ynCheck(bSellComp);
                    req.bsIo01 = ynCheck(bCoinDeposit);
                    req.bsIo02 = ynCheck(bCoinTrasnfer);
                    req.bsIo03 = ynCheck(bRecharge);
                    req.bsIo04 = ynCheck(bWithdrawl);
                    req.alrmTmYn = ynCheck(timeCheck);

                    req.tmCoin01 = ynCheck(tBTC);
                    req.tmCoin02 = ynCheck(tETH);
                    req.tmCoin03 = ynCheck(tBCH);
                    req.tmCoin04 = ynCheck(tXRP);
                    req.tmCoin05 = ynCheck(tERB);
                    //req.tmCoin05 = ynCheck(tQTUM);
                    //req.tmCoin06 = ynCheck(tBTG);
                    //req.tmCoin07 = ynCheck(tDASH);
                    //req.tmCoin08 = ynCheck(tLTC);
                    //req.tmCoin09 = ynCheck(tSECRET);
                    //req.tmCoin10 = ynCheck(tHDAC);
                    //req.tmCoin11 = ynCheck(tSGC);
                    //req.tmCoin12 = ynCheck(tVSTC);
                    //req.tmCoin13 = ynCheck(tADM);
                    //req.tmCoin14 = ynCheck(tBSV);
                    //req.tmCoin15 = ynCheck(tTRX);
                    //req.tmCoin16 = ynCheck(tXLM);
                    //req.tmCoin17 = ynCheck(tWAVES);
                    //req.tmCoin18 = ynCheck(tPOLY);
                    //req.tmCoin19 = ynCheck(tOMG);
                    //req.tmCoin20 = ynCheck(tMITH);
                    //req.tmCoin21 = ynCheck(tICX);
                    //req.tmCoin22 = ynCheck(tXEM);
                    //req.tmCoin23 = ynCheck(tPAN);
                    //req.tmCoin24 = ynCheck(tXLAB);
                    //req.tmCoin25 = ynCheck(tHEC);
                    //req.tmCoin26 = ynCheck(tGIFO);
                    //req.tmCoin27 = ynCheck(tHAMA);


                    req.sttHh = SelFrom.Value;
                    req.endHh = SelTo.Value;
                    req.termHh = SelInterval.Value;
                    req.regIp = MainViewModel.LoginDataModel.regIp;

                    using (ResponseAlarmSetModel res = await WebApiLib.AsyncCall<ResponseAlarmSetModel, RequestAlarmSetModel>(req))
                    {
                        alert = new Alert(Localization.Resource.Common_Alert_2);
                        alert.ShowDialog();
                        IsBusy = false;
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

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            img_text_basic_alarm = sLanguage + "img_text_basic_alarm.png";
            img_text_time = sLanguage + "img_text_time.png";
            img_text_cost = sLanguage + "img_text_cost.png";
            btn_alarm_save = sLanguage + "btn_alarm_save.png";
            btn_alarm_save_on = sLanguage + "btn_alarm_save_on.png";
            btn_more = sLanguage + "btn_more.png";
            btn_more_on = sLanguage + "btn_more_on.png";
        }
    }
}