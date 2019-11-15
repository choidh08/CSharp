using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Views;
using System.Collections.ObjectModel;
using coinBlock.Model.AdditionalService;
using System.Linq;
using coinBlock.Utility;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class ArbitrageSetMainExchangePopAdditionalServiceViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }

        public virtual bool sSelectEnabled { get; set; } = true;
        public virtual ObservableCollection<ResponseArbitrageGetSignInfoListModel> exchangeList { get; set; }
        public virtual ResponseArbitrageGetSignInfoListModel SelExchangeItem { get; set; }
        public virtual string btn_popup_confirm_color { get; set; }
        public virtual string btn_popup_confirm_color_on { get; set; }
        public virtual string btn_popup_cancel { get; set; }
        public virtual string btn_popup_cancel_on { get; set; }

        Alert alert = null;

        public ArbitrageSetMainExchangePopAdditionalServiceViewModel()
        {
            try
            {
                ImageSet();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static ArbitrageSetMainExchangePopAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new ArbitrageSetMainExchangePopAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                if (exchangeList != null)
                {
                    ResponseArbitrageGetSignInfoListModel item = exchangeList.Where(x => x.mainYn == "Y").FirstOrDefault();
                    if (item != null)
                    {
                        SelExchangeItem = item;
                        sSelectEnabled = false;
                    }
                    else
                    {
                        SelExchangeItem = exchangeList[0];
                        sSelectEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Unloaded()
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSetMainExchange()
        {
            try
            {
                //주거래소 설정
                using (RequestArbitrageSetMainExchangeModel req = new RequestArbitrageSetMainExchangeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.exchgeId = SelExchangeItem.exchgeId;

                    using (ResponseArbitrageSetMainExchangeModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageSetMainExchangeModel, RequestArbitrageSetMainExchangeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd.Equals(""))
                            {
                                alert = new Alert(Localization.Resource.Arbitrage_SetMainExchange_Alert_1);
                                alert.ShowDialog();

                                SelExchangeItem.mainYn = "Y";
                                exchangeList.Where(x => x.exchgeId == SelExchangeItem.exchgeId).FirstOrDefault().mainYn = "Y";

                                WindowService.Close();
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