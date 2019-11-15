using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.ViewModels.DepositWithdraw;
using System.Collections.ObjectModel;
using coinBlock.Utility;
using System.Windows.Threading;
using coinBlock.Model.AdditionalService;
using coinBlock.Views;
using coinBlock.Views.AdditionalService;
using coinBlock.Views.AdditionalService.Popup;

namespace coinBlock.ViewModels.AdditionalService
{
    [POCOViewModel]
    public class TrustRequestAdditionalServiceViewModel
    {
        DispatcherTimer RepeatTimer;
        DispatcherTimer RepeatTimer2;
        DispatcherTimer RepeatTimer3;

        public virtual ObservableCollection<ComboBoxStrData> curcyList { get; set; }
        public virtual ComboBoxStrData selCurcy { get; set; }
        public virtual ObservableCollection<ComboBoxStrData> periodList { get; set; }
        public virtual ComboBoxStrData selPeriod { get; set; }
        public virtual ObservableCollection<ResponseTrustContentListModel> trustRequestList { get; set; }
        public virtual ObservableCollection<ResponseTrustInterestListListModel> trustInterestRateList { get; set; }

        Alert alert = null;

        public virtual decimal minReqAmt { get; set; } = 0;
        public virtual decimal holdAmt { get; set; } = 0;
        public virtual decimal reqAmt { get; set; } = 0;
        public virtual string strInterestRate { get; set; }
        public virtual decimal InterestRate { get; set; }
        public virtual decimal expectInterest { get; set; } = 0;
        public virtual decimal totalAmt { get; set; } = 0;
        public virtual bool termsCheck { get; set; } = false;
        public virtual bool termsEnable { get; set; } = false;

        bool listEnable { get; set; } = true;
        bool listEnable2 { get; set; } = true;
        int listSize { get; set; } = 10;
        int listSize2 { get; set; } = 10;

        public virtual string fromDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public virtual string toDate { get; set; } = DateTime.Now.AddDays(3*30).AddDays(-1).ToString("yyyy-MM-dd");

        public virtual string btn_auto_request { get; set; }
        public virtual string btn_auto_request_on { get; set; }
        public virtual string btn_cost_all { get; set; }
        public virtual string btn_cost_all_on { get; set; }
        public virtual string btn_more { get; set; }
        public virtual string btn_more_on { get; set; }
        public virtual string btn_view_terms { get; set; }
        public virtual string btn_view_terms_on { get; set; }

        protected TrustRequestAdditionalServiceViewModel()
        {
            try
            {
                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 5, 0);

                RepeatTimer2 = new DispatcherTimer();
                RepeatTimer2.Tick += RepeatTimer2_Tick; ;
                RepeatTimer2.Interval = new TimeSpan(0, 0, 10);

                RepeatTimer3 = new DispatcherTimer();
                RepeatTimer3.Tick += RepeatTimer3_Tick; ;
                RepeatTimer3.Interval = new TimeSpan(0, 0, 10);

                curcyList = new ObservableCollection<ComboBoxStrData>();
                curcyList.Add(new ComboBoxStrData { Name = "BTC", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC) });
                curcyList.Add(new ComboBoxStrData { Name = "ETH", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.ETH) });
                curcyList.Add(new ComboBoxStrData { Name = "BCH", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BCH) });
                curcyList.Add(new ComboBoxStrData { Name = "XRP", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.XRP) });
                curcyList.Add(new ComboBoxStrData { Name = "PAN", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.PAN) });
                curcyList.Add(new ComboBoxStrData { Name = "HEC", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.HEC) });
                curcyList.Add(new ComboBoxStrData { Name = "ADM", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.ADM) });

                if (curcyList.Count > 0)
                {
                    selCurcy = curcyList[0];
                }

                ImageSet();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
      
        public static TrustRequestAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new TrustRequestAdditionalServiceViewModel());
        }
        
        public void Loaded()
        {
            try
            {
                listSize = 10;
                listSize2 = 10;
                GetRequestList();
                GetInterestRateList();
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
                RepeatTimer.Stop();
                ControlClear();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnselCurcyChanged()
        {
            try
            {
                if (selCurcy == null) return;

                ControlClear();
                GetCoinAmt();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnselPeriodChanged()
        {
            try
            {
                if (selPeriod == null) return;

                toDate = DateTime.Now.AddDays(int.Parse(selPeriod.Value2) * 30).AddDays(-1).ToString("yyyy-MM-dd");

                //decimal tempTotalInterest = 0;              
                //string tempInterestRate = string.Format(Localization.Resource.TrustRequest_1_6_1, selPeriod.Value2) + "(" +selPeriod.Value + "%)";
                //string totalInterestRate = string.Empty;

                //foreach (ComboBoxStrData item in periodList)
                //{
                //    if (decimal.Parse(item.Value2) <= decimal.Parse(selPeriod.Value2))
                //    {
                //        tempTotalInterest += decimal.Parse(item.Value);
                //        totalInterestRate += "+" + string.Format(string.Format(Localization.Resource.TrustRequest_1_6_1, item.Value2) + "({0}%)", item.Value);
                //    }
                //}

                //InterestRate = decimal.Parse(selPeriod.Value);
                //strInterestRate = tempInterestRate;

                //if (!string.IsNullOrWhiteSpace(totalInterestRate))
                //{
                //    strInterestRate = tempTotalInterest + "% (" + totalInterestRate.Substring(1) + ")";
                //}

                decimal tempTotalInterest = 0;
                //string tempInterestRate = string.Format(Localization.Resource.TrustRequest_1_6_1, periodList[0].Value2) + "({0}%)";
                string totalInterestRate = string.Empty;

                foreach (ComboBoxStrData item in periodList)
                {
                    if (decimal.Parse(item.Value2) <= decimal.Parse(selPeriod.Value2))
                    {
                        tempTotalInterest += decimal.Parse(item.Value);
                        totalInterestRate += "+" + string.Format(string.Format(Localization.Resource.TrustRequest_1_6_1, item.Value2) + "({0}%)", item.Value);
                    }
                }

                InterestRate = tempTotalInterest;

                if (!string.IsNullOrWhiteSpace(totalInterestRate))
                {
                    strInterestRate = tempTotalInterest + "% (" + totalInterestRate.Substring(1) + ")";
                }

                InterestCalc();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnReqAmtChanged()
        {
            try
            {
                InterestCalc();
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
                GetRequestList();
                GetInterestRateList();
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

        private void RepeatTimer3_Tick(object sender, EventArgs e)
        {
            try
            {
                listEnable2 = true;
                RepeatTimer3.Stop();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                listEnable2 = true;
            }
        }

        public void InterestCalc()
        {
            try
            {
                expectInterest = reqAmt * (InterestRate * (decimal)0.01);
                totalAmt = reqAmt + expectInterest;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void AllCoinSetting()
        {
            try
            {
                if (holdAmt < 0) return;

                reqAmt = Math.Truncate(holdAmt);
                OnReqAmtChanged();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //이용약관 팝업
        public void TermsPopup()
        {
            try
            {
                TrustRequestPopAdditionalService pop = new TrustRequestPopAdditionalService();             
                pop.ShowDialog();

                termsEnable = true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //신청하기
        public void TrustRequest()
        {
            try
            {
                if (selCurcy == null) return;
                if (selPeriod == null) return;

                GetCoinAmt(false);

                if (reqAmt == 0)
                {
                    alert = new Alert(Localization.Resource.TrustRequest_Alert_2);
                    alert.ShowDialog();
                    return;
                }
                else if(reqAmt > holdAmt)
                {
                    alert = new Alert(Localization.Resource.TrustRequest_Alert_3);
                    alert.ShowDialog();
                    return;
                }
                else if(reqAmt < minReqAmt)
                {
                    alert = new Alert(Localization.Resource.TrustRequest_Alert_6);
                    alert.ShowDialog();
                    return;
                }
                else if(!termsCheck)
                {
                    alert = new Alert(Localization.Resource.TrustRequest_Alert_9);
                    alert.ShowDialog();
                    return;
                }

                alert = new Alert(Localization.Resource.TrustRequest_Alert_4, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    using (RequestTrustInsertModel req = new RequestTrustInsertModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = selCurcy.Value;
                        req.reqAmt = reqAmt;
                        req.month = decimal.Parse(selPeriod.Value2);
                        req.rate = InterestRate;
                        req.expInterest = expectInterest;
                        req.reqDt = fromDate.Replace("-","");
                        req.expireDt = toDate.Replace("-", "");
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseTrustInsertModel res = WebApiLib.SyncCall<ResponseTrustInsertModel, RequestTrustInsertModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                alert = new Alert(Localization.Resource.TrustRequest_Alert_7);
                                alert.ShowDialog();

                                listSize = 10;
                                GetRequestList();
                                GetInterestRateList();
                                ControlClear();
                                GetCoinAmt(true);

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
        //해지하기
        public void TrustCancel(ResponseTrustContentListModel selectItem)
        {
            try
            {
                if (string.IsNullOrEmpty(selectItem.trustReqCode)) return;

                alert = new Alert();

                if (selectItem.cmmNm.Equals("HEC") || selectItem.cmmNm.Equals("PAN") || selectItem.cmmNm.Equals("ADM"))
                {
                    alert = new Alert(Localization.Resource.TrustRequest_Alert_10, Alert.ButtonType.YesNo, 300);
                }
                else
                {
                    alert = new Alert(Localization.Resource.TrustRequest_Alert_5, Alert.ButtonType.YesNo);
                }
                
                if (alert.ShowDialog() == true)
                {
                    using (RequestTrustCancelModel req = new RequestTrustCancelModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.trustReqCode = selectItem.trustReqCode;
                        req.cancelDt = DateTime.Now.ToString("yyyyMMdd");

                        using (ResponseTrustCancelModel res = WebApiLib.SyncCall<ResponseTrustCancelModel, RequestTrustCancelModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                alert = new Alert(Localization.Resource.TrustRequest_Alert_8);
                                alert.ShowDialog();

                                GetRequestList();
                                GetInterestRateList();
                                GetCoinAmt(false);

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

        public void ControlClear()
        {
            try
            {
                minReqAmt = 0;
                holdAmt = 0;
                reqAmt = 0;
                strInterestRate = string.Empty;
                expectInterest = 0;
                totalAmt = 0;
                termsCheck = false;
                termsEnable = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetCoinAmt(bool bPeriodCheck = true)
        {
            try
            {
                using (RequestTrustInfoModel req = new RequestTrustInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.cnKndCd = selCurcy.Value;

                    using (ResponseTrustInfoModel res = WebApiLib.SyncCall<ResponseTrustInfoModel, RequestTrustInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            minReqAmt = res.data.coinMinAmt ?? 0;
                            holdAmt = decimal.Parse(res.data.amt.ToString());

                            if (bPeriodCheck)
                            {
                                periodList = new ObservableCollection<ComboBoxStrData>();
                                foreach (ResponseTrustInfoListModel item in res.data.list)
                                {
                                    periodList.Add(new ComboBoxStrData { Name = string.Format(Localization.Resource.TrustRequest_1_6_1, item.month.ToString()), Value = item.rate.ToString(), Value2 = item.month.ToString() });
                                }

                                if (periodList.Count > 0)
                                {
                                    selPeriod = periodList[0];
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

        public async void GetRequestList()
        {
            try
            {
                using (RequestTrustContentModel req = new RequestTrustContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.listSize = listSize;

                    using (ResponseTrustContentModel res = await WebApiLib.AsyncCall<ResponseTrustContentModel, RequestTrustContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            foreach(ResponseTrustContentListModel item in res.data.list)
                            {
                                if (item.trustStatus.Equals("CMMC00000001346"))
                                {
                                    if ((DateTime.Parse("2018-11-26") <= DateTime.Parse(decimal.Parse(item.reqDt).ToString("####-##-##"))) && (DateTime.Parse(decimal.Parse(item.reqDt).ToString("####-##-##")) < DateTime.Parse("2018-12-03")))
                                    {
                                        item.cancelVisible = System.Windows.Visibility.Collapsed;
                                    }
                                    else
                                    {
                                        item.cancelVisible = System.Windows.Visibility.Visible;
                                    }
                                }
                            }

                            trustRequestList = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetInterestRateList()
        {
            try
            {
                using (RequestTrustInterestListModel req = new RequestTrustInterestListModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.listSize = listSize2;

                    using (ResponseTrustInterestListModel res = await WebApiLib.AsyncCall<ResponseTrustInterestListModel, RequestTrustInterestListModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            foreach (ResponseTrustInterestListListModel item in res.data.list)
                            {
                                item.list_gb = Localization.Resource.TrustRequest_3_1;
                            }

                            trustInterestRateList = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void MoreRequestList()
        {
            try
            {
                if (listEnable)
                {
                    listEnable = false;

                    listSize += 10;
                    GetRequestList();

                    RepeatTimer2.Start();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void MoreInterestList()
        {
            try
            {
                if (listEnable2)
                {
                    listEnable2 = false;

                    listSize2 += 10;
                    GetInterestRateList();

                    RepeatTimer3.Start();
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

            btn_auto_request = sLanguage + "btn_auto_request.png";
            btn_auto_request_on = sLanguage + "btn_auto_request_on.png";
            btn_cost_all = sLanguage + "btn_cost_all.png";
            btn_cost_all_on = sLanguage + "btn_cost_all_on.png";
            btn_more = sLanguage + "btn_more.png";
            btn_more_on = sLanguage + "btn_more_on.png";
            btn_view_terms = sLanguage + "btn_view_terms.png";
            btn_view_terms_on = sLanguage + "btn_view_terms_on.png";
        }
    }
}