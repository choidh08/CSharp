using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Views;
using coinBlock.Model;
using coinBlock.Utility;
using System.Collections.Generic;
using coinBlock.Model.DepositWithdraw;
using System.Windows.Input;

namespace coinBlock.ViewModels.Common
{
    [POCOViewModel]
    public class AtmPwdSettingViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }

        public static string CardNumStatic { get; set; }

        public virtual string CardNum { get; set; }
        public virtual string InsertPwd { get; set; }
        public virtual string CheckPwd { get; set; }

        public virtual string btn_atm_cancel { get; set; }
        public virtual string btn_atm_cancel_on { get; set; }
        public virtual string btn_regist { get; set; }
        public virtual string btn_regist_on { get; set; }

        Alert alert = null;

        protected AtmPwdSettingViewModel()
        {
            CardNum = CommonLib.CardNumChange(CardNumStatic);
            ImageSet();
        }

        public static AtmPwdSettingViewModel Create()
        {
            return ViewModelSource.Create(() => new AtmPwdSettingViewModel());
        }

        public async void CmdRegistCard()
        {
            try
            {
                int intValue = 0;

                if (string.IsNullOrEmpty(CardNumStatic))
                {
                    alert = new Alert(Localization.Resource.AtmPwdSetting_14, 330);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(InsertPwd) || string.IsNullOrWhiteSpace(CheckPwd))
                {
                    alert = new Alert(Localization.Resource.AtmPwdSetting_13);
                    alert.ShowDialog();
                    return;
                }
                else if (InsertPwd.Length < 4 || CheckPwd.Length < 4)
                {
                    alert = new Alert(Localization.Resource.AtmPwdSetting_12);
                    alert.ShowDialog();
                    return;
                }
                else if(InsertPwd != CheckPwd)
                {
                    alert = new Alert(Localization.Resource.Login_14);
                    alert.ShowDialog();
                    return;
                }
                else if (!int.TryParse(InsertPwd, out intValue) || !int.TryParse(CheckPwd, out intValue))
                {
                    alert = new Alert(Localization.Resource.AtmPwdSetting_12);
                    alert.ShowDialog();
                    return;
                }

                alert = new Alert(Localization.Resource.AtmPwdSetting_15, Alert.ButtonType.YesNo, 330);
                if (alert.ShowDialog() == true)
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();

                    using (RequestPublicKeyModel req = new RequestPublicKeyModel())
                    {
                        using (ResponsePublicKeyModel res = WebApiLib.SyncCall<ResponsePublicKeyModel, RequestPublicKeyModel>(req))
                        {
                            dict = EncodingLib.AesEncryptKey(res.data.pubKeyModule, res.data.pubKeyExponent);
                        }
                    }

                    using (RequestCardPwdSetModel req2 = new RequestCardPwdSetModel())
                    {                       
                        req2.cardNum = EncodingLib.AesEncrypt(CardNumStatic, dict["gid"]);
                        req2.atmPwd = EncodingLib.AesEncrypt(InsertPwd, dict["gid"]);
                        req2.clientPe = dict["acekey"];

                        using (ResponseCardPwdSetModel res2 = await WebApiLib.AsyncCall<ResponseCardPwdSetModel, RequestCardPwdSetModel>(req2))
                        {
                            if(res2.resultStrCode == "000")
                            {
                                string resultCd = res2.data.failCd;

                                if(resultCd.Equals(""))
                                {
                                    alert = new Alert(Localization.Resource.AtmPwdSetting_16);
                                    alert.ShowDialog();
                                    WindowService.Close();
                                }
                                else if(resultCd.Equals("999"))
                                {
                                    alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Common_15);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("998"))
                                {
                                    alert = new Alert(Localization.Resource.AtmPwdSetting_19);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("997"))
                                {
                                    alert = new Alert(Localization.Resource.AtmPwdSetting_18);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("996"))
                                {
                                    alert = new Alert(Localization.Resource.AtmPwdSetting_17);
                                    alert.ShowDialog();
                                }
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

        public void CmdCancel()
        {
            try
            {
                this.WindowService.Close();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        //public void CmdStrVailDate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        public void CmdStrNonInput(TextCompositionEventArgs e)
        {
            try
            {
                int value = 0;
                if (e.Text == null) return;
                if (string.IsNullOrEmpty(e.Text.ToString())) return;

                if (!int.TryParse(e.Text.ToString(), out value))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            try
            {
                string sLanguage = LoginViewModel.LanguagePack;

                btn_atm_cancel = sLanguage + "btn_atm_cancel.png";
                btn_atm_cancel_on = sLanguage + "btn_atm_cancel_on.png";
                btn_regist = sLanguage + "btn_regist.png";
                btn_regist_on = sLanguage + "btn_regist_on.png";
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }

        }
    }
}