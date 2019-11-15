using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.AdditionalService;
using coinBlock.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using coinBlock.Views;
using coinBlock.Model.Common;
using System.Collections.Generic;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class ArbitrageTradingPopAdditionalServiceViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }

        public virtual List<ArbitrageTradingCoinList> coinList { get; set; }
        public virtual string SelectedCoinCode { get; set; }
        public virtual string PayTitle { get; set; }
        public virtual string PayPrc { get; set; }
        public virtual string btn_popup_pay { get; set; }
        public virtual string btn_popup_pay_on { get; set; }

        public virtual bool IsBusy { get; set; }

        protected ArbitrageTradingPopAdditionalServiceViewModel()
        {
            try
            {
                coinList = new List<ArbitrageTradingCoinList>();
                coinList.Add(ViewModelSource.Create(() => new ArbitrageTradingCoinList() { coinNm = CommonLib.StandardCurcyNm, coinCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW), isCheck = true }));
                coinList.Add(ViewModelSource.Create(() => new ArbitrageTradingCoinList() { coinNm = EnumLib.ExchangeCurrencyCode.BTC.ToString(), coinCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC), isCheck = true }));
                coinList.Add(ViewModelSource.Create(() => new ArbitrageTradingCoinList() { coinNm = EnumLib.ExchangeCurrencyCode.ETH.ToString(), coinCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.ETH), isCheck = true }));
                coinList.Add(ViewModelSource.Create(() => new ArbitrageTradingCoinList() { coinNm = EnumLib.ExchangeCurrencyCode.BCH.ToString(), coinCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BCH), isCheck = true }));
                coinList.Add(ViewModelSource.Create(() => new ArbitrageTradingCoinList() { coinNm = EnumLib.ExchangeCurrencyCode.XRP.ToString(), coinCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.XRP), isCheck = true }));
                coinList.Add(ViewModelSource.Create(() => new ArbitrageTradingCoinList() { coinNm = EnumLib.ExchangeCurrencyCode.PAN.ToString(), coinCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.PAN), isCheck = true }));
                //foreach (ResponseGetMkCoinListListtModel item in MainViewModel.CoinList.KRW)
                //{
                //    coinList.Add(ViewModelSource.Create(() => new ArbitrageTradingCoinList() { coinNm = item.curcyNm, coinCd = item.curcyCd, isCheck = true }));
                //}

                ImageSet();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static ArbitrageTradingPopAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new ArbitrageTradingPopAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                CmdGetPrc(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW));
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void CmdGetPrc(string CoinCode)
        {
            try
            {
                foreach (ArbitrageTradingCoinList item in coinList)
                {
                    item.Cancel();
                }
                coinList.Where(x => x.coinCd == CoinCode).FirstOrDefault().Select();

                IsBusy = true;

                using (RequestArbitrageCoinPayInfoModel req = new RequestArbitrageCoinPayInfoModel())
                {
                    req.cnKndCd = CoinCode;
                    SelectedCoinCode = CoinCode;
                    using (ResponseArbitrageCoinPayInfoModel res = await WebApiLib.AsyncCallEncrypt<ResponseArbitrageCoinPayInfoModel, RequestArbitrageCoinPayInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            //if (res.data.list.Count > 0)
                            //{
                            //ResponseArbiCoinPayInfoListModel list = res.data.list[0];
                            if (res.data.cnkndnm.Equals(CommonLib.StandardCurcyNm))
                            {
                                PayTitle = Localization.Resource.AutoTradingAdditionalPop_2_1;
                                PayPrc = decimal.Parse(res.data.payAmt.ToString()).ToString("###,##0");
                            }
                            else
                            {
                                PayTitle = Localization.Resource.AutoTradingAdditionalPop_2_2;
                                PayPrc = decimal.Parse(res.data.payAmt.ToString()).ToString("#,0.########");
                            }
                            //}
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

        public async void CmdPayment()
        {
            try
            {
                //if (!PayPrc.Equals("0"))
                //{
                IsBusy = true;

                using (RequestArbitrageServiceReqModel req = new RequestArbitrageServiceReqModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.cnKndCd = SelectedCoinCode;

                    using (ResponseArbitrageServiceReqModel res = await WebApiLib.AsyncCallEncrypt<ResponseArbitrageServiceReqModel, RequestArbitrageServiceReqModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string resultCd = res.data.failCd;
                            if (resultCd.Equals(""))
                            {
                                Messenger.Default.Send("AssetsRefresh");
                                WindowService.Close();
                            }
                            else if (resultCd.Equals("998"))
                            {
                                Alert alert = new Alert(Localization.Resource.ArbitrageRequestPop_Alert_1, 300);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("997"))
                            {
                                Alert alert = new Alert(Localization.Resource.ArbitrageRequestPop_Alert_2, 300);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("786"))
                            {
                                Alert alert = new Alert(Localization.Resource.ArbitrageRequestPop_Alert_3, 300);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("785"))
                            {
                                Alert alert = new Alert(Localization.Resource.ArbitrageRequestPop_Alert_4, 300);
                                alert.ShowDialog();
                            }
                        }
                        else
                        {
                            Alert alert = new Alert(Localization.Resource.AutoTradingAdditionalPop_6, 300);
                            alert.ShowDialog();
                        }
                    }
                }
                //}
                //else
                //{
                //    Alert alert = new Alert(Localization.Resource.AutoTradingAdditionalPop_5, 350);
                //    alert.ShowDialog();
                //}
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

            btn_popup_pay = sLanguage + "btn_popup_pay.png";
            btn_popup_pay_on = sLanguage + "btn_popup_pay_on.png";
        }

        public class ArbitrageTradingCoinList
        {
            public virtual string coinNm { get; set; }
            public virtual string coinCd { get; set; }
            public virtual bool isCheck { get; set; }

            public void Cancel()
            {
                isCheck = false;
            }
            public void Select()
            {
                isCheck = true;
            }
        }
    }
}