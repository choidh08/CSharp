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
    public class AutoTradingPopAdditionalServiceViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }

        public virtual List<AutoTradingCoinList> coinList { get; set; }
        public virtual string SelectedCoinCode { get; set; }
        public virtual string PayTitle { get; set; }
        public virtual string PayPrc { get; set; }
        public virtual string btn_popup_pay { get; set; }
        public virtual string btn_popup_pay_on { get; set; }

        public virtual bool IsBusy { get; set; }

        protected AutoTradingPopAdditionalServiceViewModel()
        {
            try
            {
                coinList = new List<AutoTradingCoinList>();
                coinList.Add(ViewModelSource.Create(() => new AutoTradingCoinList() { coinNm = CommonLib.StandardCurcyNm, coinCd = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW), isCheck = true }));
                foreach (ResponseGetMkCoinListListtModel item in MainViewModel.CoinList.KRW)
                {
                    coinList.Add(ViewModelSource.Create(() => new AutoTradingCoinList() { coinNm = item.curcyNm, coinCd = item.curcyCd, isCheck = true }));
                }

                ImageSet();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static AutoTradingPopAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new AutoTradingPopAdditionalServiceViewModel());
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
                foreach(AutoTradingCoinList item in coinList)
                {
                    item.Cancel();
                }
                coinList.Where(x => x.coinCd == CoinCode).FirstOrDefault().Select();

                IsBusy = true;

                using (RequestCoinPayInfoModel req = new RequestCoinPayInfoModel())
                {
                    req.cnKndCd = CoinCode;
                    SelectedCoinCode = CoinCode;
                    using (ResponseCoinPayInfoModel res = await WebApiLib.AsyncCall<ResponseCoinPayInfoModel, RequestCoinPayInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.list.Count > 0)
                            {
                                ResponseCoinPayInfoListModel list = res.data.list[0];
                                if (list.cnkndnm.Equals(CommonLib.StandardCurcyNm))
                                {
                                    PayTitle = Localization.Resource.AutoTradingAdditionalPop_2_1;
                                    PayPrc = decimal.Parse(list.payAmt.ToString()).ToString("#,#"); 
                                }
                                else
                                {
                                    PayTitle = Localization.Resource.AutoTradingAdditionalPop_2_2;
                                    PayPrc = decimal.Parse(list.payAmt.ToString()).ToString("#,0.########");                                    
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
            finally
            {
                IsBusy = false;
            }
        }

        public async void CmdPayment()
        {
            try
            {
                if (!PayPrc.Equals("0"))
                {
                    IsBusy = true;

                    using (RequestAutoServiceReqMdoel req = new RequestAutoServiceReqMdoel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = SelectedCoinCode;

                        using (ResponseAutoServiceReqMdoel res = await WebApiLib.AsyncCall<ResponseAutoServiceReqMdoel, RequestAutoServiceReqMdoel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                Messenger.Default.Send("AutoTradeState");
                                Messenger.Default.Send("AssetsRefresh");
                                WindowService.Close();
                            }
                            else
                            {
                                Alert alert = new Alert(Localization.Resource.AutoTradingAdditionalPop_6, 300);
                                alert.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    Alert alert = new Alert(Localization.Resource.AutoTradingAdditionalPop_5, 350);
                    alert.ShowDialog();
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

            btn_popup_pay = sLanguage + "btn_popup_pay.png";
            btn_popup_pay_on = sLanguage + "btn_popup_pay_on.png";
        }

        public class AutoTradingCoinList
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