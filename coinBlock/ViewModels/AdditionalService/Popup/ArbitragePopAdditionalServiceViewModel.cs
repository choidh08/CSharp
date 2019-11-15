using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Views;
using System.Windows.Threading;
using coinBlock.Model.AdditionalService;
using coinBlock.Utility;
using static coinBlock.Utility.EnumLib;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class ArbitragePopAdditionalServiceViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }

        DispatcherTimer RepeatTimer;

        public virtual string sTradeInfo { get; set; }
        public virtual string sOrderCode { get; set; }
        public virtual string sLowId { get; set; }
        public virtual string sHighId { get; set; }
        public virtual decimal? dNowAmountTemp { get; set; }

        public virtual bool bHereTrade { get; set; } = true;
        public virtual bool bAllTrade { get; set; }
        public virtual bool bTrading { get; set; }
        public virtual bool bTimeOver { get; set; }

        public virtual decimal dNowAmount { get; set; }
        public virtual decimal dCoinPrc { get; set; }
        public virtual string sCnKndCd { get; set; }
        public virtual string sExchgeNm { get; set; }

        public virtual string sQuantityTitle { get; set; } = Localization.Resource.Arbitrage_TradePop_1_2;

        public virtual int iTime { get; set; }
        public virtual string sTimeColor { get; set; } = "Black";
        public virtual string nowTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public virtual string btn_popup_confirm_color { get; set; }
        public virtual string btn_popup_confirm_color_on { get; set; }
        public virtual string btn_popup_cancel { get; set; }
        public virtual string btn_popup_cancel_on { get; set; }

        Alert alert = null;

        protected ArbitragePopAdditionalServiceViewModel()
        {
            try
            {
                ImageSet();

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 0, 1);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static ArbitragePopAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new ArbitragePopAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                iTime = 10;
                bTrading = false;
                RepeatTimer.Start();

                if (sTradeInfo.Equals("B"))
                {
                    sQuantityTitle = Localization.Resource.Arbitrage_TradePop_1_2;
                }
                else if (sTradeInfo.Equals("S"))
                {
                    sQuantityTitle = Localization.Resource.AssetsMyPage_4_2;
                }
                GetData();
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
                sTimeColor = "Black";
                RepeatTimer.Stop();
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
                //nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                iTime -= 1;

                //if(iTime <= 5)
                //{
                //    sTimeColor = "Red";
                //}

                if (iTime < 0)
                {
                    bTimeOver = true;
                    WindowService.Close();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSelect()
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

        public void CmdTrade()
        {
            try
            {
                if (string.IsNullOrEmpty(sOrderCode)) return;

                if (dNowAmount.Equals(0))
                {
                    if (sTradeInfo.Equals("B"))
                    {
                        alert = new Alert(Localization.Resource.Arbitrage_TradePop_Alert_2);
                        alert.ShowDialog();
                    }
                    else if (sTradeInfo.Equals("S"))
                    {
                        alert = new Alert(Localization.Resource.Arbitrage_TradePop_Alert_3);
                        alert.ShowDialog();
                    }
                    return;
                }

                alert = new Alert(Localization.Resource.Arbitrage_TradePop_Alert_1, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    //거래실행 API
                    if (sTradeInfo.Equals("B"))
                    {
                        using (RequestArbitrageBuyTradeCoinModel req = new RequestArbitrageBuyTradeCoinModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.regIp = MainViewModel.LoginDataModel.regIp;
                            if (bHereTrade)
                            {
                                req.type = "One";
                            }
                            else if (bAllTrade)
                            {
                                req.type = "All";
                            }
                            req.cnKndCd = sOrderCode;
                            req.lowId = sLowId;
                            req.highId = sHighId;

                            using (ResponseArbitrageBuyTradeCoinModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageBuyTradeCoinModel, RequestArbitrageBuyTradeCoinModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    if (res.data.failCd.Equals(""))
                                    {
                                        bTrading = true;
                                        alert = new Alert(Localization.Resource.Arbitrage_Alert_2);
                                        alert.ShowDialog();
                                        WindowService.Close();
                                    }
                                    else if (res.data.failCd.Equals("778"))
                                    {
                                        RepeatTimer.Stop();
                                        alert = new Alert(Localization.Resource.Arbitrage_Alert_6, 330);
                                        alert.ShowDialog();
                                        WindowService.Close();
                                    }
                                    //else if (res.data.failCd.Equals("777"))
                                    //{
                                    //    alert = new Alert();
                                    //    alert.ShowDialog();
                                    //    return;
                                    //}
                                }
                            }
                        }
                    }
                    else if (sTradeInfo.Equals("S"))
                    {
                        using (RequestArbitrageSellTradeCoinModel req = new RequestArbitrageSellTradeCoinModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req.regIp = MainViewModel.LoginDataModel.regIp;
                            if (bHereTrade)
                            {
                                req.type = "One";
                            }
                            else if (bAllTrade)
                            {
                                req.type = "All";
                            }
                            req.cnKndCd = sOrderCode;
                            req.lowId = sHighId;
                            req.highId = sLowId;

                            using (ResponseArbitrageSellTradeCoinModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageSellTradeCoinModel, RequestArbitrageSellTradeCoinModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    if (res.data.failCd.Equals(""))
                                    {
                                        bTrading = true;
                                        alert = new Alert(Localization.Resource.Arbitrage_Alert_2);
                                        alert.ShowDialog();
                                        WindowService.Close();
                                    }
                                    else if (res.data.failCd.Equals("778"))
                                    {
                                        RepeatTimer.Stop();
                                        alert = new Alert(Localization.Resource.Arbitrage_Alert_6, 330);
                                        alert.ShowDialog();
                                        WindowService.Close();
                                    }
                                    //else if (res.data.failCd.Equals("777"))
                                    //{
                                    //    alert = new Alert();
                                    //    alert.ShowDialog();
                                    //    return;
                                    //}
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

        public void GetData()
        {
            try
            {
                //정보 호출 API
                if (sTradeInfo.Equals("B"))
                {
                    using (RequestArbitrageGetUserAssetCashModel req = new RequestArbitrageGetUserAssetCashModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.regIp = MainViewModel.LoginDataModel.regIp;
                        if (bHereTrade)
                        {
                            req.isAllYn = "N";
                        }
                        else if (bAllTrade)
                        {
                            req.isAllYn = "Y";
                        }
                        req.targetId = sLowId;
                        req.cnKndNm = StringEnum.ToEnum<ExchangeCurrencyCode>(sOrderCode).ToString();

                        using (ResponseArbitrageGetUserAssetCashModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageGetUserAssetCashModel, RequestArbitrageGetUserAssetCashModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                if (res.data.failCd.Equals(""))
                                {
                                    //if (bHereTrade)
                                    //{
                                    //    dNowAmount = (decimal)dNowAmountTemp;
                                    //}
                                    //else if (bAllTrade)
                                    //{
                                    dNowAmount = res.data.data;
                                    //}
                                    dCoinPrc = res.data.cnPtcPrc;
                                    sExchgeNm = res.data.exchgeNm;
                                }
                            }
                        }
                    }
                }
                else if (sTradeInfo.Equals("S"))
                {
                    using (RequestArbitrageGetUserAssetCoinModel req = new RequestArbitrageGetUserAssetCoinModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.regIp = MainViewModel.LoginDataModel.regIp;
                        if (bHereTrade)
                        {
                            req.isAllYn = "N";
                        }
                        else if (bAllTrade)
                        {
                            req.isAllYn = "Y";
                        }
                        req.targetId = sLowId;
                        req.cnKndNm = StringEnum.ToEnum<ExchangeCurrencyCode>(sOrderCode).ToString();

                        using (ResponseArbitrageGetUserAssetCoinModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageGetUserAssetCoinModel, RequestArbitrageGetUserAssetCoinModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                if (res.data.failCd.Equals(""))
                                {
                                    //if (bHereTrade)
                                    //{
                                    //    dNowAmount = (decimal)dNowAmountTemp;
                                    //}
                                    //else if (bAllTrade)
                                    //{
                                    dNowAmount = res.data.data;
                                    //}
                                    dCoinPrc = res.data.cnPtcPrc;
                                    sCnKndCd = res.data.cnKndCd;
                                    sExchgeNm = res.data.exchgeNm;
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

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_popup_confirm_color = sLanguage + "btn_popup_confirm_color.png";
            btn_popup_confirm_color_on = sLanguage + "btn_popup_confirm_color_on.png";
            btn_popup_cancel = sLanguage + "btn_popup_cancel.png";
            btn_popup_cancel_on = sLanguage + "btn_popup_cancel_on.png";
        }
    }
}