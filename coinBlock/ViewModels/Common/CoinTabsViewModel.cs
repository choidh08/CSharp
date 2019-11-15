using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using coinBlock.Utility;
using coinBlock.ViewModels.Dashboard;
using System.Linq;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.Dashboard;
using System.Windows;
using coinBlock.ViewModels.Trading;
using System.Collections.Generic;
using System.Threading.Tasks;
using coinBlock.Views;

namespace coinBlock.ViewModels.Common
{
    [POCOViewModel]
    public class CoinTabsViewModel : ISupportParentViewModel
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;
        public virtual ObservableCollection<TimeItem> TimeList { get; set; }
        public virtual ObservableCollection<TimeItem> ChartTimeList { get; set; }
        public virtual TimeItem SelectTime { get; set; }
        public virtual ResponseExchangeDashboardCoinListModel SelectItem { get; set; }
        public virtual ObservableCollection<ResponseExchangeDashboardCoinListModel> CoinPriceData { get; set; }

        public EnumLib.ExchangeCurrencyCode SelectCoinCode { get; set; }
        public virtual object ParentViewModel { get; set; }

        public virtual string img_main_coin { get; set; }
        public virtual decimal txt_Quotes { get; set; }
        public virtual decimal txt_Rate { get; set; }
        public virtual decimal txt_Volume { get; set; }

        public virtual string tempCoin { get; set; } = string.Empty;

        public virtual string tempCoinCd { get; set; }
        public virtual string tempPriceType { get; set; }
        public virtual string ParentModel { get; set; }
        public virtual Visibility HeaderVisible { get; set; }
        public virtual int HeaderHeight { get; set; } = 65;
        public virtual bool AmtVisible { get; set; } = true;
        public virtual string HeaderName { get; set; }
        public virtual string HeaderCode { get; set; }
        public virtual string CommonFloat { get; set; } = "n0";
        bool IsLoad = false;

        public static CoinTabsViewModel Create()
        {
            return ViewModelSource.Create(() => new CoinTabsViewModel());
        }

        protected CoinTabsViewModel()
        {
            try
            {
                Messenger.Default.Register<ResponseExchangeDashboardCoinDataModel>(this, CoinSocketData);

                TimeList = new ObservableCollection<TimeItem>();
                TimeList.Add(new TimeItem() { Name = "24" + Localization.Resource.ExchangeDashoard_Gird_1_8, Value = "24" });
                TimeList.Add(new TimeItem() { Name = "12" + Localization.Resource.ExchangeDashoard_Gird_1_8, Value = "12" });
                TimeList.Add(new TimeItem() { Name = "1" + Localization.Resource.ExchangeDashoard_Gird_1_8, Value = "01" });
                SelectTime = TimeList[0];

                HeaderName = CommonLib.StandardCurcyNm;
                HeaderCode = CommonLib.StandardCurcyCd;

                if (MainViewModel.mQClient.CoinPriceData2.ToList().Where(w => w.Key.Key == HeaderCode && w.Key.Value == SelectTime.Value).Count() > 0)
                {
                    CoinSocketData(MainViewModel.mQClient.CoinPriceData2[new KeyValuePair<string, string>(HeaderCode, SelectTime.Value)]);
                }

                if (CoinPriceData != null)
                {
                    CmdSelectCoin(CoinPriceData[0]);
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

        protected void OnParentViewModelChanged()
        {
            try
            {
                ParentModel = ParentViewModel.ToString().Split('_')[0];

                if (ParentModel.Equals("CoinTradingViewModel"))
                {
                    HeaderVisible = Visibility.Collapsed;
                }
                else if (ParentModel.Equals("TradeHistoryTradingViewModel"))
                {
                    HeaderVisible = Visibility.Collapsed;
                }
                else if (ParentModel.Equals("ExchangeDashboardViewModel"))
                {
                    HeaderVisible = Visibility.Visible;
                }
                else
                {
                    HeaderVisible = Visibility.Collapsed;
                }

                if (HeaderVisible == Visibility.Collapsed)
                {
                    HeaderHeight = 0;
                    AmtVisible = false;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnSelectTimeChanged()
        {
            try
            {
                if (MainViewModel.mQClient.CoinPriceData2.ToList().Where(w => w.Key.Key == HeaderCode && w.Key.Value == SelectTime.Value).Count() > 0)
                {
                    CoinSocketData(MainViewModel.mQClient.CoinPriceData2[new KeyValuePair<string, string>(HeaderCode, SelectTime.Value)]);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CoinSocketData(ResponseExchangeDashboardCoinDataModel item)
        {
            try
            {
                if (!IsLoad)
                {
                    return;
                }

                if (item.marketCode == HeaderCode && int.Parse(SelectTime.Value) == int.Parse(item.Time))
                {
                    DispatcherService.BeginInvoke(() =>
                    {
                        ObservableCollection<ResponseExchangeDashboardCoinListModel> deleteItemList = new ObservableCollection<ResponseExchangeDashboardCoinListModel>();

                        for (int i = 0; i < item.list.Count; i++)
                        {
                            if (item.list[i].chgPrc > 0)
                            {
                                item.list[i].Image = "/Images/ico_up_arr_red.png";
                            }
                            else if (item.list[i].chgPrc == 0)
                            {
                                item.list[i].Image = "/Images/ico_stop_change.png";
                            }
                            else if (item.list[i].chgPrc < 0)
                            {
                                item.list[i].Image = "/Images/ico_down_arr_blue.png";
                            }

                            if (SelectCoinCode == EnumLib.ExchangeCurrencyCode.KRW)
                            {
                                CmdSelectCoin(item.list[0]);
                            }
                            else
                            {
                                if ((tempCoin != StringEnum.GetStringValue(SelectCoinCode)) && (item.list[i].curcyCd == StringEnum.GetStringValue(SelectCoinCode)))
                                {
                                    tempCoin = StringEnum.GetStringValue(SelectCoinCode);
                                    if (SelectItem == null)
                                    {
                                        CmdSelectCoin(item.list[i]);
                                    }
                                    SelectItem = item.list[i];
                                }
                                else if (item.list[i].curcyCd == StringEnum.GetStringValue(SelectCoinCode))
                                {
                                    txt_Quotes = item.list[i].coinPrc;
                                    txt_Rate = item.list[i].volume;
                                    txt_Volume = item.list[i].svcTranAmt;
                                }
                            }

                            if (item.list[i].curcyCd == StringEnum.GetStringValue(SelectCoinCode))
                            {
                                item.list[i].bold_Gb = FontWeights.Bold;
                            }
                            else
                            {
                                item.list[i].bold_Gb = FontWeights.Normal;
                            }
                            item.list[i].coinImage = "/Images/ico_nav_" + item.list[i].coinNm + ".png";
                            //item.list[i].ieoImage = "/Images/btn_ieo_s.png";
                            item.list[i].coinNmView = item.list[i].coinNm + "/" + HeaderName;

                            //if (item.list[i].coinNm.Equals("ADM"))
                            //{
                            //    item.list[i].ieoVisible = Visibility.Visible;
                            //}

                            if (item.list[i].coinNm == HeaderName)
                            {
                                ResponseExchangeDashboardCoinListModel temp = item.list.Where(x => x.coinNm == HeaderName).First();
                                if (temp != null)
                                {
                                    deleteItemList.Add(item.list[i]);
                                }
                            }

                            if (item.list[i].coinNm.Equals("USDT"))
                            {
                                deleteItemList.Add(item.list[i]);
                            }
                        }

                        foreach (var dItem in deleteItemList)
                        {
                            item.list.Remove(dItem);
                        }

                        CoinPriceData = item.list;
                    });
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
                string temp = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(((DevExpress.Xpf.Core.DXTabItem)e.NewSelectedItem).Tag.ToString()).ToString();

                HeaderName = temp.Equals("KRW") ? CommonLib.StandardCurcyNm : temp; 
                HeaderCode = ((DevExpress.Xpf.Core.DXTabItem)e.NewSelectedItem).Tag.ToString();

                if (MainViewModel.mQClient.CoinPriceData2.ToList().Where(w => w.Key.Key == HeaderCode && w.Key.Value == SelectTime.Value).Count() > 0)
                {
                    CoinSocketData(MainViewModel.mQClient.CoinPriceData2[new KeyValuePair<string, string>(HeaderCode, SelectTime.Value)]);
                }

                if (HeaderCode.Equals(CommonLib.StandardCurcyCd))
                {
                    CommonFloat = "n0";
                }
                else
                {
                    CommonFloat = "n8";
                }

                CmdSelectCoin(SelectItem);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSelectCoin(ResponseExchangeDashboardCoinListModel list)
        {
            try
            {
                DispatcherService.BeginInvoke(() =>
                {
                    if (list == null)
                    {
                        return;
                    }

                    if (ParentModel == null)
                    {
                        return;
                    }

                    //if (list.coinNm.Equals("VSTC"))
                    //{
                    //    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    //    alert.ShowDialog();
                    //    return;
                    //}

                    if (tempCoinCd != list.curcyCd || HeaderName != tempPriceType)
                    {
                        tempCoinCd = list.curcyCd;
                        tempPriceType = HeaderName;

                        if (HeaderName == list.coinNm || CoinPriceData.Any(x => x.coinNm == list.coinNm) == false)
                        {
                            SelectItem = CoinPriceData[0];
                        }
                        else
                        {
                            SelectItem = list;
                        }

                        if (HeaderVisible == Visibility.Visible)
                        {
                            img_main_coin = "/Images/img_ex_" + SelectItem.coinNm + ".png";
                            txt_Quotes = SelectItem.coinPrc;
                            txt_Rate = SelectItem.volume;
                            txt_Volume = SelectItem.svcTranAmt;
                        }

                        SelectCoinCode = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(SelectItem.curcyCd);

                        if (ParentModel.Equals("ExchangeDashboardViewModel"))
                        {
                            ((ExchangeDashboardViewModel)ParentViewModel).ChangeCoin(HeaderName, HeaderCode, StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(SelectItem.curcyCd));
                        }
                        else if (ParentModel.Equals("CoinTradingViewModel"))
                        {
                            ((CoinTradingViewModel)ParentViewModel).SetCoin(HeaderName, HeaderCode, SelectItem.curcyCd);
                        }
                        else if (ParentModel.Equals("TradeHistoryTradingViewModel"))
                        {
                            ((TradeHistoryTradingViewModel)ParentViewModel).SetCoinTradeHistory(HeaderName, SelectCoinCode);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class TimeItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}