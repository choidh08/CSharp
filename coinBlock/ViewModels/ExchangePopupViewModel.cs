using System;
using DevExpress.Mvvm;
using System.Windows;
using DevExpress.Mvvm.POCO;

namespace coinBlock.ViewModels
{
    public class ExchangePopupViewModel : ViewModelBase
    {
        public virtual string title { get; set; } = "CoinBlock 거래소 이용 유의사항";
        public virtual string Check1 { get; set; } = "위 유의사항에 동의합니다.";
        public virtual string Check2 { get; set; } = "오늘 하루 이 창을 열지 않음";
        public virtual Visibility termsKorVisible { get; set; } = Visibility.Visible;
        public virtual Visibility termsEngVisible { get; set; } = Visibility.Collapsed;

        

        protected ExchangePopupViewModel()
        {

        }

        public static ExchangePopupViewModel Create()
        {
            return ViewModelSource.Create(() => new ExchangePopupViewModel());
        }

        public void Loaded()
        {
            try
            {
                string sLanguage = LoginViewModel.LanguagePack;
                if (sLanguage.IndexOf("ko") > 0)
                {
                    title = "CoinBlock 거래소 이용 유의사항";
                    Check1 = "위 유의사항에 동의합니다.";
                    Check2 = "오늘 하루 이 창을 열지 않음";
                    termsKorVisible = Visibility.Visible;
                    termsEngVisible = Visibility.Collapsed;
                }
                else
                {
                    title = "Notice on CoinBlock Exchange Usage";
                    Check1 = "I agree to the above.";
                    Check2 = "Close the window today.";
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