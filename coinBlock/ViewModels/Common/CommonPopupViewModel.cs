using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace coinBlock.ViewModels.Common
{
    [POCOViewModel]
    public class CommonPopupViewModel
    {
        public virtual string imagePath { get; set; }

        protected CommonPopupViewModel()
        {

        }

        public static CommonPopupViewModel Create()
        {
            return ViewModelSource.Create(() => new CommonPopupViewModel());
        }

        public void Loaded()
        {
            try
            {
                //if (LoginViewModel.LanguagePack.IndexOf("ko") > 0)
                //{
                //    imagePath = "/Images/popup_1121_kor.jpg";
                //}
                //else
                //{
                //    imagePath = "/Images/popup_1121_eng.jpg";
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}