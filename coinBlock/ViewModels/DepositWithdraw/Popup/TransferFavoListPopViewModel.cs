using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows.Input;

namespace coinBlock.ViewModels.DepositWithdraw.Popup
{
    [POCOViewModel]
    public class TransferFavoListPopViewModel
    {
        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }
        public virtual string btn_favo_reg { get; set; }
        public virtual string btn_favo_reg_on { get; set; }

        protected TransferFavoListPopViewModel()
        {
            ImageSet();
        }

        public static TransferFavoListPopViewModel Create()
        {
            return ViewModelSource.Create(() => new TransferFavoListPopViewModel());
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

        #region Command

        public void CmdDoubleClick(MouseButtonEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSearchContent()
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Method

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;
       
            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
            btn_favo_reg = sLanguage + "btn_favo_reg.png";
            btn_favo_reg_on = sLanguage + "btn_favo_reg_on.png";
        }

        #endregion
    }
}