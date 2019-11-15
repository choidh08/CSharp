using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using coinBlock.ViewModels.DepositWithdraw;
using coinBlock.Utility;
using coinBlock.Views;
using coinBlock.Views.AdditionalService.Popup;
using coinBlock.ViewModels.AdditionalService.Popup;
using DevExpress.Mvvm.Xpf;
using System.Collections.Generic;
using coinBlock.Model.AdditionalService;
using System.Windows.Threading;

namespace coinBlock.ViewModels.AdditionalService
{
    [POCOViewModel]
    public class LendingRequestAdditionalServiceViewModel
    {
        //protected IDialogService DialogService { get { return this.GetService<IDialogService>(); } }

        DispatcherTimer RepeatTimer;
        DispatcherTimer RepeatTimer2;
        DispatcherTimer RepeatTimer3;

        public virtual ObservableCollection<ResponseLendingReqContentListModel> lendingReqList { get; set; }
        public virtual ObservableCollection<ResponseLendingRepaymentListModel> lendingRepayList { get; set; }
        public virtual ObservableCollection<ComboBoxStrData> curcyList { get; set; }
        public virtual ComboBoxStrData selCurcy { get; set; }
        public virtual ObservableCollection<ComboBoxLendingData> cbxLendingReq { get; set; }
        public virtual ComboBoxLendingData selLendingReq { get; set; }

        Alert alert = null;

        public virtual decimal minReqAmt { get; set; } = 0;
        public virtual decimal maxReqAmt { get; set; } = 0;
        public virtual decimal reqAmt { get; set; } = 0;
        public virtual string periodMonth { get; set; }
        public virtual decimal interestRate { get; set; }
        public virtual string strInterestRate { get; set; }
        public virtual decimal expectInterest { get; set; }
        public virtual decimal totalAmt { get; set; }

        public virtual string fromDate { get; set; }
        public virtual string toDate { get; set; }

        public virtual string rPeriodMonth { get; set; }
        public virtual string rStrInterestRate { get; set; }

        public virtual bool termsCheck { get; set; } = false;
        public virtual bool termsEnable { get; set; } = false;

        bool listEnable { get; set; } = true;
        int listSize { get; set; } = 10;

        bool rListEnable { get; set; } = true;
        int rListSize { get; set; } = 10;

        public virtual bool allEnable { get; set; } = true;
        public virtual string allMonth { get; set; } = "12";

        public virtual string btn_auto_request { get; set; }
        public virtual string btn_auto_request_on { get; set; }
        public virtual string btn_cost_all { get; set; }
        public virtual string btn_cost_all_on { get; set; }
        public virtual string btn_more { get; set; }
        public virtual string btn_more_on { get; set; }
        public virtual string btn_view_terms { get; set; }
        public virtual string btn_view_terms_on { get; set; }

        public static LendingRequestAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new LendingRequestAdditionalServiceViewModel());
        }

        protected LendingRequestAdditionalServiceViewModel()
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
                RepeatTimer3.Tick += RepeatTimer3_Tick;
                RepeatTimer3.Interval = new TimeSpan(0, 0, 10);

                curcyList = new ObservableCollection<ComboBoxStrData>();
                curcyList.Add(new ComboBoxStrData { Name = "BTC", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC) });
                curcyList.Add(new ComboBoxStrData { Name = "ETH", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.ETH) });
                curcyList.Add(new ComboBoxStrData { Name = "BCH", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BCH) });
                curcyList.Add(new ComboBoxStrData { Name = "XRP", Value = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.XRP) });

                //if (curcyList.Count > 0)
                //{
                //    selCurcy = curcyList[0];
                //}

                periodMonth = string.Format(Localization.Resource.Lending_1_7_1, allMonth);
                ImageSet();
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
                if (curcyList.Count > 0)
                {
                    selCurcy = curcyList[0];
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
                RepeatTimer.Stop();
                RepeatTimer2.Stop();
                RepeatTimer3.Stop();
                ControlClear();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #region Event

        public void OnselCurcyChanged()
        {
            try
            {
                if (selCurcy == null) return;

                using (RequestLendingInfoModel req = new RequestLendingInfoModel())
                {
                    req.cnKndCd = selCurcy.Value;

                    using (ResponseLendingInfoModel res = WebApiLib.SyncCall<ResponseLendingInfoModel, RequestLendingInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            minReqAmt = res.data.minAmt;
                            maxReqAmt = res.data.maxAmt;
                            interestRate = res.data.interest;
                            strInterestRate = string.Format(Localization.Resource.Lending_2_6_1, interestRate);
                            fromDate = DateTime.Parse(res.data.applyDt).ToString("yyyy-MM-dd");
                            toDate = DateTime.Parse(res.data.expireDt).ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnselLendingReqChanged()
        {
            try
            {
                if (selLendingReq == null) return;

                GetRepayData();
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
                GetData();
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
                rListEnable = true;
                RepeatTimer3.Stop();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                rListEnable = true;
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

        #endregion

        #region Method

        //컨트롤 초기화
        public void ControlClear()
        {
            try
            {
                minReqAmt = 0;
                maxReqAmt = 0;
                reqAmt = 0;
                strInterestRate = string.Empty;
                expectInterest = 0;
                totalAmt = 0;
                termsCheck = false;
                termsEnable = false;

                rPeriodMonth = string.Empty;
                rStrInterestRate = string.Empty;

                listSize = 10;
                rListSize = 10;
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
                alert = new Alert(Localization.Resource.Common_Alert_1);
                alert.ShowDialog();
                return;

                //TrustRequestPopAdditionalService pop = new TrustRequestPopAdditionalService();
                //pop.ShowDialog();

                //termsEnable = true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //신청하기
        public void LendingRequest()
        {
            try
            {
                if (selCurcy == null) return;

                if (reqAmt == 0)
                {
                    alert = new Alert(Localization.Resource.Lending_Alert_3);
                    alert.ShowDialog();
                    return;
                }
                else if (minReqAmt > reqAmt)
                {
                    alert = new Alert(Localization.Resource.Lending_Alert_4, 330);
                    alert.ShowDialog();
                    return;
                }

                else if (maxReqAmt < reqAmt)
                {
                    alert = new Alert(Localization.Resource.Lending_Alert_5, 330);
                    alert.ShowDialog();
                    return;
                }

                alert = new Alert(Localization.Resource.Lending_Alert_6, Alert.ButtonType.YesNo, 330);
                if (alert.ShowDialog() == true)
                {
                    using (RequestLendingApplyModel req = new RequestLendingApplyModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.cnKndCd = selCurcy.Value;
                        req.applyDt = fromDate.Replace("-", "").ToString();
                        req.applyAmt = reqAmt;
                        req.mthCmt = allMonth;
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseLendingApplyModel res = WebApiLib.SyncCall<ResponseLendingApplyModel, RequestLendingApplyModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string resultCd = res.data.failCd;
                                if (resultCd.Equals(""))
                                {
                                    alert = new Alert(Localization.Resource.Common_Alert_24);
                                    alert.ShowDialog();                                    
                                    allEnable = false;
                                    GetData();
                                    ControlClear();

                                    Messenger.Default.Send("AssetsRefresh");
                                }
                                else if (resultCd.Equals("999"))
                                {
                                    alert = new Alert(Localization.Resource.Lending_Alert_7);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("998"))
                                {
                                    alert = new Alert(Localization.Resource.Lending_Alert_8);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("997"))
                                {
                                    alert = new Alert(Localization.Resource.Lending_Alert_9);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("996"))
                                {
                                    alert = new Alert(Localization.Resource.Lending_Alert_10);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("995"))
                                {
                                    alert = new Alert(Localization.Resource.Lending_Alert_11);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("994"))
                                {
                                    alert = new Alert(Localization.Resource.Lending_Alert_12);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("993"))
                                {
                                    alert = new Alert(Localization.Resource.Lending_Alert_13);
                                    alert.ShowDialog();
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
        //상환하기 or 연장하기
        public void CmdRepay(ResponseLendingReqContentListModel item)
        {
            try
            {
                LendingRequestReapyPopup pop = new LendingRequestReapyPopup(item.renDftCode);
                if (pop.ShowDialog() == true)
                {
                    GetData();
                    Messenger.Default.Send("AssetsRefresh");
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdExtend(ResponseLendingReqContentListModel item)
        {
            try
            {
                LendingRequestExtendPopup pop = new LendingRequestExtendPopup(item.renDftCode);
                if (pop.ShowDialog() == true)
                {
                    GetData();
                    Messenger.Default.Send("AssetsRefresh");
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        //리스트 호출
        public void GetData()
        {
            try
            {
                using (RequestLendingReqContentModel req = new RequestLendingReqContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.listSize = listSize;

                    using (ResponseLendingReqContentModel res = WebApiLib.SyncCall<ResponseLendingReqContentModel, RequestLendingReqContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            allEnable = true;

                            foreach (ResponseLendingReqContentListModel item in res.data.list)
                            {
                                if (item.applyStatus.Equals("CMMC00000001411"))
                                {
                                    allEnable = false;
                                }

                                if (item.btnView.Equals("0"))
                                {
                                    item.repayVisible = System.Windows.Visibility.Collapsed;
                                    item.extendVisible = System.Windows.Visibility.Collapsed;
                                }
                                else if (item.btnView.Equals("1"))
                                {
                                    item.repayVisible = System.Windows.Visibility.Visible;
                                    item.extendVisible = System.Windows.Visibility.Collapsed;
                                }
                                else if (item.btnView.Equals("2"))
                                {
                                    item.repayVisible = System.Windows.Visibility.Visible;
                                    item.extendVisible = System.Windows.Visibility.Visible;
                                }
                            }

                            lendingReqList = res.data.list;

                            if (lendingReqList.Count > 0)
                            {
                                LendingReqContent();
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

        public void GetRepayData()
        {
            try
            {
                using (RequestLendingRepaymentModel req = new RequestLendingRepaymentModel())
                {
                    req.renDftCode = selLendingReq.Value.renDftCode;
                    req.listSize = rListSize;

                    using (ResponseLendingRepaymentModel res = WebApiLib.SyncCall<ResponseLendingRepaymentModel, RequestLendingRepaymentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            lendingRepayList = res.data.list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        //더보기 버튼
        public void MoreRequestList()
        {
            try
            {
                if (listEnable)
                {
                    listEnable = false;

                    listSize += 10;
                    GetData();

                    RepeatTimer2.Start();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void MoreRepayList()
        {
            try
            {
                if (rListEnable)
                {
                    rListEnable = false;

                    rListSize += 10;
                    GetRepayData();

                    RepeatTimer3.Start();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void LendingReqContent()
        {
            try
            {
                if (lendingReqList == null) return;

                cbxLendingReq = new ObservableCollection<ComboBoxLendingData>();

                foreach(ResponseLendingReqContentListModel item in lendingReqList)
                {
                    cbxLendingReq.Add(ViewModelSource.Create(() => new ComboBoxLendingData() {Name= item.cnKndNm + " (" + item.applyDt +")", Value = item }));                    
                }

                if (cbxLendingReq.Count > 0)
                {
                    selLendingReq = cbxLendingReq[0];
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void InterestCalc()
        {
            try
            {
                expectInterest = reqAmt * ((interestRate * (decimal)0.01) * 12);
                totalAmt = reqAmt + expectInterest;
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

        #endregion
    }

    public class ComboBoxLendingData
    {
        public string Name { get; set; }
        public ResponseLendingReqContentListModel Value { get; set; }
    }
}