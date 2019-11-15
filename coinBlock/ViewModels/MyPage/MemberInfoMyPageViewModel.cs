using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.Model.MyPage;
using coinBlock.Views;
using coinBlock.Model.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using coinBlock.Model.DepositWithdraw;
using System.Windows.Threading;
using System.Windows.Input;
using coinBlock.Views.Common;
using System.Collections.Generic;
using coinBlock.Views.MyPage.Popup;

namespace coinBlock.ViewModels.MyPage
{
    [POCOViewModel]
    public class MemberInfoMyPageViewModel
    {
        public virtual string btn_inquiry { get; set; }
        public virtual string btn_inquiry_on { get; set; }
        public virtual string btn_change_pw { get; set; }
        public virtual string btn_change_pw_on { get; set; }
        public virtual string btn_bookmark_save { get; set; }
        public virtual string btn_bookmark_save_on { get; set; }
        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }
        public virtual string btn_cert_request { get; set; }
        public virtual string btn_cert_request_on { get; set; }
        public virtual string btn_cert_cancel { get; set; }
        public virtual string btn_cert_cancel_on { get; set; }
        public virtual string btn_cert_confirm { get; set; }
        public virtual string btn_cert_confirm_on { get; set; }
        public virtual string btn_cert_resend { get; set; }
        public virtual string btn_cert_resend_on { get; set; }
        public virtual string btn_certnum_send { get; set; }
        public virtual string btn_certnum_send_on { get; set; }
        public virtual string btn_cert_resend_color { get; set; }
        public virtual string btn_cert_resend_color_on { get; set; }
        public virtual string btn_pin_reset { get; set; }
        public virtual string btn_pin_reset_on { get; set; }
        public virtual string btn_phone_cert { get; set; }
        public virtual string btn_phone_cert_on { get; set; }
        public virtual string btn_auth_account { get; set; }
        public virtual string btn_auth_account_on { get; set; }

        public virtual string userEmail { get; set; } = MainViewModel.LoginDataModel.userEmail;
        public virtual string userNm { get; set; } = MainViewModel.LoginDataModel.userNm;
        public virtual string userPhoneNumber { get; set; }
        public virtual string userMobile { get; set; }
        public virtual string userMobileOri { get; set; }
        public virtual string userLocal { get; set; }
        public virtual string userRcmd { get; set; }
        public virtual string subUserEmail { get; set; }
        public virtual string userBirthDay { get; set; }        
        public virtual string userBirthDayView { get; set; }
        public virtual string userPost { get; set; }
        public virtual string userAddr1 { get; set; }
        public virtual string userAddr2 { get; set; }
        public virtual string userBrhCode { get; set; }
        public virtual string userRcmdCode { get; set; }
        public virtual string userAccountNo { get; set; } = string.Empty;
        public virtual string userAccountNoOri { get; set; }
        public virtual string userBirthMaskType { get; set; }
        public virtual string userBirthMaskNowType { get; set; }             

        public virtual Visibility MobileTxtVisible { get; set; } = Visibility.Visible;
        public virtual Visibility MobileCertYn { get; set; } = Visibility.Visible;
        public virtual bool MobileTxtEnable { get; set; }
        public virtual bool MobileBtnEnable { get; set; }
        //public virtual Visibility SmsRequest { get; set; } = Visibility.Visible;
        //public virtual Visibility SmsConfirm { get; set; } = Visibility.Collapsed;
        //public virtual Visibility SmsComplete { get; set; } = Visibility.Collapsed;

        public virtual bool SmsOverTime { get; set; } = false;
        public virtual bool SmsAuthCodeEnabled { get; set; } = false;
        public virtual string IP { get; set; }
        public virtual string SmsAuthCode { get; set; } = string.Empty;
        public virtual string SmsTime { get; set; } = "10 : 00";
        public virtual bool CertCheck { get; set; } = false;

        DateTime dt;
        DispatcherTimer RepeatTimer;
        Alert alert = null;

        string sMobileCertYn = string.Empty;
        string sAccountCertYn = string.Empty;
        string sUserInfoYn = string.Empty;
        string sAccountUseYn = string.Empty;

        public virtual bool bAccountEnabled { get; set; }
        public virtual Visibility vAccountVisible { get; set; }
        public virtual bool bDefaultEnabled { get; set; } = false;
        public virtual bool bOhterEnabled { get; set; }
        public virtual bool bBirthDayEnalbed { get; set; }

        public virtual ObservableCollection<CommonCombobox> BirthTypeList { get; set; }
        public virtual CommonCombobox SelBirthType { get; set; }
        public virtual ObservableCollection<CommonCombobox> NatnList { get; set; }
        public virtual CommonCombobox SelNatn { get; set; }
        public virtual ObservableCollection<CommonCombobox> BranchList { get; set; }
        public virtual CommonCombobox SelBranch { get; set; }
        public virtual ObservableCollection<CommonCombobox> RcmdPersonList { get; set; }
        public virtual CommonCombobox SelRcmdPerson { get; set; }
        public virtual ObservableCollection<CommonCombobox> MailPushList { get; set; }
        public virtual CommonCombobox SelMailPush { get; set; }

        public virtual ObservableCollection<CommonCombobox> CountryList { get; set; }
        public virtual CommonCombobox SelectedCountry { get; set; }

        public virtual ObservableCollection<CommonCombobox> BankList { get; set; }
        public virtual CommonCombobox SelectedBank { get; set; }
        public virtual CommonCombobox SelectedBankOri { get; set; }

        public virtual bool IsBusy { get; set; }

        protected MemberInfoMyPageViewModel()
        {
            ImageSet();
            GetBirthTypeList();
            GetCountry();
            GetNatnBankList();

            dt = new DateTime();
            dt = dt.AddMinutes(10);

            //타이머
            //RepeatTimer = new DispatcherTimer();
            //RepeatTimer.Tick += RepeatTimer_Tick;
            //RepeatTimer.Interval = new TimeSpan(0, 0, 1);
        }

        public static MemberInfoMyPageViewModel Create()
        {
            return ViewModelSource.Create(() => new MemberInfoMyPageViewModel());
        }

        public void Loaded()
        {
            try
            {
                GetRcmd();
                GetMailPush();
                GetData();
                GetUserAccountIInfo();
                GetUserAuth();

                sAccountCertYn = MainViewModel.LoginDataModel.accCertYn;
                sUserInfoYn = MainViewModel.LoginDataModel.userInfoYn;
                sMobileCertYn = MainViewModel.LoginDataModel.smsCertYn;

                initCheck();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void UnLoaded()
        {
            try
            {
                //RepeatTimer.Stop();

                //SmsRequest = Visibility.Visible;
                //SmsConfirm = Visibility.Collapsed;
                //SmsComplete = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnSelNatnChanged()
        {
            try
            {
                if (SelNatn == null) return;

                if (SelNatn.Value.Equals("00"))
                {
                    if (BranchList == null) return;
                    BranchList = new ObservableCollection<CommonCombobox>();
                    BranchList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_8 + " " + Localization.Resource.Common_Alert_18, Value = "00" });
                    SelBranch = BranchList[0];
                    return;
                }

                using (RequestBrhCodeModel req = new RequestBrhCodeModel())
                {
                    req.natnCode = SelNatn.Value;

                    using (ResponseBrhCodeModel res = WebApiLib.SyncCall<ResponseBrhCodeModel, RequestBrhCodeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            BranchList = new ObservableCollection<CommonCombobox>();
                            BranchList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_8 + " " + Localization.Resource.Common_Alert_18, Value = "00" });

                            foreach (ResponseBrhCodeListModel item in res.data.list)
                            {
                                BranchList.Add(new CommonCombobox { Name = item.brhNm, Value = item.brhCode });
                            }
                            if (BranchList.Count > 0)
                            {
                                CommonCombobox temp3 = BranchList.Where(x => x.Value == userBrhCode).FirstOrDefault();
                                if (temp3 != null)
                                {
                                    SelBranch = temp3;
                                    userBrhCode = string.Empty;
                                }
                                else if (BranchList.Count > 0)
                                {
                                    SelBranch = BranchList[0];
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

        public void OnSelBranchChanged()
        {
            try
            {
                if (SelBranch == null) return;

                if (SelBranch.Value.Equals("00"))
                {
                    if (RcmdPersonList == null) return;
                    RcmdPersonList = new ObservableCollection<CommonCombobox>();
                    RcmdPersonList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_9 + " " + Localization.Resource.Common_Alert_18, Value = "00" });
                    SelRcmdPerson = RcmdPersonList[0];
                    return;
                }

                using (RequestRcmdCodeModel req = new RequestRcmdCodeModel())
                {
                    req.brhCode = SelBranch.Value;

                    using (ResponseRcmdCodeModel res = WebApiLib.SyncCall<ResponseRcmdCodeModel, RequestRcmdCodeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            RcmdPersonList = new ObservableCollection<CommonCombobox>();
                            RcmdPersonList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_9 + " " + Localization.Resource.Common_Alert_18, Value = "00" });

                            foreach (ResponseRcmdCodeListModel item in res.data.list)
                            {
                                RcmdPersonList.Add(new CommonCombobox { Name = item.rcmdNm, Value = item.rcmdCode });
                            }

                            if (RcmdPersonList.Count > 0)
                            {
                                CommonCombobox temp4 = RcmdPersonList.Where(x => x.Value == userRcmdCode).FirstOrDefault();
                                if (temp4 != null)
                                {
                                    SelRcmdPerson = temp4;
                                    userRcmdCode = string.Empty;
                                }
                                else if (RcmdPersonList.Count > 0)
                                {
                                    SelRcmdPerson = RcmdPersonList[0];
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

        public void OnSelBirthTypeChanged()
        {
            try
            {
                if (SelBirthType == null) return;

                if (SelBirthType.Value.Equals("01"))
                {           
                    if (!string.IsNullOrWhiteSpace(userBirthDayView) && userBirthDayView.Length.Equals(8))
                    {
                        if (userBirthMaskNowType.Equals("02"))
                        {
                            userBirthDayView = userBirthDayView.Substring(4, 4) + userBirthDayView.Substring(2, 2) + userBirthDayView.Substring(0, 2);
                        }
                        else if (userBirthMaskNowType.Equals("03"))
                        {
                            userBirthDayView = userBirthDayView.Substring(4, 4) + userBirthDayView.Substring(0, 2) + userBirthDayView.Substring(2, 2);
                        }
                        userBirthDay = userBirthDayView;
                    }
                    userBirthMaskNowType = "01";
                }
                else if (SelBirthType.Value.Equals("02"))
                {
                    if (!string.IsNullOrWhiteSpace(userBirthDayView) && userBirthDayView.Length.Equals(8))
                    {
                        if (userBirthMaskNowType.Equals("01"))
                        {
                            userBirthDayView = userBirthDayView.Substring(6, 2) + userBirthDayView.Substring(4, 2) + userBirthDayView.Substring(0, 4);
                        }
                        else if (userBirthMaskNowType.Equals("03"))
                        {
                            userBirthDayView = userBirthDayView.Substring(2, 2) + userBirthDayView.Substring(0, 2) + userBirthDayView.Substring(4, 4);
                        }
                        userBirthDay = userBirthDayView;
                    }
                    userBirthMaskNowType = "02";
                }
                else if (SelBirthType.Value.Equals("03"))
                {                   
                    if (!string.IsNullOrWhiteSpace(userBirthDayView) && userBirthDayView.Length.Equals(8))
                    {
                        if (userBirthMaskNowType.Equals("01"))
                        {
                            userBirthDayView = userBirthDayView.Substring(4, 2) + userBirthDayView.Substring(6, 2) + userBirthDayView.Substring(0, 4);
                        }
                        else if (userBirthMaskNowType.Equals("02"))
                        {
                            userBirthDayView = userBirthDayView.Substring(2, 2) + userBirthDayView.Substring(0, 2) + userBirthDayView.Substring(4, 4);
                        }
                        userBirthDay = userBirthDayView;
                    }
                    userBirthMaskNowType = "03";
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        //public void OnSelectedCountryChanged()
        //{
        //    try
        //    {
        //        if (SelectedCountry.Value.Equals("82"))
        //        {
        //            MobileCertYn = Visibility.Visible;
        //        }
        //        else
        //        {
        //            MobileCertYn = Visibility.Collapsed;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}

        public void GetData()
        {
            try
            {
                using (RequestGetUserInfoModel req = new RequestGetUserInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetUserInfoModel res = WebApiLib.SyncCall<ResponseGetUserInfoModel, RequestGetUserInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            userPhoneNumber = res.data.userMobile;
                            userMobile = res.data.userMobile;
                            userMobileOri = res.data.userMobile;
                            userRcmd = res.data.rcmdUserCode;
                            subUserEmail = res.data.subUserEmail;                            
                            userBirthDay = res.data.birthDay;
                            userBirthDayView = res.data.birthDay;
                            userPost = res.data.postCd;
                            userAddr1 = res.data.adrs;
                            userAddr2 = res.data.dtlAdrs;
                            userBrhCode = res.data.brhCode;
                            userRcmdCode = res.data.rcmdCode;                            

                            CommonCombobox temp = CountryList.Where(x => x.Value == res.data.countryCd).FirstOrDefault();
                            if (temp != null)
                            {
                                SelectedCountry = temp;
                            }
                            else if (CountryList.Count > 0)
                            {
                                SelectedCountry = CountryList[0];
                            }

                            CommonCombobox temp2 = NatnList.Where(x => x.Value == res.data.natnCode).FirstOrDefault();
                            if (temp2 != null)
                            {
                                SelNatn = temp2;
                            }
                            else if (NatnList.Count > 0)
                            {
                                SelNatn = NatnList[0];
                            }

                            CommonCombobox temp3 = MailPushList.Where(x => x.Value == res.data.langCd).FirstOrDefault();
                            if (temp3 != null)
                            {
                                SelMailPush = temp3;
                            }
                            else if (MailPushList.Count > 0)
                            {
                                SelMailPush = MailPushList[0];
                            }

                            CommonCombobox temp4 = BirthTypeList.Where(x => x.Value == res.data.birthType).FirstOrDefault();
                            if (temp4 != null)
                            {
                                userBirthMaskNowType = "01";
                                SelBirthType = null;
                                SelBirthType = temp4;
                            }
                            else if (BirthTypeList.Count > 0)
                            {
                                userBirthMaskNowType = "01";
                                SelBirthType = null;
                                SelBirthType = BirthTypeList[0];;
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

        public void GetUserAuth()
        {
            try
            {
                using (RequestCertifyInfoModel req = new RequestCertifyInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseCertifyInfoModel res = WebApiLib.SyncCall<ResponseCertifyInfoModel, RequestCertifyInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            MainViewModel.LoginDataModel.accCertYn = res.data.accCertYn;
                            MainViewModel.LoginDataModel.userInfoYn = res.data.userInfoYn;
                            MainViewModel.LoginDataModel.smsCertYn = res.data.smsCertYn;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetMailPush()
        {
            try
            {      
                MailPushList = new ObservableCollection<CommonCombobox>();
                MailPushList.Add(new CommonCombobox { Name = "KOREAN", Value = "ko" });
                MailPushList.Add(new CommonCombobox { Name = "ENGLISH", Value = "en" });
                MailPushList.Add(new CommonCombobox { Name = "JAPANESE", Value = "ja" });
                MailPushList.Add(new CommonCombobox { Name = "Thai", Value = "th" });
                MailPushList.Add(new CommonCombobox { Name = "CHINESE", Value = "zh" });
                MailPushList.Add(new CommonCombobox { Name = "INDONESIAN", Value = "in" });
                MailPushList.Add(new CommonCombobox { Name = "RUSSIAN", Value = "kg" });
                MailPushList.Add(new CommonCombobox { Name = "VIETNAMESE", Value = "vi" });                

                if (MailPushList.Count > 0)
                {
                    SelMailPush = MailPushList[0];
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdMobileCert()
        {
            try
            {
                alert = new Alert(Localization.Resource.Common_Alert_23, 330);
                alert.ShowDialog();
                return;

                //휴대폰 번호가 바뀌었을 때 중복체크
                if (userMobileOri != userMobile)
                {
                    int phoneCheck = 0;

                    using (RequestPhoneNumberCheckModel req = new RequestPhoneNumberCheckModel())
                    {
                        req.userMobile = userMobile.Trim();

                        using (ResponsePhoneNumberCheckModel res = WebApiLib.SyncCall<ResponsePhoneNumberCheckModel, RequestPhoneNumberCheckModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                phoneCheck = res.data.result; // 0이면 중복아님, 1이면 중복

                                if (phoneCheck.Equals(1))
                                {
                                    alert = new Alert(Localization.Resource.MemberInfo_Pop_5);
                                    alert.ShowDialog();
                                    return;
                                }
                            }
                        }
                    }
                }

                string sTelNo = string.Empty;
                string sTelCertYn = string.Empty;

                using (RequestUserTelCertiryModel req = new RequestUserTelCertiryModel())
                {
                    req.userEmail = userEmail;

                    using (ResponseUserTelCertiryModel res = WebApiLib.SyncCall<ResponseUserTelCertiryModel, RequestUserTelCertiryModel>(req))
                    {
                        sTelNo = res.data.telNo;
                        sTelCertYn = res.data.telCertYn;
                    }
                }

                if (sTelNo != userMobile)
                {
                    DanalMobile dm = new DanalMobile(userEmail, userMobile, userNm, userBirthDay);
                    dm.ShowDialog();
                }            
                
                //sMobileCertYn = dm.sMobileCertYn;
                //MessageBox.Show(sMobileCertYn);                

                //다날 인증 띄우기.
                //if (MainViewModel.LoginDataModel.smsCertYn.Equals("Y"))
                //{
                //    if (userMobileOri != userMobile)
                //    {
                //        DanalMobile dm = new DanalMobile(userEmail, userMobile, userNm, userBirthDay);
                //        if (dm.ShowDialog() == false)
                //        {
                //            sMobileCertYn = dm.sMobileCertYn;
                //        }
                //    }
                //}
                //else
                //{
                //    DanalMobile dm = new DanalMobile(userEmail, userMobile, userNm, userBirthDay);
                //    if (dm.ShowDialog() == false)
                //    {
                //        sMobileCertYn =dm.sMobileCertYn;
                //    }
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdAccountCert()
        {
            try
            {
                if (!bAccountEnabled)
                {
                    if (sUserInfoYn.Equals("N"))
                    {
                        alert = new Alert(Localization.Resource.MemberInfo_Pop_34, 330);
                        alert.ShowDialog();
                        return;
                    }
                }

                IsBusy = true;

                sAccountCertYn = "N";

                using (RequestAccountCertModel req = new RequestAccountCertModel())
                {
                    req.userEmail = userEmail;
                    req.bankCd = SelectedBank.Value.Equals("00") ? "" : SelectedBank.Value;
                    req.birthDay = BirthTypeCheck(userBirthDayView).Length.Equals(8) ? BirthTypeCheck(userBirthDayView).Substring(2, 6) : BirthTypeCheck(userBirthDayView);
                    req.searchAcctNo = userAccountNo;
                    req.userNm = userNm;

                    using (ResponseAccountCertModel res = WebApiLib.SyncCallWeb<ResponseAccountCertModel, RequestAccountCertModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string resultCd = res.data.failCd;

                            if (resultCd.Equals(""))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_25);
                                alert.ShowDialog();
                                sAccountCertYn = "Y";                                
                            }
                            else if (resultCd.Equals("999"))
                            {
                                alert = new Alert(Localization.Resource.Login_13);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("998"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_27);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("997"))
                            {
                                alert = new Alert(Localization.Resource.CertifyMyPage_Alert_1);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("996"))
                            {
                                alert = new Alert(Localization.Resource.CertifyMyPage_Alert_2);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("995"))
                            {
                                alert = new Alert(Localization.Resource.CertifyMyPage_Alert_2);
                                alert.ShowDialog();                            
                            }
                            else if (resultCd.Equals("994"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_28);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("993"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_29);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("992"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_30);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("991"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_31);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("990"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_32);
                                alert.ShowDialog();
                            }
                            else if(resultCd.Equals("989"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_33);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("988"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_29);
                                alert.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region SMS 인증
        /// <summary>
        /// 인증번호 요청
        /// </summary>
        //public async void CmdSmsRequest()
        //{
        //    try
        //    {
        //        //휴대폰 번호가 바뀌었을 때 중복체크
        //        if (userMobileOri != userMobile)
        //        {
        //            int phoneCheck = 0;

        //            using (RequestPhoneNumberCheckModel req = new RequestPhoneNumberCheckModel())
        //            {
        //                req.userMobile = userMobile.Trim();

        //                using (ResponsePhoneNumberCheckModel res = await WebApiLib.AsyncCall<ResponsePhoneNumberCheckModel, RequestPhoneNumberCheckModel>(req))
        //                {
        //                    if (res.resultStrCode == "000")
        //                    {
        //                        phoneCheck = res.data.result; // 0이면 중복아님, 1이면 중복

        //                        if (phoneCheck.Equals(1))
        //                        {
        //                            alert = new Alert(Localization.Resource.MemberInfo_Pop_5);
        //                            alert.ShowDialog();
        //                            return;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        IsBusy = true;

        //        using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //            req.userMobile = userMobile;

        //            using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
        //            {
        //                if (res.resultStrCode == "000")
        //                {
        //                    string resultCd = res.data.failCd;

        //                    if (resultCd.Equals(""))
        //                    {
        //                        alert = new Alert(Localization.Resource.MemberInfo_Pop_11, 400);
        //                        alert.ShowDialog();

        //                        dt = new DateTime();
        //                        dt = dt.AddMinutes(10);
        //                        SmsTime = dt.ToString("mm : ss");
        //                        RepeatTimer.Start();

        //                        SmsOverTime = false;
        //                        SmsAuthCode = string.Empty;
        //                        SmsAuthCodeEnabled = true;
        //                        MobileTxtVisible = Visibility.Collapsed;
        //                        MobileCertYn = Visibility.Collapsed;
        //                        //SmsRequest = Visibility.Collapsed;
        //                        //SmsConfirm = Visibility.Visible;
        //                        //SmsComplete = Visibility.Collapsed;
        //                    }
        //                    else if(resultCd.Equals("998"))
        //                    {
        //                        alert = new Alert(Localization.Resource.MemberInfo_Pop_12, 300);
        //                        alert.ShowDialog();
        //                    }
        //                    else if (resultCd.Equals("997"))
        //                    {
        //                        alert = new Alert(Localization.Resource.MemberInfo_Pop_13, 300);
        //                        alert.ShowDialog();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
        /// <summary>
        /// 인증 확인
        /// </summary>
        //public async void CmdSmsConfirm()
        //{
        //    try
        //    {
        //        if (SmsOverTime)
        //        {
        //            alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
        //            alert.ShowDialog();
        //            return;
        //        }

        //        using (RequestUserCertSmsModel req = new RequestUserCertSmsModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //            req.code = SmsAuthCode;

        //            using (ResponseUserCertSmsModel res = await WebApiLib.AsyncCall<ResponseUserCertSmsModel, RequestUserCertSmsModel>(req))
        //            {
        //                if (res.resultStrCode == "000")
        //                {
        //                    string resultCd = res.data.failCd;

        //                    if (resultCd.Equals(""))
        //                    {
        //                        alert = new Alert(Localization.Resource.IP_Registration_4_13);
        //                        alert.ShowDialog();

        //                        CertCheck = true;

        //                        RepeatTimer.Stop();
        //                        MobileTxtVisible = Visibility.Collapsed;
        //                        MobileCertYn = Visibility.Collapsed;
        //                        //SmsRequest = Visibility.Collapsed;
        //                        //SmsConfirm = Visibility.Collapsed;
        //                        //SmsComplete = Visibility.Visible;
        //                    }
        //                    else if (resultCd.Equals("999"))
        //                    {
        //                        alert = new Alert(Localization.Resource.Login_13, 300);
        //                        alert.ShowDialog(); 
        //                    }
        //                    else if (resultCd.Equals("998") || resultCd.Equals("996"))
        //                    {
        //                        alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
        //                        alert.ShowDialog();
        //                    }
        //                    else if (resultCd.Equals("997"))
        //                    {
        //                        alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
        //                        alert.ShowDialog();
        //                    }
        //                }
        //                else
        //                {
        //                    alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
        //                    alert.ShowDialog();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
        //        alert.ShowDialog();
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}
        /// <summary>
        /// 재발송
        /// </summary>
        //public async void CmdSmsResend()
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;
        //            req.userMobile = userMobile;

        //            using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
        //            {
        //                alert = new Alert(Localization.Resource.IP_Registration_4_14, 300);
        //                alert.ShowDialog();
        //                dt = new DateTime();
        //                dt = dt.AddMinutes(10);
        //                SmsTime = dt.ToString("mm : ss");

        //                SmsAuthCode = string.Empty;
        //                SmsOverTime = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
        /// <summary>
        /// 취소
        /// </summary>
        //public void CmdSmsCancel()
        //{
        //    try
        //    {
        //        alert = new Alert(Localization.Resource.Common_Alert_6, Alert.ButtonType.YesNo);
        //        if (alert.ShowDialog() == true)
        //        {
        //            userMobile = userMobileOri;
        //            RepeatTimer.Stop();
        //            dt = new DateTime();
        //            dt = dt.AddMinutes(10);
        //            SmsTime = dt.ToString("mm : ss");

        //            SmsOverTime = false;
        //            SmsAuthCode = string.Empty;
        //            SmsAuthCodeEnabled = false;
        //            MobileTxtVisible = Visibility.Visible;
        //            if (MainViewModel.LoginDataModel.smsCertYn.Equals("Y"))
        //            {
        //                MobileCertYn = Visibility.Visible;
        //                SmsRequest = Visibility.Collapsed;
        //            }
        //            else
        //            {
        //                MobileCertYn = Visibility.Collapsed;
        //                SmsRequest = Visibility.Visible;
        //            }
        //            SmsConfirm = Visibility.Collapsed;
        //            SmsComplete = Visibility.Collapsed;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}

        //private void RepeatTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        dt = dt.AddSeconds(-1);
        //        SmsTime = dt.ToString("mm : ss");
        //        if (dt.Minute.Equals(0) && dt.Second.Equals(0))
        //        {
        //            RepeatTimer.Stop();
        //            SmsOverTime = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
        //    }
        //}
        #endregion

        public void GetRcmd()
        {
            try
            {
                using (RequestNatnCodeModel req = new RequestNatnCodeModel())
                {
                    using (ResponseNatnCodeModel res = WebApiLib.SyncCall<ResponseNatnCodeModel, RequestNatnCodeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            NatnList = new ObservableCollection<CommonCombobox>();
                            NatnList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_7 + " " + Localization.Resource.Common_Alert_18, Value = "00" });

                            foreach (ResponseNatnCodeListModel item in res.data.list)
                            {
                                NatnList.Add(new CommonCombobox { Name = item.natnNm, Value = item.natnCode });
                            }

                            if (NatnList.Count > 0)
                            {
                                SelNatn = NatnList[0];
                            }

                            BranchList = new ObservableCollection<CommonCombobox>();
                            BranchList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_8 + " " + Localization.Resource.Common_Alert_18, Value = "00" });
                            SelBranch = BranchList[0];
                            RcmdPersonList = new ObservableCollection<CommonCombobox>();
                            RcmdPersonList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_9 + " " + Localization.Resource.Common_Alert_18, Value = "00" });
                            SelRcmdPerson = RcmdPersonList[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetCountry()
        {
            try
            {
                using (RequestGetCountryModel req = new RequestGetCountryModel())
                {
                    using (ResponseGetCountryModel res = WebApiLib.SyncCall<ResponseGetCountryModel, RequestGetCountryModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            CountryList = new ObservableCollection<CommonCombobox>();

                            foreach (ResponseGetCountryListModel item in res.data.list)
                            {
                                CountryList.Add(new CommonCombobox { Name = item.countryNm, Value = item.countryCd });
                            }

                            if (CountryList.Count > 0)
                            {
                                SelectedCountry = CountryList[0];
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

        public void GetNatnBankList()
        {
            try
            {
                using (RequestGetNatnBankModel req = new RequestGetNatnBankModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    if (LoginViewModel.LanguagePack.IndexOf("ko-KR") > 0)
                    {
                        req.langCd = "ko";
                    }
                    else
                    {
                        req.langCd = "en";
                    }

                    using (ResponseGetNatnBankModel res = WebApiLib.SyncCall<ResponseGetNatnBankModel, RequestGetNatnBankModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            BankList = new ObservableCollection<CommonCombobox>();

                            BankList.Add(new CommonCombobox { Name = Localization.Resource.Common_Alert_18, Value = "00" });
                            foreach (ResponseGetNatnBankListModel item in res.data.list)
                            {
                                BankList.Add(new CommonCombobox { Name = item.bankNm, Value = item.natnBankCode });
                            }

                            if (BankList.Count > 0)
                            {
                                SelectedBank = BankList[0];
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

        public void GetUserAccountIInfo()
        {
            try
            {
                using (RequestUserWithdrawInfoModel req = new RequestUserWithdrawInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseUserWithdrawInfoModel res = WebApiLib.SyncCall<ResponseUserWithdrawInfoModel, RequestUserWithdrawInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (BankList.Count > 0)
                            {
                                CommonCombobox temp = new CommonCombobox();
                                temp = BankList.Where(x => x.Value == res.data.bankCd).FirstOrDefault();

                                if (temp != null)
                                {
                                    SelectedBank = temp;
                                    SelectedBankOri = temp;
                                }
                                else
                                {
                                    SelectedBank = BankList[0];
                                    SelectedBankOri = BankList[0];
                                }
                            }

                            sAccountUseYn = res.data.useYn;
                            userAccountNo = res.data.bankAccNo.ToString().Trim();
                            userAccountNoOri = res.data.bankAccNo.ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetBirthTypeList()
        {
            try
            {
                BirthTypeList = new ObservableCollection<CommonCombobox>();
                BirthTypeList.Add(new CommonCombobox { Name = Localization.Resource.MemberInfo_2_12, Value = "00" });
                BirthTypeList.Add(new CommonCombobox { Name = "YYYYMMDD", Value = "01" });
                BirthTypeList.Add(new CommonCombobox { Name = "DDMMYYYY", Value = "02" });
                BirthTypeList.Add(new CommonCombobox { Name = "MMDDYYYY", Value = "03" });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

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

        public void CmdGoWirtePage()
        {
            try
            {
                Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_3"), Id = "QnaHelpDesk", IconPath = "/Images/ico_nav_qna.png" });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdPassWordChange()
        {
            try
            {
                Alert alert = new Alert(Localization.Resource.MemberInfo_Pop_1, 300, 200, Localization.Resource.MemberInfo_Pop_2);
                if (alert.ShowDialog() == true)
                {
                    using (RequestPwdChangeModel req = new RequestPwdChangeModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;

                        using (ResponsePwdChagneModel res = WebApiLib.SyncCall<ResponsePwdChagneModel, RequestPwdChangeModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_3, 330);
                                alert.ShowDialog();
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

        public async void CmdPinNumberInit()
        {
            try
            {
                using (RequestPinNumberCheckModel req = new RequestPinNumberCheckModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponsePinNumberCheckModel res = await WebApiLib.AsyncCall<ResponsePinNumberCheckModel, RequestPinNumberCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.pinResetYn == "Y")
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_16 + "\n" + Localization.Resource.MemberInfo_Pop_17, 330);
                                alert.ShowDialog();
                            }
                            else
                            {
                                PinNumberReset pop = new PinNumberReset();
                                pop.ShowDialog();
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
        // 저장        
        public async void CmdUserInfoSave()
        {
            try
            {
                IsBusy = true;

                if (userMobileOri != userMobile)
                {
                    int phoneCheck = 0;

                    using (RequestPhoneNumberCheckModel req = new RequestPhoneNumberCheckModel())
                    {
                        req.userMobile = userMobile.Trim();

                        using (ResponsePhoneNumberCheckModel res = await WebApiLib.AsyncCall<ResponsePhoneNumberCheckModel, RequestPhoneNumberCheckModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                phoneCheck = res.data.result; // 0이면 중복아님, 1이면 중복

                                if (phoneCheck.Equals(1))
                                {
                                    alert = new Alert(Localization.Resource.MemberInfo_Pop_5);
                                    alert.ShowDialog();
                                    return;
                                }
                            }
                        }
                    }
                }           

                //본인계좌인증이 됬는지 안됬는지 확인
                if (!SelectedBankOri.Value.Equals("00"))
                {
                    if (!SelectedBank.Value.Equals("00") && !string.IsNullOrWhiteSpace(userAccountNo))
                    {
                        if ((SelectedBankOri != SelectedBank) || (userAccountNoOri != userAccountNo)) 
                        {
                            if (sAccountCertYn.Equals("N"))
                            {
                                alert = new Alert(Localization.Resource.MemberInfo_Pop_24);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }         
                }
                if (SelBirthType.Value.Equals("00"))
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_38);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(userBirthDay.Trim()))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_1);
                    alert.ShowDialog();
                    return;
                }
                else if (!userBirthDay.Length.Equals(8))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_2);
                    alert.ShowDialog();
                    return;
                }
                else if (!BirthDayCheck())
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_2);
                    alert.ShowDialog();
                    return;
                }
                else if (!AdultCheck())
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_4);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(userMobile.Trim()) || userMobile.Equals("0000000000"))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_3);
                    alert.ShowDialog();
                    return;
                }
                else if (!SelectedBank.Value.Equals("00") && string.IsNullOrWhiteSpace(userAccountNo))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_4);
                    alert.ShowDialog();
                    return;
                }
                else if (SelectedBank.Value.Equals("00") && !string.IsNullOrWhiteSpace(userAccountNo))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_Alert_6);
                    alert.ShowDialog();
                    return;
                }
                else if (!SelectedBank.Value.Equals("00") && !string.IsNullOrWhiteSpace(userAccountNo))
                {
                    if (sAccountCertYn.Equals("N"))
                    {
                        alert = new Alert(Localization.Resource.MemberInfo_Pop_24);
                        alert.ShowDialog();
                        return;
                    }
                }
                else if (SelNatn.Value.Equals("00"))
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_6);
                    alert.ShowDialog();
                    return;
                }
                else if (SelBranch.Value.Equals("00"))
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_7);
                    alert.ShowDialog();
                    return;
                }
                else if (SelRcmdPerson.Value.Equals("00"))
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_8);
                    alert.ShowDialog();
                    return;
                }
                if (string.IsNullOrWhiteSpace(userAccountNo))
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_9 +"\n" + Localization.Resource.MemberInfo_Pop_10, Alert.ButtonType.YesNo);
                    sAccountCertYn = "N";                  
                }
                else
                {
                    alert = new Alert(Localization.Resource.MemberInfo_Pop_10, Alert.ButtonType.YesNo);
                }

                if (alert.ShowDialog() == true)
                {
                    using (RequestUserInsUdtModel req = new RequestUserInsUdtModel())
                    {
                        req.userEmail = userEmail;
                        req.birthDay = BirthTypeCheck(userBirthDay);
                        req.birthType = SelBirthType.Value;
                        req.userMobile = userMobile.Replace("-", "");
                        req.country = SelectedCountry.Value;
                        req.postCd = userPost;
                        req.adrs = userAddr1;
                        req.dtlAdrs = userAddr2;
                        req.brhCode = SelBranch.Value.Equals("00") ? string.Empty : SelBranch.Value;
                        req.rcmdCode = SelRcmdPerson.Value.Equals("00") ? string.Empty : SelRcmdPerson.Value;
                        req.langCd = SelMailPush.Value.Equals("00") ? string.Empty : SelMailPush.Value;
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseUserInsUdtModel res = await WebApiLib.AsyncCall<ResponseUserInsUdtModel, RequestUserInsUdtModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                using (RequestUserAccountInsUpdModel req2 = new RequestUserAccountInsUpdModel())
                                {
                                    if (SelectedBank.Value.Equals("00"))
                                    {
                                        req2.useYn = "N";
                                    }
                                    else
                                    {
                                        if (string.IsNullOrWhiteSpace(userAccountNo.Trim()))
                                        {
                                            alert = new Alert(Localization.Resource.CertifyMyPage_Alert_4);
                                            alert.ShowDialog();
                                            return;
                                        }
                                        else
                                        {
                                            req2.useYn = "Y";
                                        }
                                    }

                                    req2.userEmail = userEmail;
                                    req2.bankCd = SelectedBank.Value.Equals("00") ? "" : SelectedBank.Value;
                                    req2.bankAccNo = userAccountNo;
                                    req2.accntNm = userNm;
                                    req2.regIp = MainViewModel.LoginDataModel.regIp;

                                    using (ResponseUserAccountInsUpdModel res2 = await WebApiLib.AsyncCall<ResponseUserAccountInsUpdModel, RequestUserAccountInsUpdModel>(req2))
                                    {
                                        if (res2.resultStrCode == "000")
                                        {
                                            using (RequestUserCertModel req3 = new RequestUserCertModel())
                                            {
                                                sUserInfoYn = "N";
                                                sMobileCertYn = "Y";

                                                req3.userEmail = MainViewModel.LoginDataModel.userEmail;                                                
                                                req3.accCertYn = sAccountCertYn;
                                                req3.userInfoYn = sUserInfoYn;
                                                req3.smsCertYn = sMobileCertYn;

                                                using (ResponseUserCertModel res3 = WebApiLib.SyncCall<ResponseUserCertModel, RequestUserCertModel>(req3))
                                                {
                                                    if (res3.resultStrCode == "000")
                                                    {
                                                        alert = new Alert(Localization.Resource.Common_Alert_2);
                                                        alert.ShowDialog();

                                                        GetData();
                                                        GetUserAccountIInfo(); 
                                                        CertCheck = false;
                                                       
                                                        MainViewModel.LoginDataModel.accCertYn = sAccountCertYn;
                                                        MainViewModel.LoginDataModel.userInfoYn = sUserInfoYn;
                                                        MainViewModel.LoginDataModel.smsCertYn = sMobileCertYn;

                                                        initCheck();

                                                        IsBusy = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
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
            finally
            {
                IsBusy = false;
            }
        }

        public void OnBirthDayCheck()
        {
            try
            {
                userBirthDay = userBirthDayView;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void initCheck()
        {
            try
            {
                //계좌 체크
                if (userAccountNo.Equals("") && (sAccountUseYn.Equals("N") || sAccountUseYn.Equals("")))
                {
                    bAccountEnabled = true;
                }
                else
                {
                    bAccountEnabled = false;
                }

                //추천인등록 체크
                if (sUserInfoYn.Equals("N"))
                {
                    bOhterEnabled = false;
                }
                else
                {
                    bOhterEnabled = true;
                }

                if (string.IsNullOrWhiteSpace(userBirthDay))
                {
                    bBirthDayEnalbed = true;
                }
                else
                {
                    bBirthDayEnalbed = false;
                }

                //국가 체크
                if (SelectedCountry.Value.Equals("82"))
                {
                    vAccountVisible = Visibility.Visible;
                }
                else
                {
                    vAccountVisible = Visibility.Collapsed;
                }

                //모바일 체크
                MobileTxtVisible = Visibility.Visible;
                if (MainViewModel.LoginDataModel.foreignIp.Equals("Y"))
                {
                    if (sMobileCertYn.Equals("N") && userMobile == "00000000000")
                    {
                        MobileCertYn = Visibility.Collapsed;
                        MobileTxtEnable = true;
                    }
                    else
                    {
                        MobileCertYn = Visibility.Collapsed;
                        MobileTxtEnable = false;
                    }
                }
                else
                {
                    MobileCertYn = Visibility.Visible;
                    MobileTxtEnable = false;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public bool AdultCheck()
        {
            try
            {
                string sTemp = BirthTypeCheck(userBirthDay);

                int sYear = int.Parse(sTemp.Substring(0, 4));
                int sMonth = int.Parse(sTemp.Substring(4, 2));
                int sDay = int.Parse(sTemp.Substring(6, 2));

                int cYear = DateTime.Now.Year;
                int cMonth = DateTime.Now.Month;
                int cDay = DateTime.Now.Day;

                int age = cYear - sYear;

                if (sMonth * 100 + sDay > cMonth * 100 + cDay)
                {
                    age--;
                }

                if (age < 19)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return false;
            }
        }

        public bool BirthDayCheck()
        {
            try
            {
                string sTemp = BirthTypeCheck(userBirthDay);

                string sMonth = sTemp.Substring(4, 2);
                int iDay = int.Parse(sTemp.Substring(6, 2));

                int tempDay = 0;

                switch (sMonth)
                {
                    case "01":
                    case "03":
                    case "05":
                    case "07":
                    case "08":
                    case "10":
                    case "12":
                        tempDay = 31;
                        break; ;
                    case "04":
                    case "06":
                    case "09":
                    case "11":
                        tempDay = 30;
                        break;
                    case "02":
                        tempDay = 28;
                        break;
                    default:
                        tempDay = 0;
                        break;
                }

                if (tempDay == 0)
                {
                    return false;
                }
                if (tempDay < iDay)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return false;
            }
        }

        public string BirthTypeCheck(string birthDay)
        {
            try
            {
                string sBirthTemp = string.Empty;

                if (SelBirthType.Value.Equals("01"))
                {
                    sBirthTemp = userBirthDay;
                }
                else if (SelBirthType.Value.Equals("02"))
                {
                    sBirthTemp = userBirthDay.Substring(4, 4) + userBirthDay.Substring(2, 2) + userBirthDay.Substring(0, 2);
                }
                else if (SelBirthType.Value.Equals("03"))
                {
                    sBirthTemp = userBirthDay.Substring(4, 4) + userBirthDay.Substring(0, 2) + userBirthDay.Substring(2, 2);
                }

                return sBirthTemp;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return string.Empty;                        
            }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_inquiry = sLanguage + "btn_inquiry.png";
            btn_inquiry_on = sLanguage + "btn_inquiry_on.png";
            btn_change_pw = sLanguage + "btn_change_pw.png";
            btn_change_pw_on = sLanguage + "btn_change_pw_on.png";
            btn_bookmark_save = sLanguage + "btn_bookmark_save.png";
            btn_bookmark_save_on = sLanguage + "btn_bookmark_save_on.png";
            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
            btn_cert_request = sLanguage + "btn_cert_request.png";
            btn_cert_request_on = sLanguage + "btn_cert_request_on.png";
            btn_cert_cancel = sLanguage + "btn_cert_cancel.png";
            btn_cert_cancel_on = sLanguage + "btn_cert_cancel_on.png";
            btn_cert_confirm = sLanguage + "btn_cert_confirm.png";
            btn_cert_confirm_on = sLanguage + "btn_cert_confirm_on.png";
            btn_cert_resend = sLanguage + "btn_cert_resend.png";
            btn_cert_resend_on = sLanguage + "btn_cert_resend_on.png";
            btn_certnum_send = sLanguage + "btn_certnum_send.png";
            btn_certnum_send_on = sLanguage + "btn_certnum_send_on.png";
            btn_cert_resend_color = sLanguage + "btn_cert_resend_color.png";
            btn_cert_resend_color_on = sLanguage + "btn_cert_resend_color_on.png";
            btn_pin_reset = sLanguage + "btn_pin_reset.png";
            btn_pin_reset_on = sLanguage + "btn_pin_reset_on.png";
            btn_phone_cert = sLanguage + "btn_phone_cert.png";
            btn_phone_cert_on = sLanguage + "btn_phone_cert_on.png";
            btn_auth_account = sLanguage + "btn_auth_account.png";
            btn_auth_account_on = sLanguage + "btn_auth_account_on.png";
        }
    }
}