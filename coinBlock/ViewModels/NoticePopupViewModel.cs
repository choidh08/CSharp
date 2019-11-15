using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.DepositWithdraw;
using coinBlock.Utility;
using coinBlock.ViewModels;
using coinBlock.Model.MyPage;

namespace coinBlock.Views
{
    [POCOViewModel]
    public class NoticePopupViewModel
    {
        public static NoticePopupViewModel Create()
        {
            return ViewModelSource.Create(() => new NoticePopupViewModel());
        }
        protected NoticePopupViewModel()
        {
            ImageSet();
            GetDepositNm();
        }

        public virtual string SC { get; } = CommonLib.StandardCurcyNm;

        public virtual string btn_popup_cancel { get; set; }
        public virtual string btn_popup_cancel_on { get; set; }
        public virtual string btn_popup_confirm { get; set; }
        public virtual string btn_popup_confirm_on { get; set; }       
        public virtual string btn_popup_confirm_color { get; set; }
        public virtual string btn_popup_confirm_color_on { get; set; }

        public virtual string DepositNm { get; set; }
        public static string UserName { get; set; }

        #region 원화충전(Recharge) Pop
        public async void GetDepositNm()
        {
            try
            {
                if (MainViewModel.LoginDataModel != null)
                {
                    using (RequestGetUserInfoModel req = new RequestGetUserInfoModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;

                        using (ResponseGetUserInfoModel res = await WebApiLib.AsyncCall<ResponseGetUserInfoModel, RequestGetUserInfoModel>(req))
                        {
                            if (res.data.userMobile.Equals(string.Empty) || res.data.userMobile.Equals("00000000000"))
                            {
                                DepositNm = MainViewModel.LoginDataModel.userNm;
                            }
                            else
                            {
                                DepositNm = MainViewModel.LoginDataModel.userNm + res.data.userMobile.Substring(res.data.userMobile.Length - 4, 4);
                            }

                            if (DepositNm.Length >= 7)
                            {
                                DepositNm = DepositNm.Substring(0, 7);
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
        #endregion

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_popup_cancel = sLanguage + "btn_popup_cancel.png";
            btn_popup_cancel_on = sLanguage + "btn_popup_cancel_on.png";
            btn_popup_confirm = sLanguage + "btn_popup_confirm.png";
            btn_popup_confirm_on = sLanguage + "btn_popup_confirm_on.png";
            btn_popup_confirm_color = sLanguage + "btn_popup_confirm_color.png";
            btn_popup_confirm_color_on = sLanguage + "btn_popup_confirm_color_on.png";
        }
    }
}