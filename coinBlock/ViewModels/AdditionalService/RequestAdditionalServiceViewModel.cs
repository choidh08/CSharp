using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.Views;
using coinBlock.Model.AdditionalService;
using coinBlock.Views.AdditionalService.Popup;

namespace coinBlock.ViewModels.AdditionalService
{
    [POCOViewModel]
    public class RequestAdditionalServiceViewModel
    {
        string sLanguage = LoginViewModel.LanguagePack;
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;

        public virtual string btn_auto_request { get; set; }
        public virtual string btn_auto_request_on { get; set; }

        public virtual string arbi_btn_auto_request { get; set; }
        public virtual string arbi_btn_auto_request_on { get; set; }

        public virtual string fYYYYMMDD { get; set; } = $"{DateTime.Now.Year.ToString()}. {DateTime.Now.Month.ToString()}. {DateTime.Now.Day.ToString()}";
        public virtual string tYYYYMMDD { get; set; }

        public virtual string NonRequestColor { get; set; } = "#0090d5";
        public virtual string RequestColor { get; set; } = "#666";
        
        public virtual bool RequestEnable { get; set; }

        public virtual string arbi_fYYYYMMDD { get; set; } = $"{DateTime.Now.Year.ToString()}. {DateTime.Now.Month.ToString()}. {DateTime.Now.Day.ToString()}";
        public virtual string arbi_tYYYYMMDD { get; set; }

        public virtual string arbi_NonRequestColor { get; set; } = "#0090d5";
        public virtual string arbi_RequestColor { get; set; } = "#666";

        public virtual bool arbi_RequestEnable { get; set; }

        protected RequestAdditionalServiceViewModel()
        {

        }

        public static RequestAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new RequestAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                string tempDate = DateTime.Now.AddDays(29).ToString("yyyyMMdd");
                fYYYYMMDD = $"{DateTime.Now.Year.ToString()}. {DateTime.Now.Month.ToString()}. {DateTime.Now.Day.ToString()}"; ;
                tYYYYMMDD = $"{tempDate.Substring(0, 4)}. {tempDate.Substring(4, 2)}. {tempDate.Substring(6, 2)}";

                arbi_fYYYYMMDD = $"{DateTime.Now.Year.ToString()}. {DateTime.Now.Month.ToString()}. {DateTime.Now.Day.ToString()}";
                arbi_tYYYYMMDD = $"{tempDate.Substring(0, 4)}. {tempDate.Substring(4, 2)}. {tempDate.Substring(6, 2)}";

                GetList();
                //GetArbiList();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdPayment()
        {
            try
            {
                AutoTradingPopAdditionalService pop = new AutoTradingPopAdditionalService();
                pop.ShowDialog();

                GetList();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdArbiPayment()
        {
            try
            {
                ArbitrageTradingPopAdditionalService pop = new ArbitrageTradingPopAdditionalService();
                pop.ShowDialog();

                GetArbiList();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        

        public async void GetList()
        {
            try
            {
                using (RequestAutoTradeContentModel req = new RequestAutoTradeContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseAutoTradeContentModel res = await WebApiLib.AsyncCall<ResponseAutoTradeContentModel, RequestAutoTradeContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.cnKndCd != null)
                            {
                                fYYYYMMDD = $"{res.data.reqDt.Substring(0, 4)}. {res.data.reqDt.Substring(4, 2)}. {res.data.reqDt.Substring(6, 2)}";
                                tYYYYMMDD = $"{res.data.endDt.Substring(0, 4)}. {res.data.endDt.Substring(4, 2)}. {res.data.endDt.Substring(6, 2)}";

                                NonRequestColor = "#666";
                                RequestColor = "#0090d5";

                                RequestEnable = false;
                                btn_auto_request = sLanguage + "btn_auto_request_complete.png";
                            }
                            else
                            {
                                NonRequestColor = "#0090d5";
                                RequestColor = "#666";

                                RequestEnable = true;
                                btn_auto_request = sLanguage + "btn_auto_request.png";
                                btn_auto_request_on = sLanguage + "btn_auto_request_on.png";
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

        public async void GetArbiList()
        {
            try
            {
                using (RequestArbitrageContentModel req = new RequestArbitrageContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseArbitrageContentModel res = await WebApiLib.AsyncCallEncrypt<ResponseArbitrageContentModel, RequestArbitrageContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.cnKndCd != "")
                            {
                                arbi_fYYYYMMDD = $"{res.data.reqDt.Substring(0, 4)}. {res.data.reqDt.Substring(4, 2)}. {res.data.reqDt.Substring(6, 2)}";
                                arbi_tYYYYMMDD = $"{res.data.endDt.Substring(0, 4)}. {res.data.endDt.Substring(4, 2)}. {res.data.endDt.Substring(6, 2)}";

                                arbi_NonRequestColor = "#666";
                                arbi_RequestColor = "#0090d5";

                                arbi_RequestEnable = false;
                                arbi_btn_auto_request = sLanguage + "btn_auto_request_complete.png";
                            }
                            else
                            {
                                arbi_NonRequestColor = "#0090d5";
                                arbi_RequestColor = "#666";

                                arbi_RequestEnable = true;
                                arbi_btn_auto_request = sLanguage + "btn_auto_request.png";
                                arbi_btn_auto_request_on = sLanguage + "btn_auto_request_on.png";
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
    }
}