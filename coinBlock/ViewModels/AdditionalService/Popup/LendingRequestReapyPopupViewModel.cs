using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace coinBlock.ViewModels.AdditionalService.Popup
{
    [POCOViewModel]
    public class LendingRequestReapyPopupViewModel
    {
        public static LendingRequestReapyPopupViewModel Create()
        {
            return ViewModelSource.Create(() => new LendingRequestReapyPopupViewModel());
        }

        protected LendingRequestReapyPopupViewModel()
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Loaded()
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