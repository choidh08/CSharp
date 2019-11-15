using coinBlock.Model;
using coinBlock.Model.Dashboard;
using coinBlock.Model.Trading;
using coinBlock.Utility;
using coinBlock.Views;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using coinBlock.Model.AdditionalService;
using coinBlock.Model.Common;
using System.Threading;
using System.Windows.Threading;

namespace coinBlock.ViewModels.Trading
{
    [POCOViewModel]
    public class CoinTradingViewModel
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }        

        DispatcherTimer RepeatTimer;

        public static ResponseGetAskingDataModel AskingData { get; set; }

        public virtual List<ResponseExchangeDashboardFillListModel> FillData { get; set; }
        public virtual ObservableCollection<ResponseExchangeDashboardOrderListModel> BuyOrdelData { get; set; }
        public virtual ObservableCollection<ResponseExchangeDashboardOrderListModel> SellOrdelData { get; set; }
        public virtual ObservableCollection<ResponseTradingNotConListModel> NotConList { get; set; }
        public virtual ObservableCollection<ResponseTradingTradeHistoryListModel> TradeHistory { get; set; }
        public virtual ObservableCollection<ResponseTradingGetAutoBuyListModel> AutoTradeHistory { get; set; }
        public virtual ResponseTradingAbleBuyDataModel AbleBuy { get; set; }
        public virtual ResponseTradingAbleSellDataModel AbleSell { get; set; }
        public virtual ResponseCoinInfoDataModel CoinInfo { get; set; }
        public virtual ResponseGetMarketLimitListModel LimitData { get; set; }
        public virtual ResponseGetMarketLimitContentModel LimitContentData { get; set; }

        public virtual string SC { get; } = CommonLib.StandardCurcyNm;
        public virtual string SD { get; } = CommonLib.StandardCurcyCd;
        public virtual decimal? CoinMarketPrc { get; set; } = null;

        public virtual Visibility CoinPriceHeaderVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility AutoTradingVisible { get; set; } = Visibility.Visible;

        public virtual string mask { get; set; }        

        public virtual CommonOrderVM BuyCommonOrder { get; set; }
        public virtual CommonOrderVM SellCommonOrder { get; set; }

        #region 자동거래 변수 생성
        /// <summary>
        /// 자동거래 구매주문
        /// </summary>
        public virtual AutoOrderVM BuyAutoOrder { get; set; }
        /// <summary>
        /// 자동거래 구매주문2
        /// </summary>
        public virtual AutoOrderVM BuyAutoOrder2 { get; set; }
        /// <summary>
        /// 자동거래 구매주문3
        /// </summary>
        public virtual AutoOrderVM BuyAutoOrder3 { get; set; }

        /// <summary>
        /// 자동거래 판매주문
        /// </summary>
        public virtual AutoOrderVM SellAutoOrder { get; set; }
        /// <summary>
        /// 자동거래 판매주문3
        /// </summary>
        public virtual AutoOrderVM SellAutoOrder2 { get; set; }
        /// <summary>
        /// 자동거래 판매주문3
        /// </summary>
        public virtual AutoOrderVM SellAutoOrder3 { get; set; }

        /// <summary>
        /// 자동거래 판매주문4
        /// </summary>
        public virtual AutoOrderVM LossAutoOrder { get; set; }
        /// <summary>
        /// 자동거래 판매주문5
        /// </summary>
        public virtual AutoOrderVM LossAutoOrder2 { get; set; }
        /// <summary>
        /// 자동거래 판매주문6
        /// </summary>
        public virtual AutoOrderVM LossAutoOrder3 { get; set; }
        #endregion

        public virtual decimal? TotalPrice { get; set; }

        AutoOrderVM_ALL BuySellOrderVM = new AutoOrderVM_ALL();
        AutoOrderVM_ALL LossOrderVM = new AutoOrderVM_ALL();
        public virtual bool IsBuySellTradeStart { get; set; } = true;
        public virtual bool IsBuySellTradeCancel { get; set; } = false;
        public virtual bool IsLossTradeStart { get; set; } = true;
        public virtual bool IsLossTradeCancel { get; set; } = false;

        public virtual bool IsEnabledBuySellTab { get; set; } = true;
        public virtual bool IsEnabledLossTab { get; set; } = true;

        public virtual bool autoCheck { get; set; } = false;

        public virtual string btn_reset { get; set; }
        public virtual string btn_reset_on { get; set; }
        public virtual string btn_color_purchase { get; set; }
        public virtual string btn_color_purchase_on { get; set; }
        public virtual string btn_color_sell { get; set; }
        public virtual string btn_color_sell_on { get; set; }
        public virtual string btn_auto_exchange_set_small { get; set; }
        public virtual string btn_auto_exchange_set_small_on { get; set; }
        public virtual string btn_auto_reset_small { get; set; }
        public virtual string btn_auto_reset_small_on { get; set; }
        public virtual string btn_auto_exchange_cancel_small { get; set; }
        public virtual string btn_auto_exchange_cancel_small_on { get; set; }
        public virtual string btn_damage_sell_set_small { get; set; }
        public virtual string btn_damage_sell_set_small_on { get; set; }
        public virtual string btn_damage_sell_cancel_small { get; set; }
        public virtual string btn_damage_sell_cancel_small_on { get; set; }
        public virtual string btn_daily_market { get; set; }
        public virtual string btn_daily_market_on { get; set; }
        public virtual string btn_more { get; set; }
        public virtual string btn_more_on { get; set; }

        public virtual string PriceType { get; set; } = CommonLib.StandardCurcyNm;
        EnumLib.ExchangeCurrencyCode PriceCode = EnumLib.ExchangeCurrencyCode.KRW;
        public virtual string PriceCode2 { get; set; } = CommonLib.StandardCurcyCd;
        public virtual string ViewPriceType { get; set; }
        public virtual string TempPriceType { get; set; }
        public virtual string CommonFloat { get; set; }
        public virtual string CommonNum { get; set; } = "n0";
        public virtual decimal CommonAutoPrice { get; set; }
        public virtual string selectBuySellListTitle { get; set; }
        public virtual string selectBuySellListTitle2 { get; set; }

        public virtual string minOrderAmt { get; set; }
        public virtual string minOrderPrc { get; set; }
        public virtual decimal feePercent { get; set; }
        public virtual int nonConListCnt { get; set; } = 10;
        public virtual bool nonConListCntEnable { get; set; } = true;

        public virtual string sTempFloat { get; set; } = "###,###,###,###,##0";
        Alert alert = null;

        public virtual bool IsBusy { get; set; }

        public EnumLib.ExchangeCurrencyCode KindOfCoin = EnumLib.ExchangeCurrencyCode.BTC;
     
        ResponseCoinListModel CucyCoin;
        ResponseCoinListModel PriceCoin;
        public virtual string ViewCoinName { get; set; } = null;

        bool IsLoad = false;

        Dictionary<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>> dict = new Dictionary<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>>();
        Dictionary<string, EnumLib.ExchangeCurrencyCode> dictTemp = new Dictionary<string, EnumLib.ExchangeCurrencyCode>();

        public static CoinTradingViewModel Create()
        {
            return ViewModelSource.Create(() => new CoinTradingViewModel());
        }

        ~CoinTradingViewModel()
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public CoinTradingViewModel()
        {
            try
            {
                Messenger.Default.Register<ResponseExchangeDashboardOrderDataModel>(this, OrderSocketData);
                Messenger.Default.Register<ResponseExchangeDashboardFillDataModel>(this, FillSocketData);
                Messenger.Default.Register<ResponseCoinInfoDataModel>(this, CoinInfoSocketData);

                Messenger.Default.Register<string>(this, CallAutoSetting);
                Messenger.Default.Register<string>(this, CallNonCoinList);
                Messenger.Default.Register<string>(this, CallTradeHistory);
                Messenger.Default.Register<string>(this, SetAsset);

                BuyOrdelData = new ObservableCollection<ResponseExchangeDashboardOrderListModel>();
                SellOrdelData = new ObservableCollection<ResponseExchangeDashboardOrderListModel>();
                FillData = new List<ResponseExchangeDashboardFillListModel>();
                CoinInfo = new ResponseCoinInfoDataModel();
                AutoTradeHistory = new ObservableCollection<ResponseTradingGetAutoBuyListModel>();

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 0, 10);


                ImageSet();
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
                //SetCoin(PriceType, StringEnum.GetStringValue(KindOfCoin));
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
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void SetCoin(string sPriceType, string sPriceCode, string CoinCode)
        {
            try
            {
                //DispatcherService.BeginInvoke(() =>
                //{
                IsBusy = true;

                nonConListCnt = 10;

                PriceType = sPriceType;
                PriceCode2 = sPriceCode;
                ViewPriceType = sPriceType;

                if (MainViewModel.CoinData == null) return;
                if (MainViewModel.MarketLimitData == null) return;

                dictTemp.Clear();
                dict.Clear();

                dictTemp.Add(PriceCode2, StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(CoinCode));
                dict.Add(this.GetType().BaseType.Name, dictTemp);
                Messenger.Default.Send(dict);

                PriceCode = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(PriceCode2);

                if (PriceCoin == null)
                {
                    PriceCoin = new ResponseCoinListModel();
                }

                if (PriceCode != EnumLib.ExchangeCurrencyCode.KRW)
                {
                    PriceCoin = MainViewModel.CoinData.list.Where(x => x.CoinCode == StringEnum.GetStringValue(PriceCode)).First();
                }

                if (TempPriceType != PriceType)
                {
                    TempPriceType = PriceType;
                    GetBidding();
                }

                LimitData = MainViewModel.MarketLimitData.list.Where(x => x.marketCd == StringEnum.GetStringValue(PriceCode)).FirstOrDefault();

                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    if (item.CoinCode == CoinCode)
                    {
                        CucyCoin = item;
                        KindOfCoin = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(CucyCoin.CoinCode);
                        LimitContentData = LimitData.list.Where(x => x.curcyCd == CucyCoin.CoinCode).FirstOrDefault();
                        ViewCoinName = KindOfCoin.ToString();
                        selectBuySellListTitle = ViewCoinName + "/" + ViewPriceType + " ";
                        selectBuySellListTitle2 = Localization.Resource.CoinTrading_4;

                        if (MainViewModel.mQClient.CoinInfoData2.ToList().Where(w => w.Key.Key == sPriceCode && w.Key.Value == StringEnum.GetStringValue(KindOfCoin)).Count() > 0)
                        {
                            CoinInfoSocketData(MainViewModel.mQClient.CoinInfoData2[new KeyValuePair<string, string>(sPriceCode, StringEnum.GetStringValue(KindOfCoin))]);
                        }

                        if (MainViewModel.mQClient.OrderData2.ToList().Where(w => w.Key.Key == sPriceCode && w.Key.Value == StringEnum.GetStringValue(KindOfCoin)).Count() > 0)
                        {
                            OrderSocketData(MainViewModel.mQClient.OrderData2[new KeyValuePair<string, string>(sPriceCode, StringEnum.GetStringValue(KindOfCoin))]);
                        }

                        if (MainViewModel.mQClient.FillData2.ToList().Where(w => w.Key.Key == sPriceCode && w.Key.Value == StringEnum.GetStringValue(KindOfCoin)).Count() > 0)
                        {
                            FillSocketData(MainViewModel.mQClient.FillData2[new KeyValuePair<string, string>(sPriceCode, StringEnum.GetStringValue(KindOfCoin))]);
                        }

                        mask = "###,###,##0.";
                        for (int i = 1; i <= CucyCoin.CoinFloatCnt; i++)
                        {
                            mask += "#";
                            if (i == CucyCoin.CoinFloatCnt)
                            {
                                mask += ";";
                            }
                        }

                        if (BuyCommonOrder == null)
                        {
                            BuyCommonOrder = ViewModelSource.Create(() => new CommonOrderVM("B", PriceCode, KindOfCoin, KindOfCoin, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            BuyCommonOrder.Set("B", PriceCode, KindOfCoin, KindOfCoin, CucyCoin.CashDecimal);
                        }

                        if (SellCommonOrder == null)
                        {
                            SellCommonOrder = ViewModelSource.Create(() => new CommonOrderVM("S", KindOfCoin, PriceCode, KindOfCoin, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            SellCommonOrder.Set("S", KindOfCoin, PriceCode, KindOfCoin, CucyCoin.CashDecimal);
                        }

                        if (PriceType.Equals(CommonLib.StandardCurcyNm))
                        {
                            CommonFloat = "###,###,###,###,##0";
                            CommonNum = "n" + CucyCoin.CashDecimal;
                            CommonAutoPrice = 1;

                            for (int i = 1; i <= CucyCoin.CashDecimal; i++)
                            {
                                if (i == 1)
                                {
                                    CommonFloat += ".";
                                }
                                CommonFloat += "#";
                                if (i == CucyCoin.CashDecimal)
                                {
                                    CommonFloat += ";";
                                }
                            }

                            minOrderAmt = CucyCoin.TradeMinCnt.ToString("##,##0.########");
                            minOrderPrc = CucyCoin.TradeFillMinPrc.ToString("##,##0.########");
                            feePercent = MainViewModel.CoinData.mkKRW;

                            AutoTradingVisible = Visibility.Visible;

                            SellCommonOrder.rowSpan = 1;
                        }
                        else
                        {
                            if (PriceType.Equals("USDT"))
                            {
                                CommonFloat = "###,###,###,###,##0.###";
                                CommonNum = "n3";
                                CommonAutoPrice = (decimal)0.001;

                                minOrderAmt = CucyCoin.TradeMinCnt.ToString("#,##0.###");
                                minOrderPrc = PriceCoin.TradeMinCnt.ToString("#,##0.###");
                            }
                            else
                            {
                                CommonFloat = "###,###,###,###,##0.########";
                                CommonNum = "n3";
                                CommonAutoPrice = (decimal)0.00000001;

                                minOrderAmt = CucyCoin.TradeMinCnt.ToString("#,##0.########");
                                minOrderPrc = PriceCoin.TradeMinCnt.ToString("#,##0.########");
                            }
                            if (PriceType.Equals(EnumLib.ExchangeCurrencyCode.ETH.ToString()))
                            {
                                feePercent = MainViewModel.CoinData.mkETH;
                            }
                            else if (PriceType.Equals(EnumLib.ExchangeCurrencyCode.BTC.ToString()))
                            {
                                feePercent = MainViewModel.CoinData.mkBTC;
                            }
                            else if (PriceType.Equals(EnumLib.ExchangeCurrencyCode.USDT.ToString()))
                            {
                                feePercent = MainViewModel.CoinData.mkUSDT;
                            }

                            AutoTradingVisible = Visibility.Visible;

                            BuyCommonOrder.BuyMarketCoin = Visibility.Collapsed;
                            SellCommonOrder.SellMarketCoin = Visibility.Collapsed;
                            SellCommonOrder.rowSpan = 2;
                        }

                        #region 자동거래 초기화

                        if (BuySellOrderVM == null)
                        {
                            BuySellOrderVM = new AutoOrderVM_ALL();
                        }
                        else
                        {
                            BuySellOrderVM.list.Clear();
                        }

                        if (LossOrderVM == null)
                        {
                            LossOrderVM = new AutoOrderVM_ALL();
                        }
                        else
                        {
                            LossOrderVM.list.Clear();
                        }

                        if (BuyAutoOrder == null)
                        {
                            BuyAutoOrder = ViewModelSource.Create(() => new AutoOrderVM("B", "B", 1, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            BuyAutoOrder.Reset(KindOfCoin, PriceCode);
                        }

                        if (BuyAutoOrder2 == null)
                        {
                            BuyAutoOrder2 = ViewModelSource.Create(() => new AutoOrderVM("B", "B", 2, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            BuyAutoOrder2.Reset(KindOfCoin, PriceCode);
                        }

                        if (BuyAutoOrder3 == null)
                        {
                            BuyAutoOrder3 = ViewModelSource.Create(() => new AutoOrderVM("B", "B", 3, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            BuyAutoOrder3.Reset(KindOfCoin, PriceCode);
                        }

                        if (SellAutoOrder == null)
                        {
                            SellAutoOrder = ViewModelSource.Create(() => new AutoOrderVM("S", "S", 1, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            SellAutoOrder.Reset(KindOfCoin, PriceCode);
                        }

                        if (SellAutoOrder2 == null)
                        {
                            SellAutoOrder2 = ViewModelSource.Create(() => new AutoOrderVM("S", "S", 2, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            SellAutoOrder2.Reset(KindOfCoin, PriceCode);
                        }

                        if (SellAutoOrder3 == null)
                        {
                            SellAutoOrder3 = ViewModelSource.Create(() => new AutoOrderVM("S", "S", 3, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            SellAutoOrder3.Reset(KindOfCoin, PriceCode);
                        }

                        if (LossAutoOrder == null)
                        {
                            LossAutoOrder = ViewModelSource.Create(() => new AutoOrderVM("L", "L", 1, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            LossAutoOrder.Reset(KindOfCoin, PriceCode);
                        }

                        if (LossAutoOrder2 == null)
                        {
                            LossAutoOrder2 = ViewModelSource.Create(() => new AutoOrderVM("L", "L", 2, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            LossAutoOrder2.Reset(KindOfCoin, PriceCode);
                        }

                        if (LossAutoOrder3 == null)
                        {
                            LossAutoOrder3 = ViewModelSource.Create(() => new AutoOrderVM("L", "L", 3, KindOfCoin, PriceCode, CucyCoin.CashDecimal));
                        }
                        else
                        {
                            LossAutoOrder3.Reset(KindOfCoin, PriceCode);
                        }

                        BuySellOrderVM.list.Add(BuyAutoOrder);
                        BuySellOrderVM.list.Add(BuyAutoOrder2);
                        BuySellOrderVM.list.Add(BuyAutoOrder3);
                        BuySellOrderVM.list.Add(SellAutoOrder);
                        BuySellOrderVM.list.Add(SellAutoOrder2);
                        BuySellOrderVM.list.Add(SellAutoOrder3);

                        LossOrderVM.list.Add(LossAutoOrder);
                        LossOrderVM.list.Add(LossAutoOrder2);
                        LossOrderVM.list.Add(LossAutoOrder3);

                        AutoSetting();

                        #endregion

                        SearchAbleBuy();
                        SearchAbleSell();
                        SearchNotCon();
                        SearchTradeHistory();
                        //SearchAutoTradeList();

                        if (!autoCheck)
                        {
                            GetAutoTradeStatus();
                        }
                    }
                }
                IsBusy = false;
                //});
            }
            catch (Exception ex)
            {
                IsBusy = false;
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void SetAsset(string Message)
        {
            try
            {
                if (IsLoad)
                {
                    if (Message.Equals("AssetsRefresh_CoinTrading"))
                    {
                        Task.Run(() =>
                        {
                            SearchAbleBuy(false);
                            SearchAbleSell(false);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CoinInfoSocketData(ResponseCoinInfoDataModel item)
        {
            try
            {
                if (!IsLoad)
                {
                    return;
                }

                if (item.marketCode == PriceCode2 && item.curcyCd == StringEnum.GetStringValue(KindOfCoin))
                {
                    CoinMarketPrc = item.ytdPrc;

                    DispatcherService.BeginInvoke(() =>
                    {
                        if (decimal.Parse(item.ytdPer) > 0)
                            item.ytdColor = "#fc7777";
                        else if (decimal.Parse(item.ytdPer) == 0)
                            item.ytdColor = "#bdbdbd";
                        else
                            item.ytdColor = "#0093cf";

                        if (decimal.Parse(item.maxPer) > 0)
                            item.maxColor = "#fc7777";
                        else if (decimal.Parse(item.maxPer) == 0)
                            item.maxColor = "#bdbdbd";
                        else
                            item.maxColor = "#0093cf";

                        if (decimal.Parse(item.minPer) > 0)
                            item.minColor = "#fc7777";
                        else if (decimal.Parse(item.minPer) == 0)
                            item.minColor = "#bdbdbd";
                        else
                            item.minColor = "#0093cf";

                        if (item.volume > 0)
                        {
                            item.nowColor = "#fc7777";
                            item.volumeArrow = "/Images/ico_up_arr_red.png";
                        }
                        else if (item.volume == 0)
                        {
                            item.nowColor = "#bdbdbd";
                            item.volumeArrow = "/Images/ico_stop_change.png";
                        }
                        else
                        {
                            item.nowColor = "#0093cf";
                            item.volumeArrow = "/Images/ico_down_arr_blue.png";
                        }

                        item.totAmt = Math.Round(item.totAmt);
                        item.ytdAmt = Math.Round(item.ytdAmt);

                        CoinInfo = item;
                    });
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void FillSocketData(ResponseExchangeDashboardFillDataModel item)
        {
            try
            {
                if (!IsLoad)
                {
                    return;
                }

                //sTempFloat = "###,###,###,###,##0";

                //if (PriceType.Equals(CommonLib.StandardCurcy))
                //{
                //    sTempFloat = "###,###,###,###,##0";
                //}
                //else
                //{
                //    sTempFloat = "###,###,###,###,##0.00000000";
                //}

                if (item.marketCode == PriceCode2 && item.curcyCd == StringEnum.GetStringValue(KindOfCoin))
                {
                    DispatcherService.BeginInvoke(() =>
                    {
                        if (item != null && item.list.Count > 0)
                        {
                            for (int i = 0; i <= item.list.Count - 1; i++)
                            {
                                if (i + 1 <= item.list.Count - 1)
                                {
                                    item.list[i].fillPrc = decimal.Parse(item.list[i].coinPrc) >= decimal.Parse(item.list[i + 1].coinPrc) ? "0" : "1";
                                }
                                else
                                {
                                    item.list[i].fillPrc = "0";
                                }
                                //item.list[i].floatDivision = sTempFloat;
                            }
                            FillData = item.list.Take(15).ToList();
                        }
                        else
                        {
                            FillData = new List<ResponseExchangeDashboardFillListModel>();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void OrderSocketData(ResponseExchangeDashboardOrderDataModel item)
        {
            try
            {
                if (!IsLoad)
                {
                    return;
                }

                if (CoinMarketPrc == null)
                {
                    return;
                }
                if (item.marketCode == PriceCode2 && item.curcyCd == StringEnum.GetStringValue(KindOfCoin))
                {
                    var task = Task.Run(() =>
                    {   
                        List<ResponseExchangeDashboardOrderListModel> SellOrderTemp = item.list.Where(w => w.sellTranPrc != "" || w.sellCnAmt != "").Select(s => new ResponseExchangeDashboardOrderListModel { sellCnAmt = s.sellCnAmt, sellTranPrc = s.sellTranPrc, floatFormat = s.floatFormat }).Take(16).Reverse().ToList();
                        List<ResponseExchangeDashboardOrderListModel> BuyOrderTemp = item.list.Where(w => w.buyTranPrc != "" || w.buyCnAmt != "").Select(s => new ResponseExchangeDashboardOrderListModel { buyCnAmt = s.buyCnAmt, buyTranPrc = s.buyTranPrc, floatFormat = s.floatFormat }).Take(16).ToList();

                        if (BuyOrderTemp != null)
                        {
                            DispatcherService.BeginInvoke(() =>
                            {
                                BuyOrderListUpdateProcess(BuyOrderTemp, BuyOrdelData);
                            });
                        }
                        if (SellOrderTemp != null)
                        {
                            DispatcherService.BeginInvoke(() =>
                            {
                                SellOrderListUpdateProcess(SellOrderTemp, SellOrdelData);
                            });
                        }
                    });
                    await task;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        void BuyOrderListUpdateProcess(List<ResponseExchangeDashboardOrderListModel> source, ObservableCollection<ResponseExchangeDashboardOrderListModel> target)
        {
            try
            {
                //fillPrc
                //CoinMarketPrc
                //판매 주문 리스트 갱신
                if (source != null)
                {
                    lock (target)
                    {
                        int sourceCnt = source.Count();
                        int targetCnt = target.Count();

                        string tempMaxCnAmt = source.Max(x => x.buyCnAmt);

                        //화면데이터 없을 경우 새로운 데이터 입력
                        if (targetCnt == 0 && sourceCnt > 0)
                        {
                            for (int i = 0; i < sourceCnt; i++)
                            {
                                string tempbuyCnAmt = source[i].buyCnAmt;
                                string tempbuyTranPrc = source[i].buyTranPrc;
                                string tempbuyfloatFormat = source[i].floatFormat;
                                string tempbuyTranPrcPer = "0%";

                                if (tempbuyTranPrc != "0")
                                {
                                    if ((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                    {
                                        tempbuyTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                    }
                                }

                                tempbuyTranPrcPer = tempbuyTranPrcPer.IndexOf('-') < 0 ? "+" + tempbuyTranPrcPer : tempbuyTranPrcPer;
                                string tempfillPrc = decimal.Parse(source[i].buyTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].buyTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());
                                target.Add(ViewModelSource.Create(() => new ResponseExchangeDashboardOrderListModel() { buyCnAmt = tempbuyCnAmt, buyTranPrc = tempbuyTranPrc, buyTranPrcPer = tempbuyTranPrcPer, fillPrc = tempfillPrc, maxCnAmt = tempMaxCnAmt, floatFormat = tempbuyfloatFormat }));
                            }
                            return;
                        }

                        //새로운 데이터가 없는 경우 화면 삭제
                        if (sourceCnt == 0)
                        {
                            if (targetCnt > 0)
                            {
                                target.Clear();
                            }
                            return;
                        }

                        if (targetCnt <= sourceCnt)
                        {
                            //새로운 데이터 가 많은 경우
                            //적은쪽(기존)을 기준으로 그만큼만 데이터 갱신
                            for (int i = 0; i < targetCnt; i++)
                            {
                                if (target[i].buyCnAmt != source[i].buyCnAmt || target[i].buyTranPrc != source[i].buyTranPrc)
                                {
                                    string tempbuyCnAmt = source[i].buyCnAmt;
                                    string tempbuyTranPrc = source[i].buyTranPrc;
                                    string tempbuyfloatFormat = source[i].floatFormat;
                                    string tempbuyTranPrcPer = "0%";

                                    if (tempbuyTranPrc != "0")
                                    {
                                        if ((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                        {
                                            tempbuyTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                        }
                                    }

                                    tempbuyTranPrcPer = tempbuyTranPrcPer.IndexOf('-') < 0 ? "+" + tempbuyTranPrcPer : tempbuyTranPrcPer;
                                    string tempfillPrc = decimal.Parse(source[i].buyTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].buyTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());

                                    target[i].buyCnAmt = tempbuyCnAmt;
                                    target[i].buyTranPrc = tempbuyTranPrc;
                                    target[i].buyTranPrcPer = tempbuyTranPrcPer;
                                    target[i].fillPrc = tempfillPrc;
                                    target[i].maxCnAmt = tempMaxCnAmt;
                                    target[i].floatFormat = tempbuyfloatFormat;
                                }
                            }

                            if (targetCnt < sourceCnt)
                            {
                                //위에 로직 처리하고 남는 데이터 화면에 추가
                                for (int i = targetCnt; i < sourceCnt; i++)
                                {
                                    string tempbuyCnAmt = source[i].buyCnAmt;
                                    string tempbuyTranPrc = source[i].buyTranPrc;
                                    string tempbuyfloatFormat = source[i].floatFormat;
                                    string tempbuyTranPrcPer = "0%";

                                    if (tempbuyTranPrc != "0")
                                    {
                                        if ((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                        {
                                            tempbuyTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                        }
                                    }

                                    tempbuyTranPrcPer = tempbuyTranPrcPer.IndexOf('-') < 0 ? "+" + tempbuyTranPrcPer : tempbuyTranPrcPer;
                                    string tempfillPrc = decimal.Parse(source[i].buyTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].buyTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());
                                    target.Add(ViewModelSource.Create(() => new ResponseExchangeDashboardOrderListModel() { buyCnAmt = tempbuyCnAmt, buyTranPrc = tempbuyTranPrc, buyTranPrcPer = tempbuyTranPrcPer, fillPrc = tempfillPrc, maxCnAmt = tempMaxCnAmt, floatFormat = tempbuyfloatFormat }));
                                }
                            }
                        }
                        else
                        {
                            //새로운 데이터가 적을 경우
                            //적은쪽(신규)을 기준으로 그만큼만 데이터 갱신
                            for (int i = 0; i < sourceCnt; i++)
                            {
                                if (target[i].buyCnAmt != source[i].buyCnAmt || target[i].buyTranPrc != source[i].buyTranPrc)
                                {
                                    string tempbuyCnAmt = source[i].buyCnAmt;
                                    string tempbuyTranPrc = source[i].buyTranPrc;
                                    string tempbuyfloatFormat = source[i].floatFormat;
                                    string tempbuyTranPrcPer = "0%";

                                    if (tempbuyTranPrc != "0")
                                    {
                                        if ((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                        {
                                            tempbuyTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempbuyTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                        }
                                    }

                                    tempbuyTranPrcPer = tempbuyTranPrcPer.IndexOf('-') < 0 ? "+" + tempbuyTranPrcPer : tempbuyTranPrcPer;
                                    string tempfillPrc = decimal.Parse(source[i].buyTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].buyTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());

                                    target[i].buyCnAmt = tempbuyCnAmt;
                                    target[i].buyTranPrc = tempbuyTranPrc;
                                    target[i].buyTranPrcPer = tempbuyTranPrcPer;
                                    target[i].fillPrc = tempfillPrc;
                                    target[i].maxCnAmt = tempMaxCnAmt;
                                    target[i].floatFormat = tempbuyfloatFormat;
                                }
                            }

                            //삭제된 데이터 화면에서 삭제
                            if (targetCnt > sourceCnt)
                            {
                                for (int i = targetCnt - 1; i > sourceCnt - 1; i--)
                                {
                                    if (i != sourceCnt)
                                    {
                                        target.RemoveAt(i);
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
        }

        void SellOrderListUpdateProcess(List<ResponseExchangeDashboardOrderListModel> source, ObservableCollection<ResponseExchangeDashboardOrderListModel> target)
        {
            try
            {
                //판매 주문 리스트 갱신
                if (source != null)
                {
                    int sourceCnt = source.Count();
                    int targetCnt = target.Count();

                    string tempMaxCnAmt = source.Max(x => x.sellCnAmt);

                    lock (target)
                    {
                        //화면데이터 없을 경우 새로운 데이터 입력
                        if (targetCnt == 0)
                        {
                            for (int i = 0; i < sourceCnt; i++)
                            {
                                string tempsellCnAmt = source[i].sellCnAmt;
                                string tempsellTranPrc = source[i].sellTranPrc;
                                string tempsellfloatFormat = source[i].floatFormat;
                                string tempsellTranPrcPer = "0%";

                                if (tempsellTranPrc != "0")
                                {
                                    if ((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                    {
                                        tempsellTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                    }
                                }
                                tempsellTranPrcPer = tempsellTranPrcPer.IndexOf('-') < 0 ? "+" + tempsellTranPrcPer : tempsellTranPrcPer;
                                string tempfillPrc = decimal.Parse(source[i].sellTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].sellTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());
                                target.Add(ViewModelSource.Create(() => new ResponseExchangeDashboardOrderListModel() { sellCnAmt = tempsellCnAmt, sellTranPrc = tempsellTranPrc, sellTranPrcPer = tempsellTranPrcPer, fillPrc = tempfillPrc, maxCnAmt = tempMaxCnAmt, floatFormat = tempsellfloatFormat }));
                            }
                            return;
                        }

                        //새로운 데이터가 없는 경우 화면 삭제
                        if (sourceCnt == 0)
                        {
                            if (targetCnt > 0)
                            {
                                target.Clear();
                            }
                            return;
                        }

                        if (targetCnt <= sourceCnt)
                        {
                            //새로운 데이터 가 많은 경우
                            //적은쪽(기존)을 기준으로 그만큼만 데이터 갱신
                            for (int i = 0; i < targetCnt; i++)
                            {
                                if (target[i].sellCnAmt != source[i].sellCnAmt || target[i].sellTranPrc != source[i].sellTranPrc)
                                {
                                    string tempsellCnAmt = source[i].sellCnAmt;
                                    string tempsellTranPrc = source[i].sellTranPrc;
                                    string tempsellfloatFormat = source[i].floatFormat;
                                    string tempsellTranPrcPer = "0%";

                                    if (tempsellTranPrc != "0")
                                    {
                                        if ((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                        {
                                            tempsellTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                        }
                                    }
                                    tempsellTranPrcPer = tempsellTranPrcPer.IndexOf('-') < 0 ? "+" + tempsellTranPrcPer : tempsellTranPrcPer;
                                    string tempfillPrc = decimal.Parse(source[i].sellTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].sellTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());

                                    target[i].sellCnAmt = tempsellCnAmt;
                                    target[i].sellTranPrc = tempsellTranPrc;
                                    target[i].sellTranPrcPer = tempsellTranPrcPer;
                                    target[i].fillPrc = tempfillPrc;
                                    target[i].maxCnAmt = tempMaxCnAmt;
                                    target[i].floatFormat= tempsellfloatFormat;
                                }
                            }

                            if (targetCnt < sourceCnt)
                            {
                                //위에 로직 처리하고 남는 데이터 화면에 추가
                                for (int i = targetCnt; i < sourceCnt; i++)
                                {
                                    string tempsellCnAmt = source[i].sellCnAmt;
                                    string tempsellTranPrc = source[i].sellTranPrc;
                                    string tempsellfloatFormat = source[i].floatFormat;
                                    string tempsellTranPrcPer = "0%";

                                    if (tempsellTranPrc != "0")
                                    {
                                        if ((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                        {
                                            tempsellTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                        }
                                    }

                                    tempsellTranPrcPer = tempsellTranPrcPer.IndexOf('-') < 0 ? "+" + tempsellTranPrcPer : tempsellTranPrcPer;
                                    string tempfillPrc = decimal.Parse(source[i].sellTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].sellTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());
                                    target.Add(ViewModelSource.Create(() => new ResponseExchangeDashboardOrderListModel() { sellCnAmt = tempsellCnAmt, sellTranPrc = tempsellTranPrc, sellTranPrcPer = tempsellTranPrcPer, fillPrc = tempfillPrc, maxCnAmt = tempMaxCnAmt, floatFormat= tempsellfloatFormat }));
                                }
                            }
                        }
                        else
                        {
                            //새로운 데이터가 적을 경우
                            //적은쪽(신규)을 기준으로 그만큼만 데이터 갱신
                            for (int i = 0; i < sourceCnt; i++)
                            {
                                if (target[i].sellCnAmt != source[i].sellCnAmt || target[i].sellTranPrc != source[i].sellTranPrc)
                                {
                                    string tempsellCnAmt = source[i].sellCnAmt;
                                    string tempsellTranPrc = source[i].sellTranPrc;
                                    string tempsellfloatFormat = source[i].floatFormat;
                                    string tempsellTranPrcPer = "0%";

                                    if (tempsellTranPrc != "0")
                                    {
                                        if ((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) != 0 && CoinMarketPrc != 0)
                                        {
                                            tempsellTranPrcPer = string.Format("{0:0.00}", (((decimal.Parse(tempsellTranPrc) - CoinMarketPrc) / CoinMarketPrc) * 100)) + "%";
                                        }
                                    }

                                    tempsellTranPrcPer = tempsellTranPrcPer.IndexOf('-') < 0 ? "+" + tempsellTranPrcPer : tempsellTranPrcPer;
                                    string tempfillPrc = decimal.Parse(source[i].sellTranPrc.Replace(",", "")) > CoinMarketPrc ? Brushes.Red.ToString() : (decimal.Parse(source[i].sellTranPrc.Replace(",", "")) != CoinMarketPrc ? Brushes.DodgerBlue.ToString() : Brushes.Black.ToString());

                                    target[i].sellCnAmt = tempsellCnAmt;
                                    target[i].sellTranPrc = tempsellTranPrc;
                                    target[i].sellTranPrcPer = tempsellTranPrcPer;
                                    target[i].fillPrc = tempfillPrc;
                                    target[i].maxCnAmt = tempMaxCnAmt;
                                    target[i].floatFormat = tempsellfloatFormat;
                                }
                            }

                            //삭제된 데이터 화면에서 삭제
                            if (targetCnt > sourceCnt)
                            {
                                for (int i = targetCnt - 1; i > sourceCnt - 1; i--)
                                {
                                    target.RemoveAt(i);
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
        }

        public void CmdMenuSelected(DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e)
        {
            try
            {
                if (e.NewSelectedIndex.Equals(1))
                {
                    if (!autoCheck)
                    {
                        alert = new Alert(Localization.Resource.Common_Alert_13, 400);
                        alert.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdMarketTabSelected(DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e)
        {
            try
            {
                if (e.NewSelectedIndex.Equals(0))
                {
                    SearchAbleBuy();
                    BuyCommonOrder.Clear();
                    BuyCommonOrder.SelectedErrorRate = BuyCommonOrder.ErrorType[2];
                    BuyCommonOrder.OrderCoinType = PriceCode;
                    //if (BuyCommonOrder.OrderOriginalType.Equals(EnumLib.ExchangeCurrencyCode.ADM))
                    //{
                    //    BuyCommonOrder.MarketMode = true;
                    //}
                    //else
                    //{
                        BuyCommonOrder.MarketMode = false;
                    //}
                }
                else
                {
                    SearchAbleSell();
                    SellCommonOrder.Clear();
                    SellCommonOrder.SelectedErrorRate = SellCommonOrder.ErrorType[2];
                    SellCommonOrder.RecCoinType = PriceCode;
                    SellCommonOrder.MarketMode = false;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdBuyCommonCnt(string Percentage)
        {
            try
            {
                if (BuyCommonOrder.OrderAbleCnt.Equals(0) || BuyCommonOrder.OrderAbleCnt.Equals(null)) return;

                if (PriceType.Equals(CommonLib.StandardCurcyNm))
                {
                    if (Percentage.Equals("5%"))
                        BuyCommonOrder.OrderPreCnt = Math.Truncate(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.05")).ToString()));
                    else if (Percentage.Equals("10%"))
                        BuyCommonOrder.OrderPreCnt = Math.Truncate(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.1")).ToString()));
                    else if (Percentage.Equals("25%"))
                        BuyCommonOrder.OrderPreCnt = Math.Truncate(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.25")).ToString()));
                    else if (Percentage.Equals("50%"))
                        BuyCommonOrder.OrderPreCnt = Math.Truncate(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.5")).ToString()));
                    else if (Percentage.Equals("100%"))
                        BuyCommonOrder.OrderPreCnt = Math.Truncate((decimal)BuyCommonOrder.OrderAbleCnt);
                }
                else
                {
                    if (Percentage.Equals("5%"))
                        BuyCommonOrder.OrderPreCnt = Math.Round(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.05")).ToString()), 8);
                    else if (Percentage.Equals("10%"))
                        BuyCommonOrder.OrderPreCnt = Math.Round(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.1")).ToString()), 8);
                    else if (Percentage.Equals("25%"))
                        BuyCommonOrder.OrderPreCnt = Math.Round(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.25")).ToString()), 8);
                    else if (Percentage.Equals("50%"))
                        BuyCommonOrder.OrderPreCnt = Math.Round(decimal.Parse((BuyCommonOrder.OrderAbleCnt * decimal.Parse("0.5")).ToString()), 8);
                    else if (Percentage.Equals("100%"))
                        BuyCommonOrder.OrderPreCnt = Math.Round((decimal)BuyCommonOrder.OrderAbleCnt, 8);
                }

                if (BuyCommonOrder.OrderCnt < 0)
                    BuyCommonOrder.OrderPreCnt = 0;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSellCommonCnt(string Percentage)
        {
            try
            {
                if (SellCommonOrder.OrderAbleCnt.Equals(0) || SellCommonOrder.OrderAbleCnt.Equals(null)) return;

                if (Percentage.Equals("5%"))
                    SellCommonOrder.OrderCnt = Math.Round(decimal.Parse((SellCommonOrder.OrderAbleCnt * decimal.Parse("0.05")).ToString()), 8);
                else if (Percentage.Equals("10%"))
                    SellCommonOrder.OrderCnt = Math.Round(decimal.Parse((SellCommonOrder.OrderAbleCnt * decimal.Parse("0.1")).ToString()), 8);
                else if (Percentage.Equals("25%"))
                    SellCommonOrder.OrderCnt = Math.Round(decimal.Parse((SellCommonOrder.OrderAbleCnt * decimal.Parse("0.25")).ToString()), 8);
                else if (Percentage.Equals("50%"))
                    SellCommonOrder.OrderCnt = Math.Round(decimal.Parse((SellCommonOrder.OrderAbleCnt * decimal.Parse("0.5")).ToString()), 8);
                else if (Percentage.Equals("100%"))
                    SellCommonOrder.OrderCnt = Math.Round((decimal)SellCommonOrder.OrderAbleCnt, 8);

                if (SellCommonOrder.OrderCnt < 0)
                    SellCommonOrder.OrderCnt = 0;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public decimal Round(decimal num, int digt)
        {
            try
            {
                decimal d = decimal.Parse(Math.Pow(10.0, digt).ToString());
                return Math.Truncate(num * d) / d;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return 0;
            }
        }

        /// <summary>
        /// 주문내역 구매가 클릭시 지정가 구매/판매에 값 설정
        /// </summary>
        /// <param name="p"></param>
        public void BuyOrderSelect(string p)
        {
            try
            {
                p = p.Replace(",", "");
                BuyCommonOrder.OrderPrc = decimal.Parse(p);
                SellCommonOrder.OrderPrc = decimal.Parse(p);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 주문내역 판매가 클릭시 지정가 구매/판매에 값 설정
        /// </summary>
        /// <param name="p"></param>
        public void SellOrderSelect(string p)
        {
            try
            {
                p = p.Replace(",", "");
                BuyCommonOrder.OrderPrc = decimal.Parse(p);
                SellCommonOrder.OrderPrc = decimal.Parse(p);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 구매
        /// </summary>
        public void CmdBuyComOrder()
        {
            try
            {
                if (CommonLib.UserFuncCheck() == "Y")
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                //구매 제한 체크
                using (RequestBuyCheckModel req = new RequestBuyCheckModel())
                {
                    req.mkKndCd = StringEnum.GetStringValue(PriceCode);//마켓 코드
                    req.cnKndCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType); //구매하려는 통화
                    if (BuyCommonOrder.MarketMode)
                    {
                        req.tradePrc = (decimal?)BuyCommonOrder.OrderMarketPrc == null ? 0 : (decimal)BuyCommonOrder.OrderMarketPrc;
                    }
                    else
                    {
                        req.tradePrc = (decimal?)BuyCommonOrder.OrderPrc == null ? 0 : (decimal)BuyCommonOrder.OrderPrc; //거래 금액
                    }

                    using (ResponseBuyCheckModel res = WebApiLib.SyncCall<ResponseBuyCheckModel, RequestBuyCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.buyCheck.Equals("N"))
                            {
                                //구매 제한 경고
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }
                            else if (res.data.buyCheck.Equals("Y"))
                            {
                                string resultCd = res.data.failCd;

                                if (resultCd.Equals("996"))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_51, res.data.askingPrc.ToString("#,0.########"), PriceType), 330);
                                    alert.ShowDialog();
                                    return;
                                }
                            }
                        }
                    }
                }

                alert = new Alert(Localization.Resource.CoinTrading_Alert_1, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    #region 공통체크

                    //if (PriceCode.Equals(EnumLib.ExchangeCurrencyCode.KRW))
                    //{
                    //    if (!BuyCommonOrder.MarketMode)
                    //    {
                    //        if (BuyCommonOrder.OrderOriginalType.Equals(EnumLib.ExchangeCurrencyCode.ADM))
                    //        {
                    //            if (BuyCommonOrder.OrderPrc < 50)
                    //            {
                    //                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_52, CommonLib.StandardCurcyNm), 320);
                    //                alert.ShowDialog();
                    //                return;
                    //            }
                    //        }
                    //    }
                    //}

                    if (BuyCommonOrder.OrderCnt < CucyCoin.TradeMinCnt)
                    {
                        alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_8, CucyCoin.TradeMinCnt.ToString("#,0.########")));
                        alert.ShowDialog();
                        return;
                    }

                    if (BuyCommonOrder.OrderPreCnt == 0)
                    {
                        alert = new Alert(Localization.Resource.CoinTrading_Alert_18_1);
                        alert.ShowDialog();
                        return;
                    }

                    using (RequestTradingLmtAmtCheckModel req = new RequestTradingLmtAmtCheckModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.mkState = StringEnum.GetStringValue(PriceCode);
                        req.curcyCd = CucyCoin.CoinCode;
                        req.tradeType = "B";
                        req.phsAmt = (decimal)BuyCommonOrder.OrderPreCnt;

                        using (ResponseTradingLmtAmtCheckModel res = WebApiLib.SyncCall<ResponseTradingLmtAmtCheckModel, RequestTradingLmtAmtCheckModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string failCd = res.data.failCd;
                                if (failCd.Equals("993"))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_49, decimal.Parse(res.data.buyLmt1Day.ToString()).ToString("#,0.########"), PriceType), 320); //1회
                                    alert.ShowDialog();
                                    return;
                                }
                                if (failCd.Equals("994"))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_48, decimal.Parse(res.data.buyLmt1Once.ToString()).ToString("#,0.########"), PriceType), 320); //1회
                                    alert.ShowDialog();
                                    return;
                                }
                            }
                        }
                    }

                    #endregion

                    IsBusy = true;

                    decimal? OrdPrc;

                    if (BuyCommonOrder.MarketMode)
                    {
                        if (PriceType.Equals(CommonLib.StandardCurcyNm))
                        {
                            if (BuyCommonOrder.OrderPreCnt < CucyCoin.TradeFillMinPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, CucyCoin.TradeFillMinPrc.ToString("##,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }
                        }
                        else
                        {
                            if (BuyCommonOrder.OrderPreCnt < PriceCoin.TradeMinCnt)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, PriceCoin.TradeMinCnt.ToString("#,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }
                        }

                        using (RequestTradingCoinToCoinBuyModel req = new RequestTradingCoinToCoinBuyModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType);  //구매하려는 통화
                            req.payCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType); //결제하려는 통화

                            using (ResponseTradingCoinToCoinBuyModel res = WebApiLib.SyncCall<ResponseTradingCoinToCoinBuyModel, RequestTradingCoinToCoinBuyModel>(req))
                            {
                                if (BuyCommonOrder.OrderCoinType == EnumLib.ExchangeCurrencyCode.KRW)
                                {
                                    OrdPrc = res.data.price;
                                }
                                else
                                {
                                    OrdPrc = res.data.cnAmt;
                                }
                            }
                        }

                        if (CommonLib.Market.Contains(PriceCode))
                        {
                            if (BuyCommonOrder.OrderPreCnt > OrdPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_15, BuyCommonOrder.OrderCoinType.ToString()));
                                alert.ShowDialog();
                                return;
                            }

                            if (BuyCommonOrder.OrderPreCnt > BuyCommonOrder.OrderAbleCnt)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_17);
                                alert.ShowDialog();
                                return;
                            }

                            using (RequestTradingBuyModel req = new RequestTradingBuyModel())
                            {
                                req.apntPhsYn = "N";//시장가
                                req.apntPhsCd = StringEnum.GetStringValue(EnumLib.TradeContentCode.buyMarketPrice); //거래내용 추가
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                                req.buyCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType);//수령코인
                                req.payCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);//주문코인
                                req.mkState = PriceType;
                                req.usePrc = BuyCommonOrder.OrderPreCnt;
                                req.usePointPrc = 0;
                                req.phsPrc = BuyCommonOrder.OrderMarketPrc.ToString();//"주문가격";
                                req.phsAmt = BuyCommonOrder.OrderCnt.ToString();//"주문수량";
                                req.regIp = MainViewModel.LoginDataModel.regIp;

                                using (ResponseTradingBuyModel res = WebApiLib.SyncCall<ResponseTradingBuyModel, RequestTradingBuyModel>(req))
                                {
                                    if (res.resultStrCode == "000")
                                    {
                                        alert = new Alert(Localization.Resource.Common_Alert_17, 320);
                                        alert.ShowDialog();

                                        BuyCommonOrder.Clear();
                                        SearchAbleBuy();
                                        SearchAbleSell();
                                        SearchNotCon();
                                        SearchTradeHistory();
                                        Messenger.Default.Send("AssetsRefresh");
                                    }
                                }
                            }
                        }
                        //else
                        //{
                        //    if (BuyCommonOrder.OrderPreCnt > OrdPrc)
                        //    {
                        //        alert = new Alert(Localization.Resource.CoinTrading_Alert_21);
                        //        alert.ShowDialog();
                        //        return;
                        //    }
                        //    if (BuyCommonOrder.OrderPreCnt > BuyCommonOrder.OrderAbleCnt)
                        //    {
                        //        alert = new Alert(Localization.Resource.CoinTrading_Alert_22);
                        //        alert.ShowDialog();
                        //        return;
                        //    }
                        //    //코인스왑 API
                        //    using (RequestTradingCoinSwapModel req = new RequestTradingCoinSwapModel())
                        //    {
                        //        req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                        //        req.buyCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType);//수령코인
                        //        req.payCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);//주문코인                           
                        //        req.phsAmt = BuyCommonOrder.OrderPreCnt.ToString();//"주문수량";
                        //        req.regIp = MainViewModel.LoginDataModel.regIp;

                        //        using (ResponseTradingCoinSwapModel res = WebApiLib.SyncCall<ResponseTradingCoinSwapModel, RequestTradingCoinSwapModel>(req))
                        //        {
                        //            if (res.resultStrCode == "000")
                        //            {
                        //                alert = new Alert(Localization.Resource.Common_Alert_17, 320);
                        //                alert.ShowDialog();

                        //                BuyCommonOrder.Clear();
                        //                SearchAbleBuy();
                        //                SearchAbleSell();
                        //                SearchNotCon();
                        //                SearchTradeHistory();
                        //                Messenger.Default.Send("AssetsRefresh");
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (PriceType.Equals(CommonLib.StandardCurcyNm))
                        {
                            if (BuyCommonOrder.OrderPreCnt < CucyCoin.TradeFillMinPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, CucyCoin.TradeFillMinPrc.ToString("##,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }

                            //DB 조회
                            using (RequestTradingAbleBuyModel req = new RequestTradingAbleBuyModel())
                            {
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;
                                req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);

                                using (ResponseTradingAbleBuyModel res = WebApiLib.SyncCall<ResponseTradingAbleBuyModel, RequestTradingAbleBuyModel>(req))
                                {
                                    //지정가
                                    OrdPrc = res.data.price;
                                }
                            }

                            if (BuyCommonOrder.OrderPreCnt > OrdPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_15, BuyCommonOrder.OrderCoinType.ToString()));
                                alert.ShowDialog();
                                return;
                            }
                        }
                        else
                        {
                            if (BuyCommonOrder.OrderPreCnt < PriceCoin.TradeMinCnt)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, PriceCoin.TradeMinCnt.ToString("#,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }

                            using (RequestTradingCoinToCoinSellModel req = new RequestTradingCoinToCoinSellModel())
                            {
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;
                                req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);  //구매하려는 통화
                                req.rcvCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.RecCoinType); //결제하려는 통화
                                req.mkState = PriceType;

                                using (ResponseTradingCoinToCoinSellModel res = WebApiLib.SyncCall<ResponseTradingCoinToCoinSellModel, RequestTradingCoinToCoinSellModel>(req))
                                {
                                    OrdPrc = res.data.curcyAmt;
                                }
                            }

                            if (BuyCommonOrder.OrderPreCnt > OrdPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_15, BuyCommonOrder.OrderCoinType.ToString()));
                                alert.ShowDialog();
                                return;
                            }
                        }

                        // 최종금액
                        if (BuyCommonOrder.OrderPreCnt > BuyCommonOrder.OrderAbleCnt)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_17);
                            alert.ShowDialog();
                            return;
                        }

                        using (RequestTradingBuyModel req = new RequestTradingBuyModel())
                        {
                            req.apntPhsYn = "Y";//지정가
                            req.apntPhsCd = StringEnum.GetStringValue(EnumLib.TradeContentCode.buyLimitPrice); //거래내용 추가
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                            req.buyCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType);//수령코인
                            req.payCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);//주문코인 
                            req.mkState = PriceType;
                            req.usePrc = BuyCommonOrder.OrderPreCnt;      //
                            req.usePointPrc = 0;
                            req.phsPrc = BuyCommonOrder.OrderPrc.ToString();//"주문가격";
                            req.phsAmt = BuyCommonOrder.OrderCnt.ToString();//"주문수량";
                            req.regIp = MainViewModel.LoginDataModel.regIp;

                            using (ResponseTradingBuyModel res = WebApiLib.SyncCall<ResponseTradingBuyModel, RequestTradingBuyModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    alert = new Alert(Localization.Resource.Common_Alert_17, 320);
                                    alert.ShowDialog();

                                    BuyCommonOrder.Clear();
                                    SearchAbleBuy();
                                    SearchAbleSell();
                                    SearchNotCon();
                                    SearchTradeHistory();
                                    Messenger.Default.Send("AssetsRefresh");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                alert = new Alert(Localization.Resource.CoinTrading_Alert_16_1 + "\n" + Localization.Resource.CoinTrading_Alert_16_2, 350, 150);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 판매
        /// </summary>
        public void CmdSellComOrder()
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

                //판매제한 확인
                using (RequestTradeSellLimitModel req = new RequestTradeSellLimitModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.mkState = StringEnum.GetStringValue(PriceCode);
                    req.curcyCd = CucyCoin.CoinCode;

                    using (ResponseTradeSellLimitModel res = WebApiLib.SyncCall<ResponseTradeSellLimitModel, RequestTradeSellLimitModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string tradeType = res.data.tradeType;

                            if (tradeType.Equals("1"))
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_50);
                                alert.ShowDialog();
                                return;
                            }
                            else if (tradeType.Equals("4"))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                }

                alert = new Alert(Localization.Resource.CoinTrading_Alert_2, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    #region 공통체크   

                    //if (PriceCode.Equals(EnumLib.ExchangeCurrencyCode.KRW))
                    //{
                    //    if (!SellCommonOrder.MarketMode)
                    //    {
                    //        if (SellCommonOrder.OrderOriginalType.Equals(EnumLib.ExchangeCurrencyCode.ADM))
                    //        {
                    //            if (SellCommonOrder.OrderPrc < 50)
                    //            {
                    //                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_53, CommonLib.StandardCurcyNm), 320);
                    //                alert.ShowDialog();
                    //                return;
                    //            }
                    //        }
                    //    }
                    //}

                    if (SellCommonOrder.OrderCnt < CucyCoin.TradeMinCnt)
                    {
                        alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_8, CucyCoin.TradeMinCnt.ToString("#,0.########")));
                        alert.ShowDialog();
                        return;
                    }

                    if (SellCommonOrder.OrderPreCnt == 0)
                    {
                        alert = new Alert(Localization.Resource.CoinTrading_Alert_18_1);
                        alert.ShowDialog();
                        return;
                    }

                    if (!CommonLib.sgcTradeOkList.Contains(MainViewModel.LoginDataModel.userEmail))
                    {
                        using (RequestTradingLmtAmtCheckModel req = new RequestTradingLmtAmtCheckModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.mkState = StringEnum.GetStringValue(PriceCode);
                            req.curcyCd = CucyCoin.CoinCode;
                            req.tradeType = "S";
                            req.phsAmt = (decimal)SellCommonOrder.OrderCnt;

                            using (ResponseTradingLmtAmtCheckModel res = WebApiLib.SyncCall<ResponseTradingLmtAmtCheckModel, RequestTradingLmtAmtCheckModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    string failCd = res.data.failCd;
                                    if (failCd.Equals("991"))
                                    {
                                        alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_49, decimal.Parse(res.data.selLmt1Day.ToString()).ToString("#,0.########"), KindOfCoin.ToString()), 320); //1회
                                        alert.ShowDialog();
                                        return;
                                    }
                                    else if (failCd.Equals("992"))
                                    {
                                        alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_48, decimal.Parse(res.data.selLmt1Once.ToString()).ToString("#,0.########"), KindOfCoin.ToString()), 320); //1회
                                        alert.ShowDialog();
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    decimal? OrdSellAmt;

                    if (SellCommonOrder.MarketMode)
                    {
                        if (PriceType.Equals(CommonLib.StandardCurcyNm))
                        {
                            if (SellCommonOrder.OrderPreCnt < CucyCoin.TradeFillMinPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, CucyCoin.TradeFillMinPrc.ToString("##,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }
                        }
                        else
                        {
                            if (SellCommonOrder.OrderPreCnt < PriceCoin.TradeMinCnt)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, PriceCoin.TradeMinCnt.ToString("#,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }
                        }

                        //DB 조회
                        using (RequestTradingAbleSellModel req = new RequestTradingAbleSellModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);

                            using (ResponseTradingAbleSellModel res = WebApiLib.SyncCall<ResponseTradingAbleSellModel, RequestTradingAbleSellModel>(req))
                            {
                                //판매가능수량
                                OrdSellAmt = res.data.avaSellAmt;
                            }
                        }

                        if (SellCommonOrder.OrderAbleCnt > OrdSellAmt)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_19);
                            alert.ShowDialog();
                            return;
                        }
                        if (SellCommonOrder.OrderAbleCnt < SellCommonOrder.OrderCnt)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_20);
                            alert.ShowDialog();
                            return;
                        }

                        IsBusy = true;

                        if (CommonLib.Market.Contains(PriceCode))
                        {
                            using (RequestTradingSellModel req = new RequestTradingSellModel())
                            {
                                req.apntPhsYn = "N";//시장가
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                                req.sellCurcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);//주문코인
                                req.rcvCurcyCd = StringEnum.GetStringValue(SellCommonOrder.RecCoinType);//수령코인
                                req.mkState = PriceType;
                                req.phsPrc = "1"; //SellMarketOrder.OrderPrc.ToString();//"주문가격";
                                req.phsAmt = SellCommonOrder.OrderCnt.ToString();//"주문수량";
                                req.regIp = MainViewModel.LoginDataModel.regIp;

                                using (ResponseTradingSellModel res = WebApiLib.SyncCall<ResponseTradingSellModel, RequestTradingSellModel>(req))
                                {
                                    if (res.resultStrCode == "000")
                                    {
                                        alert = new Alert(Localization.Resource.Common_Alert_17, 320);
                                        alert.ShowDialog();

                                        SellCommonOrder.Clear();
                                        SearchAbleBuy();
                                        SearchAbleSell();
                                        SearchNotCon();
                                        SearchTradeHistory();
                                        Messenger.Default.Send("AssetsRefresh");
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (RequestTradingCoinSwapModel req = new RequestTradingCoinSwapModel())
                            {
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                                req.buyCurcyCd = StringEnum.GetStringValue(SellCommonOrder.RecCoinType);//수령코인
                                req.payCurcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);//주문코인                           
                                req.phsAmt = SellCommonOrder.OrderCnt.ToString();//"주문수량";
                                req.regIp = MainViewModel.LoginDataModel.regIp;

                                using (ResponseTradingCoinSwapModel res = WebApiLib.SyncCall<ResponseTradingCoinSwapModel, RequestTradingCoinSwapModel>(req))
                                {
                                    if (res.resultStrCode == "000")
                                    {
                                        alert = new Alert(Localization.Resource.Common_Alert_17, 320);
                                        alert.ShowDialog();

                                        SellCommonOrder.Clear();
                                        SearchAbleBuy();
                                        SearchAbleSell();
                                        SearchNotCon();
                                        SearchTradeHistory();
                                        Messenger.Default.Send("AssetsRefresh");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PriceType.Equals(CommonLib.StandardCurcyNm))
                        {
                            if (SellCommonOrder.OrderPreCnt < CucyCoin.TradeFillMinPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, CucyCoin.TradeFillMinPrc.ToString("##,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }

                            //DB 조회
                            using (RequestTradingAbleSellModel req = new RequestTradingAbleSellModel())
                            {
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;
                                req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);

                                using (ResponseTradingAbleSellModel res = WebApiLib.SyncCall<ResponseTradingAbleSellModel, RequestTradingAbleSellModel>(req))
                                {
                                    //판매가능수량
                                    OrdSellAmt = res.data.avaSellAmt;
                                }
                            }

                            if (SellCommonOrder.OrderAbleCnt > OrdSellAmt)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_19);
                                alert.ShowDialog();
                                SearchAbleSell();
                                return;
                            }
                        }
                        else
                        {
                            if (SellCommonOrder.OrderPreCnt < PriceCoin.TradeMinCnt)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_11, PriceCoin.TradeMinCnt.ToString("#,##0.####"), PriceType));
                                alert.ShowDialog();
                                return;
                            }

                            using (RequestTradingCoinToCoinSellModel req = new RequestTradingCoinToCoinSellModel())
                            {
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;
                                req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderCoinType);  //구매하려는 통화
                                req.rcvCurcyCd = StringEnum.GetStringValue(SellCommonOrder.RecCoinType); //결제하려는 통화
                                req.mkState = PriceType;

                                using (ResponseTradingCoinToCoinSellModel res = WebApiLib.SyncCall<ResponseTradingCoinToCoinSellModel, RequestTradingCoinToCoinSellModel>(req))
                                {
                                    OrdSellAmt = res.data.curcyAmt;
                                }
                            }

                            if (SellCommonOrder.OrderAbleCnt > OrdSellAmt)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_19);
                                alert.ShowDialog();
                                SearchAbleSell();
                                return;
                            }
                        }

                        if (SellCommonOrder.OrderPrc <= 0)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_18);
                            alert.ShowDialog();
                            return;
                        }

                        if (SellCommonOrder.OrderAbleCnt < SellCommonOrder.OrderCnt)
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_20);
                            alert.ShowDialog();
                            SearchAbleSell();
                            return;
                        }

                        IsBusy = true;

                        using (RequestTradingSellModel req = new RequestTradingSellModel())
                        {
                            req.apntPhsYn = "Y";//지정가
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                            req.sellCurcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);//주문코인
                            req.rcvCurcyCd = StringEnum.GetStringValue(SellCommonOrder.RecCoinType);//수령코인
                            req.mkState = PriceType;
                            req.phsPrc = SellCommonOrder.OrderPrc.ToString();//"주문가격";
                            req.phsAmt = SellCommonOrder.OrderCnt.ToString();//"주문수량";
                            req.regIp = MainViewModel.LoginDataModel.regIp;

                            using (ResponseTradingSellModel res = WebApiLib.SyncCall<ResponseTradingSellModel, RequestTradingSellModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    alert = new Alert(Localization.Resource.Common_Alert_17, 320);
                                    alert.ShowDialog();

                                    SellCommonOrder.Clear();
                                    SearchAbleBuy();
                                    SearchAbleSell();
                                    SearchNotCon();
                                    SearchTradeHistory();
                                    Messenger.Default.Send("AssetsRefresh");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                alert = new Alert(Localization.Resource.CoinTrading_Alert_16_1 + "\n" + Localization.Resource.CoinTrading_Alert_16_2, 350, 150);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 자동거래 구매 설정
        /// </summary>
        public async void CmdBuySellAutoOrder()
        {
            try
            {
                if (CommonLib.UserFuncCheck() == "Y")
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                if (!autoCheck)
                {
                    alert = new Alert(Localization.Resource.Common_Alert_13, 400);
                    alert.ShowDialog();
                    return;
                }

                decimal dAskingPrc = 0;

                //구매 제한 체크
                using (RequestBuyCheckModel req = new RequestBuyCheckModel())
                {
                    req.mkKndCd = StringEnum.GetStringValue(PriceCode);//마켓 코드
                    req.cnKndCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType); //구매하려는 통화

                    using (ResponseBuyCheckModel res = WebApiLib.SyncCall<ResponseBuyCheckModel, RequestBuyCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.buyCheck.Equals("N"))
                            {
                                //구매 제한 경고
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }
                            else if (res.data.buyCheck.Equals("Y"))
                            {
                                dAskingPrc = res.data.askingPrc;
                            }
                        }
                    }
                }

                if (IsBuySellTradeStart.Equals(false))
                {
                    alert = new Alert(Localization.Resource.CoinTrading_Alert_23, 400);
                    alert.ShowDialog();
                    return;
                }

                alert = new Alert(Localization.Resource.CoinTrading_Alert_25, Alert.ButtonType.YesNo, 320);
                if (alert.ShowDialog() == true)
                {
                    #region 공통체크
                    bool prcCheck = false;

                    foreach (var item in BuySellOrderVM.list)
                    {
                        if (item.OrderPrc == null)
                        {
                            prcCheck = true;
                        }
                        else if (item.OrderPrc != 0)
                        {
                            prcCheck = false;
                            break;
                        }
                        else
                        {
                            prcCheck = true;
                        }
                    }

                    if (prcCheck)
                    {
                        alert = new Alert(Localization.Resource.CoinTrading_Alert_26);
                        alert.ShowDialog();
                        return;
                    }

                    decimal? TempBuyOrderPrc = null;
                    foreach (var item in BuySellOrderVM.list.Where(w => w.TradeType == "B").OrderBy(o => o.Rank))
                    {
                        //if ((item.OrderPrc * item.OrderCnt) > LimitContentData.buyLmt1Once)
                        //{
                        //    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_48, LimitContentData.buyLmt1Once.ToString("#,#"), item.OrderCoinType.ToString()), 320);
                        //    alert.ShowDialog();
                        //    return;
                        //}

                        if (item.Rank == 1)
                        {
                            TempBuyOrderPrc = item.OrderPrc;
                        }
                        else
                        {
                            string sMsg = string.Empty;
                            if ((TempBuyOrderPrc == null || TempBuyOrderPrc == 0) && item.OrderPrc != 0)
                            {
                                sMsg = string.Format(Localization.Resource.CoinTrading_Alert_40, (item.Rank - 1).ToString());
                                alert = new Alert(sMsg, 400);
                                alert.ShowDialog();
                                return;
                            }
                            else
                            {
                                TempBuyOrderPrc = item.OrderPrc;
                            }
                        }

                        if ((item.OrderPrc != null) && item.OrderErrorRate.Equals(0) && (item.OrderPrc != 0) && item.OrderErrorRate.Equals(0))
                        {
                            string sMsg = string.Empty;
                            sMsg = string.Format(Localization.Resource.CoinTrading_Alert_33, item.Rank.ToString());
                            alert = new Alert(sMsg, 400);
                            alert.ShowDialog();
                            return;
                        }

                        if (!dAskingPrc.Equals(0))
                        {
                            if (item.OrderPrc < dAskingPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_51, dAskingPrc.ToString("#,0.########"), PriceType), 330);
                                alert.ShowDialog();
                                return;
                            }
                        }

                        if (item.OrderErrorRate.Equals(0)) continue;
                        if (item.OrderCnt == null || item.OrderCnt == 0)
                        {
                            string sMsg = string.Empty;
                            sMsg = string.Format(Localization.Resource.CoinTrading_Alert_44, item.Rank.ToString());

                            alert = new Alert(sMsg, 400);
                            alert.ShowDialog();
                            return;
                        }
                        if (item.OrderCnt < CucyCoin.TradeMinCnt)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_8, CucyCoin.TradeMinCnt));
                            alert.ShowDialog();
                            return;
                        }



                        if (item.OrderErrorRate.Equals(0)) continue;
                        if (item.OrderPrc == null) continue;
                    }

                    decimal? TempSellOrderPrc = null;
                    bool bSellCheck = false;
                    foreach (var item in BuySellOrderVM.list.Where(w => w.TradeType == "S").OrderBy(o => o.Rank))
                    {
                        if (item.OrderCnt > LimitContentData.selLmt1Once)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_48, LimitContentData.selLmt1Once.ToString("#,#"), item.OrderCoinType.ToString()), 320);
                            alert.ShowDialog();
                            return;
                        }

                        if (item.Rank == 1)
                        {
                            TempSellOrderPrc = item.OrderPrc;
                        }
                        else
                        {
                            string sMsg = string.Empty;
                            if ((TempSellOrderPrc == null || TempSellOrderPrc == 0) && item.OrderPrc != 0)
                            {
                                sMsg = string.Format(Localization.Resource.CoinTrading_Alert_40, (item.Rank - 1).ToString());
                                alert = new Alert(sMsg, 400);
                                alert.ShowDialog();
                                return;
                            }
                            else
                            {
                                TempSellOrderPrc = item.OrderPrc;
                            }
                        }

                        if ((item.OrderPrc != null) && item.OrderErrorRate.Equals(0) && (item.OrderPrc != 0) && item.OrderErrorRate.Equals(0))
                        {
                            string sMsg = string.Empty;
                            sMsg = string.Format(Localization.Resource.CoinTrading_Alert_34, item.Rank.ToString());
                            alert = new Alert(sMsg, 400);
                            alert.ShowDialog();
                            return;
                        }
                        if (item.OrderErrorRate.Equals(0)) continue;
                        if (item.OrderCnt == null || item.OrderCnt == 0)
                        {
                            string sMsg = string.Empty;
                            sMsg = string.Format(Localization.Resource.CoinTrading_Alert_45, item.Rank.ToString());
                            alert = new Alert(sMsg, 400);
                            alert.ShowDialog();
                            return;
                        }
                        if (item.OrderCnt < CucyCoin.TradeMinCnt)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_8, CucyCoin.TradeMinCnt));
                            alert.ShowDialog();
                            return;
                        }


                        if (item.OrderErrorRate.Equals(0)) continue;
                        if (item.OrderPrc == null) continue;

                        bSellCheck = true;
                    }


                    #endregion

                    if (bSellCheck)
                    {
                        //판매제한 확인
                        using (RequestTradeSellLimitModel req = new RequestTradeSellLimitModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.mkState = StringEnum.GetStringValue(PriceCode);
                            req.curcyCd = CucyCoin.CoinCode;

                            using (ResponseTradeSellLimitModel res = WebApiLib.SyncCall<ResponseTradeSellLimitModel, RequestTradeSellLimitModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    string tradeType = res.data.tradeType;

                                    if (tradeType.Equals("1"))
                                    {
                                        alert = new Alert(Localization.Resource.CoinTrading_Alert_50);
                                        alert.ShowDialog();
                                        return;
                                    }
                                    else if (tradeType.Equals("4"))
                                    {
                                        alert = new Alert(Localization.Resource.Common_Alert_1);
                                        alert.ShowDialog();
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    IsBusy = true;

                    using (RequestTradingGetAutoSellModel req = new RequestTradingGetAutoSellModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;

                        using (ResponseTradingGetAutoSellModel res = await WebApiLib.AsyncCall<ResponseTradingGetAutoSellModel, RequestTradingGetAutoSellModel>(req))
                        {
                            if (res.data.list.Count > 0)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_23, 400);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }

                    using (RequestTradingAutoBuyModel req = new RequestTradingAutoBuyModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.mkState = PriceType;
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        foreach (var item in BuySellOrderVM.list)
                        {
                            item.IsBuyTradeEnabled = false;
                            item.IsSellTradeEnabled = false;
                            if (!item.OrderErrorRate.Equals(0))
                            {
                                req.cnKndCd = StringEnum.GetStringValue(item.OrderCoinType);
                                req.udFlag = item.TradeType;
                                req.sn = item.Rank;
                                req.prc = item.OrderPrc;
                                req.prcPer = item.OrderErrorRate;
                                req.rtPlsPrc = item.OrderErrorHighPrc;
                                req.rtMnsPrc = item.OrderErrorLowPrc;
                                req.amt = item.OrderCnt;

                                using (ResponseTradingAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingAutoBuyModel, RequestTradingAutoBuyModel>(req))
                                {
                                    //완료                                   
                                    Messenger.Default.Send(item.Clone() as AutoOrderVM);
                                }
                            }
                        }

                        IsBuySellTradeStart = false;
                        IsBuySellTradeCancel = true;
                        IsLossTradeStart = false;
                        IsEnabledLossTab = false;

                        SearchAutoTradeList();

                        using (RequestSendPushModel req2 = new RequestSendPushModel())
                        {
                            req2.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req2.regIp = MainViewModel.LoginDataModel.regIp;
                            req2.contents = Localization.Resource.CoinTrading_Alert_47;
                            req2.pushType = StringEnum.GetStringValue(EnumLib.PushCode.AutoTrade);

                            using (ResponseSendPushModel res2 = await WebApiLib.AsyncCall<ResponseSendPushModel, RequestSendPushModel>(req2))
                            {
                                //푸쉬성공
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                alert = new Alert(Localization.Resource.CoinTrading_Alert_26);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 자동거래 구매 설정 취소
        /// </summary>
        public async void CmdBuySellAutoOrderCancel()
        {
            try
            {
                alert = new Alert(Localization.Resource.CoinTrading_Alert_27, Alert.ButtonType.YesNo, 400);
                if (alert.ShowDialog() == true)
                {
                    IsBusy = true;

                    using (RequestTradingDelAutoBuyModel req = new RequestTradingDelAutoBuyModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;

                        using (ResponseTradingDelAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingDelAutoBuyModel, RequestTradingDelAutoBuyModel>(req))
                        {
                            foreach (var item in BuySellOrderVM.list)
                            {
                                item.IsBuyTradeEnabled = true;
                                item.IsSellTradeEnabled = true;

                                if (!item.OrderErrorRate.Equals(0))
                                {
                                    Messenger.Default.Send(item.Clone() as AutoOrderVM);
                                    item.Clear();
                                }
                            }

                            IsBuySellTradeStart = true;
                            IsBuySellTradeCancel = false;
                            IsLossTradeStart = true;
                            IsEnabledLossTab = true;
                            SearchAutoTradeList();
                            SearchAbleBuy();
                            SearchAbleSell();

                            using (RequestSendPushModel req2 = new RequestSendPushModel())
                            {
                                req2.userEmail = MainViewModel.LoginDataModel.userEmail;
                                req2.regIp = MainViewModel.LoginDataModel.regIp;
                                req2.contents = Localization.Resource.CoinTrading_Alert_47;
                                req2.pushType = StringEnum.GetStringValue(EnumLib.PushCode.AutoTrade);

                                using (ResponseSendPushModel res2 = await WebApiLib.AsyncCall<ResponseSendPushModel, RequestSendPushModel>(req2))
                                {
                                    //푸쉬성공
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                alert = new Alert(Localization.Resource.CoinTrading_Alert_28);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 손절 판매 설정
        /// </summary>
        public async void CmdLossAutoOrder()
        {
            try
            {
                if (CommonLib.UserFuncCheck() == "Y")
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                if (!autoCheck)
                {
                    alert = new Alert(Localization.Resource.Common_Alert_13, 400);
                    alert.ShowDialog();
                    return;
                }

                if (IsLossTradeStart.Equals(false))
                {
                    alert = new Alert(Localization.Resource.CoinTrading_Alert_24, 400);
                    alert.ShowDialog();
                    return;
                }

                //판매제한 확인
                using (RequestTradeSellLimitModel req = new RequestTradeSellLimitModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.mkState = StringEnum.GetStringValue(PriceCode);
                    req.curcyCd = CucyCoin.CoinCode;

                    using (ResponseTradeSellLimitModel res = WebApiLib.SyncCall<ResponseTradeSellLimitModel, RequestTradeSellLimitModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string tradeType = res.data.tradeType;

                            if (tradeType.Equals("1"))
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_50);
                                alert.ShowDialog();
                                return;
                            }
                            else if (tradeType.Equals("4"))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                }

                alert = new Alert(Localization.Resource.CoinTrading_Alert_29, Alert.ButtonType.YesNo, 320);
                if (alert.ShowDialog() == true)
                {
                    #region 공통체크

                    bool prcCheck = false;

                    foreach (var item in LossOrderVM.list)
                    {
                        if (item.OrderPrc == null)
                        {
                            prcCheck = true;
                        }
                        else if (item.OrderPrc != 0)
                        {
                            prcCheck = false;
                            break;
                        }
                        else
                        {
                            prcCheck = true;
                        }
                    }

                    if (prcCheck)
                    {
                        alert = new Alert(Localization.Resource.CoinTrading_Alert_26);
                        alert.ShowDialog();
                        return;
                    }

                    decimal? TempOrderPrc = null;
                    foreach (var item in LossOrderVM.list.OrderBy(o => o.Rank))
                    {
                        if (item.OrderCnt > LimitContentData.selLmt1Once)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_48, LimitContentData.selLmt1Once.ToString("#,#"), item.OrderCoinType.ToString()), 320);
                            alert.ShowDialog();
                            return;
                        }

                        if (item.Rank == 1)
                        {
                            TempOrderPrc = item.OrderPrc;
                        }
                        else
                        {
                            string sMsg = string.Empty;
                            if ((TempOrderPrc == null || TempOrderPrc == 0) && item.OrderPrc != 0)
                            {
                                sMsg = string.Format(Localization.Resource.CoinTrading_Alert_40, (item.Rank - 1).ToString());
                                alert = new Alert(sMsg, 400);
                                alert.ShowDialog();
                                return;
                            }
                            else
                            {
                                TempOrderPrc = item.OrderPrc;
                            }
                        }

                        if ((item.OrderPrc != null) && item.OrderErrorRate.Equals(0) && (item.OrderPrc != 0) && item.OrderErrorRate.Equals(0))
                        {
                            string sMsg = string.Format(Localization.Resource.CoinTrading_Alert_35, item.Rank.ToString());
                            alert = new Alert(sMsg, 400);
                            alert.ShowDialog();
                            return;
                        }
                        if (item.OrderErrorRate.Equals(0)) continue;
                        if (item.OrderCnt == null || item.OrderCnt == 0)
                        {
                            string sMsg = string.Empty;
                            sMsg = string.Format(Localization.Resource.CoinTrading_Alert_46, item.Rank.ToString());
                            alert = new Alert(sMsg, 400);
                            alert.ShowDialog();
                            return;
                        }

                        if (item.OrderCnt < CucyCoin.TradeMinCnt)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_8, CucyCoin.TradeMinCnt));
                            alert.ShowDialog();
                            return;
                        }

                        ////시세별 단위                        
                        //if ((item.OrderPrc % CucyCoin.TradeBidding) != 0)
                        //{
                        //    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_12, CucyCoin.TradeBidding));
                        //    alert.ShowDialog();
                        //    return;
                        //}
                    }

                    #endregion                    

                    IsBusy = true;

                    using (RequestTradingGetAutoBuyModel req = new RequestTradingGetAutoBuyModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;

                        using (ResponseTradingGetAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingGetAutoBuyModel, RequestTradingGetAutoBuyModel>(req))
                        {
                            if (res.data.list.Count > 0)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_24, 400);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }

                    using (RequestTradingAutoSellModel req = new RequestTradingAutoSellModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.mkState = PriceType;
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        foreach (var item in LossOrderVM.list)
                        {
                            item.IsLossTradeEnabled = false;
                            if (!item.OrderErrorRate.Equals(0))
                            {
                                req.cnKndCd = StringEnum.GetStringValue(item.OrderCoinType);
                                req.udFlag = item.TradeType;
                                req.sn = item.Rank;
                                req.prc = item.OrderPrc;
                                req.prcPer = item.OrderErrorRate;
                                req.rtPlsPrc = item.OrderErrorHighPrc;
                                req.rtMnsPrc = item.OrderErrorLowPrc;
                                req.amt = item.OrderCnt;

                                using (ResponseTradingAutoSellModel res = await WebApiLib.AsyncCall<ResponseTradingAutoSellModel, RequestTradingAutoSellModel>(req))
                                {
                                    //완료                                    
                                    Messenger.Default.Send(item.Clone() as AutoOrderVM);
                                }
                            }
                        }

                        IsLossTradeStart = false;
                        IsLossTradeCancel = true;
                        IsBuySellTradeStart = false;
                        IsEnabledBuySellTab = false;
                        SearchAutoTradeList();

                        using (RequestSendPushModel req2 = new RequestSendPushModel())
                        {
                            req2.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req2.regIp = MainViewModel.LoginDataModel.regIp;
                            req2.contents = Localization.Resource.CoinTrading_Alert_47;
                            req2.pushType = StringEnum.GetStringValue(EnumLib.PushCode.AutoTrade);

                            using (ResponseSendPushModel res2 = await WebApiLib.AsyncCall<ResponseSendPushModel, RequestSendPushModel>(req2))
                            {
                                //푸쉬성공
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                alert = new Alert(Localization.Resource.CoinTrading_Alert_30);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 손절 판매 설정 취소
        /// </summary>
        public async void CmdLossAutoOrderCancel()
        {
            try
            {
                alert = new Alert(Localization.Resource.CoinTrading_Alert_31, Alert.ButtonType.YesNo, 400);
                if (alert.ShowDialog() == true)
                {
                    IsBusy = true;
                    //Messenger.Default.Send(SellAutoOrder);
                    using (RequestTradingDelAutoSellModel req = new RequestTradingDelAutoSellModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;
                        req.udFlag = "L";

                        using (ResponseTradingDelAutoSellModel res = await WebApiLib.AsyncCall<ResponseTradingDelAutoSellModel, RequestTradingDelAutoSellModel>(req))
                        {
                            foreach (var item in LossOrderVM.list)
                            {
                                item.IsLossTradeEnabled = true;
                                item.conclusionSellCheck = true;

                                if (!item.OrderErrorRate.Equals(0))
                                {
                                    Messenger.Default.Send(item.Clone() as AutoOrderVM);
                                    item.Clear();
                                }
                            }

                            IsLossTradeStart = true;
                            IsLossTradeCancel = false;
                            IsBuySellTradeStart = true;
                            IsEnabledBuySellTab = true;
                            SearchAutoTradeList();
                            SearchAbleBuy();
                            SearchAbleSell();

                            using (RequestSendPushModel req2 = new RequestSendPushModel())
                            {
                                req2.userEmail = MainViewModel.LoginDataModel.userEmail;
                                req2.regIp = MainViewModel.LoginDataModel.regIp;
                                req2.contents = Localization.Resource.CoinTrading_Alert_47;
                                req2.pushType = StringEnum.GetStringValue(EnumLib.PushCode.AutoTrade);

                                using (ResponseSendPushModel res2 = await WebApiLib.AsyncCall<ResponseSendPushModel, RequestSendPushModel>(req2))
                                {
                                    //푸쉬성공
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                alert = new Alert(Localization.Resource.CoinTrading_Alert_32);
                alert.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        //public async void SearchAbleBuySell()
        //{
        //    try
        //    {
        //        using (RequestTradingAbleBuyModel req = new RequestTradingAbleBuyModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //            req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);

        //            using (ResponseTradingAbleBuyModel res = await WebApiLib.AsyncCall<ResponseTradingAbleBuyModel, RequestTradingAbleBuyModel>(req))
        //            {
        //                DispatcherService.BeginInvoke(() =>
        //                {
        //                    //지정가
        //                    BuyCommonOrder.OrderAbleCnt = res.data.price;
        //                });
        //            }
        //        }

        //        using (RequestTradingAbleSellModel req = new RequestTradingAbleSellModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //            req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);

        //            using (ResponseTradingAbleSellModel res = await WebApiLib.AsyncCall<ResponseTradingAbleSellModel, RequestTradingAbleSellModel>(req))
        //            {
        //                DispatcherService.BeginInvoke(() =>
        //                {
        //                    //시장가
        //                    SellCommonOrder.OrderAbleCnt = res.data.avaSellAmt;
        //                    //지정가
        //                    //SellMarketOrder.OrderAbleCnt = res.data.avaSellAmt;
        //                    //자동거래
        //                    //SellAutoOrder.OrderAbleCnt = res.data.avaSellAmt;
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}

        /// <summary>
        /// 구매가능수량 및 미체결 최저 판매가 조회
        /// </summary>
        public async void SearchAbleBuy(bool bCheck = true)
        {
            try
            {
                if (bCheck)
                {
                    if (PriceType.Equals(CommonLib.StandardCurcyNm))
                    {
                        using (RequestTradingAbleBuyModel req = new RequestTradingAbleBuyModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType);

                            using (ResponseTradingAbleBuyModel res = await WebApiLib.AsyncCall<ResponseTradingAbleBuyModel, RequestTradingAbleBuyModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    if (!BuyCommonOrder.MarketMode)
                                        BuyCommonOrder.OrderPrc = res.data.minPrc;
                                    BuyCommonOrder.OrderTempPrc = res.data.minPrc;
                                    BuyCommonOrder.OrderAbleCnt = decimal.Parse(CommonLib.GetFloatPoint(res.data.price.ToString(), CommonNum));
                                    BuyCommonOrder.OrderCnt = 0;
                                    SellCommonOrder.OrderMarketPrc = res.data.minPrc.ToString().Equals("0") ? 1 : res.data.minPrc;

                                    BuyCommonOrder.nowPriceVisible = Visibility.Collapsed;

                                    //SellMarketOrder.OrderPrc = res.data.minPrc.ToString().Equals("0") ? 1 : res.data.minPrc;
                                    //BuyMarketOrder.OrderAbleCnt = res.data.price;//res.data.price;                        

                                    //자동거래
                                    var item = MainViewModel.AutoOrderData.Where(s => s.RecCoinType == PriceCode).Where(s => s.OrderCoinType == KindOfCoin).ToList();
                                    if (item.Count <= 0)
                                    {
                                        BuyAutoOrder.OrderPrc = res.data.minPrc;
                                        LossAutoOrder.OrderPrc = res.data.minPrc;
                                    }
                                });
                            }
                        }
                    }
                    else
                    {
                        using (RequestTradingCoinToCoinSellModel req = new RequestTradingCoinToCoinSellModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);  //구매하려는 통화
                            req.rcvCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.RecCoinType); //결제하려는 통화
                            req.mkState = PriceType;

                            using (ResponseTradingCoinToCoinSellModel res = await WebApiLib.AsyncCall<ResponseTradingCoinToCoinSellModel, RequestTradingCoinToCoinSellModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    BuyCommonOrder.OrderPrc = res.data.maxPrc; //수령(판매)코인 시세
                                    BuyCommonOrder.OrderTempPrc = res.data.maxPrc;
                                    BuyCommonOrder.OrderAbleCnt = res.data.curcyAmt;

                                    SellCommonOrder.OrderMarketPrc = res.data.maxPrc.ToString().Equals("0") ? 1 : res.data.maxPrc;

                                    BuyCommonOrder.krwPrice = res.data.krwPrice;
                                    if (BuyCommonOrder.OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.ETH) || BuyCommonOrder.OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.BTC))
                                    {
                                        BuyCommonOrder.krwPriceStr = string.Format(Localization.Resource.CoinTrading_1_6, BuyCommonOrder.OrderCoinType.ToString()) + " :";
                                        BuyCommonOrder.nowPriceVisible = Visibility.Visible;
                                    }
                                    else
                                    {
                                        BuyCommonOrder.nowPriceVisible = Visibility.Collapsed;
                                    }

                                    var item = MainViewModel.AutoOrderData.Where(s => s.RecCoinType == PriceCode).Where(s => s.OrderCoinType == KindOfCoin).ToList();
                                    if (item.Count <= 0)
                                    {
                                        BuyAutoOrder.OrderPrc = res.data.maxPrc;
                                        LossAutoOrder.OrderPrc = res.data.maxPrc;
                                    }
                                });
                            }
                        }
                    }
                }
                else
                {
                    if (PriceType.Equals(CommonLib.StandardCurcyNm))
                    {
                        using (RequestTradingAbleBuyModel req = new RequestTradingAbleBuyModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderOriginalType);

                            using (ResponseTradingAbleBuyModel res = await WebApiLib.AsyncCall<ResponseTradingAbleBuyModel, RequestTradingAbleBuyModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    BuyCommonOrder.OrderTempPrc = res.data.minPrc;
                                    BuyCommonOrder.OrderAbleCnt = decimal.Parse(CommonLib.GetFloatPoint(res.data.price.ToString(), CommonNum));
                                    SellCommonOrder.OrderMarketPrc = res.data.minPrc.ToString().Equals("0") ? 1 : res.data.minPrc;

                                    BuyCommonOrder.nowPriceVisible = Visibility.Collapsed;
                                });
                            }
                        }
                    }
                    else
                    {
                        using (RequestTradingCoinToCoinSellModel req = new RequestTradingCoinToCoinSellModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(BuyCommonOrder.OrderCoinType);  //구매하려는 통화
                            req.rcvCurcyCd = StringEnum.GetStringValue(BuyCommonOrder.RecCoinType); //결제하려는 통화
                            req.mkState = PriceType;

                            using (ResponseTradingCoinToCoinSellModel res = await WebApiLib.AsyncCall<ResponseTradingCoinToCoinSellModel, RequestTradingCoinToCoinSellModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    BuyCommonOrder.OrderTempPrc = res.data.maxPrc;
                                    BuyCommonOrder.OrderAbleCnt = res.data.curcyAmt;

                                    SellCommonOrder.OrderMarketPrc = res.data.maxPrc.ToString().Equals("0") ? 1 : res.data.maxPrc;

                                    BuyCommonOrder.krwPrice = res.data.krwPrice;
                                    if (BuyCommonOrder.OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.ETH) || BuyCommonOrder.OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.BTC))
                                    {
                                        BuyCommonOrder.krwPriceStr = string.Format(Localization.Resource.CoinTrading_1_6, BuyCommonOrder.OrderCoinType.ToString()) + " :";
                                        BuyCommonOrder.nowPriceVisible = Visibility.Visible;
                                    }
                                    else
                                    {
                                        BuyCommonOrder.nowPriceVisible = Visibility.Collapsed;
                                    }
                                });
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

        /// <summary>
        /// 판매가능수량 및 미체결 최고 구매가 조회
        /// </summary>
        public async void SearchAbleSell(bool bCheck = true)
        {
            try
            {
                if (bCheck)
                {
                    if (PriceType.Equals(CommonLib.StandardCurcyNm))
                    {
                        using (RequestTradingAbleSellModel req = new RequestTradingAbleSellModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);

                            using (ResponseTradingAbleSellModel res = await WebApiLib.AsyncCall<ResponseTradingAbleSellModel, RequestTradingAbleSellModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {

                                    if (!SellCommonOrder.MarketMode)
                                        SellCommonOrder.OrderPrc = res.data.maxPrc;
                                    SellCommonOrder.OrderTempPrc = res.data.maxPrc;
                                    SellCommonOrder.OrderAbleCnt = res.data.avaSellAmt;
                                    BuyCommonOrder.OrderMarketPrc = res.data.maxPrc.ToString().Equals("0") ? 1 : res.data.maxPrc;
                                    //SellMarketOrder.OrderAbleCnt = res.data.avaSellAmt;

                                    SellCommonOrder.nowPriceVisible = Visibility.Collapsed;

                                    //자동거래
                                    var item = MainViewModel.AutoOrderData.Where(s => s.RecCoinType == PriceCode).Where(s => s.OrderCoinType == KindOfCoin).ToList();
                                    if (item.Count <= 0)
                                    {
                                        SellAutoOrder.OrderPrc = res.data.maxPrc;
                                    }
                                });
                            }
                        }
                    }
                    else
                    {
                        using (RequestTradingCoinToCoinSellModel req = new RequestTradingCoinToCoinSellModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.RecCoinType);  //구매하려는 통화
                            req.rcvCurcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderCoinType); //결제하려는 통화
                            req.mkState = PriceType;

                            using (ResponseTradingCoinToCoinSellModel res = await WebApiLib.AsyncCall<ResponseTradingCoinToCoinSellModel, RequestTradingCoinToCoinSellModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    SellCommonOrder.OrderPrc = res.data.minPrc; //수령(판매)코인 시세
                                    SellCommonOrder.OrderTempPrc = res.data.minPrc;
                                    SellCommonOrder.OrderAbleCnt = res.data.rcvCurcyAmt;

                                    BuyCommonOrder.OrderMarketPrc = res.data.minPrc.ToString().Equals("0") ? 1 : res.data.minPrc;

                                    SellCommonOrder.krwPrice = res.data.krwPrice;
                                    if (SellCommonOrder.RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.ETH) || SellCommonOrder.RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.BTC))
                                    {
                                        SellCommonOrder.krwPriceStr = string.Format(Localization.Resource.CoinTrading_1_6, SellCommonOrder.RecCoinType.ToString()) + " :";
                                        SellCommonOrder.nowPriceVisible = Visibility.Visible;
                                    }
                                    else
                                    {
                                        SellCommonOrder.nowPriceVisible = Visibility.Collapsed;
                                    }

                                    var item = MainViewModel.AutoOrderData.Where(s => s.RecCoinType == PriceCode).Where(s => s.OrderCoinType == KindOfCoin).ToList();
                                    if (item.Count <= 0)
                                    {
                                        SellAutoOrder.OrderPrc = res.data.minPrc;
                                    }
                                });
                            }
                        }
                    }
                }
                else
                {
                    if (PriceType.Equals(CommonLib.StandardCurcyNm))
                    {
                        using (RequestTradingAbleSellModel req = new RequestTradingAbleSellModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderOriginalType);

                            using (ResponseTradingAbleSellModel res = await WebApiLib.AsyncCall<ResponseTradingAbleSellModel, RequestTradingAbleSellModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    SellCommonOrder.OrderTempPrc = res.data.maxPrc;
                                    SellCommonOrder.OrderAbleCnt = res.data.avaSellAmt;
                                    BuyCommonOrder.OrderMarketPrc = res.data.maxPrc.ToString().Equals("0") ? 1 : res.data.maxPrc;

                                    SellCommonOrder.nowPriceVisible = Visibility.Collapsed;
                                });
                            }
                        }
                    }
                    else
                    {
                        using (RequestTradingCoinToCoinSellModel req = new RequestTradingCoinToCoinSellModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.curcyCd = StringEnum.GetStringValue(SellCommonOrder.RecCoinType);  //구매하려는 통화
                            req.rcvCurcyCd = StringEnum.GetStringValue(SellCommonOrder.OrderCoinType); //결제하려는 통화
                            req.mkState = PriceType;

                            using (ResponseTradingCoinToCoinSellModel res = await WebApiLib.AsyncCall<ResponseTradingCoinToCoinSellModel, RequestTradingCoinToCoinSellModel>(req))
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    SellCommonOrder.OrderTempPrc = res.data.minPrc;
                                    SellCommonOrder.OrderAbleCnt = res.data.rcvCurcyAmt;

                                    BuyCommonOrder.OrderMarketPrc = res.data.minPrc.ToString().Equals("0") ? 1 : res.data.minPrc;

                                    SellCommonOrder.krwPrice = res.data.krwPrice;
                                    if (SellCommonOrder.RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.ETH) || SellCommonOrder.RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.BTC))
                                    {
                                        SellCommonOrder.krwPriceStr = string.Format(Localization.Resource.CoinTrading_1_6, SellCommonOrder.RecCoinType.ToString()) + " :";
                                        SellCommonOrder.nowPriceVisible = Visibility.Visible;
                                    }
                                    else
                                    {
                                        SellCommonOrder.nowPriceVisible = Visibility.Collapsed;
                                    }
                                });
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

        bool TradeListCheck = true;
        /// <summary>
        /// 자동거래 설정 내역
        /// </summary>
        public async void SearchAutoTradeList()
        {
            try
            {
                if (TradeListCheck)
                {
                    TradeListCheck = false;

                    AutoTradeHistory = new ObservableCollection<ResponseTradingGetAutoBuyListModel>();

                    using (RequestTradingGetAutoBuyModel req = new RequestTradingGetAutoBuyModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;
                        req.udFlag = "B";

                        using (ResponseTradingGetAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingGetAutoBuyModel, RequestTradingGetAutoBuyModel>(req))
                        {
                            DispatcherService.BeginInvoke(() =>
                            {
                                foreach (var item in res.data.list)
                                {
                                    item.trade = "B";
                                    item.prcPer = int.Parse("-" + (item.prcPer - 1).ToString());
                                    AutoTradeHistory.Add(item);
                                }
                            });
                        }
                    }

                    using (RequestTradingGetAutoBuyModel req = new RequestTradingGetAutoBuyModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;
                        req.udFlag = "S";

                        using (ResponseTradingGetAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingGetAutoBuyModel, RequestTradingGetAutoBuyModel>(req))
                        {
                            DispatcherService.BeginInvoke(() =>
                            {
                                foreach (var item in res.data.list)
                                {
                                    item.trade = "S";
                                    item.prcPer = item.prcPer - 1;
                                    AutoTradeHistory.Add(item);
                                }
                            });
                        }
                    }

                    using (RequestTradingGetAutoSellModel req = new RequestTradingGetAutoSellModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;
                        req.udFlag = "L";

                        using (ResponseTradingGetAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingGetAutoBuyModel, RequestTradingGetAutoSellModel>(req))
                        {
                            DispatcherService.BeginInvoke(() =>
                            {
                                foreach (var item in res.data.list)
                                {
                                    item.trade = "L";
                                    item.prcPer = item.prcPer - 1;
                                    AutoTradeHistory.Add(item);
                                }
                            });
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
                TradeListCheck = true;
            }
        }

        public void CallNonCoinList(string Message)
        {
            try
            {
                if (IsLoad)
                {
                    if (Message.Equals("NonCoinListRefresh"))
                    {
                        Task.Run(() =>
                        {
                            SearchNotCon();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        bool bNotCon = true;
        /// <summary>
        /// 미체결 주문 조회
        /// </summary>dl
        public async void SearchNotCon()
        {
            try
            {
                if (CucyCoin == null)
                {
                    return;
                }

                if (bNotCon)
                {
                    bNotCon = false;

                    using (RequestTradingNotConModel req = new RequestTradingNotConModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.curcyCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;
                        req.listSize = nonConListCnt;

                        using (ResponseTradingNotConModel res = await WebApiLib.AsyncCall<ResponseTradingNotConModel, RequestTradingNotConModel>(req))
                        {
                            DispatcherService.BeginInvoke(() =>
                            {
                                foreach (ResponseTradingNotConListModel item in res.data.list)
                                {
                                    if (PriceType.Equals(CommonLib.StandardCurcyNm))
                                    {
                                        ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == StringEnum.GetStringValue(KindOfCoin)).FirstOrDefault();                                       
                                        item.floatCash = "n" + MainViewModel.CoinData.CashDecimal;
                                        item.floatCoin = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                                    }
                                    else
                                    {
                                        item.floatCash = "n8";
                                        item.floatCoin = "n8";
                                    }

                                    item.orderAmt = decimal.Parse(item.orderAmt).ToString("###,###,###,###,##0.########") + " " + CucyCoin.CoinName;
                                    item.tradeAmt = decimal.Parse(item.tradeAmt).ToString("###,###,###,###,##0.########") + " " + CucyCoin.CoinName;
                                    //item.avrTradePrc = decimal.Parse(item.avrTradePrc).ToString(CommonFloat) + " " + ViewPriceType;
                                    //item.orderPrc = decimal.Parse(item.orderPrc).ToString(CommonFloat) + " " + ViewPriceType;
                                }

                                NotConList = res.data.list;
                            });
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
                bNotCon = true;
            }
        }
        public void CmdMoreNonCoinList()
        {
            try
            {
                if (nonConListCntEnable)
                {
                    nonConListCntEnable = false;
                    nonConListCnt += 10;
                    SearchNotCon();
                    RepeatTimer.Start();
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
                nonConListCntEnable = true;
                RepeatTimer.Stop();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                nonConListCntEnable = true;
            }
        }

        public void CallTradeHistory(string Message)
        {
            try
            {
                if (IsLoad)
                {
                    if (Message.Equals("TradeHistoryRefresh"))
                    {
                        Task.Run(() =>
                        {
                            SearchTradeHistory();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        bool bTradeHistory = true;
        /// <summary>
        /// 최근 거래내역
        /// </summary>
        public async void SearchTradeHistory()
        {
            try
            {
                if (bTradeHistory)
                {
                    bTradeHistory = false;

                    using (RequestTradingTradeHistoryModel req = new RequestTradingTradeHistoryModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.curcyCd = StringEnum.GetStringValue(KindOfCoin);
                        req.mkState = PriceType;
                        req.listSize = 10;

                        using (ResponseTradingTradeHistoryModel res = await WebApiLib.AsyncCall<ResponseTradingTradeHistoryModel, RequestTradingTradeHistoryModel>(req))
                        {
                            DispatcherService.BeginInvoke(() =>
                            {
                                foreach (ResponseTradingTradeHistoryListModel item in res.data.list)
                                {
                                    if (PriceType.Equals(CommonLib.StandardCurcyNm))
                                    {
                                        ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == StringEnum.GetStringValue(KindOfCoin)).FirstOrDefault();
                                        item.floatCash = "n" + MainViewModel.CoinData.CashDecimal;
                                        item.floatCoin = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                                    }
                                    else
                                    {
                                        item.floatCash = "n8";
                                        item.floatCoin = "n8";
                                    }

                                    if (item.orderCd.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Buy)))
                                    {
                                        item.fee = decimal.Parse(item.fee).ToString("###,###,###,###,##0.########") + " " + CucyCoin.CoinName;
                                    }
                                    else if (item.orderCd.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Sell)))
                                    {
                                        if(PriceType.Equals(CommonLib.StandardCurcyNm))
                                        {
                                            item.fee = decimal.Parse(item.fee).ToString(CommonFloat) + " " + ViewPriceType;
                                        }
                                        else
                                        {
                                            item.fee = decimal.Parse(item.fee).ToString("###,###,###,###,##0.########") + " " + ViewPriceType;
                                        }                                        
                                    }

                                    item.conAmt = decimal.Parse(item.conAmt).ToString("###,###,###,###,##0.########") + " " + CucyCoin.CoinName;
                                }

                                TradeHistory = res.data.list;
                            });
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
                bTradeHistory = true;
            }
        }

        /// <summary>
        /// 미체결 주문 취소
        /// </summary>
        /// <param name="selectItem"></param>
        public async void CmdOrderCancel(ResponseTradingNotConListModel selectItem)
        {
            try
            {
                alert = new Alert(Localization.Resource.CoinTrading_Alert_6, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    //미체결 주문 취소 구현
                    using (RequestTradingTradeCancelModel req = new RequestTradingTradeCancelModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                        req.orderNo = selectItem.orderNo; //주문번호
                        req.mkState = PriceType;
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseTradingTradeCancelModel res = await WebApiLib.AsyncCall<ResponseTradingTradeCancelModel, RequestTradingTradeCancelModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_7);
                                alert.ShowDialog();
                                SearchNotCon();
                                Messenger.Default.Send("AssetsRefresh");
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

        public void CmdPopupChart()
        {
            try
            {
                if (MainViewModel.ecd == null || MainViewModel.ecd.IsLoaded == false)
                {
                    MainViewModel.ecd = new Views.Dashboard.ExchangeChartDashboard(CucyCoin.CoinCode, PriceType, PriceCode2);
                    //ecd.WindowStyle = WindowStyle.ToolWindow;
                    MainViewModel.ecd.WindowStyle = WindowStyle.SingleBorderWindow;
                    MainViewModel.ecd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    MainViewModel.ecd.Width = 1400;
                    MainViewModel.ecd.Height = 800;
                    MainViewModel.ecd.Title = Localization.Resource.ExchangeChartDashboard_1 + "(" + (PriceType.Equals(CommonLib.StandardCurcyNm) ? CommonLib.StandardCurcyNm : PriceType) + " Market)";
                    MainViewModel.ecd.Show();
                }
                else
                {
                    MainViewModel.ecd.Show();
                    MainViewModel.ecd.Activate();
                    if (MainViewModel.ecd.WindowState == WindowState.Minimized)
                        MainViewModel.ecd.WindowState = WindowState.Normal;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OrderInit(string KindOfBuySell)
        {
            try
            {
                if (KindOfBuySell.Equals("CommonBuy"))
                {
                    BuyCommonOrder.OrderPrc = 0;
                    BuyCommonOrder.OrderCnt = 0;
                    BuyCommonOrder.OrderPreCnt = 0;
                }
                else if (KindOfBuySell.Equals("CommonSell"))
                {
                    SellCommonOrder.OrderPrc = 0;
                    SellCommonOrder.OrderCnt = 0;
                    SellCommonOrder.OrderPreCnt = 0;
                }
                //if (KindOfBuySell.Equals("MarketBuy"))
                //{
                //    BuyMarketOrder.OrderCnt = 0;
                //    BuyMarketOrder.OrderPreCnt = 0;
                //}
                //else if (KindOfBuySell.Equals("MarketSell"))
                //{
                //    SellMarketOrder.OrderCnt = 0;
                //    SellMarketOrder.OrderPreCnt = 0;
                //}
                if (KindOfBuySell.Equals("AutoBuySell"))
                {
                    BuyAutoOrder.OrderPrc = 0;
                    BuyAutoOrder.SelectedErrorRate = BuyAutoOrder.ErrorType[0];
                    BuyAutoOrder.OrderCnt = 0;
                    BuyAutoOrder2.OrderPrc = 0;
                    BuyAutoOrder2.SelectedErrorRate = BuyAutoOrder2.ErrorType[0];
                    BuyAutoOrder2.OrderCnt = 0;
                    BuyAutoOrder3.OrderPrc = 0;
                    BuyAutoOrder3.SelectedErrorRate = BuyAutoOrder3.ErrorType[0];
                    BuyAutoOrder3.OrderCnt = 0;
                    SellAutoOrder.OrderPrc = 0;
                    SellAutoOrder.SelectedErrorRate = SellAutoOrder.ErrorType[0];
                    SellAutoOrder.OrderCnt = 0;
                    SellAutoOrder2.OrderPrc = 0;
                    SellAutoOrder2.SelectedErrorRate = SellAutoOrder2.ErrorType[0];
                    SellAutoOrder2.OrderCnt = 0;
                    SellAutoOrder3.OrderPrc = 0;
                    SellAutoOrder3.SelectedErrorRate = SellAutoOrder3.ErrorType[0];
                    SellAutoOrder3.OrderCnt = 0;
                }
                else if (KindOfBuySell.Equals("AutoLoss"))
                {
                    LossAutoOrder.OrderPrc = 0;
                    LossAutoOrder.SelectedErrorRate = LossAutoOrder.ErrorType[0];
                    LossAutoOrder.OrderCnt = 0;
                    LossAutoOrder2.OrderPrc = 0;
                    LossAutoOrder2.SelectedErrorRate = LossAutoOrder2.ErrorType[0];
                    LossAutoOrder2.OrderCnt = 0;
                    LossAutoOrder3.OrderPrc = 0;
                    LossAutoOrder3.SelectedErrorRate = LossAutoOrder3.ErrorType[0];
                    LossAutoOrder3.OrderCnt = 0;
                }
                SearchAbleBuy();
                SearchAbleSell();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdChangePrice(AutoOrderVM AutoOrder)
        {
            if (AutoOrder == null) return;
            if (AutoOrder.OrderPrc == null || AutoOrder.OrderPrc == 0)
            {
                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                return;
            }

            AutoOrder.LostFocusPrc();
            if (AutoOrder.SelectedErrorRate.value == -1) return;

            AutoOrder.OrderErrorRate = AutoOrder.ErrorType.IndexOf(AutoOrder.SelectedErrorRate);
            if (AutoOrder.OrderType.Equals("B"))
            {
                AutoOrder.OrderErrorLowPrc = AutoOrder.LostFocusPrc(AutoOrder.OrderPrc - (AutoOrder.OrderPrc * AutoOrder.SelectedErrorRate.value) - CommonAutoPrice);
                AutoOrder.OrderErrorHighPrc = AutoOrder.OrderPrc;
            }
            else
            {
                AutoOrder.OrderErrorLowPrc = AutoOrder.OrderPrc;
                AutoOrder.OrderErrorHighPrc = AutoOrder.LostFocusPrc(AutoOrder.OrderPrc + (AutoOrder.OrderPrc * AutoOrder.SelectedErrorRate.value) + CommonAutoPrice);
            }

            CmdChangeErrorRate(AutoOrder);
        }

        public void CmdChangeErrorRate(AutoOrderVM AutoOrder)
        {
            try
            {
                if (MainViewModel.AutoOrderData.Where(s => s.RecCoinType == PriceCode).Where(s => s.OrderCoinType == KindOfCoin).ToList<AutoOrderVM>().Count() > 0) return;

                if (AutoOrder == null) return;
                if (AutoOrder.OrderPrc == null || AutoOrder.OrderPrc == 0)
                {
                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                    return;
                }
                if (AutoOrder.SelectedErrorRate.value == -1) return;

                //구매
                if (AutoOrder.OrderType.Equals("B"))
                {
                    if (BuyAutoOrder.OrderPrc == null || BuyAutoOrder.OrderPrc == 0) return;

                    //1순위
                    if (AutoOrder.Rank.Equals(1))
                    {
                        decimal NowMarketPrice = 0;

                        if (BuyOrdelData != null)
                        {
                            if (BuyOrdelData.Count > 0)
                            {
                                if (!BuyOrdelData[0].buyTranPrc.ToString().Equals("") && !BuyOrdelData[0].buyTranPrc.ToString().Equals("0"))
                                {
                                    NowMarketPrice = decimal.Parse(BuyOrdelData[0].buyTranPrc);
                                }
                            }
                        }

                        if (NowMarketPrice.Equals(0))
                        {
                            //alert = new Alert("구매 가능한 시세가 없습니다.");
                            //alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (AutoOrder.OrderPrc > NowMarketPrice)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_36, 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                        }

                        if (BuyAutoOrder2.OrderPrc != null && BuyAutoOrder2.OrderPrc != 0)
                        {
                            if (AutoOrder.OrderPrc <= BuyAutoOrder2.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 2), 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                BuyAutoOrder2.SelectedErrorRate = BuyAutoOrder2.ErrorType[0];
                                return;
                            }
                            else
                            {
                                BuyAutoOrder2.OrderPrc = AutoOrder.OrderErrorLowPrc;// - CommonAutoPrice;
                            }
                        }
                        else
                        {
                            BuyAutoOrder2.OrderPrc = AutoOrder.OrderErrorLowPrc;// - CommonAutoPrice;
                        }

                        if (BuyAutoOrder3.OrderPrc != null && BuyAutoOrder3.OrderPrc != 0)
                        {
                            if (AutoOrder.OrderPrc <= BuyAutoOrder3.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 3), 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                BuyAutoOrder2.SelectedErrorRate = BuyAutoOrder2.ErrorType[0];
                                BuyAutoOrder3.SelectedErrorRate = BuyAutoOrder3.ErrorType[0];
                                return;
                            }
                        }
                    }
                    //2순위
                    else if (AutoOrder.Rank.Equals(2))
                    {
                        if (BuyAutoOrder.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 1), 280);
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (BuyAutoOrder.OrderPrc <= AutoOrder.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_39, 1), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else
                            {
                                if ((BuyAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < BuyAutoOrder.OrderErrorHighPrc)
                                  || (BuyAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < BuyAutoOrder.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 1), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (BuyAutoOrder.OrderErrorHighPrc < AutoOrder.OrderErrorHighPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_42, 1), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                            }

                            BuyAutoOrder3.OrderPrc = AutoOrder.OrderErrorLowPrc;// - CommonAutoPrice;
                        }
                    }
                    //3순위
                    else if (AutoOrder.Rank.Equals(3))
                    {
                        if (BuyAutoOrder.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 1));
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else if (BuyAutoOrder2.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 2));
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (AutoOrder.OrderPrc >= BuyAutoOrder.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_39, 1), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else if (AutoOrder.OrderPrc >= BuyAutoOrder2.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_39, 2), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else
                            {

                                if ((BuyAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < BuyAutoOrder.OrderErrorHighPrc)
                                  || (BuyAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < BuyAutoOrder.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 1), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if ((BuyAutoOrder2.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < BuyAutoOrder2.OrderErrorHighPrc)
                                  || (BuyAutoOrder2.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < BuyAutoOrder2.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 2), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (BuyAutoOrder.OrderErrorHighPrc < AutoOrder.OrderErrorHighPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_42, 1), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (BuyAutoOrder2.OrderErrorHighPrc < AutoOrder.OrderErrorHighPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_42, 2), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                            }
                        }
                    }
                }
                //판매
                else if (AutoOrder.OrderType.Equals("S"))
                {
                    if (SellAutoOrder.OrderPrc == null || SellAutoOrder.OrderPrc == 0) return;

                    if (AutoOrder.Rank.Equals(1))
                    {
                        decimal NowMarketPrice = 0;

                        if (SellOrdelData != null)
                        {
                            if (SellOrdelData.Count > 0)
                            {
                                int SellIndex = SellOrdelData.Count - 1;
                                if (!SellOrdelData[SellIndex].sellTranPrc.ToString().Equals("") && !SellOrdelData[SellIndex].sellTranPrc.ToString().Equals("0"))
                                {
                                    NowMarketPrice = decimal.Parse(SellOrdelData[SellIndex].sellTranPrc);
                                }
                            }
                        }

                        if (NowMarketPrice.Equals(0))
                        {
                            //alert = new Alert("판매 가능한 시세가 없습니다.");
                            //alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (AutoOrder.OrderPrc < NowMarketPrice)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_37, 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                        }


                        if (SellAutoOrder2.OrderPrc == null || SellAutoOrder2.OrderPrc == 0)
                        {
                            if (AutoOrder.OrderPrc <= SellAutoOrder2.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 2), 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                SellAutoOrder2.SelectedErrorRate = SellAutoOrder2.ErrorType[0];
                                return;
                            }
                            else
                            {
                                SellAutoOrder2.OrderPrc = AutoOrder.OrderErrorHighPrc;// + CommonAutoPrice;
                            }
                        }
                        else
                        {
                            SellAutoOrder2.OrderPrc = AutoOrder.OrderErrorHighPrc;// + CommonAutoPrice;
                        }

                        if (SellAutoOrder3.OrderPrc == null || SellAutoOrder3.OrderPrc == 0)
                        {
                            if (AutoOrder.OrderPrc <= SellAutoOrder3.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 3), 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                SellAutoOrder2.SelectedErrorRate = SellAutoOrder2.ErrorType[0];
                                SellAutoOrder3.SelectedErrorRate = SellAutoOrder3.ErrorType[0];
                                return;
                            }
                        }
                    }
                    //2순위
                    else if (AutoOrder.Rank.Equals(2))
                    {
                        if (SellAutoOrder.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 1), 280);
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (AutoOrder.OrderPrc <= SellAutoOrder.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 1), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else
                            {
                                if ((SellAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < SellAutoOrder.OrderErrorHighPrc)
                                  || (SellAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < SellAutoOrder.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 1), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (SellAutoOrder.OrderErrorLowPrc > AutoOrder.OrderErrorLowPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_43, 1), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                            }

                            SellAutoOrder3.OrderPrc = AutoOrder.OrderErrorHighPrc;// + CommonAutoPrice;
                        }
                    }
                    //3순위
                    else if (AutoOrder.Rank.Equals(3))
                    {
                        if (SellAutoOrder.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 1));
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else if (SellAutoOrder2.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 2));
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (AutoOrder.OrderPrc <= SellAutoOrder.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 1), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else if (AutoOrder.OrderPrc <= SellAutoOrder2.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 2), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else
                            {
                                if ((SellAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < SellAutoOrder.OrderErrorHighPrc)
                                 || (SellAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < SellAutoOrder.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 1), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if ((SellAutoOrder2.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < SellAutoOrder2.OrderErrorHighPrc)
                                  || (SellAutoOrder2.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < SellAutoOrder2.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 2), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (SellAutoOrder.OrderErrorLowPrc > AutoOrder.OrderErrorLowPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_43, 1), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (SellAutoOrder2.OrderErrorLowPrc > AutoOrder.OrderErrorLowPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_43, 2), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                            }
                        }
                    }
                }
                else if (AutoOrder.OrderType.Equals("L"))
                {
                    if (LossAutoOrder.OrderPrc == null || LossAutoOrder.OrderPrc == 0) return;

                    //1순위
                    if (AutoOrder.Rank.Equals(1))
                    {
                        decimal NowMarketPrice = 0;

                        if (BuyOrdelData != null)
                        {
                            if (BuyOrdelData.Count > 0)
                            {
                                if (!BuyOrdelData[0].buyTranPrc.ToString().Equals("") && !BuyOrdelData[0].buyTranPrc.ToString().Equals("0"))
                                {
                                    NowMarketPrice = decimal.Parse(BuyOrdelData[0].buyTranPrc);
                                }
                            }
                        }

                        if (NowMarketPrice.Equals(0))
                        {
                            //alert = new Alert("구매 가능한 시세가 없습니다.");
                            //alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (AutoOrder.OrderPrc > NowMarketPrice)
                            {
                                alert = new Alert(Localization.Resource.CoinTrading_Alert_36, 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                        }

                        if (LossAutoOrder2.OrderPrc != null && LossAutoOrder2.OrderPrc != 0)
                        {
                            if (AutoOrder.OrderPrc <= LossAutoOrder2.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 2), 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                LossAutoOrder2.SelectedErrorRate = LossAutoOrder2.ErrorType[0];
                                return;
                            }
                            else
                            {
                                LossAutoOrder2.OrderPrc = AutoOrder.OrderErrorLowPrc;// - CommonAutoPrice;
                            }
                        }
                        else
                        {
                            LossAutoOrder2.OrderPrc = AutoOrder.OrderErrorLowPrc;// - CommonAutoPrice;
                        }

                        if (LossAutoOrder3.OrderPrc != null && LossAutoOrder3.OrderPrc != 0)
                        {
                            if (AutoOrder.OrderPrc <= LossAutoOrder3.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_38, 3), 350);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                LossAutoOrder2.SelectedErrorRate = LossAutoOrder2.ErrorType[0];
                                LossAutoOrder3.SelectedErrorRate = LossAutoOrder3.ErrorType[0];
                                return;
                            }
                        }
                    }
                    //2순위
                    else if (AutoOrder.Rank.Equals(2))
                    {
                        if (LossAutoOrder.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 1), 280);
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (LossAutoOrder.OrderPrc <= AutoOrder.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_39, 1), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else
                            {
                                if ((LossAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < LossAutoOrder.OrderErrorHighPrc)
                                  || (LossAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < LossAutoOrder.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 1), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (LossAutoOrder.OrderErrorHighPrc < AutoOrder.OrderErrorHighPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_42, 1), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                            }

                            LossAutoOrder3.OrderPrc = AutoOrder.OrderErrorLowPrc;// - CommonAutoPrice;
                        }
                    }
                    //3순위
                    else if (AutoOrder.Rank.Equals(3))
                    {
                        if (LossAutoOrder.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 1));
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else if (LossAutoOrder2.SelectedErrorRate.value == -1)
                        {
                            alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_40, 2));
                            alert.ShowDialog();
                            AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                            return;
                        }
                        else
                        {
                            if (AutoOrder.OrderPrc >= LossAutoOrder.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_39, 1), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else if (AutoOrder.OrderPrc >= LossAutoOrder2.OrderPrc)
                            {
                                alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_39, 2), 280);
                                alert.ShowDialog();
                                AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                return;
                            }
                            else
                            {

                                if ((LossAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < LossAutoOrder.OrderErrorHighPrc)
                                  || (LossAutoOrder.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < LossAutoOrder.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 1), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if ((LossAutoOrder2.OrderErrorLowPrc < AutoOrder.OrderErrorLowPrc) && (AutoOrder.OrderErrorLowPrc < LossAutoOrder2.OrderErrorHighPrc)
                                  || (LossAutoOrder2.OrderErrorLowPrc < AutoOrder.OrderErrorHighPrc) && (AutoOrder.OrderErrorHighPrc < LossAutoOrder2.OrderErrorHighPrc))
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_41, 2), 350);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (LossAutoOrder.OrderErrorHighPrc < AutoOrder.OrderErrorHighPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_42, 1), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
                                }
                                else if (LossAutoOrder2.OrderErrorHighPrc < AutoOrder.OrderErrorHighPrc)
                                {
                                    alert = new Alert(string.Format(Localization.Resource.CoinTrading_Alert_42, 2), 330);
                                    alert.ShowDialog();
                                    AutoOrder.SelectedErrorRate = AutoOrder.ErrorType[0];
                                    return;
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
        }

        public void CmdAutoTradeTabChagned(TabControlSelectionChangedEventArgs e)
        {
            try
            {
                if (e.NewSelectedIndex.Equals(0))
                {
                    if (!IsEnabledBuySellTab)
                    {
                        alert = new Alert(Localization.Resource.CoinTrading_Alert_23, 400);
                        alert.ShowDialog();
                        return;
                    }
                }

                if (e.NewSelectedIndex.Equals(1))
                {
                    if (!IsEnabledLossTab)
                    {
                        alert = new Alert(Localization.Resource.CoinTrading_Alert_24, 400);
                        alert.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdDayPrcPopup()
        {
            try
            {
                CoinDayPirceTrading pop = new CoinDayPirceTrading();
                CoinDayPirceTradingViewModel.sPriceType = PriceType;
                CoinDayPirceTradingViewModel.sCoinCode = StringEnum.GetStringValue(KindOfCoin);
                pop.WindowStyle = WindowStyle.None;
                pop.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pop.Width = 900;
                pop.Height = 500;
                pop.Show();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
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

        public void CallAutoSetting(string Message)
        {
            try
            {
                if (IsLoad)
                {
                    if (Message.Equals("AutoSettingRefresh"))
                    {
                        AutoSetting(false);
                    }
                }

                if (Message.Equals("AutoTradeState"))
                {
                    autoCheck = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        bool bAutoSetting = true;
        public void AutoSetting(bool reset = true)
        {
            try
            {
                DispatcherService.BeginInvoke(() =>
                {
                    if (bAutoSetting)
                    {
                        bAutoSetting = false;

                        bool BuySellCheck = false;
                        bool LossCheck = false;

                        if (MainViewModel.AutoOrderData.Where(s => s.RecCoinType == PriceCode).Where(s => s.OrderCoinType == KindOfCoin).ToList().Count() > 0)
                        {
                            if (!reset)
                            {
                                foreach (AutoOrderVM BuySellOrderItem in BuySellOrderVM.list)
                                {
                                    BuySellOrderItem.Reset(KindOfCoin, PriceCode);
                                }

                                foreach (AutoOrderVM LossOrderItem in LossOrderVM.list)
                                {
                                    LossOrderItem.Reset(KindOfCoin, PriceCode);
                                }
                            }

                            List<AutoOrderVM> item = MainViewModel.AutoOrderData.Where(s => s.RecCoinType == PriceCode).Where(s => s.OrderCoinType == KindOfCoin).ToList();
                            foreach (AutoOrderVM v in item)
                            {
                                //자동거래
                                AutoOrderVM tempAutoOrder = BuySellOrderVM.list.Where(w => w.OrderType == v.OrderType && w.Rank == v.Rank).FirstOrDefault();
                                //손절
                                AutoOrderVM tempLossOrder = LossOrderVM.list.Where(w => w.OrderType == v.OrderType && w.Rank == v.Rank).FirstOrDefault();

                                if (tempAutoOrder != null)
                                {
                                    tempAutoOrder.Set(v.OrderPrc, v.OrderErrorRate, v.OrderErrorLowPrc, v.OrderErrorHighPrc, v.OrderCnt);
                                    BuySellCheck = true;
                                }

                                if (tempLossOrder != null)
                                {
                                    tempLossOrder.Set(v.OrderPrc, v.OrderErrorRate, v.OrderErrorLowPrc, v.OrderErrorHighPrc, v.OrderCnt);
                                    LossCheck = true;
                                }
                            }

                            if (BuySellCheck)
                            {
                                BuyAutoOrder.IsBuyTradeEnabled = false;
                                BuyAutoOrder2.IsBuyTradeEnabled = false;
                                BuyAutoOrder3.IsBuyTradeEnabled = false;
                                SellAutoOrder.IsSellTradeEnabled = false;
                                SellAutoOrder2.IsSellTradeEnabled = false;
                                SellAutoOrder3.IsSellTradeEnabled = false;
                                IsBuySellTradeStart = false;
                                IsBuySellTradeCancel = true;
                                IsLossTradeStart = false;
                                IsEnabledBuySellTab = true;
                                IsEnabledLossTab = false;
                            }

                            if (LossCheck)
                            {
                                LossAutoOrder.IsLossTradeEnabled = false;
                                LossAutoOrder2.IsLossTradeEnabled = false;
                                LossAutoOrder3.IsLossTradeEnabled = false;
                                IsLossTradeStart = false;
                                IsLossTradeCancel = true;
                                IsBuySellTradeStart = false;
                                IsEnabledBuySellTab = false;
                                IsEnabledLossTab = true;
                            }
                        }
                        else
                        {
                            BuyAutoOrder.IsBuyTradeEnabled = true;
                            BuyAutoOrder2.IsBuyTradeEnabled = true;
                            BuyAutoOrder3.IsBuyTradeEnabled = true;
                            SellAutoOrder.IsSellTradeEnabled = true;
                            SellAutoOrder2.IsSellTradeEnabled = true;
                            SellAutoOrder3.IsSellTradeEnabled = true;
                            IsBuySellTradeStart = true;
                            IsBuySellTradeCancel = false;
                            IsLossTradeStart = true;
                            IsEnabledLossTab = true;

                            LossAutoOrder.IsLossTradeEnabled = true;
                            LossAutoOrder2.IsLossTradeEnabled = true;
                            LossAutoOrder3.IsLossTradeEnabled = true;
                            IsLossTradeStart = true;
                            IsLossTradeCancel = false;
                            IsBuySellTradeStart = true;
                            IsEnabledBuySellTab = true;
                        }
                        SearchAutoTradeList();
                    }
                });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                bAutoSetting = true;
            }
        }

        public async void GetAutoTradeStatus()
        {
            try
            {
                using (RequestAutoTradeContentModel req = new RequestAutoTradeContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseAutoTradeContentModel res = await WebApiLib.AsyncCall<ResponseAutoTradeContentModel, RequestAutoTradeContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.cnKndCd == null)
                            {
                                autoCheck = false;
                            }
                            else
                            {
                                autoCheck = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                autoCheck = false;
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetBidding()
        {
            try
            {
                //호가단위 데이터 호출
                using (RequestGetAskingModel req = new RequestGetAskingModel())
                {
                    req.mkState = StringEnum.GetStringValue(PriceCode);

                    using (ResponseGetAskingModel res = await WebApiLib.AsyncCall<ResponseGetAskingModel, RequestGetAskingModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            AskingData = res.data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                autoCheck = false;
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_reset = sLanguage + "btn_reset.png";
            btn_reset_on = sLanguage + "btn_reset_on.png";
            btn_color_purchase = sLanguage + "btn_color_purchase.png";
            btn_color_purchase_on = sLanguage + "btn_color_purchase_on.png";
            btn_color_sell = sLanguage + "btn_color_sell.png";
            btn_color_sell_on = sLanguage + "btn_color_sell_on.png";
            btn_auto_exchange_set_small = sLanguage + "btn_auto_exchange_set_small.png";
            btn_auto_exchange_set_small_on = sLanguage + "btn_auto_exchange_set_small_on.png";
            btn_auto_reset_small = sLanguage + "btn_auto_reset_small.png";
            btn_auto_reset_small_on = sLanguage + "btn_auto_reset_small_on.png";
            btn_auto_exchange_cancel_small = sLanguage + "btn_auto_exchange_cancel_small.png";
            btn_auto_exchange_cancel_small_on = sLanguage + "btn_auto_exchange_cancel_small_on.png";
            btn_damage_sell_set_small = sLanguage + "btn_damage_sell_set_small.png";
            btn_damage_sell_set_small_on = sLanguage + "btn_damage_sell_set_small_on.png";
            btn_damage_sell_cancel_small = sLanguage + "btn_damage_sell_cancel_small.png";
            btn_damage_sell_cancel_small_on = sLanguage + "btn_damage_sell_cancel_small_on.png";
            btn_daily_market = sLanguage + "btn_daily_market.png";
            btn_daily_market_on = sLanguage + "btn_daily_market_on.png";
            btn_more = sLanguage + "btn_more.png";
            btn_more_on = sLanguage + "btn_more_on.png";
        }
    }

    public class CoinType
    {
        public virtual string name { get; set; }
        public EnumLib.ExchangeCurrencyCode value { get; set; }
    }

    public class ErrorRate
    {
        public virtual string name { get; set; }
        public decimal value { get; set; }
    }

    /// <summary>
    /// 구매/판매 공통
    /// </summary>
    public class CommonOrderVM
    {
        /// <summary>
        /// 구매/판매 구분자(구매:"B" ,판매:"S")
        /// </summary>
        private string OrderType;
        /// <summary>
        /// 결제코인 구분자("BTC","ETH","BCH","XRP",CommonLib.StandardCurcy)
        /// </summary>
        public virtual EnumLib.ExchangeCurrencyCode OrderCoinType { get; set; }
        /// <summary>
        /// 기존 결제코인 코드
        /// </summary>
        public virtual EnumLib.ExchangeCurrencyCode OrderOriginalType { get; set; }
        /// <summary>
        /// 수령코인 구분자("BTC","ETH","BCH","XRP",CommonLib.StandardCurcy)
        /// </summary>
        public virtual EnumLib.ExchangeCurrencyCode RecCoinType { get; set; }

        /// <summary>
        /// 판매코인 종류
        /// </summary>
        public virtual List<CoinType> BuyCoinType { get; set; }
        /// <summary>
        /// 수령코인 종류
        /// </summary>
        public virtual List<CoinType> SellCoinType { get; set; }
        /// <summary>
        /// 판매코인 종류 선택 정보
        /// </summary>
        //public virtual CoinType SelectedBuyCoinType { get; set; }
        /// <summary>
        /// 수령코인 종류 선택 정보
        /// </summary>
        //public virtual CoinType SelectedSellCoinType { get; set; }

        /// <summary>
        /// 결제코인 시세
        /// </summary>
        //public virtual decimal? PayCoinPrc { get; set; }
        public virtual decimal? PayCoinCurr { get; set; }
        /// <summary>
        /// 수령코인 시세
        /// </summary>
        //public virtual decimal? RecCoinPrc { get; set; }
        public virtual decimal? RecCoinCurr { get; set; }

        /// <summary>
        /// 주문가격
        /// </summary>
        public virtual decimal? OrderPrc { get; set; }
        /// <summary>
        /// 주문 시장가 가격
        /// </summary>
        public virtual decimal? OrderMarketPrc { get; set; }
        /// <summary>
        /// 주문가격 임시저장
        /// </summary>
        public virtual decimal? OrderTempPrc { get; set; }
        /// <summary>
        /// 주문수량
        /// </summary>
        public virtual decimal? OrderCnt { get; set; }
        /// <summary>
        /// 주문가능량
        /// </summary>
        public virtual decimal? OrderAbleCnt { get; set; }
        /// <summary>
        /// (구매/판매)금액
        /// </summary>
        public virtual decimal? OrderTotPrc { get; set; }
        /// <summary>
        /// 예상수량
        /// </summary>
        public virtual decimal? OrderPreCnt { get; set; }
        /// <summary>
        /// 시장가거래 체크(T : 시장가 F: 지정가 D: 지정가)
        /// </summary>
        public virtual bool MarketMode { get; set; } = false;
        public virtual bool MarketModeEnabled { get; set; } = true;

        public virtual decimal? krwPrice { get; set; }

        public virtual string krwPriceStr { get; set; }

        public virtual Visibility nowPriceVisible { get; set; } = Visibility.Collapsed;
        /// <summary>
        /// 호가 단위
        /// </summary>
        decimal CashDecimal;

        public virtual int rowSpan { get; set; } = 1;

        public virtual bool cbxBuyPerPrcEnable { get; set; } = true;
        public virtual bool cbxBuySelCoinEnable { get; set; } = false;
        public virtual bool cbxSellPerPrcEnable { get; set; } = true;
        public virtual bool cbxSellSelCoinEnable { get; set; } = false;

        public virtual Visibility BuyMarketCoin { get; set; } = Visibility.Hidden;
        public virtual Visibility SellMarketCoin { get; set; } = Visibility.Hidden;

        public virtual string KrwCoinBuyDivision { get; set; } = "###,###,###,###,##0;";
        public virtual string KrwCoinSellDivision { get; set; } = "###,###,###,###,##0;";
        public virtual string CommonDivision { get; set; } = "###,###,###,###,##0;";

        public virtual List<ErrorRate> ErrorType { get; set; }
        /// <summary>
        /// 선택된 오차율
        /// </summary>
        public virtual ErrorRate SelectedErrorRate { get; set; }

        bool CoinChange = true;
        bool LogicCheck { get; set; } = true;

        /// <summary>
        /// 시장가거래 체크변경
        /// </summary>
        public void OnMarketModeChanged()
        {
            try
            {
                if (MarketMode)
                {
                    if (OrderType.Equals("B"))
                    {
                        cbxBuyPerPrcEnable = false;
                        cbxBuySelCoinEnable = true;
                    }
                    else
                    {
                        cbxSellPerPrcEnable = false;
                        cbxSellSelCoinEnable = true;
                    }
                    OrderPrc = 0;
                }
                else
                {
                    if (OrderType.Equals("B"))
                    {
                        cbxBuyPerPrcEnable = true;
                        cbxBuySelCoinEnable = false;
                    }
                    else
                    {
                        cbxSellPerPrcEnable = true;
                        cbxSellSelCoinEnable = false;
                    }

                    SelectedErrorRate = ErrorType[2];
                    OrderPrc = OrderTempPrc;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 결제코인 구분자 변경시
        /// </summary>
        public async void OnOrderCoinTypeChanged()
        {
            try
            {
                OrderCnt = 0;
                CoinChange = false;

                if (OrderCoinType == EnumLib.ExchangeCurrencyCode.KRW)
                {
                    KrwCoinBuyDivision = "###,###,###,###,##0;";
                    BuyMarketCoin = Visibility.Hidden;
                }
                else
                {
                    if (OrderCoinType == EnumLib.ExchangeCurrencyCode.XRP)
                        KrwCoinBuyDivision = "###,###,###,###,##0.######;";
                    else
                        KrwCoinBuyDivision = "###,###,###,###,##0.########;";

                    BuyMarketCoin = Visibility.Visible;
                }

                using (RequestGetMaxMinPrcModel req = new RequestGetMaxMinPrcModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.buyCuycyCd = StringEnum.GetStringValue(OrderOriginalType);  //구매하려는 통화
                    req.payCuycyCd = StringEnum.GetStringValue(OrderCoinType); //결제하려는 통화

                    using (ResponseGetMaxMinPrcModel res = await WebApiLib.AsyncCall<ResponseGetMaxMinPrcModel, RequestGetMaxMinPrcModel>(req))
                    {
                        if (OrderCoinType == EnumLib.ExchangeCurrencyCode.KRW)
                            OrderAbleCnt = res.data.price;
                        else
                            OrderAbleCnt = res.data.payCnAmt; //코인 수량

                        PayCoinCurr = res.data.payMaxPrc;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 수령코인 구분자 변경시
        /// </summary>
        public async void OnRecCoinTypeChanged()
        {
            try
            {
                OrderCnt = 0;
                CoinChange = false;

                if (RecCoinType == EnumLib.ExchangeCurrencyCode.KRW)
                {
                    KrwCoinSellDivision = "###,###,###,###,##0;";
                    SellMarketCoin = Visibility.Hidden;
                }
                else
                {
                    if (RecCoinType == EnumLib.ExchangeCurrencyCode.XRP)
                        KrwCoinSellDivision = "###,###,###,###,##0.######;";
                    else
                        KrwCoinSellDivision = "###,###,###,###,##0.########;";

                    SellMarketCoin = Visibility.Visible;
                }

                using (RequestGetMaxMinPrcModel req = new RequestGetMaxMinPrcModel())
                {
                    //if (RecCoinPrc == null) return;

                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.buyCuycyCd = StringEnum.GetStringValue(OrderOriginalType);  //구매하려는 통화
                    req.payCuycyCd = StringEnum.GetStringValue(RecCoinType); //결제하려는 통화

                    using (ResponseGetMaxMinPrcModel res = await WebApiLib.AsyncCall<ResponseGetMaxMinPrcModel, RequestGetMaxMinPrcModel>(req))
                    {
                        RecCoinCurr = res.data.payMinPrc;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnSelectedErrorRateChanged()
        {
            try
            {
                OrderPrc = OrderTempPrc + (OrderTempPrc * SelectedErrorRate.value);
                LostFocusPrc();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void LostFocusPrc()
        {
            try
            {
                if (CoinTradingViewModel.AskingData == null) return;
                if (CoinTradingViewModel.AskingData.marketCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                {
                    if (OrderPrc > 0)
                    {
                        decimal tempAsk = 0;

                        foreach (ResponseGetAskingListModel item in CoinTradingViewModel.AskingData.list)
                        {
                            if (OrderPrc >= item.minAmt)
                            {
                                //소수점 사용 X
                                if (CashDecimal.Equals(0))
                                {
                                    //호가단위가 소수점 단위이면
                                    if (item.asking < 0)
                                    {
                                        tempAsk = 1;
                                    }
                                    else
                                    {
                                        tempAsk = (int)item.asking;
                                    }
                                }
                                else
                                {
                                    tempAsk = item.asking;
                                }

                                break;
                            }
                            else
                            {
                                tempAsk = 1;
                            }
                        }

                        if (tempAsk != 0)
                        {
                            if (CashDecimal.Equals(0))
                            {                               
                                OrderPrc = (int)(OrderPrc / tempAsk) * tempAsk;
                            }
                            else
                            {
                                double temp = Math.Pow(10.0, double.Parse(CashDecimal.ToString()));
                                OrderPrc = Math.Truncate(((int)(OrderPrc / tempAsk) * tempAsk) * (decimal)temp) / (decimal)temp;
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

        /// <summary>
        /// 주문수량 변경시
        /// </summary>
        public void OnOrderCntChanged(decimal? oldvalue)
        {
            try
            {
                if (LogicCheck)
                {
                    // if (oldvalue == 0 && OrderCnt != null && OrderCnt != 0 && OrderCnt.ToString().Length <= 1)
                    if (oldvalue == 0 && OrderCnt != null && OrderCnt != 0 && OrderCnt >= 10)
                    {
                        OrderCnt = decimal.Parse(OrderCnt.ToString().Replace("0", ""));
                        //return;
                    }

                    LogicCheck = false;
                    OrderProcess();
                    LogicCheck = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 주문금액 변경시
        /// </summary>
        /// <param name="oldvalue"></param>
        public void OnOrderPreCntChanged(decimal? oldvalue)
        {
            try
            {
                if (MarketMode)
                {
                    if (oldvalue == 0 && OrderPreCnt != null && OrderPreCnt != 0 && OrderPreCnt.ToString().Length <= 2) //OrderPreCnt <= 10 
                    {
                        if (OrderCnt == 0)
                        {
                            OrderPreCnt = decimal.Parse(OrderPreCnt.ToString().Replace("0", ""));
                            //return;
                        }
                    }

                    if (LogicCheck)
                    {
                        LogicCheck = false;
                        if (OrderPreCnt == null)
                        {
                            OrderPreCnt = 0;
                            LogicCheck = true;
                            return;
                        }
                        if (OrderPrc == null)
                        {
                            OrderPrc = 0;
                            LogicCheck = true;
                            return;
                        }
                        if (OrderCnt == null)
                        {
                            OrderCnt = 0;
                            LogicCheck = true;
                            return;
                        }

                        if (OrderType == "B")
                        {
                            if (CommonLib.Market.Contains(OrderCoinType))
                            {
                                OrderCnt = decimal.Parse(((decimal)((OrderPreCnt) / OrderMarketPrc)).ToString("#,##0.########"));
                            }
                            //else
                            //{
                            //    if (!CoinChange)
                            //        OrderCnt = decimal.Parse(((decimal)((OrderPreCnt * PayCoinPrc) / RecCoinPrc)).ToString("#,##0.########"));
                            //}
                        }
                        else
                        {
                            if (CommonLib.Market.Contains(RecCoinType))
                            {
                                OrderCnt = decimal.Parse(((decimal)((OrderPreCnt) / OrderMarketPrc)).ToString("#,##0.########"));
                            }
                            //else
                            //{
                            //    if (!CoinChange)
                            //        OrderCnt = decimal.Parse(((decimal)((OrderPreCnt / PayCoinPrc) * RecCoinPrc)).ToString("#,##0.########"));
                            //}
                        }

                        LogicCheck = true;
                    }
                }
                else
                {
                    if (oldvalue == 0 && OrderTotPrc != null && OrderTotPrc != 0 && OrderTotPrc >= 10)
                    {
                        if (OrderCnt == 0)
                        {
                            OrderTotPrc = decimal.Parse(OrderTotPrc.ToString().Replace("0", ""));
                            return;
                        }
                    }

                    if (LogicCheck)
                    {
                        LogicCheck = false;
                        if (OrderTotPrc == null)
                        {
                            LogicCheck = true;
                            OrderTotPrc = 0;
                            return;
                        }
                        if (OrderPrc == null)
                        {
                            LogicCheck = true;
                            OrderPrc = 0;
                            return;
                        }
                        if (OrderPrc == 0)
                        {
                            LogicCheck = true;
                            return;
                        }

                        OrderCnt = decimal.Parse(((decimal)((OrderPreCnt) / OrderPrc)).ToString("#,##0.########"));

                        LogicCheck = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                LogicCheck = true;
            }
        }
        /// <summary>
        /// 주문가격 변경시
        /// </summary>
        public void OnOrderPrcChanged()
        {
            try
            {
                if (LogicCheck)
                {
                    LogicCheck = false;
                    OrderProcess();
                    LogicCheck = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 구매금액,수수료,예상수량 계산
        /// </summary>
        void OrderProcess()
        {
            try
            {
                if (MarketMode)
                {
                    //구매
                    if (OrderType == "B")
                    {
                        //지불코인 종류
                        if (CommonLib.Market.Contains(OrderCoinType))
                        {
                            if (OrderCnt == null)
                            {
                                OrderCnt = 0;
                            }
                            //사용금액
                            OrderTotPrc = decimal.Parse(((decimal)(OrderCnt)).ToString("#,##0.########"));

                            //예상수량
                            if (OrderMarketPrc == null || OrderMarketPrc == 0)
                            {
                                OrderPreCnt = 0;
                            }
                            else
                            {
                                var Temp = decimal.Parse(((decimal)(OrderCnt * OrderMarketPrc)).ToString(KrwCoinSellDivision));
                                OrderPreCnt = decimal.Parse(((decimal)Temp).ToString(KrwCoinSellDivision)) <= 0 ? 0 : decimal.Parse(((decimal)Temp).ToString(KrwCoinSellDivision));
                            }
                        }
                        //else
                        //{
                        //    //코인스왑(스왑할 코인을 시장가 판매하고 후 받을 한화로 받을코인을 시장가에 구매한다)
                        //    if (OrderCnt == null || OrderCnt == 0 || OrderMarketPrc == null || OrderMarketPrc == 0 || RecCoinPrc == null || RecCoinPrc == 0 || PayCoinPrc == null || PayCoinPrc == 0)
                        //    {
                        //        OrderTotPrc = 0;
                        //        OrderPreCnt = 0;
                        //        return;
                        //    }
                        //    else
                        //    {
                        //        //판매수량
                        //        OrderTotPrc = decimal.Parse(((decimal)(RecCoinPrc * OrderCnt)).ToString("#,##0.########")); //결제코인의시세와 주문코인 갯수를 곱한 것                                                                                                                            
                        //        decimal? RecOrderTotPrc = decimal.Parse(((decimal)(OrderTotPrc / PayCoinPrc)).ToString("#,##0.########")); //수령하려는 코인의 시세와 나누어 갯수를 만듬.
                        //        OrderPreCnt = decimal.Parse(((decimal)RecOrderTotPrc).ToString("#,##0.########")) <= 0 ? 0 : decimal.Parse(((decimal)RecOrderTotPrc).ToString("#,##0.########"));
                        //    }
                        //}
                    }

                    //판매
                    if (OrderType == "S")
                    {
                        //수령코인 종류
                        if (CommonLib.Market.Contains(RecCoinType))
                        {
                            if (OrderMarketPrc == null | OrderMarketPrc == 0 || OrderCnt == null | OrderCnt == 0)
                            {
                                OrderPreCnt = 0;
                            }
                            else
                            {
                                //판매수량
                                decimal? OrderTotPrc = decimal.Parse(((decimal)(OrderMarketPrc * OrderCnt)).ToString("#,##0.########"));
                                //예상수량
                                OrderPreCnt = decimal.Parse(((decimal)OrderTotPrc).ToString("#,##0.########")) <= 0 ? 0 : decimal.Parse(((decimal)OrderTotPrc).ToString("#,##0.########"));
                            }
                        }
                        //else
                        //{
                        //    //코인스왑(스왑할 코인을 시장가 판매하고 후 받을 한화로 받을코인을 시장가에 구매한다)
                        //    if (OrderMarketPrc == null | OrderMarketPrc == 0 || OrderCnt == null | OrderCnt == 0 || RecCoinPrc == null || RecCoinPrc == 0)
                        //    {
                        //        OrderPreCnt = 0;
                        //    }
                        //    else
                        //    {
                        //        //판매금액
                        //        decimal? OrderTotPrc = OrderMarketPrc * OrderCnt;
                        //        //판매예상수량
                        //        decimal? RecOrderPreCnt = decimal.Parse(((decimal)OrderTotPrc).ToString("#,##0.########")) <= 0 ? 0 : decimal.Parse(((decimal)OrderTotPrc).ToString("#,##0.########"));

                        //        //구매수량
                        //        decimal? RecOrderTotPrc = decimal.Parse(((decimal)(RecOrderPreCnt / RecCoinPrc)).ToString("#,##0.########"));
                        //        //구매예상수량
                        //        OrderPreCnt = decimal.Parse(((decimal)RecOrderTotPrc).ToString("#,##0.########")) <= 0 ? 0 : decimal.Parse(((decimal)RecOrderTotPrc).ToString("#,##0.########"));
                        //    }
                        //}
                    }
                }
                else
                {
                    if (OrderCnt == null)
                    {
                        LogicCheck = true;
                        OrderCnt = 0;
                        return;
                    }
                    if (OrderPrc == null)
                    {
                        LogicCheck = true;
                        OrderPrc = 0;
                        return;
                    }
                    if (OrderTotPrc == null)
                    {
                        LogicCheck = true;
                        OrderTotPrc = 0;
                        return;
                    }

                    if (OrderType == "B")
                    {
                        OrderPreCnt = decimal.Parse(((decimal)((OrderPrc * OrderCnt))).ToString(CommonDivision));
                    }
                    if (OrderType == "S")
                    {
                        OrderPreCnt = decimal.Parse(((decimal)((OrderPrc * OrderCnt))).ToString(CommonDivision));
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 주문가격 -
        /// </summary>
        public void CmdOrderPrcMinus()
        {
            try
            {
                if (CoinTradingViewModel.AskingData.marketCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                {
                    if (OrderPrc > 0)
                    {
                        decimal tempAsk = 0;

                        foreach (ResponseGetAskingListModel item in CoinTradingViewModel.AskingData.list)
                        {
                            if (OrderPrc > item.minAmt)
                            {
                                //소수점 사용 X
                                if (CashDecimal.Equals(0))
                                {
                                    //호가단위가 소수점 단위이면
                                    if (item.asking < 0)
                                    {
                                        tempAsk = 1;
                                    }
                                    else
                                    {
                                        tempAsk = (int)item.asking;
                                    }
                                }
                                else
                                {
                                    tempAsk = item.asking;
                                }

                                break;
                            }
                            else
                            {
                                tempAsk = 0;
                            }
                        }

                        if (OrderPrc - tempAsk > 0)
                        {
                            if (CashDecimal.Equals(0))
                            {
                                OrderPrc -= tempAsk;
                                OrderPrc = Math.Truncate((decimal)OrderPrc);
                            }
                            else
                            {
                                OrderPrc -= tempAsk;

                                double temp = Math.Pow(10.0, double.Parse(CashDecimal.ToString()));
                                OrderPrc = Math.Truncate((int)(OrderPrc) * (decimal)temp) / (decimal)temp;
                            }                            
                        }
                    }
                }
                else
                {
                    ResponseGetAskingListModel item = CoinTradingViewModel.AskingData.list[0];

                    if (OrderPrc > 0)
                    {
                        OrderPrc -= item.asking;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 주문가격 +
        /// </summary>
        public void CmdOrderPrcPlus()
        {
            try
            {
                if (CoinTradingViewModel.AskingData.marketCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                {
                    if (OrderPrc > 0)
                    {
                        decimal tempAsk = 0;

                        foreach (ResponseGetAskingListModel item in CoinTradingViewModel.AskingData.list)
                        {
                            if (OrderPrc >= item.minAmt)
                            {
                                //소수점 사용 X
                                if (CashDecimal.Equals(0))
                                {
                                    //호가단위가 소수점 단위이면
                                    if (item.asking < 0)
                                    {
                                        tempAsk = 1;
                                    }
                                    else
                                    {
                                        tempAsk = (int)item.asking;
                                    }
                                }
                                else
                                {
                                    tempAsk = item.asking;
                                }

                                break;
                            }
                            else
                            {
                                tempAsk = 0;
                            }
                        }

                        if (CashDecimal.Equals(0))
                        {
                            OrderPrc += tempAsk;
                            OrderPrc = Math.Truncate((decimal)OrderPrc);
                        }
                        else
                        {
                            OrderPrc += tempAsk;
                            double temp = Math.Pow(10.0, double.Parse(CashDecimal.ToString()));
                            OrderPrc = Math.Truncate((int)(OrderPrc) * (decimal)temp) / (decimal)temp;
                        }
                    }
                }
                else
                {
                    ResponseGetAskingListModel item = CoinTradingViewModel.AskingData.list[0];

                    if (OrderPrc > 0)
                    {
                        OrderPrc += item.asking;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public CommonOrderVM(string orderType, EnumLib.ExchangeCurrencyCode orderCoinType, EnumLib.ExchangeCurrencyCode recCoinType, EnumLib.ExchangeCurrencyCode originalCoinType, decimal cashDecimal)
        {
            try
            {
                this.OrderType = orderType;
                this.OrderCoinType = orderCoinType;
                this.OrderOriginalType = originalCoinType;
                this.RecCoinType = recCoinType;
                this.CashDecimal = cashDecimal;

                int[] arr = { -10, -5, 0, 5, 10 };

                ErrorType = new List<ErrorRate>();
                foreach (int i in arr)
                {
                    ErrorType.Add(new ErrorRate() { name = i.ToString() + "%", value = (decimal)i / 100 });
                }

                SelectedErrorRate = ErrorType[2];

                if (OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.KRW) || RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.KRW))
                {
                    CommonDivision = "###,###,###,###,##0";

                    for (int i = 1; i <= CashDecimal; i++)
                    {
                        if (i == 1)
                        {
                            CommonDivision += ".";
                        }
                        CommonDivision += "#";
                        if (i == CashDecimal)
                        {
                            CommonDivision += ";";
                        }
                    }
                }
                else
                {
                    if (OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.USDT) || RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.USDT))
                    {
                        KrwCoinBuyDivision = "###,###,###,###,##0.###;";
                        CommonDivision = "###,###,###,###,##0.###;";
                    }
                    else
                    {
                        KrwCoinBuyDivision = "###,###,###,###,##0.########;";
                        CommonDivision = "###,###,###,###,##0.########;";
                    }
                }

                if (OrderType.Equals("B"))
                {
                    //if (OrderOriginalType.Equals(EnumLib.ExchangeCurrencyCode.ADM))
                    //{
                    //    MarketMode = true;
                    //    MarketModeEnabled = false;
                    //}
                    //else
                    //{
                        MarketMode = false;
                        MarketModeEnabled = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Set(string orderType, EnumLib.ExchangeCurrencyCode orderCoinType, EnumLib.ExchangeCurrencyCode recCoinType, EnumLib.ExchangeCurrencyCode originalCoinType, decimal cashDecimal)
        {
            try
            {
                this.OrderType = orderType;
                this.OrderCoinType = orderCoinType;
                this.OrderOriginalType = originalCoinType;
                this.RecCoinType = recCoinType;
                this.CashDecimal = cashDecimal;

                SelectedErrorRate = ErrorType[2];

                if (OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.KRW) || RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.KRW))
                {               
                    CommonDivision = "###,###,###,###,##0";

                    for (int i = 1; i <= CashDecimal; i++)
                    {
                        if (i == 1)
                        {
                            CommonDivision += ".";
                        }
                        CommonDivision += "#";
                        if (i == CashDecimal)
                        {
                            CommonDivision += ";";
                        }
                    }
                }
                else
                {
                    if (OrderCoinType.Equals(EnumLib.ExchangeCurrencyCode.USDT) || RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.USDT))
                    {
                        KrwCoinBuyDivision = "###,###,###,###,##0.###;";
                        CommonDivision = "###,###,###,###,##0.###;";
                    }
                    else
                    {
                        KrwCoinBuyDivision = "###,###,###,###,##0.########;";
                        CommonDivision = "###,###,###,###,##0.########;";
                    }
                }

                if (OrderType.Equals("B"))
                {
                    //if (OrderOriginalType.Equals(EnumLib.ExchangeCurrencyCode.ADM))
                    //{
                    //    MarketMode = true;
                    //    MarketModeEnabled = false;
                    //}
                    //else
                    //{
                        MarketMode = false;
                        MarketModeEnabled = true;
                    //}
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
                OrderPrc = null;
                OrderCnt = 0;
                OrderTotPrc = null;
                //OrderAbleCnt = null;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
    /// <summary>
    /// 자동거래
    /// </summary>
    public class AutoOrderVM : ICloneable
    {
        /// <summary>
        /// 구매/판매 구분자(구매:"B" ,판매:"S")
        /// </summary>
        public string OrderType;
        /// <summary>
        /// 결제코인 구분자("BTC","ETH","BCH","XRP",CommonLib.StandardCurcy)
        /// </summary>
        public EnumLib.ExchangeCurrencyCode OrderCoinType { get; set; }
        /// <summary>
        /// 수령코인 구분자("BTC","ETH","BCH","XRP",CommonLib.StandardCurcy)
        /// </summary>
        public virtual EnumLib.ExchangeCurrencyCode RecCoinType { get; set; }
        /// <summary>
        /// 순서
        /// </summary>
        public int Rank;
        /// <summary>
        /// 자동거래가격
        /// </summary>
        public virtual decimal? OrderPrc { get; set; } = 0;
        /// <summary>
        /// 주문수량
        /// </summary>
        public virtual decimal? OrderCnt { get; set; } = 0;
        /// <summary>
        /// 오차율
        /// </summary>
        public virtual int OrderErrorRate { get; set; }
        /// <summary>
        /// 오차금액(Low)
        /// </summary>
        public virtual decimal? OrderErrorLowPrc { get; set; }
        /// <summary>
        /// 오차금액(High)
        /// </summary>
        public virtual decimal? OrderErrorHighPrc { get; set; }
        public virtual string TradeType { get; set; }
        /// <summary>
        /// 오차 콤보박스 리스트
        /// </summary>
        public virtual List<ErrorRate> ErrorType { get; set; }
        /// <summary>
        /// 선택된 오차율
        /// </summary>
        public virtual ErrorRate SelectedErrorRate { get; set; }
        public virtual decimal CommonFloat { get; set; }

        /// <summary>
        /// 미체결 최저가 체결금액
        /// </summary>
        public virtual decimal? conclusionMinPrc { get; set; } = 0;
        /// <summary>
        /// 같은 금액에 구매 체결 체크
        /// </summary>
        public virtual bool conclusionBuyCheck { get; set; } = true;
        /// <summary>
        /// 미체결 최고가 체결금액
        /// </summary>
        public virtual decimal? conclusionMaxPrc { get; set; } = 0;
        /// <summary>
        /// 같은 금액에 판매 체결 체크
        /// </summary>
        public virtual bool conclusionSellCheck { get; set; } = true;

        public virtual bool IsBuyTradeEnabled { get; set; }
        public virtual bool IsSellTradeEnabled { get; set; }
        public virtual bool IsLossTradeEnabled { get; set; }
        public virtual decimal CashDecimal { get; set; }
        /// <summary>
        /// 자동거래 가격 변경시
        /// </summary>
        public void OnOrderPrcChanged(decimal? oldvalue)
        {
            //if (oldvalue == 0 && OrderPrc != null && OrderPrc != 0 && OrderPrc >= 10)
            //{
            //    OrderPrc = decimal.Parse(OrderPrc.ToString().Replace("0", ""));
            //    return;
            //}
        }

        /// <summary>
        /// 자동거래 수량 변경시
        /// </summary>
        public void OnOrderCntChanged(decimal? oldvalue)
        {
            //if (oldvalue == 0 && OrderCnt != null && OrderCnt != 0 && OrderCnt >= 10)
            //{                
            //    OrderCnt = decimal.Parse(OrderCnt.ToString().Replace("0", ""));
            //    return;                
            //}
        }

        public void OnOrderErrorRateChanged()
        {
            if (OrderErrorRate == 0) return;

            SelectedErrorRate = ErrorType[OrderErrorRate];
        }

        public void OnSelectedErrorRateChanged()
        {
            //if (OrderPrc == null || OrderPrc == 0)
            //{
            //    SelectedErrorRate = ErrorType[0];
            //    OrderErrorRate = 0;
            //    return;
            //}
            //if (SelectedErrorRate.value == -1) return;

            OrderErrorRate = ErrorType.IndexOf(SelectedErrorRate);

            if (OrderType.Equals("B") || OrderType.Equals("L"))
            {
                OrderErrorLowPrc = LostFocusPrc(OrderPrc - (OrderPrc * SelectedErrorRate.value) - CommonFloat);
                OrderErrorHighPrc = OrderPrc;
            }
            else
            {
                OrderErrorLowPrc = OrderPrc;
                OrderErrorHighPrc = LostFocusPrc(OrderPrc + (OrderPrc * SelectedErrorRate.value) + CommonFloat);
            }
        }

        public void LostFocusPrc()
        {
            try
            {
                if (CoinTradingViewModel.AskingData.marketCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                {
                    if (OrderPrc > 0)
                    {
                        decimal tempAsk = 0;

                        foreach (ResponseGetAskingListModel item in CoinTradingViewModel.AskingData.list)
                        {
                            if (OrderPrc >= item.minAmt)
                            {
                                tempAsk = item.asking;
                                break;
                            }
                            else
                            {
                                tempAsk = 1;
                            }
                        }

                        if (tempAsk != 0)
                        {
                            if (CashDecimal.Equals(0))
                            {
                                OrderPrc = (int)(OrderPrc / tempAsk) * tempAsk;
                            }
                            else
                            {
                                double temp = Math.Pow(10.0, double.Parse(CashDecimal.ToString()));
                                OrderPrc = Math.Truncate(((int)(OrderPrc / tempAsk) * tempAsk) * (decimal)temp) / (decimal)temp;
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

        public decimal? LostFocusPrc(decimal? OrderPrc)
        {
            try
            {
                if (CoinTradingViewModel.AskingData == null) return 0;
                if (CoinTradingViewModel.AskingData.marketCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                {
                    if (OrderPrc > 0)
                    {
                        decimal tempAsk = 0;

                        foreach (ResponseGetAskingListModel item in CoinTradingViewModel.AskingData.list)
                        {
                            if (OrderPrc >= item.minAmt)
                            {
                                tempAsk = item.asking;
                                break;
                            }
                            else
                            {
                                tempAsk = 1;
                            }
                        }

                        if (tempAsk != 0)
                        {
                            if (CashDecimal.Equals(0))
                            {
                                OrderPrc = (int)(OrderPrc / tempAsk) * tempAsk;
                            }
                            else
                            {
                                double temp = Math.Pow(10.0, double.Parse(CashDecimal.ToString()));
                                OrderPrc = Math.Truncate(((int)(OrderPrc / tempAsk) * tempAsk) * (decimal)temp) / (decimal)temp;
                            }
                        }
                    }
                }

                return OrderPrc;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return OrderPrc;
            }
        }

        /// <summary>
        /// 주문금액 계산
        /// </summary>
        void OrderProcess()
        {
            try
            {
                if (OrderCnt == null)
                {
                    OrderCnt = 0;
                    return;
                }
                if (OrderPrc == null)
                {
                    OrderPrc = 0;
                    return;
                }

                if (OrderType == "B")
                {                   
                    //if (OrderPoint >= (OrderPrc * OrderCnt))
                    //{
                    //    OrderTotPrc = 0;
                    //}
                    //else
                    //{
                    //OrderTotPrc = decimal.Parse(((decimal)(OrderPrc * OrderCnt)).ToString("#,##0"));
                    //}
                }
                //판매
                if (OrderType == "S")
                {
                    //판매금액
                    //if (OrderPrc == 0 || OrderCnt == 0)
                    //{
                    //    OrderTotPrc = 0;
                    //    return;
                    //}
                    //OrderTotPrc = decimal.Parse(((decimal)(OrderPrc * OrderCnt)).ToString("#,##0"));
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 자동거래가격 -1000
        /// </summary>
        public void CmdAutoOrderPrcMinus()
        {
            try
            {
                if (CoinTradingViewModel.AskingData.marketCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                {
                    if (OrderPrc > 0)
                    {
                        decimal tempAsk = 0;

                        foreach (ResponseGetAskingListModel item in CoinTradingViewModel.AskingData.list)
                        {
                            if (OrderPrc > item.minAmt)
                            {
                                tempAsk = item.asking;
                                break;
                            }
                            else
                            {
                                tempAsk = 0;
                            }
                        }

                        if (OrderPrc - tempAsk > 0)
                        {
                            if (CashDecimal.Equals(0))
                            {
                                OrderPrc -= tempAsk;
                                OrderPrc = Math.Truncate((decimal)OrderPrc);
                            }
                            else
                            {
                                OrderPrc -= tempAsk;

                                double temp = Math.Pow(10.0, double.Parse(CashDecimal.ToString()));
                                OrderPrc = Math.Truncate((int)(OrderPrc) * (decimal)temp) / (decimal)temp;
                            }
                        }
                    }
                }
                else
                {
                    ResponseGetAskingListModel item = CoinTradingViewModel.AskingData.list[0];

                    if (OrderPrc > 0)
                    {
                        OrderPrc -= item.asking;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 자동거래가격 +1000
        /// </summary>
        public void CmdAutoOrderPrcPlus()
        {
            try
            {
                if (CoinTradingViewModel.AskingData.marketCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                {
                    if (OrderPrc > 0)
                    {
                        decimal tempAsk = 0;

                        foreach (ResponseGetAskingListModel item in CoinTradingViewModel.AskingData.list)
                        {
                            if (OrderPrc >= item.minAmt)
                            {
                                tempAsk = item.asking;
                                break;
                            }
                            else
                            {
                                tempAsk = 0;
                            }
                        }

                        if (CashDecimal.Equals(0))
                        {
                            OrderPrc += tempAsk;
                            OrderPrc = Math.Truncate((decimal)OrderPrc);
                        }
                        else
                        {
                            OrderPrc += tempAsk;
                            double temp = Math.Pow(10.0, double.Parse(CashDecimal.ToString()));
                            OrderPrc = Math.Truncate((int)(OrderPrc) * (decimal)temp) / (decimal)temp;
                        }
                    }
                }
                else
                {
                    ResponseGetAskingListModel item = CoinTradingViewModel.AskingData.list[0];

                    if (OrderPrc > 0)
                    {
                        OrderPrc += item.asking;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public AutoOrderVM(string orderType, string tradeType, int iRank, EnumLib.ExchangeCurrencyCode coinType, EnumLib.ExchangeCurrencyCode recCoinType, decimal cashDecimal)
        {
            try
            {
                this.OrderType = orderType;
                this.OrderCoinType = coinType;
                this.TradeType = tradeType;
                this.RecCoinType = recCoinType;
                this.Rank = iRank;
                this.CashDecimal = cashDecimal;

                this.IsBuyTradeEnabled = true;
                this.IsSellTradeEnabled = true;
                this.IsLossTradeEnabled = true;

                string sPlusMinus = string.Empty;
                if (OrderType.Equals("B") || OrderType.Equals("L")) sPlusMinus = "-";

                if (RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.KRW))
                {
                    CommonFloat = 0;
                }
                else if (RecCoinType.Equals(EnumLib.ExchangeCurrencyCode.USDT))
                {
                    CommonFloat = (decimal)0.001;
                }
                else
                {
                    CommonFloat = (decimal)0.00000001;
                }


                ErrorType = new List<ErrorRate>();
                ErrorType.Add(new ErrorRate() { name = Localization.Resource.CoinTrading_8_6, value = -1 });
                ErrorType.Add(new ErrorRate() { name = "0%", value = 0 });
                for (int i = 1; i <= 10; i++)
                {
                    ErrorType.Add(new ErrorRate() { name = sPlusMinus + i.ToString() + "%", value = (decimal)i / 100 });
                }
                SelectedErrorRate = ErrorType[0];
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Reset(EnumLib.ExchangeCurrencyCode coinType, EnumLib.ExchangeCurrencyCode recCoinType)
        {
            try
            {
                OrderPrc = 0;
                OrderCnt = 0;
                SelectedErrorRate = ErrorType[0];

                //this.OrderType = orderType;
                this.OrderCoinType = coinType;
                //this.TradeType = tradeType;
                this.RecCoinType = recCoinType;
                //this.Rank = iRank;

                this.IsBuyTradeEnabled = true;
                this.IsSellTradeEnabled = true;
                this.IsLossTradeEnabled = true;

                //string sPlusMinus = string.Empty;
                //if (OrderType.Equals("B") || OrderType.Equals("L")) sPlusMinus = "-";
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Set(decimal? orderPrc, int orderErrorRate, decimal? orderErrorLowPrc, decimal? orderErrorHighPrc, decimal? orderCnt)
        {
            try
            {
                this.OrderPrc = orderPrc;
                this.OrderErrorRate = orderErrorRate;
                this.OrderErrorLowPrc = orderErrorLowPrc;
                this.OrderErrorHighPrc = orderErrorHighPrc;
                this.OrderCnt = orderCnt;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public object Clone()
        {
            AutoOrderVM c = (AutoOrderVM)this.MemberwiseClone();
            return c;
        }

        /// <summary>
        /// 초기화
        /// </summary>
        public void Clear()
        {
            OrderPrc = 0;
            OrderCnt = 0;
            OrderErrorLowPrc = null;
            OrderErrorHighPrc = null;
            SelectedErrorRate = ErrorType[0];
            conclusionMinPrc = 0;
            conclusionMaxPrc = 0;
            conclusionBuyCheck = true;
            conclusionSellCheck = true;
            //OrderTotPrc = null;
            //OrderAbleCnt = null;
        }
    }
    /// <summary>
    /// 자동거래 묶음
    /// </summary>
    public class AutoOrderVM_ALL
    {
        public virtual List<AutoOrderVM> list { get; set; }
        public AutoOrderVM_ALL()
        {
            list = new List<AutoOrderVM>();
        }
    }
}