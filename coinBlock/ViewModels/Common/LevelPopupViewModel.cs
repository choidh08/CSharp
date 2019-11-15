using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class LevelPopupViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }

        public virtual string btn_popup_goCert { get; set; }
        public virtual string btn_popup_goCert_on { get; set; }
        public virtual string btn_popup_confirm { get; set; }
        public virtual string btn_popup_confirm_on { get; set; }

        protected LevelPopupViewModel()
        {
            ImageSet();
        }

        public static LevelPopupViewModel Create()
        {
            return ViewModelSource.Create(() => new LevelPopupViewModel());
        }   

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_popup_goCert = sLanguage + "btn_popup_goCert.png";
            btn_popup_goCert_on = sLanguage + "btn_popup_goCert_on.png";
            btn_popup_confirm = sLanguage + "btn_popup_confirm.png";
            btn_popup_confirm_on = sLanguage + "btn_popup_confirm_on.png";
        }
    }
}