using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Utility;

namespace coinBlock.ViewModels.DepositWithdraw.Popup
{
    [POCOViewModel]
    public class CurrencyTransferPopViewModel
    {
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;
        public virtual string btn_popup_confirm_color { get; set; }
        public virtual string btn_popup_confirm_color_on { get; set; }
        public virtual string btn_popup_cancel { get; set; }
        public virtual string btn_popup_cancel_on { get; set; }

        protected CurrencyTransferPopViewModel()
        {
            ImageSet();
        }

        public static CurrencyTransferPopViewModel Create()
        {
           return  ViewModelSource.Create(() => new CurrencyTransferPopViewModel());
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