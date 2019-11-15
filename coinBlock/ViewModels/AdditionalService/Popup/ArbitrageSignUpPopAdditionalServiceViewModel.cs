using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Views;
using coinBlock.Model.AdditionalService;
using coinBlock.Utility;
using System.Collections.ObjectModel;
using coinBlock.Views.AdditionalService.Popup;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class ArbitrageSignUpPopAdditionalServiceViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }

        public virtual bool bCheck { get; set; } = false;
        public virtual bool bEnabled { get; set; } = false;
        public virtual bool bSignCheck { get; set; }
        public virtual bool bDialogResult { get; set; }

        public virtual ObservableCollection<ResponseArbitrageGetSignInfoListModel> signUpList { get; set; }

        public virtual string btn_view_terms { get; set; }
        public virtual string btn_view_terms_on { get; set; }
        public virtual string btn_popup_confirm_color { get; set; }
        public virtual string btn_popup_confirm_color_on { get; set; }
        public virtual string btn_popup_cancel { get; set; }
        public virtual string btn_popup_cancel_on { get; set; }

        protected ArbitrageSignUpPopAdditionalServiceViewModel()
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

        public static ArbitrageSignUpPopAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new ArbitrageSignUpPopAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                //using (RequestArbitrageContentModel req = new RequestArbitrageContentModel())
                //{
                //    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                //    using (ResponseArbitrageContentModel res = WebApiLib.SyncCall<ResponseArbitrageContentModel, RequestArbitrageContentModel>(req))
                //    {
                //        if (res.resultStrCode == "000")
                //        {
                //            if (res.data.cnKndCd == "")
                //            {
                //                bSignCheck = true;
                //            }
                //        }
                //    }
                //}
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
                bCheck = false;
                bEnabled = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }


        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_view_terms = sLanguage + "btn_view_terms.png";
            btn_view_terms_on = sLanguage + "btn_view_terms_on.png";
            btn_popup_confirm_color = sLanguage + "btn_popup_confirm_color.png";
            btn_popup_confirm_color_on = sLanguage + "btn_popup_confirm_color_on.png";
            btn_popup_cancel = sLanguage + "btn_popup_cancel.png";
            btn_popup_cancel_on = sLanguage + "btn_popup_cancel_on.png";
        }

        public void CmdViewTerms()
        {
            try
            {
                ArbitrageViewTermAdditionalService pop = new ArbitrageViewTermAdditionalService();
                pop.ShowDialog();

                bEnabled = true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSignUp()
        {
            try
            {
                //if (bSignCheck)
                //{
                //    Alert alert = new Alert(Localization.Resource.Arbitrage_SignUpPop_Alert_1_1 + "\n" + Localization.Resource.Arbitrage_SignUpPop_Alert_1_2, 300, 160);
                //    alert.ShowDialog();
                //    return;
                //}

                if (bCheck.Equals(false))
                {
                    Alert alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_7, 350);
                    alert.ShowDialog();
                    return;
                }
                else
                {
                    Alert alert = new Alert(Localization.Resource.Arbitrage_SignUpPop_Alert_2, Alert.ButtonType.YesNo, 300, 170);
                    if (alert.ShowDialog() == true)
                    {
                        using (RequestArbitrageSetSignUpModel req = new RequestArbitrageSetSignUpModel())
                        {
                            req.userEmail = MainViewModel.LoginDataModel.userEmail;

                            using (ResponseArbitrageSetSignUpModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageSetSignUpModel, RequestArbitrageSetSignUpModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    bDialogResult = true;
                                    WindowService.Close();
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



        public void CmdSignUpCancel()
        {
            try
            {
                Alert alert = new Alert(Localization.Resource.Arbitrage_SignUpPop_Alert_3, Alert.ButtonType.YesNo, 300, 160);
                if (alert.ShowDialog() == true)
                {
                    bDialogResult = false;
                    WindowService.Close();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}