using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class LendingRequestExtendPopupViewModel
    {
        public static LendingRequestExtendPopupViewModel Create()
        {
            return ViewModelSource.Create(() => new LendingRequestExtendPopupViewModel());
        }

        protected LendingRequestExtendPopupViewModel()
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}