using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows.Threading;
using System.Windows.Forms;
using coinBlock.Views;
using coinBlock.Utility;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using coinBlock.Model;
using coinBlock.Model.Trading;
using System.Linq;
using coinBlock.Model.AdditionalService;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class CoinExchangeReservationTradingViewModel
    {
        DispatcherTimer RepeatTimer;
        Alert alert = null;// new Alert();
        public virtual ObservableCollection<ComboBoxCoinData> SelCoinData { get; set; }
        public virtual ObservableCollection<ComboBoxCoinData> ChgCoinData { get; set; }
        public virtual ComboBoxCoinData SelCoin { get; set; }
        public virtual ComboBoxCoinData ChgCoin { get; set; }

        public virtual string SC { get; } = CommonLib.StandardCurcyNm;
        public virtual decimal btcPrc { get; set; }
        public virtual decimal ethPrc { get; set; }
        public virtual decimal bchPrc { get; set; }
        public virtual decimal xrpPrc { get; set; }

        public virtual decimal coinSelPrc { get; set; }
        public virtual decimal nowSelPrc { get; set; }
        public virtual decimal coinChgPrc { get; set; }
        public virtual decimal nowChgPrc { get; set; }

        public virtual decimal coinSelAmt { get; set; }
        public virtual decimal coinChgAmt { get; set; }

        public virtual decimal sellCoinAmt { get; set; }
        public virtual decimal sellCoinAmt_Exe { get; set; }

        public virtual double SelSliderValue { get; set; } = 0;
        public virtual double ChgSliderValue { get; set; } = 0;

        public virtual string btn_auto_alarm_save { get; set; }
        public virtual string btn_auto_alarm_save_on { get; set; }
        public virtual string btn_auto_alarm_cancel { get; set; }
        public virtual string btn_auto_alarm_cancel_on { get; set; }
        public virtual string btn_auto_ex_trade { get; set; }
        public virtual string btn_auto_ex_trade_on { get; set; }
        public virtual string NowTime { get; set; }
        public virtual string StartTime { get; set; }

        public virtual bool AlarmReserveEnabled { get; set; } = true;
        public virtual bool AlarmCancelEnabled { get; set; } = false;

        public virtual decimal ExeSelCoinPrc { get; set; }
        public virtual decimal ExeChgCoinPrc { get; set; }

        public virtual decimal ExeSelPer { get; set; }
        public virtual decimal ExeChgPer { get; set; }

        public virtual string ExeSelUpDown { get; set; }
        public virtual string ExeChgUpDown { get; set; }

        public virtual decimal ExeChgAmt { get; set; }
        public virtual string State { get; set; }

        public virtual int tabIndex { get; set; } = 0;
        public virtual bool autoCheck { get; set; } = true;

        public virtual bool IsBusy { get; set; }
        bool IsLoad = false;
        

        bool ChangeSelPrc = true;
        bool ChangeChgPrc = true;

        public static CoinExchangeReservationTradingViewModel Create()
        {
            return ViewModelSource.Create(() => new CoinExchangeReservationTradingViewModel());
        }
        protected CoinExchangeReservationTradingViewModel()
        {
            try
            {
                Messenger.Default.Register<string>(this, CallSelectSetting);
                #region Combobox 초기화

                SelCoinData = new ObservableCollection<ComboBoxCoinData>();
                ChgCoinData = new ObservableCollection<ComboBoxCoinData>();


                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    SelCoinData.Add(new ComboBoxCoinData() { Name = item.CoinName, Value = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(item.CoinCode) });
                    ChgCoinData.Add(new ComboBoxCoinData() { Name = item.CoinName, Value = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(item.CoinCode) });
                }

                SelCoin = SelCoinData[0];
                ChgCoin = ChgCoinData[0];

                #endregion
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
                ImageSet();

                if(GetAutoTradeStatus().Equals(0))
                {
                    alert = new Alert(Localization.Resource.Common_Alert_12, 400);
                    alert.ShowDialog();
                    autoCheck = false;
                }

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 0, 1);
                RepeatTimer.Start();

                CmdSelectSetting();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public int GetAutoTradeStatus()
        {
            try
            {
                int count = 0;

                using (RequestAutoTradeContentModel req = new RequestAutoTradeContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseAutoTradeContentModel res = WebApiLib.SyncCall<ResponseAutoTradeContentModel, RequestAutoTradeContentModel>(req))
                    {
                        if(res.resultStrCode == "000")
                        {
                            if(res.data.cnKndCd == null)
                            {
                                count = 0;
                            }
                            else
                            {
                                count = 1;
                            }
                        }
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return 0;
            }
        }

        public void CmdSelectSetting()
        {
            try
            {
                if (MainViewModel.AutoCoinData != null)
                {
                    AlarmReserveEnabled = false;
                    AlarmCancelEnabled = true;

                    State = Localization.Resource.CoinExchangeReservationTrading_1_15_1;
                                        
                    //데이터 조회
                    ResponseTradingCoinAutoTradeSelDataModel SelectData = MainViewModel.AutoCoinData;

                    #region SelCoin

                    ComboBoxCoinData SelItem = SelCoinData.Where(x => x.Value == StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(SelectData.selCnKndCd)).FirstOrDefault();

                    if(SelItem != null)
                    {
                        SelCoin = SelItem;
                    }
                    else
                    {
                        SelCoin = SelCoinData[0];
                    }
                    
                    SelSliderValue = (double)SelectData.selSetRate;
                    coinSelPrc = (decimal)SelectData.selSetPrc;
                    sellCoinAmt = (decimal)SelectData.selAmt;

                    ExeSelCoinPrc = (decimal)SelectData.selCnPrc;
                    ExeSelPer = Math.Round(((nowSelPrc / ExeSelCoinPrc) - 1) * 100);
                    if (ExeSelPer > 0)
                    {
                        ExeSelUpDown = "▲";
                    }
                    else if (ExeSelPer == 0)
                    {
                        ExeSelUpDown = "－";
                    }
                    else
                    {
                        ExeSelUpDown = "▼";
                    }

                    #endregion

                    #region ChgCoin

                    ComboBoxCoinData ChgItem = ChgCoinData.Where(x => x.Value == StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(SelectData.chgCnKndCd)).FirstOrDefault();

                    if (ChgItem != null)
                    {
                        ChgCoin = ChgItem;
                    }
                    else
                    {
                        ChgCoin = ChgCoinData[0];
                    }
                    
                    ChgSliderValue = (double)SelectData.chgSetRate;
                    coinChgPrc = (decimal)SelectData.chgSetPrc;
                    coinChgAmt = (decimal)SelectData.chgHopeAmt;

                    ExeChgCoinPrc = (decimal)SelectData.chgCnPrc;
                    ExeChgPer = Math.Round(((nowChgPrc / ExeChgCoinPrc) - 1) * 100);
                    if (ExeChgPer > 0)
                    {
                        ExeChgUpDown = "▲";
                    }
                    else if (ExeChgPer == 0)
                    {
                        ExeChgUpDown = "－";
                    }
                    else
                    {
                        ExeChgUpDown = "▼";
                    }

                    StartTime = SelectData.regDt;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        public async void CmdAlarmReserve()
        {
            try
            {
                if (!autoCheck)
                {
                    alert = new Alert(Localization.Resource.Common_Alert_12, 400);
                    alert.ShowDialog();
                    return;
                }

                if (CertifyCheck() < 3)
                {
                    alert = new Alert(Localization.Resource.Common_Alert_3, Alert.ButtonType.Ok, 450);
                    alert.ShowDialog();
                    return;
                }
                if (SelCoin.Name == ChgCoin.Name)
                {
                    alert = new Alert(Localization.Resource.CoinExchangeReservationTrading_Alert_1, 350);
                    alert.ShowDialog();
                    return;
                }

                foreach(ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    if (SelCoin.Value.Equals(StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(item.CoinCode)))
                    {
                        if (sellCoinAmt < item.TradeMinCnt)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_8, item.TradeMinCnt));
                            alert.ShowDialog();
                            return;
                        }
                    }
                }

                if (sellCoinAmt > coinSelAmt)
                {
                    alert = new Alert(Localization.Resource.CoinExchangeReservationTrading_Alert_2, 350);
                    alert.ShowDialog();
                    return;
                }

                IsBusy = true;

                using (RequestTradingCoinAutoTradeInsModel req = new RequestTradingCoinAutoTradeInsModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.selCnKndCd = StringEnum.GetStringValue(SelCoin.Value);
                    req.selCnPrc = nowSelPrc;
                    req.selSetRate = (decimal)SelSliderValue;
                    req.selSetPrc = coinSelPrc;
                    req.selAmt = sellCoinAmt;
                    req.chgCnKndCd = StringEnum.GetStringValue(ChgCoin.Value);
                    req.chgCnPrc = nowChgPrc;
                    req.chgSetRate = (decimal)ChgSliderValue;
                    req.chgSetPrc = coinChgPrc;
                    req.chgHopeAmt = coinChgAmt;
                    req.regIp = MainViewModel.LoginDataModel.regIp;

                    using (ResponseTradingCoinAutoTradeInsModel res = await WebApiLib.AsyncCall<ResponseTradingCoinAutoTradeInsModel, RequestTradingCoinAutoTradeInsModel>(req))
                    {
                        AlarmReserveEnabled = false;
                        AlarmCancelEnabled = true;

                        ExeSelCoinPrc = nowSelPrc;
                        ExeSelPer = 0;
                        ExeSelUpDown = "－";

                        ExeChgCoinPrc = nowChgPrc;
                        ExeChgPer = 0;
                        ExeChgUpDown = "－";

                        State = Localization.Resource.CoinExchangeReservationTrading_1_15_1;

                        if (MainViewModel.AutoCoinData == null)
                            MainViewModel.AutoCoinData = new ResponseTradingCoinAutoTradeSelDataModel();

                        MainViewModel.AutoCoinData.selAmt = sellCoinAmt;
                        MainViewModel.AutoCoinData.selCnKndCd = StringEnum.GetStringValue(SelCoin.Value);
                        MainViewModel.AutoCoinData.selCnPrc = nowSelPrc;
                        MainViewModel.AutoCoinData.selSetPrc = coinSelPrc;
                        MainViewModel.AutoCoinData.selSetRate = (decimal)SelSliderValue;
                        MainViewModel.AutoCoinData.chgCnKndCd = StringEnum.GetStringValue(ChgCoin.Value);
                        MainViewModel.AutoCoinData.chgCnPrc = nowChgPrc;
                        MainViewModel.AutoCoinData.chgSetRate = (decimal)ChgSliderValue;
                        MainViewModel.AutoCoinData.chgSetPrc = coinChgPrc;
                        MainViewModel.AutoCoinData.chgHopeAmt = coinChgAmt;

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
        public async void CmdAlarmCancel()
        {
            try
            {
                //alert = new Alert(Localization.Resource.Common_Alert_1);
                //alert.ShowDialog();
                //return;

                alert = new Alert(Localization.Resource.Common_Alert_6);
                if (alert.ShowDialog() == true)
                {
                    using (RequestTradingCoinAutoTradeDelModel req = new RequestTradingCoinAutoTradeDelModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        using (ResponseTradingCoinAutoTradeDelModel res = await WebApiLib.AsyncCall<ResponseTradingCoinAutoTradeDelModel, RequestTradingCoinAutoTradeDelModel>(req))
                        {
                            //삭제 성공
                            AlarmReserveEnabled = true;
                            AlarmCancelEnabled = false;

                            MainViewModel.AutoCoinData = null;

                            ExeSelCoinPrc = nowSelPrc;
                            ExeSelPer = 0;
                            ExeSelUpDown = "－";

                            ExeChgCoinPrc = nowChgPrc;
                            ExeChgPer = 0;
                            ExeChgUpDown = "－";

                            State = Localization.Resource.CoinExchangeReservationTrading_1_15_2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        public void CmdTradeExecute()
        {
            //거래 실행 시 시장가로 전송
            try
            {
                //alert = new Alert(Localization.Resource.Common_Alert_1);
                //alert.ShowDialog();
                //return;

                if (SelCoin.Name == ChgCoin.Name)
                {
                    alert = new Alert(Localization.Resource.CoinExchangeReservationTrading_Alert_1, 350);
                    alert.ShowDialog();
                    return;
                }
                switch (SelCoin.Value)
                {
                    case EnumLib.ExchangeCurrencyCode.BTC:
                        if (sellCoinAmt < (decimal)0.001)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_8);
                            alert.ShowDialog();
                            return;
                        }
                        break;
                    case EnumLib.ExchangeCurrencyCode.ETH:
                        if (sellCoinAmt < (decimal)0.01)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_9);
                            alert.ShowDialog();
                            return;
                        }
                        break;
                    case EnumLib.ExchangeCurrencyCode.BCH:
                        if (sellCoinAmt < (decimal)0.01)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_9);
                            alert.ShowDialog();
                            return;
                        }
                        break;
                    case EnumLib.ExchangeCurrencyCode.XRP:
                        if (sellCoinAmt < (decimal)10)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_10);
                            alert.ShowDialog();
                            return;
                        }
                        break;
                    default:
                        if (sellCoinAmt < (decimal)5000)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_11);
                            alert.ShowDialog();
                            return;
                        }
                        break;
                }

                if (sellCoinAmt_Exe > coinSelAmt)
                {
                    alert = new Alert(Localization.Resource.CoinExchangeReservationTrading_Alert_2, 350);
                    alert.ShowDialog();
                    return;
                }

                IsBusy = true;

                using (RequestTradingCoinSwapModel req = new RequestTradingCoinSwapModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                    req.buyCurcyCd = StringEnum.GetStringValue(ChgCoin.Value);//수령코인
                    req.payCurcyCd = StringEnum.GetStringValue(SelCoin.Value);//주문코인                           
                    req.phsAmt = sellCoinAmt_Exe.ToString();
                    req.regIp = MainViewModel.LoginDataModel.regIp;

                    using (ResponseTradingCoinSwapModel res = WebApiLib.SyncCall<ResponseTradingCoinSwapModel, RequestTradingCoinSwapModel>(req))
                    {
                        Messenger.Default.Send("AssetsRefresh");
                        //초기화
                        Clear();

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

        public void CmdSelSliderValue(RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (ChangeSelPrc)
                {
                    SelSliderValue = Math.Round(e.NewValue);
                    coinSelPrc = Math.Round(nowSelPrc + (nowSelPrc * ((decimal)SelSliderValue) * (decimal)0.01), 3);
                    if (sellCoinAmt != 0)
                        coinChgAmt = Math.Round((coinSelPrc * sellCoinAmt) / (coinChgPrc), 8);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        public void CmdChgSliderValue(RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (ChangeChgPrc)
                {
                    ChgSliderValue = Math.Round(e.NewValue);
                    coinChgPrc = Math.Round(nowChgPrc + (nowChgPrc * ((decimal)ChgSliderValue) * (decimal)0.01), 3);
                    coinChgAmt = Math.Round((coinSelPrc * sellCoinAmt) / (coinChgPrc), 8);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnSelCoinChanged()
        {
            try
            {
                //if (SelCoin == null) return;

                //SearchCoinAssets();

                //foreach (var item in MainViewModel.mQClient.CoinInfoData)
                //{
                //    if (item.Value.curcyCd.Equals(StringEnum.GetStringValue(SelCoin.Value)))
                //    {
                //        nowSelPrc = item.Value.nowPrc;
                //    }
                //}

                //SelSliderValue = 0;
                //ChgSliderValue = 0;
                //sellCoinAmt = 0;
                //coinChgAmt = 0;

                //coinSelPrc = Math.Round(nowSelPrc + (nowSelPrc * ((decimal)SelSliderValue) * (decimal)0.01), 3);
                //if (sellCoinAmt != 0)
                //    coinChgAmt = Math.Round((coinSelPrc * sellCoinAmt) / (coinChgPrc), 8);

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        public void OnChgCoinChanged()
        {
            try
            {
                if (ChgCoin == null) return;

                //foreach (var item in MainViewModel.mQClient.CoinInfoData)
                //{
                //    if (item.Value.curcyCd.Equals(StringEnum.GetStringValue(ChgCoin.Value)))
                //    {
                //        nowChgPrc = item.Value.nowPrc;
                //    }
                //}

                //if (ChgCoin.Value.Equals(EnumLib.ExchangeCurrencyCode.BTC))
                //{
                //    nowChgPrc = btcPrc;
                //}
                //else if (ChgCoin.Value.Equals(EnumLib.ExchangeCurrencyCode.ETH))
                //{
                //    nowChgPrc = ethPrc;
                //}
                //else if (ChgCoin.Value.Equals(EnumLib.ExchangeCurrencyCode.BCH))
                //{
                //    nowChgPrc = bchPrc;
                //}
                //else if (ChgCoin.Value.Equals(EnumLib.ExchangeCurrencyCode.XRP))
                //{
                //    nowChgPrc = xrpPrc;
                //}

                //coinChgPrc = Math.Round(nowChgPrc + (nowChgPrc * ((decimal)ChgSliderValue) * (decimal)0.01), 3);
                //if (sellCoinAmt != 0)
                //    coinChgAmt = Math.Round((coinSelPrc * sellCoinAmt) / (coinChgPrc), 8);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void SearchCoinAssets()
        {
            try
            {
                string SelValue = StringEnum.GetStringValue(SelCoin.Value);

                RequestMainAssetModel requestMainAssetModel = new RequestMainAssetModel();
                requestMainAssetModel.userEmail = MainViewModel.LoginDataModel.userEmail;
                using (ResponseMainAssetModel res = await WebApiLib.AsyncCall<ResponseMainAssetModel, RequestMainAssetModel>(requestMainAssetModel))
                {
                    if (res != null)
                    {
                        foreach (var item in res.data.list)
                        {
                            if (SelValue.Equals(item.curcyCd))
                            {
                                coinSelAmt = item.totCoinAmt;
                                break;
                            }
                            else if (SelValue.Equals(item.curcyCd))
                            {
                                coinSelAmt = item.totCoinAmt;
                                break;
                            }
                            else if (SelValue.Equals(item.curcyCd))
                            {
                                coinSelAmt = item.totCoinAmt;
                                break;
                            }
                            else if (SelValue.Equals(item.curcyCd))
                            {
                                coinSelAmt = item.totCoinAmt;
                                break;
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

        public void OncoinSelPrcChanged()
        {
            try
            {
                if (ChangeSelPrc)
                {
                    ChangeSelPrc = false;
                    SelSliderValue = Math.Round((((double)coinSelPrc / (double)nowSelPrc) * 100) - 100);
                    if (SelSliderValue > 100)
                    {
                        alert = new Alert(Localization.Resource.CoinExchangeReservationTrading_Alert_3, 330);
                        alert.ShowDialog();
                        SelSliderValue = 0;
                        coinSelPrc = nowSelPrc;
                    }
                    ChangeSelPrc = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                ChangeSelPrc = true;
            }
        }
        public void OncoinChgPrcChanged()
        {
            try
            {
                if (ChangeChgPrc)
                {
                    ChangeChgPrc = false;
                    ChgSliderValue = Math.Round((((double)coinChgPrc / (double)nowChgPrc) * 100) - 100);
                    if (ChgSliderValue > 100)
                    {
                        alert = new Alert(Localization.Resource.CoinExchangeReservationTrading_Alert_3, 330);
                        alert.ShowDialog();
                        ChgSliderValue = 0;
                        coinChgPrc = nowChgPrc;
                    }
                    ChangeChgPrc = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                ChangeChgPrc = true;
            }
        }

        public void OnSellCoinAmtChanged()
        {
            try
            {
                coinChgAmt = Math.Round((coinSelPrc * sellCoinAmt) / (coinChgPrc), 8);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        public void OnChgCoinAmtChanged()
        {
            try
            {
                coinChgAmt = Math.Round((coinSelPrc * sellCoinAmt) / (coinChgPrc), 8);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        public void OnSellCoinAmt_Exe_Changed()
        {
            try
            {
                ExeChgAmt = Math.Round((nowSelPrc * sellCoinAmt_Exe) / (nowChgPrc), 8);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CallSelectSetting(string Message)
        {
            if (Message.Equals("AutoCoinSettingRefresh"))
            {
                if (IsLoad)
                {
                    CmdSelectSetting();
                }
            }
        }
        int CertifyCheck()
        {
            int val = 0;
            try
            {
                //이메일 인증(1)
                if (MainViewModel.LoginDataModel.emailCertYn == "Y")
                {
                    val++;
                }
                //휴대폰 인증(2)
                if (MainViewModel.LoginDataModel.smsCertYn == "Y")
                {
                    val++;
                }
                //기본인증_계좌(3)
                if (!string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.accountNo))
                {
                    val++;
                }
                //OPT 등록 여부(4)
                if (!string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                {
                    val++;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            return val;
        }
        public void ImageSet()
        {
            try
            {
                string sLanguage = LoginViewModel.LanguagePack;

                btn_auto_alarm_save = sLanguage + "btn_auto_alarm_save.png";
                btn_auto_alarm_save_on = sLanguage + "btn_auto_alarm_save_on.png";
                btn_auto_alarm_cancel = sLanguage + "btn_auto_alarm_cancel.png";
                btn_auto_alarm_cancel_on = sLanguage + "btn_auto_alarm_cancel_on.png";
                btn_auto_ex_trade = sLanguage + "btn_auto_ex_trade.png";
                btn_auto_ex_trade_on = sLanguage + "btn_auto_ex_trade_on.png";

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
                NowTime = CommonLib.GetTime;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void Clear()
        {
            try
            {
                using (RequestTradingCoinAutoTradeDelModel req = new RequestTradingCoinAutoTradeDelModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    using (ResponseTradingCoinAutoTradeDelModel res = await WebApiLib.AsyncCall<ResponseTradingCoinAutoTradeDelModel, RequestTradingCoinAutoTradeDelModel>(req))
                    {
                        //삭제
                        AlarmReserveEnabled = true;
                        AlarmCancelEnabled = false;

                        sellCoinAmt = 0;
                        sellCoinAmt_Exe = 0;
                        SelCoin = SelCoinData[0];
                        ChgCoin = ChgCoinData[0];
                        coinChgAmt = 0;
                        ExeChgAmt = 0;

                        SelSliderValue = 0;
                        ChgSliderValue = 0;
                        ExeSelCoinPrc = 0;
                        ExeChgCoinPrc = 0;
                        ExeSelPer = 0;
                        ExeChgPer = 0;
                        ExeSelUpDown = "－";
                        ExeChgUpDown = "－";

                        StartTime = string.Empty;
                        State = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }

        }
    }

    public class ComboBoxCoinData
    {
        public string Name { get; set; }
        public EnumLib.ExchangeCurrencyCode Value { get; set; }
    }
}