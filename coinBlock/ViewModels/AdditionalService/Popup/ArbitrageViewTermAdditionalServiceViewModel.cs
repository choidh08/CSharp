using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class ArbitrageViewTermAdditionalServiceViewModel
    {
        public virtual string title { get; set; }

        protected ArbitrageViewTermAdditionalServiceViewModel()
        {

        }

        public static ArbitrageViewTermAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new ArbitrageViewTermAdditionalServiceViewModel());
        }

        public void Loaded()
        {
            try
            {
                title = Localization.Resource.Arbitrage_Term_1;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}