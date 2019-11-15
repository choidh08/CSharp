using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Views;
using coinBlock.Views.AdditionalService.Popup;
using coinBlock.Utility;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using coinBlock.Model.AdditionalService;
using System.Linq;
using coinBlock.ViewModels.AdditionalService.Popup;
using System.Windows;
using System.Windows.Threading;
using coinBlock.Model;
using System.Collections.Generic;

namespace coinBlock.ViewModels.AdditionalService
{
    [POCOViewModel]
    public class ArbitrageAdditionalServiceViewModel
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        private string _OrderCode = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC);
        DispatcherTimer RepeatTimer;
        DispatcherTimer RepeatTimer2;

        public virtual bool bPush { get; set; } = false;
        public virtual bool bCheck { get; set; }
        public virtual bool bCheckEnable { get; set; } = false;
        public virtual bool bTotalSignCheck { get; set; } = false;
        public virtual bool bBuy { get; set; } = true;
        public virtual bool bSell { get; set; }
        public virtual Visibility vBuyVisible { get; set; } = Visibility.Visible;
        public virtual Visibility vSellVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility vImageVisible { get; set; } = Visibility.Visible;

        public virtual bool IsBusy { get; set; }
        public virtual bool IsLoad { get; set; }
        public virtual bool IsLoadData { get; set; } = true;
        public virtual bool bTrading { get; set; } = true;

        public virtual string nowTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public virtual string btn_more { get; set; }
        public virtual string btn_more_on { get; set; }
        public virtual string btn_view_terms { get; set; }
        public virtual string btn_view_terms_on { get; set; }
        public virtual string btn_auto_ex_trade { get; set; }
        public virtual string btn_auto_ex_trade_on { get; set; }

        public virtual string upImage { get; set; } = "/Images/ico_up_arr_red.png";
        public virtual decimal? dPrcGap { get; set; }

        public virtual string sQuantityTitle { get; set; } = string.Format(Localization.Resource.Arbitrage_3_3, CommonLib.StandardCurcyNm);

        public virtual bool listEnable { get; set; } = true;
        int listSize { get; set; } = 10;

        public virtual decimal dGap { get; set; }
        public virtual int chaHeight { get; set; } = 60;
        public virtual int arbiListHeight { get; set; }
        public virtual ObservableCollection<ResponseArbitrageGetAssetListModel> arbiDataList { get; set; }
        public virtual ObservableCollection<ResponseArbitrageGetSignInfoListModel> arbiExchangeList { get; set; }
        public virtual ObservableCollection<ResponseArbitrageTradeHistoryListModel> tradeList { get; set; }

        IniFileLib ini = new IniFileLib();

        protected ArbitrageAdditionalServiceViewModel()
        {
            try
            {
                ImageSet();

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick; ;
                RepeatTimer.Interval = new TimeSpan(0, 0, 5);


                RepeatTimer2 = new DispatcherTimer();
                RepeatTimer2.Tick += RepeatTimer2_Tick;
                RepeatTimer2.Interval = new TimeSpan(0, 0, 10);

                Messenger.Default.Register<string>(this, GetArbitrageTradeListCall);
                Messenger.Default.Register<Dictionary<string, string>>(this, GetAribtrageTradeList2Call);

                using (RequestArbitrageTradeTermsCheckModel req = new RequestArbitrageTradeTermsCheckModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseArbitrageTradeTermsCheckModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageTradeTermsCheckModel, RequestArbitrageTradeTermsCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd.Equals(""))
                            {
                                bCheck = true;
                                bCheckEnable = false;
                            }
                            else
                            {
                                bCheck = false;
                                bCheckEnable = false;
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

        public static ArbitrageAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new ArbitrageAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                IsLoad = true;
                listSize = 10;
                RepeatTimer.Start();

                //string sCheckValue = ini.GetCheckID("ViewTermAgree", "Check");
                //if (!sCheckValue.Equals(string.Empty))
                //{
                //    bCheck = true;
                //    bCheckEnable = true;
                //}
                //else
                //{
                //    bCheck = false;
                //    bCheckEnable = false;
                //}

                using (RequestArbitrageGetSignInfoModel req = new RequestArbitrageGetSignInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseArbitrageGetSignInfoModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageGetSignInfoModel, RequestArbitrageGetSignInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd.Equals(""))
                            {
                                arbiExchangeList = new ObservableCollection<ResponseArbitrageGetSignInfoListModel>();
                                arbiExchangeList = res.data.list;

                                if (res.data.list.Where(x => x.joinYn == "N").Count() + res.data.list.Where(x => x.joinYn == "F").Count() > 0)
                                {
                                    Task.Run(() =>
                                    {
                                        DispatcherService.BeginInvoke(() =>
                                        {
                                            ArbitrageSignUpPopAdditionalService pop = new ArbitrageSignUpPopAdditionalService();
                                            ((ArbitrageSignUpPopAdditionalServiceViewModel)pop.DataContext).signUpList = res.data.list;
                                            pop.ShowDialog();
                                            if (((ArbitrageSignUpPopAdditionalServiceViewModel)pop.DataContext).bDialogResult == true)
                                            {
                                                GetArbitrageData(_OrderCode);
                                                bTotalSignCheck = true;
                                            }
                                            else
                                            {
                                                bTotalSignCheck = false;
                                            }
                                        });
                                    });
                                }
                                else
                                {
                                    GetArbitrageData(_OrderCode);
                                    bTotalSignCheck = true;
                                }
                            }
                            else if (res.data.failCd.Equals("998"))
                            {
                                Alert alert = new Alert(Localization.Resource.Arbitrage_SignUpPop_Alert_1, 300, 160);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                }

                GetArbitrageTradeList();
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

                //bCheck = false;
                //bCheckEnable = false;

                RepeatTimer.Stop();
                RepeatTimer2.Stop();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_more = sLanguage + "btn_more.png";
            btn_more_on = sLanguage + "btn_more_on.png";
            btn_auto_ex_trade = sLanguage + "btn_auto_ex_trade.png";
            btn_auto_ex_trade_on = sLanguage + "btn_auto_ex_trade_on.png";
            btn_view_terms = sLanguage + "btn_view_terms.png";
            btn_view_terms_on = sLanguage + "btn_view_terms_on.png";
        }

        #region Command

        public void CmdTabChanged(DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e)
        {
            try
            {
                string OrderCode = ((DevExpress.Xpf.Core.DXTabItem)e.NewSelectedItem).Tag.ToString();
                nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //bCheck = false;
                //bCheckEnable = false;
                GetArbitrageData(OrderCode);
                GetArbitrageTradeList();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSelectBuySell()
        {
            try
            {
                if (bBuy.Equals(true))
                {
                    vBuyVisible = Visibility.Visible;
                    vSellVisible = Visibility.Collapsed;
                    sQuantityTitle = string.Format(Localization.Resource.Arbitrage_3_3, CommonLib.StandardCurcyNm);
                }
                else if (bSell.Equals(true))
                {
                    vBuyVisible = Visibility.Collapsed;
                    vSellVisible = Visibility.Visible;
                    sQuantityTitle = Localization.Resource.AssetsMyPage_4_2;
                }

                GetArbitrageData(_OrderCode);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdRefreshData(bool bRefresh = true)
        {
            try
            {
                nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                GetArbitrageData(_OrderCode, bRefresh);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSetMainTradeExchange()
        {
            try
            {
                if (arbiExchangeList != null)
                {
                    //if (arbiExchangeList.Where(x => x.mainYn == "Y").Count() == 0)
                    //{
                    ArbitrageSetMainExchangePopAdditionalService pop = new ArbitrageSetMainExchangePopAdditionalService();
                    ((ArbitrageSetMainExchangePopAdditionalServiceViewModel)pop.DataContext).exchangeList = arbiExchangeList;
                    pop.ShowDialog();

                    arbiExchangeList = ((ArbitrageSetMainExchangePopAdditionalServiceViewModel)pop.DataContext).exchangeList;
                    //}
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdTrade()
        {
            try
            {
                if (!bTotalSignCheck)
                {
                    Alert alert = new Alert(Localization.Resource.Arbitrage_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                if (!bCheck)
                {
                    Alert alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_7, 350);
                    alert.ShowDialog();
                    return;
                }
                else
                {
                    string sTradeInfo = string.Empty;

                    if (arbiDataList.Count > 1)
                    {
                        ArbitragePopAdditionalService pop = new ArbitragePopAdditionalService();
                        if (bBuy)
                        {
                            sTradeInfo = "B";
                            ResponseMainAssetListModel item = MainViewModel.Asset.Where(x => x.curcyCd == StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)).FirstOrDefault();
                            ((ArbitragePopAdditionalServiceViewModel)pop.DataContext).dNowAmountTemp = item.posCnPrc;
                        }
                        else if (bSell)
                        {
                            sTradeInfo = "S";
                            ResponseMainAssetListModel item = MainViewModel.Asset.Where(x => x.curcyCd == _OrderCode).FirstOrDefault();
                            ((ArbitragePopAdditionalServiceViewModel)pop.DataContext).dNowAmountTemp = item.posCnAmt;
                        }
                        ((ArbitragePopAdditionalServiceViewModel)pop.DataContext).sTradeInfo = sTradeInfo;
                        ((ArbitragePopAdditionalServiceViewModel)pop.DataContext).sOrderCode = _OrderCode;
                        ((ArbitragePopAdditionalServiceViewModel)pop.DataContext).sLowId = arbiDataList[1].exchgeId;
                        ((ArbitragePopAdditionalServiceViewModel)pop.DataContext).sHighId = arbiDataList[0].exchgeId;
                        pop.ShowDialog();

                        if (((ArbitragePopAdditionalServiceViewModel)pop.DataContext).bTrading == true)
                        {
                            bTrading = false;
                            if (bPush)
                            {
                                bTrading = true;
                                bPush = false;
                            }
                            bCheckEnable = false;
                        }
                        else if (((ArbitragePopAdditionalServiceViewModel)pop.DataContext).bTimeOver == true)
                        {
                            bTrading = true;
                            bCheckEnable = false;
                        }
                        else
                        {
                            bTrading = true;
                            bCheckEnable = false;
                        }

                        CmdRefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdViewTerms()
        {
            try
            {
                ArbitrageViewTermAdditionalService pop = new ArbitrageViewTermAdditionalService();
                pop.ShowDialog();

                if (bCheck)
                {
                    bCheckEnable = false;
                }
                else
                {
                    bCheckEnable = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdMoreRequestList()
        {
            try
            {
                //리스트호출
                if (listEnable)
                {
                    listEnable = false;

                    listSize += 10;
                    GetArbitrageTradeList();

                    RepeatTimer2.Start();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        //public void OnbCheckChanged()
        //{
        //    try
        //    {
        //        if (bCheck)
        //        {
        //            ini.SetCheckID(bCheck.Equals(true) ? "T" : "F", "ViewTermAgree", "Check");
        //        }
        //        else
        //        {
        //            ini.SetCheckID(string.Empty, "ViewTermAgree", "Check");
        //        } 
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}

        #endregion

        #region Method

        public void GetArbitrageData(string OrderCode, bool bRefresh = true)
        {
            try
            {
                if (bRefresh) IsBusy = true;

                if (string.IsNullOrEmpty(OrderCode)) return;

                _OrderCode = OrderCode;
                arbiDataList = new ObservableCollection<ResponseArbitrageGetAssetListModel>();
                if (IsLoadData)
                {
                    IsLoadData = false;

                    //아비트리지 코인별 현재 데이터 호출
                    using (RequestArbitrageGetAssetModel req = new RequestArbitrageGetAssetModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndNm = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(_OrderCode).ToString();
                        if (bBuy)
                        {
                            req.type = "B";
                        }
                        else if (bSell)
                        {
                            req.type = "S";
                        }

                        using (ResponseArbitrageGetAssetModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageGetAssetModel, RequestArbitrageGetAssetModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                dGap = res.data.gap ?? 0;

                                foreach (ResponseArbitrageGetAssetListModel item in res.data.list)
                                {
                                    item.sFavicon = "/Images/" + item.exchgeNm + ".ico";
                                    item.sCoinImage = "/Images/ico_nav_" + item.cnKndCd + ".png";

                                    if (bSell)
                                    {
                                        if (item.totAmt == 0)
                                        {
                                            item.sTotAmt = item.totAmt.ToString() + " (X)";
                                        }
                                        else
                                        {
                                            item.sTotAmt = decimal.Parse(item.totAmt.ToString()).ToString("###,###,##0.########") + " (O)";
                                        }

                                        if (res.data.list.Count > 1)
                                        {
                                            res.data.list[1].sImage = "/Images/ico_up_arr_red.png";
                                            dPrcGap = res.data.list[1].prcGap;
                                            vImageVisible = Visibility.Visible;
                                        }
                                        else if (res.data.list.Count.Equals(1))
                                        {
                                            vImageVisible = Visibility.Collapsed;
                                        }
                                    }
                                    else if (bBuy)
                                    {
                                        if (item.hasPtc == 0)
                                        {
                                            item.sHasPtc = item.hasPtc.ToString() + " (X)";
                                        }
                                        else
                                        {
                                            item.sHasPtc = decimal.Parse(item.hasPtc.ToString()).ToString("###,###,###,##0") + " (O)";
                                        }

                                        if (res.data.list.Count > 1)
                                        {
                                            res.data.list[1].sImage = "/Images/ico_down_arr_blue.png";
                                            dPrcGap = res.data.list[1].prcGap;
                                            vImageVisible = Visibility.Visible;
                                        }
                                        else if (res.data.list.Count.Equals(1))
                                        {
                                            vImageVisible = Visibility.Collapsed;
                                        }
                                    }
                                }

                                arbiDataList = res.data.list;
                            }
                        }
                    }

                    arbiListHeight = 30 + (arbiDataList.Count * 30);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
                IsLoadData = true;
            }
        }

        public void GetArbitrageTradeList()
        {
            try
            {
                //아비트리지 거래내역 API 호출
                using (RequestArbitrageTradeHistoryModel req = new RequestArbitrageTradeHistoryModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.cnKndNm = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(_OrderCode).ToString();
                    req.listSize = listSize;

                    using (ResponseArbitrageTradeHistoryModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageTradeHistoryModel, RequestArbitrageTradeHistoryModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            tradeList = new ObservableCollection<ResponseArbitrageTradeHistoryListModel>();
                            tradeList = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetArbitrageTradeListCall(string message)
        {
            try
            {
                if (IsLoad)
                {
                    if (message.Equals("ArbitrageListUpdate"))
                    {
                        if (bTrading.Equals(false))
                        {
                            bPush = false;
                        }
                        else
                        {
                            bPush = true;
                        }
                        bTrading = true;
                        GetArbitrageTradeList();
                        Messenger.Default.Send("AssetsRefresh");
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetAribtrageTradeList2Call(Dictionary<string, string> dic)
        {
            try
            {
                if (IsLoad)
                {
                    if (dic.Values.ElementAt(0).Equals("ArbitrageListUpdate"))
                    {
                        if (bTrading.Equals(false))
                        {
                            bPush = false;
                        }
                        else
                        {
                            bPush = true;
                        }
                        bTrading = true;
                        GetArbitrageTradeList();
                        Messenger.Default.Send("AssetsRefresh");

                        if (dic.Keys.ElementAt(0).ToString().Equals("ABTRG_01"))
                        {
                            Alert alert = new Alert(Localization.Resource.Arbitrage_Alert_3, 280, 160);
                            alert.ShowDialog();
                        }
                        else if (dic.Keys.ElementAt(0).ToString().Equals("ABTRG_02"))
                        {
                            Alert alert = new Alert(Localization.Resource.Arbitrage_Alert_4, 325, 160);
                            alert.ShowDialog();
                        }
                        else if (dic.Keys.ElementAt(0).ToString().Equals("ABTRG_03"))
                        {
                            Alert alert = new Alert(Localization.Resource.Arbitrage_Alert_5, 315, 200);
                            alert.ShowDialog();
                        }
                    }
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
                CmdRefreshData(false);
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
                listEnable = true;
                RepeatTimer2.Stop();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                listEnable = true;
            }
        }

        #endregion
    }
}