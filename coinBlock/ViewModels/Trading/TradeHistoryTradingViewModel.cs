using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using coinBlock.Model.Trading;
using DevExpress.Mvvm.POCO;
using System.Windows;
using coinBlock.Utility;
using DevExpress.Xpf.Core;
using coinBlock.Model;
using System.Linq;

namespace coinBlock.ViewModels.Trading
{
    [POCOViewModel]
    public class TradeHistoryTradingViewModel
    {
        public virtual string TabName { get; set; }
        public virtual EnumLib.ExchangeCurrencyCode Excucode { get; set; }
        public virtual ObservableCollection<ResponseTradeHistoryTradingSearchWaitListModel> buyWait { get; set; }
        public virtual ObservableCollection<ResponseTradeHistoryTradingSearchWaitListModel> sellWait { get; set; }
        public virtual ObservableCollection<ResponseTradeHistoryTradingSearchCompleteListModel> tradeComplete { get; set; }
        public virtual string buyTime { get; set; }
        public virtual string sellTime { get; set; }
        public virtual string comTime { get; set; }
        public virtual bool IsBusy { get; set; }

        public virtual string buyTitle { get; set; }
        public virtual string sellTitle { get; set; }
        public virtual string compTitle { get; set; }

        public virtual string btn_all_exchange_refresh_s { get; set; }
        public virtual string btn_all_exchange_refresh_s_on { get; set; }
        public virtual string btn_all_exchange_more_s { get; set; }
        public virtual string btn_all_exchange_more_s_on { get; set; }
        public virtual string btn_all_exchange_refresh_l { get; set; }
        public virtual string btn_all_exchange_refresh_l_on { get; set; }
        public virtual string btn_all_exchange_more_l { get; set; }
        public virtual string btn_all_exchange_more_l_on { get; set; }

        public virtual string CommonFloat { get; set; } = "n0";
        public virtual Visibility CoinPriceHeaderVisible { get; set; } = Visibility.Collapsed;
        public virtual string PriceType { get; set; } = CommonLib.StandardCurcyNm;

        public TradeHistoryTradingViewModel()
        {
            ImageSet();
        }
        
        public static TradeHistoryTradingViewModel Create()
        {
            return ViewModelSource.Create(() => new TradeHistoryTradingViewModel());
        }      

        public void Loaded()
        {
            try
            {
                //SetCoinTradeHistory(PriceType, EnumLib.ExchangeCurrencyCode.BTC);
                SetCoinTradeHistory(PriceType, Excucode);
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

        public void SetCoinTradeHistory(string sPriceTpye, EnumLib.ExchangeCurrencyCode val)
        {
            try
            {   
                PriceType = sPriceTpye;
                Excucode = val;

                buyTitle = Excucode.ToString() + "/" + PriceType;
                sellTitle = Excucode.ToString() + "/" + PriceType;
                compTitle = Excucode.ToString() + "/" + PriceType;

                if (PriceType.Equals(CommonLib.StandardCurcyNm))
                {
                    ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == StringEnum.GetStringValue(val)).FirstOrDefault();
                    CommonFloat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                }
                else
                {
                    CommonFloat = "n8";
                }                
                SearchBuyWait(10);
                SearchSellWait(10);
                SearchTradeComplete(10);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void SearchBuyWait(int Count)
        {
            try
            {
                IsBusy = true;
                using (RequestTradeHistoryTradingSearchWaitModel req = new RequestTradeHistoryTradingSearchWaitModel())
                {
                    req.curcyCd = StringEnum.GetStringValue(Excucode);
                    req.tradeCd = StringEnum.GetStringValue(EnumLib.UnsettledDivisionCode.buyWait);
                    req.mkState = PriceType;
                    req.listSize = Count;
                    using (ResponseTradeHistoryTradingSearchWaitModel res = await WebApiLib.AsyncCall<ResponseTradeHistoryTradingSearchWaitModel, RequestTradeHistoryTradingSearchWaitModel>(req))
                    {
                        buyWait = res.data.list;
                        buyTime = CommonLib.GetTime;
                    }
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                IsBusy = false;
            }
        }

        public async void SearchSellWait(int Count)
        {
            try
            {
                IsBusy = true;
                using (RequestTradeHistoryTradingSearchWaitModel req = new RequestTradeHistoryTradingSearchWaitModel())
                {
                    req.curcyCd = StringEnum.GetStringValue(Excucode);
                    req.tradeCd = StringEnum.GetStringValue(EnumLib.UnsettledDivisionCode.sellWait);
                    req.mkState = PriceType;
                    req.listSize = Count;
                    using (ResponseTradeHistoryTradingSearchWaitModel res = await WebApiLib.AsyncCall<ResponseTradeHistoryTradingSearchWaitModel, RequestTradeHistoryTradingSearchWaitModel>(req))
                    {
                        sellWait = res.data.list;
                        sellTime = CommonLib.GetTime;
                    }
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                IsBusy = false;
            }
        }

        public async void SearchTradeComplete(int Count)
        {
            try
            {
                IsBusy = true;
                using (RequestTradeHistoryTradingSearchCompleteModel req = new RequestTradeHistoryTradingSearchCompleteModel())
                {
                    req.curcyCd = StringEnum.GetStringValue(Excucode);
                    req.mkState = PriceType;
                    req.listSize = Count;

                    using (ResponseTradeHistoryTradingSearchCompleteModel res = await WebApiLib.AsyncCall<ResponseTradeHistoryTradingSearchCompleteModel, RequestTradeHistoryTradingSearchCompleteModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            //var item = res.data;
                            //if (item != null && item.list.Count > 0)
                            //{
                            //    for (int i = 0; i < item.list.Count - 1; i++)
                            //    {
                            //        if (i + 1 <= item.list.Count - 1)
                            //        {
                            //            item.list[i].fillPrc = decimal.Parse(item.list[i].tradePrc) >= decimal.Parse(item.list[i + 1].tradePrc) ? "0" : "1";
                            //        }
                            //        else
                            //        {
                            //            item.list[i].fillPrc = "0";
                            //        }
                            //    }
                            //}

                            tradeComplete = res.data.list;
                            comTime = CommonLib.GetTime;
                        }
                    }
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                IsBusy = false;
            }
        }

        public void BuyCountUp(object val)
        {
            try
            {
                int divCount = 0;
                int Count = 0;

                if (val.Equals("Refresh"))
                {
                    if (buyWait != null)
                        Count = buyWait.Count;
                    else
                        Count = 10;
                }
                else
                {
                    if (buyWait != null)
                    {
                        divCount = (buyWait.Count / 10) + 1;
                        if (divCount < 11)
                            Count = 10 * divCount;
                        else
                            Count = 100;
                    }
                    else
                        Count = 10;
                }

                SearchBuyWait(Count);

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void SellCountUp(object val)
        {
            try
            {
                int divCount = 0;
                int Count = 0;

                if (val.Equals("Refresh"))
                {
                    if (sellWait != null)
                        Count = sellWait.Count;
                    else
                        Count = 10;
                }
                else
                {
                    if (sellWait != null)
                    {
                        divCount = (sellWait.Count / 10) + 1;
                        if (divCount < 11)
                            Count = 10 * divCount;
                        else
                            Count = 100;
                    }
                    else
                        Count = 10;
                }

                SearchSellWait(Count);

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ComplateCountUp(object val)
        {
            try
            {
                int divCount = 0;
                int Count = 0;

                if (val.ToString().Equals("Refresh"))
                {
                    if (tradeComplete != null)
                        Count = tradeComplete.Count;
                    else
                        Count = 10;
                }
                else
                {
                    if (tradeComplete != null)
                    {
                        divCount = (tradeComplete.Count / 10) + 1;
                        if (divCount < 11)
                            Count = 10 * divCount;
                        else
                            Count = 100;
                    }
                    else
                        Count = 10;
                }
                SearchTradeComplete(Count);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_all_exchange_refresh_s = sLanguage + "btn_all_exchange_refresh_s.png";
            btn_all_exchange_refresh_s_on = sLanguage + "btn_all_exchange_refresh_s_on.png";
            btn_all_exchange_more_s = sLanguage + "btn_all_exchange_more_s.png";
            btn_all_exchange_more_s_on = sLanguage + "btn_all_exchange_more_s_on.png";
            btn_all_exchange_refresh_l = sLanguage + "btn_all_exchange_refresh_l.png";
            btn_all_exchange_refresh_l_on = sLanguage + "btn_all_exchange_refresh_l_on.png";
            btn_all_exchange_more_l = sLanguage + "btn_all_exchange_more_l.png";
            btn_all_exchange_more_l_on = sLanguage + "btn_all_exchange_more_l_on.png";
        }
    }
}