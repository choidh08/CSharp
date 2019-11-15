using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class TrustRequestPopAdditionalServiceViewModel
    {
        public virtual string title { get; set; } = "암호화폐수탁거래기본약관";
        public virtual Visibility termsKorVisible { get; set; } = Visibility.Visible;
        public virtual Visibility termsEngVisible { get; set; } = Visibility.Collapsed;

        protected TrustRequestPopAdditionalServiceViewModel()
        {

        }

        public static TrustRequestPopAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new TrustRequestPopAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                string sLanguage = LoginViewModel.LanguagePack;
                if (sLanguage.IndexOf("ko") > 0)
                {
                    title = "암호화폐수탁거래기본약관";
                    termsKorVisible = Visibility.Visible;
                    termsEngVisible = Visibility.Collapsed;
                }
                else
                {
                    title = "Cryptocurrency Trust Transactions General Terms and Conditions";
                    termsKorVisible = Visibility.Collapsed;
                    termsEngVisible = Visibility.Visible;
                }

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}