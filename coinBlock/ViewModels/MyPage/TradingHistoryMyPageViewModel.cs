using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using coinBlock.Model;
using coinBlock.Model.MyPage;
using coinBlock.Utility;
using coinBlock.Views.Common;
using coinBlock.Views;
using coinBlock.Model.Trading;
using System.Linq;

namespace coinBlock.ViewModels.MyPage
{
    [POCOViewModel]
    public class TradingHistoryMyPageViewModel
    {
        public virtual ObservableCollection<clsTradeHistory> TradeHis { get; set; }
        public virtual ObservableCollection<ResponseGetTradeHistoryListModel> TradeHisData { get; set; }

        public virtual Visibility listVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emptyVisible { get; set; } = Visibility.Collapsed;

        public virtual DateTime fromDate { get; set; } = DateTime.Now.AddDays(-7);
        public virtual DateTime toDate { get; set; } = DateTime.Now;

        public virtual Visibility RechargeVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility WithdrawCompVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility WithdrawWaitVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility ElseVisible { get; set; } = Visibility.Visible;
        public virtual Visibility MoreVisible { get; set; } = Visibility.Collapsed;

        public virtual ObservableCollection<CommonCombobox> ExCodeList { get; set; }
        public virtual CommonCombobox SelectedExCode { get; set; }
        public virtual ObservableCollection<CommonCombobox> MarketList { get; set; }
        public virtual CommonCombobox SelectedMarket { get; set; }
        public virtual string SelectedTrade { get; set; }

        public virtual bool bStatusComp { get; set; } = true;
        public virtual bool bStatusProc { get; set; }

        public virtual int pageIndex { get; set; } = 1;
        public virtual int pageSize { get; set; } = 1;

        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }
        public virtual string btn_more { get; set; }
        public virtual string btn_more_on { get; set; }

        public virtual string nextKey { get; set; } = string.Empty; 
        public virtual string CommonFloat { get; set; }

        public virtual bool IsBusy { get; set; }

        int iCnt = 0;

        protected TradingHistoryMyPageViewModel()
        {            
            ImageSet();
            Init();
        }
        public static TradingHistoryMyPageViewModel Create()
        {
            return ViewModelSource.Create(() => new TradingHistoryMyPageViewModel());
        }

        public void Loaded()
        {
            try
            {
                //GetData();
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
                //pageIndex = 1;
                iCnt = 0;
                nextKey = string.Empty;
                MoreVisible = Visibility.Collapsed;                
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Init()
        {
            try
            {
                ExCodeList = new ObservableCollection<CommonCombobox>();
                ExCodeList.Add(new CommonCombobox { Name = Localization.Resource.TradingHistory_1_1, Value = "All" });
                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    ExCodeList.Add(new CommonCombobox { Name = item.CoinName, Value = item.CoinCode });
                }
                SelectedExCode = ExCodeList[0];

                MarketList = new ObservableCollection<CommonCombobox>();
                MarketList.Add(new CommonCombobox { Name = CommonLib.StandardCurcyNm, Value = CommonLib.StandardCurcyNm });
                MarketList.Add(new CommonCombobox { Name = EnumLib.ExchangeCurrencyCode.ETH.ToString(), Value = EnumLib.ExchangeCurrencyCode.ETH.ToString() });
                MarketList.Add(new CommonCombobox { Name = EnumLib.ExchangeCurrencyCode.USDT.ToString(), Value = EnumLib.ExchangeCurrencyCode.USDT.ToString() });
                MarketList.Add(new CommonCombobox { Name = EnumLib.ExchangeCurrencyCode.BTC.ToString(), Value = EnumLib.ExchangeCurrencyCode.BTC.ToString() });

                SelectedMarket = MarketList[0];

                TradeHis = new ObservableCollection<clsTradeHistory>();

                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_1, sCode = "All", sBackColor = "#e5e5e5", sForeColor = "#333" }));
                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_2, sCode = StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Recharge), sBackColor = "#e5e5e5", sForeColor = "#333" }));
                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_3, sCode = StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Buy), sBackColor = "#e5e5e5", sForeColor = "#333" }));
                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_4, sCode = StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Sell), sBackColor = "#e5e5e5", sForeColor = "#333" }));
                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_5, sCode = StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Withdraw), sBackColor = "#e5e5e5", sForeColor = "#333" }));
                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_6, sCode = StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Transfer), sBackColor = "#e5e5e5", sForeColor = "#333" }));
                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_7, sCode = StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Deposit), sBackColor = "#e5e5e5", sForeColor = "#333" }));
                TradeHis.Add(ViewModelSource.Create(() => new clsTradeHistory { sName = Localization.Resource.TradingHistory_1_8, sCode = StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Etc), sBackColor = "#e5e5e5", sForeColor = "#333" }));

                CmdSelect(TradeHis[0].sCode);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnSelectedMarketChanged()
        {
            try
            {
                if (SelectedMarket.Value.Equals(CommonLib.StandardCurcyNm))
                {
                    CommonFloat = "n0";
                }
                else
                {
                    CommonFloat = "n8";
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSearchContent()
        {
            try
            {
                //pageIndex = 1;
                iCnt = 0;
                nextKey = string.Empty;
                MoreVisible = Visibility.Collapsed;
                TradeHisData = new ObservableCollection<ResponseGetTradeHistoryListModel>();                
                GetData();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSelect(string code)
        {
            try
            {
                TradeHisData = new ObservableCollection<ResponseGetTradeHistoryListModel>();

                foreach (var item in TradeHis)
                {
                    if (item.sCode.Equals(code))
                    {
                        item.sBackColor = "#0090d5";
                        item.sForeColor = "#FFF";

                        //pageIndex = 1;
                        iCnt = 0;
                        nextKey = string.Empty;
                        MoreVisible = Visibility.Collapsed;
                        TradeHisData = new ObservableCollection<ResponseGetTradeHistoryListModel>();                        
                        SelectedTrade = item.sCode;
                        GetData();
                    }
                    else
                    {
                        item.sBackColor = "#e5e5e5";
                        item.sForeColor = "#333";
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdPageCall(PageingDataEventArgs e)
        {
            try
            {
                string bUrl = e.baseUrl;
                pageIndex = e.PageNum;
                GetData();
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
                GetData();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetData()
        {
            try
            {
                IsBusy = true;

                using (RequestGetTradeHistoryModel req = new RequestGetTradeHistoryModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    if (SelectedTrade != "All")
                    {
                        req.hisCode = SelectedTrade;
                    }
                    if (bStatusComp)
                    {
                        req.orderState = StringEnum.GetStringValue(EnumLib.TradeStateCode.Complete);
                    }
                    else
                    {
                        req.orderState = StringEnum.GetStringValue(EnumLib.TradeStateCode.Waiting);
                    }
                    if (SelectedExCode.Value != "All")
                    {
                        req.curcyCd = SelectedExCode.Value;
                    }
                    //req.pageIndex = pageIndex;
                    req.stdDate = fromDate.ToString("yyyy-MM-dd");
                    req.endDate = toDate.ToString("yyyy-MM-dd");
                    req.mkState = SelectedMarket.Value;

                    if (!nextKey.Equals(""))
                    {
                        req.key01 = nextKey;
                    }

                    using (ResponseGetTradeHistoryModel res = await WebApiLib.AsyncCall<ResponseGetTradeHistoryModel, RequestGetTradeHistoryModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            nextKey = res.data.nextKey == null ? string.Empty : res.data.nextKey;

                            //if (res.data.list.Count > 0)
                            //{
                            ObservableCollection<ResponseGetTradeHistoryListModel> temp = res.data.list;

                            if (temp.Count.Equals(10))
                            {
                                MoreVisible = Visibility.Visible;
                            }
                            else
                            {
                                MoreVisible = Visibility.Collapsed;
                            }

                            foreach (ResponseGetTradeHistoryListModel item in temp)
                            {
                                item.no = ++iCnt;

                                if (SelectedMarket.Value.Equals(CommonLib.StandardCurcyNm))
                                {
                                    if (item.hisCode.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Buy)) || item.hisCode.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Sell)))
                                    {
                                        ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.curcyCd).FirstOrDefault();
                                        CommonFloat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                                    }
                                    else
                                    {
                                        CommonFloat = "n0";
                                    }
                                }
                                else
                                {
                                    CommonFloat = "n8";
                                }

                                //계좌충전
                                if (item.payKndCd.Equals(StringEnum.GetStringValue(EnumLib.PaymentWay.accountTransfer)) && item.exFlag.Equals("C"))
                                {
                                    item.reqAmt = item.totPrc;
                                    item.exFlag = "A" + item.exFlag;
                                }
                                else if (item.payKndCd.Equals(StringEnum.GetStringValue(EnumLib.PaymentWay.prepaidCard)) && item.exFlag.Equals("C"))
                                {
                                    item.reqAmt = item.totPrc;
                                    item.exFlag = "C" + item.exFlag;
                                    item.cardVisible = Visibility.Visible;
                                }
                                else if (item.payKndCd.Equals("A") && item.exFlag.Equals("W"))
                                {
                                    item.exFlag = item.payKndCd + item.exFlag;
                                    item.accountVisible = Visibility.Visible;
                                }
                                else if (item.payKndCd.Equals("C") && item.exFlag.Equals("W"))
                                {
                                    item.exFlag = item.payKndCd + item.exFlag;
                                    item.cardVisible = Visibility.Visible;
                                }
                                else if ((item.hisCode.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Buy)) || item.hisCode.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Sell))) && bStatusComp == false)
                                {
                                    item.cancelVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("P"))
                                {
                                    item.ptmVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("D") || item.exFlag.Equals("T"))
                                {
                                    item.dtVisible = Visibility.Visible;
                                }
                                else if ((item.exFlag.Equals("W") && item.payKndCd.Equals("B")) || (item.exFlag.Equals("C") && item.payKndCd.Equals("CMMC00000001685")))
                                {
                                    item.currTransVisible = Visibility.Visible;
                                }

                                string TradeType = SelectedMarket.Value;

                                if (item.exFlag.Equals("B"))
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    //item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat) + " " + TradeType;
                                    //item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat) + " " + TradeType;
                                    item.tradePrc = CommonLib.GetFloatPoint(item.tradePrc, CommonFloat) + " " + TradeType;
                                    item.totPrc = CommonLib.GetFloatPoint(item.totPrc, CommonFloat) + " " + TradeType;
                                    item.fee = decimal.Parse(item.fee).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                }
                                else if (item.exFlag.Equals("S"))
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    //item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat) + " " + TradeType;
                                    //item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat) + " " + TradeType;
                                    item.tradePrc = CommonLib.GetFloatPoint(item.tradePrc, CommonFloat) + " " + TradeType;
                                    item.totPrc = CommonLib.GetFloatPoint(item.totPrc, CommonFloat) + " " + TradeType;
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat) + " " + TradeType;
                                }
                                else if (item.exFlag.Equals("R"))
                                {
                                    item.trustVisible = Visibility.Visible;

                                    item.cnAmt = decimal.Parse(item.tradeAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);
                                    item.orderDesc = Localization.Resource.TradingHistory_Common_4;
                                    item.buysellVisible = Visibility.Collapsed;
                                    item.orderDescVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("L"))
                                {
                                    item.rockUpVisible = Visibility.Visible;

                                    item.cnAmt = decimal.Parse(item.tradeAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);
                                }
                                else if (item.exFlag.Equals("A"))
                                {
                                    item.autoTradeVisible = Visibility.Visible;

                                    if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                                    {
                                        item.cnAmt = decimal.Parse(item.tradeAmt).ToString(CommonFloat) + " " + item.curcyNm;
                                    }
                                    else
                                    {
                                        item.cnAmt = decimal.Parse(item.tradeAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    }
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);
                                    item.orderDesc = Localization.Resource.AutoTradingAdditionalPop_1;
                                    item.buysellVisible = Visibility.Collapsed;
                                    item.orderDescVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("AB"))
                                {
                                    item.autoTradeVisible = Visibility.Visible;

                                    if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                                    {
                                        item.cnAmt = decimal.Parse(item.tradeAmt).ToString(CommonFloat) + " " + item.curcyNm;
                                    }
                                    else
                                    {
                                        item.cnAmt = decimal.Parse(item.tradeAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    }                            
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);
                                    item.orderDesc = Localization.Resource.ArbitrageRequestPop_1;
                                    item.buysellVisible = Visibility.Collapsed;
                                    item.orderDescVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("LD"))
                                {
                                    if (item.hisCode.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Deposit)))
                                    {
                                        item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0") + " " + item.curcyNm;
                                        item.orderDesc = Localization.Resource.Lending_1_1;
                                    }
                                    else if (item.hisCode.Equals(StringEnum.GetStringValue(EnumLib.TradeDivisionCode.Transfer)))
                                    {
                                        item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                        item.orderDesc = item.failMsg;
                                    }

                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);

                                    item.buysellVisible = Visibility.Collapsed;
                                    item.orderDescVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("X"))
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0.0") + " " + item.curcyNm;
                                    item.exFlag = "D";

                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);

                                    item.orderDesc = Localization.Resource.TradingHistory_1_19;

                                    item.buysellVisible = Visibility.Collapsed;
                                    item.orderDescVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("E"))
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0") + " " + item.curcyNm;
                                    item.exFlag = "D";

                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);

                                    item.orderDesc = Localization.Resource.TradingHistory_1_20;

                                    item.buysellVisible = Visibility.Collapsed;
                                    item.orderDescVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("Z"))
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0") + " " + item.curcyNm;
                                    item.exFlag = "D";

                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString(CommonFloat);

                                    item.orderDesc = Localization.Resource.TradingHistory_1_21;

                                    item.buysellVisible = Visibility.Collapsed;
                                    item.orderDescVisible = Visibility.Visible;
                                }
                                else if (item.exFlag.Equals("W") && item.payKndCd.Equals("B"))
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString("###,###,###,###,##0.########");

                                    item.currTransEmail = item.receiveEmail;
                                    item.currTransNm = item.receiveNm;
                                }
                                else if (item.exFlag.Equals("C") && item.payKndCd.Equals("CMMC00000001685"))
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString("###,###,###,###,##0.########");

                                    item.currTransEmail = item.sendUserEmail;
                                    item.currTransNm = item.sendUserNm;
                                }
                                else
                                {
                                    item.cnAmt = decimal.Parse(item.cnAmt).ToString("###,###,###,###,##0.00000000") + " " + item.curcyNm;
                                    item.tradePrc = decimal.Parse(item.tradePrc).ToString(CommonFloat);
                                    item.totPrc = decimal.Parse(item.totPrc).ToString(CommonFloat);
                                    item.fee = decimal.Parse(item.fee).ToString("###,###,###,###,##0.########");
                                }

                                if (!string.IsNullOrWhiteSpace(item.orderDesc))
                                {
                                    item.orderDesc = CommonLib.CardNumChange(item.orderDesc);
                                }
                                if (!string.IsNullOrWhiteSpace(item.failMsg))
                                {
                                    if (item.failMsg.Equals("F"))
                                    {
                                        item.failMsg = Localization.Resource.WithdrawDepositWithdraw_1_23_8;
                                    }
                                    else if (item.failMsg.Equals("L1"))
                                    {
                                        item.orderDesc = Localization.Resource.Lending_Grid2_3_1;
                                    }
                                    else if (item.failMsg.Equals("L2"))
                                    {
                                        item.orderDesc = Localization.Resource.Lending_Grid2_3_2;
                                    }
                                    else if (item.failMsg.Equals("L3"))
                                    {
                                        item.orderDesc = Localization.Resource.Lending_Grid2_3_3;
                                    }
                                    else if (item.failMsg.Equals("L4"))
                                    {
                                        item.orderDesc = Localization.Resource.Lending_Grid2_3_4;
                                    }

                                    item.refusalVisible = Visibility.Visible;
                                }

                                TradeHisData.Add(item);
                            }

                            
                            //TradeHisData = temp;
                            //pageSize = res.data.pageSize;

                            if (TradeHisData.Count > 0)
                            {
                                listVisible = Visibility.Visible;
                                emptyVisible = Visibility.Collapsed;
                            }
                            else
                            {
                                //pageSize = 1;
                                listVisible = Visibility.Collapsed;
                                emptyVisible = Visibility.Visible;
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

        public async void CmdBuySellCancel(string orderNo)
        {
            try
            {
                Alert alert = new Alert(Localization.Resource.CoinTrading_Alert_6, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    //미체결 주문 취소 구현   
                    using (RequestTradingTradeCancelModel req = new RequestTradingTradeCancelModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                        req.orderNo = orderNo; //주문번호
                        req.mkState = SelectedMarket.Value.ToString();
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseTradingTradeCancelModel res = await WebApiLib.AsyncCall<ResponseTradingTradeCancelModel, RequestTradingTradeCancelModel>(req))
                        {
                            alert = new Alert(Localization.Resource.CoinTrading_Alert_7);
                            alert.ShowDialog();

                            iCnt = 0;
                            nextKey = string.Empty;
                            MoreVisible = Visibility.Collapsed;
                            TradeHisData = new ObservableCollection<ResponseGetTradeHistoryListModel>();
                            GetData();
                            Messenger.Default.Send("AssetsRefresh");
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
            pageIndex = 1;            
            pageSize = 1;
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
            btn_more = sLanguage + "btn_more.png";
            btn_more_on = sLanguage + "btn_more_on.png";
        }
    }

    public class clsTradeHistory
    {
        public virtual string sName { get; set; }

        public virtual string sCode { get; set; }

        public virtual string sBackColor { get; set; }

        public virtual string sForeColor { get; set; }
    }

    public class CommonCombobox
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
