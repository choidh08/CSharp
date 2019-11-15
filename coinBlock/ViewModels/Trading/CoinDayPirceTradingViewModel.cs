using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using coinBlock.ViewModels.HelpDesk;
using DevExpress.Mvvm.POCO;
using System.Windows;
using coinBlock.Model.Trading;
using coinBlock.Utility;
using coinBlock.Views.Common;
using coinBlock.Model;
using System.Linq;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class CoinDayPirceTradingViewModel
    {
        string sLanguage = LoginViewModel.LanguagePack;
        public virtual ObservableCollection<DataPeriod> Period { get; set; }
        public virtual ObservableCollection<clsDayPrc> DayPrc { get; set; }

        public virtual DateTime fromDate { get; set; } = DateTime.Now.AddMonths(-1);
        public virtual DateTime toDate { get; set; } = DateTime.Now;

        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }

        public virtual Visibility listVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emptyVisible { get; set; } = Visibility.Collapsed;

        public virtual int pageIndex { get; set; } = 1;
        public virtual int pageSize { get; set; } = 1;

        public static string sPriceType { get; set; }
        public static string sCoinCode { get; set; }
        public virtual string Title { get; set; }

        public virtual bool IsBusy { get; set; }

        protected CoinDayPirceTradingViewModel()
        {
            Init();
            ImageSet();         
        }

        public static CoinDayPirceTradingViewModel Create()
        {
            return ViewModelSource.Create(() => new CoinDayPirceTradingViewModel());
        }

        public void Loaded()
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

        public void Init()
        {
            try
            {
                Period = new ObservableCollection<DataPeriod>();

                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = 0, perGb = DataPeriod.PeriodGb.all, imgName = sLanguage + "btn_notice_all.png", imgTemp = sLanguage + "btn_notice_all.png", imgName_On = sLanguage + "btn_notice_all_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = 0, perGb = DataPeriod.PeriodGb.day, imgName = sLanguage + "btn_notice_today.png", imgTemp = sLanguage + "btn_notice_today.png", imgName_On = sLanguage + "btn_notice_today_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -7, perGb = DataPeriod.PeriodGb.day, imgName = sLanguage + "btn_notice_7days.png", imgTemp = sLanguage + "btn_notice_7days.png", imgName_On = sLanguage + "btn_notice_7days_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -15, perGb = DataPeriod.PeriodGb.day, imgName = sLanguage + "btn_notice_15days.png", imgTemp = sLanguage + "btn_notice_15days.png", imgName_On = sLanguage + "btn_notice_15days_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -1, perGb = DataPeriod.PeriodGb.month, imgName = sLanguage + "btn_notice_1month.png", imgTemp = sLanguage + "btn_notice_1month.png", imgName_On = sLanguage + "btn_notice_1month_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -3, perGb = DataPeriod.PeriodGb.month, imgName = sLanguage + "btn_notice_3months.png", imgTemp = sLanguage + "btn_notice_3months.png", imgName_On = sLanguage + "btn_notice_3months_on.png" }));

                CmdNumber(Period[0].imgName);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdNumber(string value)
        {
            try
            {
                foreach (var item in Period)
                {
                    if (item.imgName.Equals(value))
                    {
                        item.imgName = item.imgName_On;
                        if (item.perGb == DataPeriod.PeriodGb.all)
                        {
                            fromDate = DateTime.Parse("2017-01-01");
                        }
                        else if (item.perGb == DataPeriod.PeriodGb.day)
                        {
                            fromDate = DateTime.Now.AddDays(item.periodNum);
                        }
                        else if (item.perGb == DataPeriod.PeriodGb.month)
                        {
                            fromDate = DateTime.Now.AddMonths(item.periodNum);
                        }
                    }
                    else
                    {
                        item.imgName = item.imgTemp;
                    }
                }

                toDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdPageCall(PageingDataEventArgs e)
        {
            try
            {
                string bUrl = e.baseUrl;
                pageIndex = e.PageNum;
                GetData();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetData()
        {
            try
            {
                IsBusy = true;

                Title = Localization.Resource.CoinDayPirce_1 + "(" + StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(sCoinCode) + "/" + sPriceType + ")";

                string CommonFloat = string.Empty;

                if (sPriceType.Equals(CommonLib.StandardCurcyNm))
                {
                    ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == sCoinCode).FirstOrDefault();
                    CommonFloat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                    //CommonFloat = "###,###,###,###,##0.000";
                }
                else
                {
                    CommonFloat = "###,###,###,###,##0.00000000";
                }

                using (RequestTradingGetDayPrcModel req = new RequestTradingGetDayPrcModel())
                {
                    req.cnKndCd = sCoinCode;
                    req.stdDate = fromDate.ToString("yyyy-MM-dd");
                    req.endDate = toDate.ToString("yyyy-MM-dd");
                    req.mkState = sPriceType;
                    req.pageIndex = pageIndex;

                    using (ResponseTradingGetDayPrcModel res = await WebApiLib.AsyncCall<ResponseTradingGetDayPrcModel, RequestTradingGetDayPrcModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            DayPrc = new ObservableCollection<clsDayPrc>();
                            string sColor = string.Empty;
                            if (res.resultStrCode == "000")
                            {
                                DayPrc = new ObservableCollection<clsDayPrc>();
                                foreach (ResponseTradingGetDayPrcListModel item in res.data.list)
                                {
                                    if (item.prcPer > 0)
                                        sColor = "#FF0000";
                                    else
                                        sColor = "#0000FF";

                                    DayPrc.Add(ViewModelSource.Create(() => new clsDayPrc { day = item.regDt, openPrc = decimal.Parse(item.fstPrc.ToString()).ToString(CommonFloat), lowPrc =decimal.Parse(item.minPrc.ToString()).ToString(CommonFloat), highPrc = decimal.Parse(item.maxPrc.ToString()).ToString(CommonFloat), endPrc = decimal.Parse(item.lstPrc.ToString()).ToString(CommonFloat), prevContrast = item.prcPer, prevContrastColor = sColor, tradeAmount = item.totAmt }));                               }

                                pageSize = res.data.pageSize;

                                listVisible = Visibility.Visible;
                                emptyVisible = Visibility.Collapsed;
                            }
                            else
                            {
                                listVisible = Visibility.Collapsed;
                                emptyVisible = Visibility.Visible;
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

        public void ImageSet()
        {
            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
        }
    }

    public class clsDayPrc
    {
        public virtual string day { get; set; }
        public virtual string openPrc { get; set; }
        public virtual string lowPrc { get; set; }
        public virtual string highPrc { get; set; }
        public virtual string endPrc { get; set; }
        public virtual decimal? prevContrast { get; set; }
        public virtual string prevContrastColor { get; set; }
        public virtual decimal? tradeAmount { get; set; }
    }
}