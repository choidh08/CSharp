using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.DepositWithdraw;
using coinBlock.Utility;
using coinBlock.Model;
using coinBlock.Model.MyPage;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Linq;

namespace coinBlock.ViewModels.MyPage
{
    [POCOViewModel]
    public class AssetsMyPageViewModel
    {
        DispatcherTimer RepeatTimer;

        public virtual string SC { get; } = CommonLib.StandardCurcyNm;

        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }

        public virtual DateTime fromDate { get; set; } = DateTime.Now.AddDays(-7);
        public virtual DateTime toDate { get; set; } = DateTime.Now;

        //전자지갑주소      
        public virtual string NonAddress { get; set; } = Localization.Resource.Common_Alert_11;

        //자산현황
        public virtual ResponseMainAssetListModel UsdAsset { get; set; }
        public virtual decimal coinTotAsset { get; set; }
        public virtual decimal TotAsset { get; set; }

        public virtual ObservableCollection<ResponseCoinAddressTableListModel> CoinAddressValue { get; set; }
        public virtual int CoinAddressHeight { get; set; } = 0;

        public virtual ObservableCollection<ResponseMainAssetListModel> AssetValue { get; set; }
        public virtual int AssetHeight { get; set; } = 0;

        public virtual ObservableCollection<ResponseGetPlusValueListModel> PlusValue { get; set; }
        public virtual decimal? TotCshprc { get; set; } = 0;
        public virtual decimal? TotEvalprc { get; set; } = 0;
        public virtual decimal? TotEvalpnlprc { get; set; } = 0;
        public virtual int PlusHeight { get; set; } = 0;

        public virtual ObservableCollection<ResponseGetTimePlusValueListModel> TimePlusValue { get; set; }
        public virtual decimal? TotBuyTotPrc { get; set; } = 0;
        public virtual decimal? TotSeltotprc { get; set; } = 0;
        public virtual decimal? TotPnlprc { get; set; } = 0;
        public virtual int TimeHeight { get; set; } = 0;

        public virtual bool IsBusy { get; set; }

        bool IsLoad = false;

        protected AssetsMyPageViewModel()
        {
            RepeatTimer = new DispatcherTimer();
            RepeatTimer.Tick += RepeatTimer_Tick;
            RepeatTimer.Interval = new TimeSpan(0, 5, 0);

            //상단 호출 메신저 등록
            Messenger.Default.Register<string>(this, AssetsCall);
            ImageSet();
        }


        public static AssetsMyPageViewModel Create()
        {
            return ViewModelSource.Create(() => new AssetsMyPageViewModel());
        }

        public void Loaded()
        {
            try
            {                
                IsLoad = true;
                GetCoinAddressYn(); //전자지갑주소 조회
                GetAsset();
                GetPlusValue();
                GetTimePlusValue();

                RepeatTimer.Start();
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
                GetCoinAddressYn(); //전자지갑주소 조회
                GetAsset();
                GetPlusValue();
                GetTimePlusValue();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void AssetsCall(string Message)
        {
            try
            {
                if (IsLoad)
                {
                    if (Message.Equals("AssetsRefresh_View"))
                    {
                        GetAsset();
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetCoinAddressYn()
        {
            try
            {
                using (RequestCoinAddressTableModel req = new RequestCoinAddressTableModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    using (ResponseCoinAddressTableModel res = await WebApiLib.AsyncCall<ResponseCoinAddressTableModel, RequestCoinAddressTableModel>(req))
                    {
                        if (res != null)
                        {
                            CoinAddressHeight = 45;

                            ObservableCollection<ResponseCoinAddressTableListModel> delTemp = new ObservableCollection<ResponseCoinAddressTableListModel>();

                            foreach (var item in res.data.list)
                            {
                                if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)) || item.curcyCd.Equals(EnumLib.ExchangeCurrencyCode.DestinationTag))
                                {
                                    delTemp.Add(item);
                                }

                                if (item.accNo.Equals(string.Empty))
                                {
                                    item.accNo = NonAddress;
                                }
                                else if(!item.destiTag.Equals(string.Empty))
                                {
                                    string sTag = string.Empty;
                                    if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.XRP)))
                                    {
                                        sTag = "DestinationTag : ";
                                    }
                                    else if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.VSTC)) || item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.XEM)))
                                    {
                                        sTag = "Message : ";
                                    }
                                    else if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.XLM)) || item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.EOS)))
                                    {
                                        sTag = "Memo : ";
                                    }

                                    item.accNo = $"{item.accNo + " (" + sTag + item.destiTag + ")"}";
                                }

                                CoinAddressHeight += 30;
                            }

                            if (delTemp != null)
                            {
                                foreach (ResponseCoinAddressTableListModel temp in delTemp)
                                {
                                    res.data.list.Remove(temp);
                                    CoinAddressHeight -= 30;
                                }
                            }

                            CoinAddressValue = res.data.list;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetAsset()
        {
            try
            {
                TotAsset = 0;
                coinTotAsset = 0;

                AssetValue = new ObservableCollection<ResponseMainAssetListModel>();
                AssetHeight = 53;

                string CommonFloat = "n0";

                foreach (ResponseMainAssetListModel item in MainViewModel.Asset)
                {             
                    if(item.trLimtAmt > 0 )
                    {
                        item.trLimtColor = "Red";
                    }
                    else
                    {
                        item.trLimtColor = "Black";
                    }

                    if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                    {
                        item.curcyNm = CommonLib.StandardCurcyNm;

                        CommonFloat = "n" + MainViewModel.CoinData.CashDecimal; 

                        item.posCn = item.posCnPrc.ToString(CommonFloat);
                        item.impCn = item.impCnPrc.ToString(CommonFloat);
                        item.totCn = item.kwdPrc.ToString(CommonFloat);
                        item.trustCn = item.trustPrc.ToString(CommonFloat);
                        item.trLimtCn = item.trLimtAmt.ToString(CommonFloat);
                        UsdAsset = item;
                        TotAsset += UsdAsset.kwdPrc;
                        item.ExchangeVisible = System.Windows.Visibility.Collapsed;
                    }
                    else
                    {
                        item.curcyNm = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(item.curcyCd).ToString();

                        //ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.curcyCd).FirstOrDefault();
                        CommonFloat = "n0";

                        coinTotAsset = item.kwdPrc;
                        TotAsset += item.kwdPrc;
                        item.posCn = item.posCnAmt.ToString("#,0.00000000");
                        item.impCn = item.impCnAmt.ToString("#,0.00000000");
                        item.totCn = item.totCoinAmt.ToString("#,0.00000000");
                        item.trustCn = item.trustAmt.ToString("#,0.00000000");
                        item.trLimtCn = item.trLimtAmt.ToString("#,0.00000000");

                        if (item.posCnPrc > 0)
                        {
                            item.posExchange = "( " + "≈" + item.posCnPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }
                        else
                        {
                            item.posExchange = "( " + item.posCnPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }
                        if (item.impCnPrc > 0)
                        {
                            item.impExchange = "( " + "≈" + item.impCnPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }
                        else
                        {
                            item.impExchange = "( " + item.impCnPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }
                        if (item.kwdPrc > 0)
                        {
                            item.totExchange = "( " + "≈" + item.kwdPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }
                        else
                        {
                            item.totExchange = "( " + item.kwdPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }

                        if (item.trustPrc > 0)
                        {
                            item.trustExchange = "( " + "≈" + item.trustPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }
                        else
                        {
                            item.trustExchange = "( " + item.trustPrc.ToString(CommonFloat) + " " + CommonLib.StandardCurcyNm + ")";
                        }

                        item.ExchangeVisible = System.Windows.Visibility.Visible;
                    }

                    AssetValue.Add(item);
                    AssetHeight += 35;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetPlusValue()
        {
            try
            {
                TotCshprc = 0;
                TotEvalprc = 0;
                TotEvalpnlprc = 0;

                using (RequestGetPlusValueModel req = new RequestGetPlusValueModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetPlusValueModel res = await WebApiLib.AsyncCall<ResponseGetPlusValueModel, RequestGetPlusValueModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            PlusHeight = res.data.list.Count * 30+ 84;

                            foreach(ResponseGetPlusValueListModel item in res.data.list)
                            {
                                TotCshprc += item.cshprc;
                                TotEvalprc += item.evalprc;
                                TotEvalpnlprc += item.evalpnlprc;

                                ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.cnkndcd).FirstOrDefault();
                                item.CommonFloat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                            }

                            PlusValue = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetTimePlusValue()
        {
            try
            {
                IsBusy = true;

                TotBuyTotPrc = 0;
                TotSeltotprc = 0;
                TotPnlprc = 0;

                using (RequestGetTimePlusValueModel req = new RequestGetTimePlusValueModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.stdDate = fromDate.ToString("yyyy-MM-dd");
                    req.endDate = toDate.ToString("yyyy-MM-dd");

                    using (ResponseGetTimePlusValueModel res = await WebApiLib.AsyncCall<ResponseGetTimePlusValueModel, RequestGetTimePlusValueModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {             
                            TimeHeight = res.data.list.Count * 30 + 119;

                            foreach (ResponseGetTimePlusValueListModel item in res.data.list)
                            {
                                TotBuyTotPrc += item.buyTotPrc;
                                TotSeltotprc += item.seltotprc;
                                TotPnlprc += item.pnlprc;

                                ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.cnkndcd).FirstOrDefault();
                                item.CommonFloat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());                               
                            }

                            TimePlusValue = res.data.list;

                            IsBusy = false;
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

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
        }
    }  
}