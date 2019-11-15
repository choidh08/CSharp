using coinBlock.Model;
using coinBlock.Model.Dashboard;
using coinBlock.Utility;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Input;

namespace coinBlock.ViewModels.Dashboard
{
    [POCOViewModel]
    public class ExchangeDashboardViewModel
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

        public virtual ObservableCollection<ResponseExchangeDashboardExchangeListModel> ExchangePriceData { get; set; }
        public virtual ObservableCollection<ResponseExchangeDashboardOrderListModel> BuyOrdelData { get; set; }
        public virtual ObservableCollection<ResponseExchangeDashboardOrderListModel> SellOrdelData { get; set; }
        public virtual List<ResponseExchangeDashboardFillListModel> FillData { get; set; }

        public EnumLib.ExchangeCurrencyCode SelectCoinCode { get; set; }

        string sLanguage = LoginViewModel.LanguagePack;
        public virtual string btn_main_buysell { get; set; }
        public virtual string btn_main_buysell_on { get; set; }
        public virtual string btn_ex_zoom { get; set; }
        public virtual string btn_ex_zoom_on { get; set; }

        public virtual decimal? CoinMarketPrc { get; set; } = null;
        public virtual string CommonFloat { get; set; }
        public virtual string CommonNum { get; set; }

        public virtual string tempCoin { get; set; } = string.Empty;

        bool IsLoad = false;

        public virtual int chartColspan { get; set; } = 1;
        public virtual int chartRowspan { get; set; } = 1;
        public virtual Visibility OrderListVisible { get; set; } = Visibility.Visible;
        public virtual Visibility FillListVisible { get; set; } = Visibility.Visible;

        public virtual Visibility CoinPriceHeaderVisible { get; set; } = Visibility.Visible;
        public virtual string PriceType { get; set; } = CommonLib.StandardCurcyNm;
        public virtual string PriceCode { get; set; } = CommonLib.StandardCurcyCd;

        public static ExchangeDashboardViewModel Create()
        {
            return ViewModelSource.Create(() => new ExchangeDashboardViewModel());
        }

        protected ExchangeDashboardViewModel()
        {
            try
            {
                Messenger.Default.Register<ResponseCoinInfoDataModel>(this, CoinInfoSocketData);
                //전체 주문내역 소켓에서 받은 데이터 처리
                Messenger.Default.Register<ResponseExchangeDashboardOrderDataModel>(this, OrderSocketData);
                //전체 체결내역 소켓에서 받은 데이터 처리
                Messenger.Default.Register<ResponseExchangeDashboardFillDataModel>(this, FillSocketData);

                BuyOrdelData = new ObservableCollection<ResponseExchangeDashboardOrderListModel>();
                SellOrdelData = new ObservableCollection<ResponseExchangeDashboardOrderListModel>();
                FillData = new List<ResponseExchangeDashboardFillListModel>();

                SelectCoinCode = EnumLib.ExchangeCurrencyCode.BTC;
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

                if (item.marketCode == PriceCode && item.curcyCd == StringEnum.GetStringValue(SelectCoinCode))
                {
                    DispatcherService.BeginInvoke(() =>
                    {
                        CoinMarketPrc = item.ytdPrc;
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

                if (item.marketCode == PriceCode && item.curcyCd == StringEnum.GetStringValue(SelectCoinCode))
                {
                    DispatcherService.BeginInvoke(() =>
                    {
                        if (item != null && item.list.Count > 0)
                        {
                            for (int i = 0; i < item.list.Count - 1; i++)
                            {
                                if (i + 1 <= item.list.Count - 1)
                                {
                                    item.list[i].fillPrc = decimal.Parse(item.list[i].coinPrc) >= decimal.Parse(item.list[i + 1].coinPrc) ? "0" : "1";
                                }
                                else
                                {
                                    item.list[i].fillPrc = "0";
                                }
                            }
                            FillData = item.list.Take(20).ToList();
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

        public void OrderSocketData(ResponseExchangeDashboardOrderDataModel item)
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

                if (item.marketCode == PriceCode && item.curcyCd == StringEnum.GetStringValue(SelectCoinCode))
                {
                    //List<ResponseExchangeDashboardOrderListModel> SellOrderTemp = item.list.Where(w => w.sellTranPrc != "" || w.sellCnAmt != "").Select(s => new ResponseExchangeDashboardOrderListModel { sellCnAmt = s.sellCnAmt, sellTranPrc = s.sellTranPrc, floatFormat = s.floatFormat }).Take(16).Reverse().ToList();
                    List<ResponseExchangeDashboardOrderListModel> SellOrderTemp = item.list.Where(w => w.sellTranPrc != "" || w.sellCnAmt != "").Select(s => new ResponseExchangeDashboardOrderListModel { sellCnAmt = s.sellCnAmt, sellTranPrc = s.sellTranPrc, floatFormat = s.floatFormat }).Take(16).ToList();
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
                }
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
                                    target[i].floatFormat = tempsellfloatFormat;
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

                                    target.Add(ViewModelSource.Create(() => new ResponseExchangeDashboardOrderListModel() { sellCnAmt = tempsellCnAmt, sellTranPrc = tempsellTranPrc, sellTranPrcPer = tempsellTranPrcPer, fillPrc = tempfillPrc, maxCnAmt = tempMaxCnAmt, floatFormat = tempsellfloatFormat }));
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

        /// <summary>
        /// 실시간 코인 시세에서 구매/판매 클릭시 해당 페이지 호출(View 에서 사용 TradePageCallCommand)
        /// </summary>
        /// <param name="menuId"></param>1
        public void TradePageCall(string menuId)
        {
            try
            {
                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    if (item.CoinName.Equals(menuId))
                    {
                        Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString(item.CoinCode), Id = "CoinTrading", IconPath = "/Images/ico_nav_" + item.CoinName + ".png", Param = item, Certify = 1 });
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ChartViewMaxMin()
        {
            if (OrderListVisible == Visibility.Visible)
            {
                chartColspan = 3;
                chartRowspan = 2;
                OrderListVisible = Visibility.Collapsed;
                FillListVisible = Visibility.Collapsed;
                btn_ex_zoom = "/Images/btn_ex_zoomout.png";
                btn_ex_zoom_on = "/Images/btn_ex_zoomout_on.png";
            }
            else
            {
                chartColspan = 1;
                chartRowspan = 1;
                OrderListVisible = Visibility.Visible;
                FillListVisible = Visibility.Visible;
                btn_ex_zoom = "/Images/btn_ex_zoomin.png";
                btn_ex_zoom_on = "/Images/btn_ex_zoomin_on.png";
            }
        }

        public void RowClick()
        {
            try
            {
                if (MainViewModel.ecd == null || MainViewModel.ecd.IsLoaded == false)
                {
                    MainViewModel.ecd = new Views.Dashboard.ExchangeChartDashboard(StringEnum.GetStringValue(SelectCoinCode), PriceType, PriceCode);
                    //ecd.WindowStyle = WindowStyle.ToolWindow;
                    MainViewModel.ecd.WindowStyle = WindowStyle.SingleBorderWindow;
                    MainViewModel.ecd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    MainViewModel.ecd.Width = 1400;
                    MainViewModel.ecd.Height = 800;
                    MainViewModel.ecd.Title = Localization.Resource.ExchangeChartDashboard_1 + "(" + PriceType + " Market)";
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

        public void ChangeCoin(string sPriceType, string sPriceCode, EnumLib.ExchangeCurrencyCode val)
        {
            try
            {
                EnumLib.ExchangeCurrencyCode BeforeKindOfCoin = SelectCoinCode;

                PriceType = sPriceType;
                PriceCode = sPriceCode;

                if (PriceType.Equals(CommonLib.StandardCurcyNm))
                {
                    CommonFloat = "###,###,###,###,##0";
                    CommonNum = "n0";
                }
                else
                {
                    CommonFloat = "###,###,###,###,##0.########";
                    CommonNum = "n8";
                }

                SelectCoinCode = val;

                //if (DispatcherService != null)
                //{
                //    DispatcherService.BeginInvoke(() =>
                //    {
                //        BuyOrdelData.Clear();
                //        SellOrdelData.Clear();
                //    });
                //}

                if (MainViewModel.mQClient.CoinInfoData2.ToList().Where(w => w.Key.Key == sPriceCode && w.Key.Value == StringEnum.GetStringValue(SelectCoinCode)).Count() > 0)
                {
                    CoinInfoSocketData(MainViewModel.mQClient.CoinInfoData2[new KeyValuePair<string, string>(sPriceCode, StringEnum.GetStringValue(SelectCoinCode))]);
                }
                if (MainViewModel.mQClient.OrderData2.ToList().Where(w => w.Key.Key == sPriceCode && w.Key.Value == StringEnum.GetStringValue(SelectCoinCode)).Count() > 0)
                {
                    OrderSocketData(MainViewModel.mQClient.OrderData2[new KeyValuePair<string, string>(sPriceCode, StringEnum.GetStringValue(SelectCoinCode))]);
                }
                if (MainViewModel.mQClient.FillData2.ToList().Where(w => w.Key.Key == sPriceCode && w.Key.Value == StringEnum.GetStringValue(SelectCoinCode)).Count() > 0)
                {
                    FillSocketData(MainViewModel.mQClient.FillData2[new KeyValuePair<string, string>(sPriceCode, StringEnum.GetStringValue(SelectCoinCode))]);
                }

                //Dictionary<string, EnumLib.ExchangeCurrencyCode> dict = new Dictionary<string, EnumLib.ExchangeCurrencyCode>();
                //dict.Add(this.GetType().BaseType.Name, SelectCoinCode);
                //Messenger.Default.Send(dict);

                Dictionary<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>> dict = new Dictionary<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>>();
                Dictionary<string, EnumLib.ExchangeCurrencyCode> dictTemp = new Dictionary<string, EnumLib.ExchangeCurrencyCode>();

                dictTemp.Add(PriceCode, SelectCoinCode);
                dict.Add(this.GetType().BaseType.Name, dictTemp);
                Messenger.Default.Send(dict);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            btn_main_buysell = sLanguage + "btn_main_buysell.png";
            btn_main_buysell_on = sLanguage + "btn_main_buysell_on.png";
            btn_ex_zoom = "/Images/btn_ex_zoomin.png";
            btn_ex_zoom_on = "/Images/btn_ex_zoomin_on.png";
        }
    }

}
