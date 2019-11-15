using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows.Forms;
using coinBlock.Views;
using coinBlock.Utility;
using coinBlock.Model.DepositWithdraw;
using coinBlock.Model;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;
using System.Text;
using coinBlock.Model.Common;
using coinBlock.Model.MyPage;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using coinBlock.Views.DepositWithdraw.Popup;

namespace coinBlock.ViewModels.DepositWithdraw
{
    [POCOViewModel]
    public class TransferDepositWithdrawViewModel
    {
        DispatcherTimer RepeatTimer;

        public virtual ObservableCollection<ResponseTransferContentListModel> TransferList { get; set; }
        public virtual ObservableCollection<ResponseGetInfinityUserListModel> InfinityList { get; set; }
        public virtual TransferCheck_All TransferCheckList { get; set; }
        public virtual TransferCheck TransferItem { get; set; }

        public virtual string selectCode { get; set; }
        public virtual string transMask { get; set; } = "###,###,###,##0.########";
        public virtual string userMobile { get; set; }
        public virtual bool IsBusy { get; set; }
        public virtual bool IsLoad { get; set; }
        public string otpYn = string.Empty;

        public virtual string btn_ewallet_confirm { get; set; }
        public virtual string btn_ewallet_confirm_on { get; set; }
        public virtual string btn_cert_request { get; set; }
        public virtual string btn_cert_request_on { get; set; }
        public virtual string btn_remittance_request { get; set; }
        public virtual string btn_remittance_request_on { get; set; }
        public virtual string btn_cert_cancel { get; set; }
        public virtual string btn_cert_cancel_on { get; set; }
        public virtual string btn_cert_confirm { get; set; }
        public virtual string btn_cert_confirm_on { get; set; }
        public virtual string btn_cert_resend { get; set; }
        public virtual string btn_cert_resend_on { get; set; }
        public virtual string btn_certnum_send { get; set; }
        public virtual string btn_certnum_send_on { get; set; }
        public virtual string btn_sms_cert { get; set; }
        public virtual string btn_sms_cert_on { get; set; }
        public virtual string btn_recent_transfer { get; set; }
        public virtual string btn_recent_transfer_on { get; set; }
        public virtual string btn_favo { get; set; }
        public virtual string btn_favo_on { get; set; }
        public virtual string btn_max_once { get; set; }
        public virtual string btn_max_once_on { get; set; }


        Alert alert = null;// new Alert();

        public static TransferDepositWithdrawViewModel Create()
        {
            return ViewModelSource.Create(() => new TransferDepositWithdrawViewModel());
        }

        protected TransferDepositWithdrawViewModel()
        {
            try
            {
                ImageSet();

                CommonLib.GetOtpYn(ref otpYn);

                TransferCheckList = new TransferCheck_All();
                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list) //.OrderBy(x=>x.CoinCode)
                {
                    TransferCheckList.list.Add(ViewModelSource.Create(() => new TransferCheck(item, otpYn)));
                }

                if (TransferCheckList.list.Count > 0)
                {
                    TransferItem = new TransferCheck(MainViewModel.CoinData.list.FirstOrDefault(), otpYn); //.OrderBy(x => x.CoinCode                    
                    CmdSelectCoin(TransferCheckList.list[0]);
                }

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 5, 0);

                //SearchCoinAssets();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //SearchCoinAssets();
                SearchTrasnferList(selectCode);
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
                CommonLib.GetOtpYn(ref otpYn);

                if (TransferCheckList.list.Count > 0)
                {
                    foreach (TransferCheck item in TransferCheckList.list)
                    {
                        item.Clear(otpYn);
                    }
                }

                GetUserMobile();
                GetFee();
                GetInfinity();
                RepeatTimer.Start();
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
                IsLoad = false;

                RepeatTimer.Stop();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_ewallet_confirm = sLanguage + "btn_ewallet_confirm.png";
            btn_ewallet_confirm_on = sLanguage + "btn_ewallet_confirm_on.png";
            btn_cert_request = sLanguage + "btn_cert_request.png";
            btn_cert_request_on = sLanguage + "btn_cert_request_on.png";
            btn_remittance_request = sLanguage + "btn_remittance_request.png";
            btn_remittance_request_on = sLanguage + "btn_remittance_request_on.png";
            btn_cert_cancel = sLanguage + "btn_cert_cancel.png";
            btn_cert_cancel_on = sLanguage + "btn_cert_cancel_on.png";
            btn_cert_confirm = sLanguage + "btn_cert_confirm.png";
            btn_cert_confirm_on = sLanguage + "btn_cert_confirm_on.png";
            btn_cert_resend = sLanguage + "btn_cert_resend.png";
            btn_cert_resend_on = sLanguage + "btn_cert_resend_on.png";
            btn_certnum_send = sLanguage + "btn_certnum_send.png";
            btn_certnum_send_on = sLanguage + "btn_certnum_send_on.png";
            btn_sms_cert = sLanguage + "btn_sms_cert.png";
            btn_sms_cert_on = sLanguage + "btn_sms_cert_on.png";
            btn_recent_transfer = sLanguage + "btn_recent_transfer.png";
            btn_recent_transfer_on = sLanguage + "btn_recent_transfer_on.png";
            btn_favo = sLanguage + "btn_favo.png";
            btn_favo_on = sLanguage + "btn_favo_on.png";
            btn_max_once = sLanguage + "btn_max_once.png";
            btn_max_once_on = sLanguage + "btn_max_once_on.png";
        }

        //보유코인 갯수 조회
        //public async void SearchCoinAssets()
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        using (RequestMainAssetModel req = new RequestMainAssetModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;

        //            using (ResponseMainAssetModel res = await WebApiLib.AsyncCall<ResponseMainAssetModel, RequestMainAssetModel>(req))
        //            {
        //                if (res != null)
        //                {
        //                    foreach (var item in res.data.list)
        //                    {
        //                        foreach (TransferCheck itemTrans in TransferCheckList.list)
        //                        {
        //                            if (item.curcyCd.Equals(itemTrans.OrderCode))
        //                            {
        //                                itemTrans.AvailableCoin = item.posCnAmt;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        IsBusy = false;
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

        //public void SearchCoinAssets_Sub(TransferCheck tc)
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        using (RequestMainAssetModel req = new RequestMainAssetModel())
        //        {
        //            req.userEmail = MainViewModel.LoginDataModel.userEmail;

        //            using (ResponseMainAssetModel res = WebApiLib.SyncCall<ResponseMainAssetModel, RequestMainAssetModel>(req))
        //            {
        //                if (res != null)
        //                {
        //                    foreach (var item in res.data.list)
        //                    {
        //                        if (item.curcyCd.Equals(tc.OrderCode))
        //                        {
        //                            tc.AvailableCoin = item.posCnAmt;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        IsBusy = false;
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
        //수수료 조회
        public void GetFee()
        {
            try
            {
                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    foreach (TransferCheck itemTrans in TransferCheckList.list)
                    {
                        if (item.CoinCode.Equals(itemTrans.OrderCode))
                        {
                            itemTrans.Fee = item.TransferFee;
                            itemTrans.TransferMinAmt = item.TransferMinCnt;
                            itemTrans.TransferMaxAmt = item.TransferMaxCnt;
                            itemTrans.TransferDayAmt = item.TransferDayCnt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetInfinity()
        {
            try
            {
                using (RequestGetInfinityUserModel req = new RequestGetInfinityUserModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetInfinityUserModel res = await WebApiLib.AsyncCall<ResponseGetInfinityUserModel, RequestGetInfinityUserModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.failCd.Equals(""))
                            {
                                InfinityList = new ObservableCollection<ResponseGetInfinityUserListModel>();
                                InfinityList = res.data.list;
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

        public async void GetUserMobile()
        {
            try
            {
                using (RequestGetUserInfoModel req = new RequestGetUserInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetUserInfoModel res = await WebApiLib.AsyncCall<ResponseGetUserInfoModel, RequestGetUserInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            userMobile = res.data.userMobile;
                        }
                    }
                }
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

        #region Email 인증

        /// <summary>
        /// 인증번호 요청
        /// </summary>
        public async void CmdEmailRequest(TransferCheck tc)
        {
            try
            {
                //alert = new Alert(Localization.Resource.Common_Alert_1, Alert.ButtonType.Ok);
                //alert.ShowDialog();
                //return;

                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        //alert = new Alert(Localization.Resource.IP_Registration_4_12, 400);
                        //alert.ShowDialog();

                        tc.RequestEmail();
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
        /// <summary>
        /// 인증 확인
        /// </summary>
        public async void CmdEmailConfirm(TransferCheck tc)
        {
            try
            {
                if (tc.EmailOverTime)
                {
                    alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
                    alert.ShowDialog();
                    return;
                }

                using (RequestSmsCodeVertifyModel req = new RequestSmsCodeVertifyModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.authCode = tc.EmailAuthCode;

                    using (ResponseSmsCodeVertifyModel res = await WebApiLib.AsyncCall<ResponseSmsCodeVertifyModel, RequestSmsCodeVertifyModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            //alert = new Alert(Localization.Resource.IP_Registration_4_13);
                            //alert.ShowDialog();

                            tc.ConfirmEmail();
                        }
                        else
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                            alert.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 300);
                alert.ShowDialog();
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 재발송
        /// </summary>
        public async void CmdEmailResend(TransferCheck tc)
        {
            try
            {
                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        alert = new Alert(Localization.Resource.IP_Registration_4_14, 300);
                        alert.ShowDialog();

                        tc.ResendEmail();
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
        /// <summary>
        /// 취소
        /// </summary>
        public void CmdEmailCancel(TransferCheck tc)
        {
            try
            {
                alert = new Alert(Localization.Resource.Common_Alert_6, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    tc.CancelEmail();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Otp 인증
        public async void CmdOtpConfirm(TransferCheck tc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tc.OtpSerialNumber))
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_11);
                    alert.ShowDialog();
                    return;
                }

                IsBusy = true;

                using (RequestOtpCheckModel req = new RequestOtpCheckModel())
                {
                    req.encodedKey = MainViewModel.LoginDataModel.otpNo;
                    req.authCode = tc.OtpSerialNumber;
                    using (ResponseOtpCheckModel res = await WebApiLib.AsyncCall<ResponseOtpCheckModel, RequestOtpCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_8, 320);
                            alert.ShowDialog();

                            tc.OtpRequest = Visibility.Collapsed;
                            tc.OtpConfirm = Visibility.Collapsed;
                            tc.OtpComplete = Visibility.Visible;
                        }
                        else
                        {
                            alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_10, 320);
                            alert.ShowDialog();
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
        #endregion

        #region SMS 인증

        /// <summary>
        /// 인증번호 요청
        /// </summary>
        public async void CmdSmsRequest(TransferCheck tc)
        {
            try
            {
                if (userMobile.Equals("00000000000") || userMobile.Equals(string.Empty))
                {
                    alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_1_44, 400);
                    alert.ShowDialog();
                    return;
                }

                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_1_45, Alert.ButtonType.YesNo, 330, 160);
                if (alert.ShowDialog() == true)
                {
                    IsBusy = true;

                    using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.userMobile = userMobile;

                        using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string resultCd = res.data.failCd;

                                if (resultCd.Equals(""))
                                {
                                    //alert = new Alert(Localization.Resource.MemberInfo_Pop_11, 400);
                                    //alert.ShowDialog();

                                    tc.RequestSms();
                                }
                                else if (resultCd.Equals("998"))
                                {
                                    alert = new Alert(Localization.Resource.MemberInfo_Pop_12, 300);
                                    alert.ShowDialog();
                                }
                                else if (resultCd.Equals("997"))
                                {
                                    alert = new Alert(Localization.Resource.MemberInfo_Pop_13, 300);
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
            finally
            {
                IsBusy = false;
            }
        }
        /// <summary>
        /// 인증 확인
        /// </summary>
        public async void CmdSmsConfirm(TransferCheck tc)
        {
            try
            {
                if (tc.SmsOverTime)
                {
                    alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
                    alert.ShowDialog();
                    return;
                }

                using (RequestUserCertSmsModel req = new RequestUserCertSmsModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.code = tc.SmsAuthCode;

                    using (ResponseUserCertSmsModel res = await WebApiLib.AsyncCall<ResponseUserCertSmsModel, RequestUserCertSmsModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string resultCd = res.data.failCd;

                            if (resultCd.Equals(""))
                            {
                                //alert = new Alert(Localization.Resource.IP_Registration_4_13);
                                //alert.ShowDialog();

                                tc.ConfirmSms();
                            }
                            else if (resultCd.Equals("999"))
                            {
                                alert = new Alert(Localization.Resource.Login_13, 300);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("998") || resultCd.Equals("996"))
                            {
                                alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("997"))
                            {
                                alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
                                alert.ShowDialog();
                            }
                        }
                        else
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                            alert.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                alert = new Alert(Localization.Resource.IP_Registration_4_16_1 + "\n" + Localization.Resource.IP_Registration_4_16_2, 320);
                alert.ShowDialog();
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 재발송
        /// </summary>
        public async void CmdSmsResend(TransferCheck tc)
        {
            try
            {
                IsBusy = true;

                using (RequestSendCertSmsModel req = new RequestSendCertSmsModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.userMobile = userMobile;

                    using (ResponseSendCertSmsModel res = await WebApiLib.AsyncCall<ResponseSendCertSmsModel, RequestSendCertSmsModel>(req))
                    {
                        alert = new Alert(Localization.Resource.IP_Registration_4_14, 300);
                        alert.ShowDialog();

                        tc.ResendSms();
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
        /// <summary>
        /// 취소
        /// </summary>
        public void CmdSmsCancel(TransferCheck tc)
        {
            try
            {
                alert = new Alert(Localization.Resource.Common_Alert_6, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    tc.CancelSms();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        //송금요청
        public void RequestTransfer(TransferCheck tc)
        {
            try
            {
                //if (true)
                //{
                //    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                //    alert.ShowDialog();
                //    return;
                //}

                if (tc.OrderName.Equals("USDT"))
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                if (CommonLib.UserFuncCheck() == "Y")
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                //코인 출금 제한 확인
                using (RequestLmtWithdrowModel req = new RequestLmtWithdrowModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.curcyCd = tc.OrderCode;

                    using (ResponseLmtWithdrowModel res = WebApiLib.SyncCall<ResponseLmtWithdrowModel, RequestLmtWithdrowModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string withdrowType = res.data.withdrowType;

                            if (withdrowType.Equals("1"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_19);
                                alert.ShowDialog();
                                return;
                            }
                            else if (withdrowType.Equals("4"))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                }

                IsBusy = true;

                if (MainViewModel.LoginDataModel.userEmail.Equals("duduri1004@daum.net") || MainViewModel.LoginDataModel.userEmail.Equals("ymin2715@naver.com"))
                {
                    GetCoinAndNoitce(tc, false);
                    //SearchCoinAssets_Sub(tc);

                    //전자지갑주소 확인
                    if (string.IsNullOrWhiteSpace(tc.ElectroAddress))
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_3, 350);
                        alert.ShowDialog();
                        return;
                    }
                    if (tc.AvailableCoin < tc.RealTransperCoin)
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_2, 350);
                        alert.ShowDialog();
                        return;
                    }
                    //리플이고 데스티네이션태그 확인.
                    if (tc.OrderName.Equals("XRP") && string.IsNullOrWhiteSpace(tc.DestinationTag))
                    {
                        alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_4, "DestinationTag"), 350);
                        alert.ShowDialog();
                        return;
                    }
                    if (tc.OrderName.Equals("VSTC") && string.IsNullOrWhiteSpace(tc.DestinationTag))
                    {
                        alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_4, "Messase"), 350);
                        alert.ShowDialog();
                        return;
                    }
                    if (tc.OrderName.Equals("XLM") && string.IsNullOrWhiteSpace(tc.DestinationTag))
                    {
                        alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_4, "Memo"), 350);
                        alert.ShowDialog();
                        return;
                    }
                }
                else
                {
                    GetCoinAndNoitce(tc, false);
                    //SearchCoinAssets_Sub(tc);

                    if (tc.OtpVisible == Visibility.Visible)
                    {
                        if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                        {
                            alert = new Alert(Localization.Resource.CertifyMyPage_Alert_8, 350);
                            alert.ShowDialog();
                            return;
                        }
                    }

                    if (!MainViewModel.LoginDataModel.userEmail.Equals("kim0681@hanmail.net"))
                    {
                        bool bCheck = true;

                        foreach (ResponseGetInfinityUserListModel item in InfinityList)
                        {
                            if (tc.OrderName == item.cnKndCd)
                            {
                                bCheck = false;
                            }
                        }

                        if (bCheck)
                        {
                            if (tc.TransRequestCoin < tc.TransferMinAmt || tc.TransRequestCoin > tc.TransferMaxAmt)
                            {
                                alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_1, tc.TransferMinAmt.ToString("#,0.###"), tc.TransferMaxAmt.ToString("#,0.###")), 400);
                                alert.ShowDialog();
                                return;
                            }
                        }
                    }
                    //송금 가능 코인 수 체크
                    if (tc.AvailableCoin < tc.RealTransperCoin)
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_2, 350);
                        alert.ShowDialog();
                        return;
                    }
                    //전자지갑주소 확인
                    if (string.IsNullOrWhiteSpace(tc.ElectroAddress))
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_3, 350);
                        alert.ShowDialog();
                        return;
                    }
                    //리플이고 데스티네이션태그 확인.
                    if (tc.OrderName.Equals("XRP") && string.IsNullOrWhiteSpace(tc.DestinationTag))
                    {
                        alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_4, "DestinationTag"), 350);
                        alert.ShowDialog();
                        return;
                    }
                    if (tc.OrderName.Equals("VSTC") && string.IsNullOrWhiteSpace(tc.DestinationTag))
                    {
                        alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_4, "Messase"), 350);
                        alert.ShowDialog();
                        return;
                    }
                    if (tc.OrderName.Equals("XLM") && string.IsNullOrWhiteSpace(tc.DestinationTag))
                    {
                        alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_4, "Memo"), 350);
                        alert.ShowDialog();
                        return;
                    }
                    if (tc.OrderName.Equals("XEM") && string.IsNullOrWhiteSpace(tc.DestinationTag))
                    {
                        alert = new Alert(string.Format(Localization.Resource.TransferDepositWithdraw_Common_4, "Message"), 350);
                        alert.ShowDialog();
                        return;
                    }
                    //Email인증
                    if (tc.EmailComplete != Visibility.Visible)
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_5, 350);
                        alert.ShowDialog();
                        return;
                    }
                    if (tc.OtpVisible == Visibility.Visible)
                    {
                        //OTP번호 체크
                        if (tc.OtpComplete != Visibility.Visible)
                        {
                            alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_6, 350);
                            alert.ShowDialog();
                            return;
                        }
                    }
                    //SMS인증
                    if (MainViewModel.LoginDataModel.foreignIp.Equals("N"))
                    {
                        if (tc.SmsComplete != Visibility.Visible)
                        {
                            alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_18, 350);
                            alert.ShowDialog();
                            return;
                        }
                    }
                    //동의 체크
                    if (!tc.ContentCheck)
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_7, 350);
                        alert.ShowDialog();
                        return;
                    }
                }
                //송금 요청
                using (RequestTransferCoinModel req = new RequestTransferCoinModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.regIp = MainViewModel.LoginDataModel.regIp;
                    req.cnKndCd = tc.OrderCode;
                    req.wdrReqAmt = tc.RealTransperCoin;
                    req.cnSndFee = tc.Fee;
                    req.wdrWletAdr = tc.ElectroAddress;

                    if (tc.OrderName.Equals("XRP") || tc.OrderName.Equals("VSTC") || tc.OrderName.Equals("XLM"))
                    {
                        req.destiTag = tc.DestinationTag;
                    }

                    using (ResponseTransferCoinModel res = WebApiLib.SyncCall<ResponseTransferCoinModel, RequestTransferCoinModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string resultCd = res.data.failCd;

                            if (resultCd.Equals(""))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_8, 320);
                                alert.ShowDialog();

                                tc.Clear(otpYn);
                                Messenger.Default.Send("AssetsRefresh");
                            }
                            else if (resultCd.Equals("-1"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_12 + Localization.Resource.TransferDepositWithdraw_Common_11, 350);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("-2"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_12 + Localization.Resource.TransferDepositWithdraw_Common_10, 350);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("-3"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_12 + Localization.Resource.TransferDepositWithdraw_Common_11, 350);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("-4"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_12 + Localization.Resource.TransferDepositWithdraw_Common_14, 350);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("-5"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_12 + Localization.Resource.TransferDepositWithdraw_Common_15, 350);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("-6"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_12 + Localization.Resource.TransferDepositWithdraw_Common_16, 350);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("-7"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_12 + Localization.Resource.TransferDepositWithdraw_Common_17, 350);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("979"))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                            }
                            else if (resultCd == "777")
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
                                return;
                            }

                            IsBusy = false;
                            SearchTrasnferList(selectCode);
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

        public async void SearchTrasnferList(string Code)
        {
            //요청 리스트 조회
            try
            {
                //Messenger.Default.Send("AssetsRefresh");
                //SearchCoinAssets();

                using (RequestTransferContentModel req = new RequestTransferContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.cnKndCd = Code;

                    using (ResponseTransferContentModel res = await WebApiLib.AsyncCall<ResponseTransferContentModel, RequestTransferContentModel>(req))
                    {
                        foreach (ResponseTransferContentListModel item in res.data.list)
                        {
                            if (item.exFlag.Equals("L1"))
                            {
                                item.orderDesc = Localization.Resource.Lending_Grid2_3_1;
                            }
                            else if (item.exFlag.Equals("L2"))
                            {
                                item.orderDesc = Localization.Resource.Lending_Grid2_3_2;
                            }
                            else if (item.exFlag.Equals("L3"))
                            {
                                item.orderDesc = Localization.Resource.Lending_Grid2_3_3;
                            }
                            else if (item.exFlag.Equals("L4"))
                            {
                                item.orderDesc = Localization.Resource.Lending_Grid2_3_4;
                            }
                            else if (item.exFlag.Equals("T"))
                            {
                                item.orderDesc = Localization.Resource.TradingHistory_1_6;
                            }
                        }

                        TransferList = res.data.list;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSelectCoin(TransferCheck item)
        {
            try
            {
                TransferItem.Clear(otpYn);
                item.Clear(otpYn);

                selectCode = item.OrderCode;

                foreach (TransferCheck temp in TransferCheckList.list)
                {
                    temp.bold_Gb = FontWeights.Normal;
                }

                item.bold_Gb = FontWeights.Bold;
                GetCoinAndNoitce(item);
                GetNowLimit(item);
                TransferItem = item;
                SearchTrasnferList(TransferItem.OrderCode);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdRecentList()
        {
            try
            {
                TransferRecentListPop pop = new TransferRecentListPop();
                pop.ShowDialog();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdFavoList()
        {
            try
            {
                TransferFavoListPop pop = new TransferFavoListPop();
                pop.ShowDialog();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdMaxOnce(TransferCheck tc)
        {
            try
            {
                if (tc.unLimit.Equals("00")) //무제한
                {
                    tc.TransRequestCoin = tc.AvailableCoin;
                }
                else if (tc.unLimit.Equals("01")) //무제한이 아님
                {
                    if (tc.TransferMaxAmt < tc.AvailableCoin)
                    {
                        tc.TransRequestCoin = tc.TransferMaxAmt;
                    }
                    else
                    {
                        tc.TransRequestCoin = tc.AvailableCoin;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void GetCoinAndNoitce(TransferCheck tc, bool NoticeCheck = true)
        {
            try
            {
                string langCd = string.Empty;

                if (LoginViewModel.LanguagePack.IndexOf("ko-KR") > 0) langCd = "ko";
                else if (LoginViewModel.LanguagePack.IndexOf("en-US") > 0) langCd = "en";
                else if (LoginViewModel.LanguagePack.IndexOf("ja-JP") > 0) langCd = "ja";
                else if (LoginViewModel.LanguagePack.IndexOf("id-ID") > 0) langCd = "id";
                else if (LoginViewModel.LanguagePack.IndexOf("ru-RU") > 0) langCd = "ru";
                else if (LoginViewModel.LanguagePack.IndexOf("th-TH") > 0) langCd = "th";
                else if (LoginViewModel.LanguagePack.IndexOf("vi-VN") > 0) langCd = "vi";
                else if (LoginViewModel.LanguagePack.IndexOf("zh-CN") > 0) langCd = "zh";

                using (RequestGetCoinAndNodeModel req = new RequestGetCoinAndNodeModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.cnKndCd = tc.OrderCode;
                    req.langCd = langCd;

                    using (ResponseGetCoinAndNodeModel res = WebApiLib.SyncCall<ResponseGetCoinAndNodeModel, RequestGetCoinAndNodeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            tc.AvailableCoin = res.data.amt;
                            if (NoticeCheck)
                            {
                                tc.Notice = res.data.note;
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

        public void GetNowLimit(TransferCheck tc)
        {
            try
            {
                using (RequestTransferCheckLimitModel req = new RequestTransferCheckLimitModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.cnKndCd = tc.OrderCode;

                    using (ResponseTransferCheckLimitModel res = WebApiLib.SyncCall<ResponseTransferCheckLimitModel, RequestTransferCheckLimitModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string resultCd = res.data.failCd;

                            if (resultCd.Equals(""))
                            {
                                tc.TransferRemAmtDay = res.data.remAmtDay;
                                tc.unLimit = res.data.unLimit;
                            }
                            else if (resultCd.Equals("997"))
                            {
                                alert = new Alert(Localization.Resource.RechargeDepositWithdraw_Card_Pop_4);
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

        public void OnTransferLimitCheck(TransferCheck tc)
        {
            try
            {
                if (tc.unLimit.Equals("01"))
                {
                    if (tc.TransferMaxAmt < tc.TransRequestCoin)
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_15, 350);
                        alert.ShowDialog();

                        tc.TransRequestCoin = 0;
                    }
                    else if (tc.TransferRemAmtDay < tc.TransRequestCoin)
                    {
                        alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_16, 350);
                        alert.ShowDialog();

                        tc.TransRequestCoin = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class TransferCheck
    {
        public virtual string OrderName { get; set; }
        public virtual string OrderCode { get; set; }
        public DispatcherTimer RepeatTimer;
        public DateTime dt;
        public DispatcherTimer RepeatTimer2;
        public DateTime dt_sms;

        public virtual decimal AvailableCoin { get; set; }
        public virtual decimal TransRequestCoin { get; set; }
        public virtual decimal RealTransperCoin { get; set; }
        public virtual string ElectroAddress { get; set; }
        public virtual string CheckedElectroAddr { get; set; }
        public virtual decimal Fee { get; set; }
        public virtual decimal TransferMinAmt { get; set; }
        public virtual decimal TransferMaxAmt { get; set; }
        public virtual decimal TransferDayAmt { get; set; }
        public virtual decimal TransferRemAmtDay { get; set; }
        public virtual string OtpSerialNumber { get; set; }
        public virtual string EmailTime { get; set; } = "10 : 00";
        public virtual string EmailAuthCode { get; set; }
        public virtual string SmsTime { get; set; } = "3 : 00";
        public virtual string SmsAuthCode { get; set; }
        public virtual string DestinationTag { get; set; }
        public virtual string TagName { get; set; }
        public virtual bool ContentCheck { get; set; }
        public virtual string coinImage { get; set; }
        public virtual string Notice { get; set; }
        public virtual string unLimit { get; set; }

        public virtual FontWeight bold_Gb { get; set; }
        public virtual bool EmailAuthCodeEnabled { get; set; } = false;
        public virtual bool EmailOverTime { get; set; } = false;
        public virtual Visibility EmailRequest { get; set; } = Visibility.Visible;
        public virtual Visibility EmailConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility EmailComplete { get; set; } = Visibility.Collapsed;

        public virtual bool SmsAuthCodeEnabled { get; set; } = false;
        public virtual bool SmsOverTime { get; set; } = false;
        public virtual Visibility SmsRequest { get; set; } = Visibility.Visible;
        public virtual Visibility SmsConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility SmsComplete { get; set; } = Visibility.Collapsed;
        public virtual Visibility SmsMobileVisible { get; set; } = Visibility.Visible;
        public virtual Visibility SmsAllVisible { get; set; } = Visibility.Visible;

        public virtual Visibility OtpVisible { get; set; } = Visibility.Visible;
        public virtual Visibility OtpRequest { get; set; } = Visibility.Visible;
        public virtual Visibility OtpConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility OtpComplete { get; set; } = Visibility.Collapsed;

        public string otpYn;
        public virtual Visibility isTagY { get; set; } = Visibility.Collapsed;
        public virtual Visibility isTagN { get; set; } = Visibility.Collapsed;

        public virtual int totalHeight { get; set; } = 150;
        public virtual string smsRowHeight { get; set; } = "1*";

        Alert alert = null;// new Alert();

        public TransferCheck(ResponseCoinListModel item, string otpYn)
        {
            try
            {
                OrderName = item.CoinName;
                OrderCode = item.CoinCode;
                coinImage = "/Images/ico_nav_" + item.CoinName + ".png";

                dt = new DateTime();
                dt = dt.AddMinutes(10);
                dt_sms = new DateTime();
                dt_sms = dt_sms.AddMinutes(3);

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 0, 1);

                RepeatTimer2 = new DispatcherTimer();
                RepeatTimer2.Tick += RepeatTimer2_Tick;
                RepeatTimer2.Interval = new TimeSpan(0, 0, 1);

                if (MainViewModel.LoginDataModel.foreignIp.Equals("N"))
                {
                    totalHeight = 150;
                    smsRowHeight = "1*";
                }
                else
                {
                    if (OrderName.Equals("XRP") || OrderName.Equals("VSTC") || OrderName.Equals("XLM") || OrderName.Equals("EOS") || OrderName.Equals("XEM"))
                    {
                        SmsAllVisible = Visibility.Collapsed;
                    }
                    else
                    {
                        totalHeight = 120;
                        smsRowHeight = "0";
                    }
                }

                //CommonLib.GetOtpYn(ref otpYn);

                if (otpYn.Equals("N"))
                {
                    OtpVisible = Visibility.Collapsed;
                }
                else
                {
                    OtpVisible = Visibility.Visible;

                    if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                    {
                        OtpRequest = Visibility.Visible;
                        OtpConfirm = Visibility.Collapsed;
                        OtpComplete = Visibility.Collapsed;
                    }
                    else
                    {
                        OtpRequest = Visibility.Collapsed;
                        OtpConfirm = Visibility.Visible;
                        OtpComplete = Visibility.Collapsed;
                    }
                }

                if (item.IsTagYn.Equals("Y"))
                {
                    isTagY = Visibility.Visible;
                    if (OrderName.Equals("XRP"))
                    {
                        TagName = "DestinationTag";
                    }
                    else if (OrderName.Equals("VSTC") || OrderName.Equals("XEM"))
                    {
                        TagName = "Message";
                    }
                    else if (OrderName.Equals("XLM") || OrderName.Equals("EOS"))
                    {
                        TagName = "Memo";
                    }
                }
                else
                {
                    isTagN = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                dt = dt.AddSeconds(-1);
                EmailTime = dt.ToString("mm : ss");
                if (dt.Minute.Equals(0) && dt.Second.Equals(0))
                {
                    RepeatTimer.Stop();
                    EmailOverTime = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void RepeatTimer2_Tick(object sender, EventArgs e)
        {
            try
            {
                dt_sms = dt_sms.AddSeconds(-1);
                SmsTime = dt_sms.ToString("mm : ss");
                if (dt_sms.Minute.Equals(0) && dt_sms.Second.Equals(0))
                {
                    RepeatTimer2.Stop();
                    SmsOverTime = true;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnTransRequestCoinChanged()
        {
            try
            {
                RealTransperCoin = TransRequestCoin + Fee;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #region Email

        public void RequestEmail()
        {
            try
            {
                EmailTime = dt.ToString("mm : ss");
                RepeatTimer.Start();
                EmailAuthCodeEnabled = true;

                EmailRequest = Visibility.Collapsed;
                EmailConfirm = Visibility.Visible;
                EmailComplete = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ConfirmEmail()
        {
            try
            {
                RepeatTimer.Stop();
                EmailRequest = Visibility.Collapsed;
                EmailConfirm = Visibility.Collapsed;
                EmailComplete = Visibility.Visible;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ResendEmail()
        {
            try
            {
                dt = new DateTime();
                dt = dt.AddMinutes(10);
                EmailTime = dt.ToString("mm : ss");
                RepeatTimer.Start();

                EmailAuthCode = string.Empty;
                EmailOverTime = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CancelEmail()
        {
            try
            {
                RepeatTimer.Stop();
                dt = new DateTime();
                dt = dt.AddMinutes(10);
                EmailTime = dt.ToString("mm : ss");

                EmailOverTime = false;
                EmailAuthCode = string.Empty;
                EmailAuthCodeEnabled = false;
                EmailRequest = Visibility.Visible;
                EmailConfirm = Visibility.Collapsed;
                EmailComplete = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Sms

        public void RequestSms()
        {
            try
            {
                SmsTime = dt_sms.ToString("mm : ss");
                RepeatTimer2.Start();
                SmsAuthCodeEnabled = true;

                SmsRequest = Visibility.Collapsed;
                SmsConfirm = Visibility.Visible;
                SmsComplete = Visibility.Collapsed;
                SmsMobileVisible = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ConfirmSms()
        {
            try
            {
                RepeatTimer.Stop();
                SmsRequest = Visibility.Collapsed;
                SmsConfirm = Visibility.Collapsed;
                SmsComplete = Visibility.Visible;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ResendSms()
        {
            try
            {
                dt_sms = new DateTime();
                dt_sms = dt_sms.AddMinutes(3);
                SmsTime = dt_sms.ToString("mm : ss");
                RepeatTimer2.Start();

                SmsAuthCode = string.Empty;
                SmsOverTime = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CancelSms()
        {
            try
            {
                RepeatTimer.Stop();
                dt_sms = new DateTime();
                dt_sms = dt_sms.AddMinutes(3);
                SmsTime = dt_sms.ToString("mm : ss");

                SmsOverTime = false;
                SmsAuthCode = string.Empty;
                SmsAuthCodeEnabled = false;
                SmsRequest = Visibility.Visible;
                SmsConfirm = Visibility.Collapsed;
                SmsComplete = Visibility.Collapsed;
                SmsMobileVisible = Visibility.Visible;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        public void Clear(string otpYn)
        {
            try
            {
                TransRequestCoin = 0;
                RealTransperCoin = 0;
                ElectroAddress = string.Empty;
                OtpSerialNumber = string.Empty;
                DestinationTag = string.Empty;
                ContentCheck = false;

                RepeatTimer.Stop();
                dt = new DateTime();
                dt = dt.AddMinutes(10);

                EmailTime = dt.ToString("mm : ss");
                EmailOverTime = false;
                EmailAuthCodeEnabled = false;

                EmailRequest = Visibility.Visible;
                EmailConfirm = Visibility.Collapsed;
                EmailComplete = Visibility.Collapsed;

                RepeatTimer2.Stop();
                dt_sms = new DateTime();
                dt_sms = dt_sms.AddMinutes(3);

                SmsTime = dt.ToString("mm : ss");
                SmsOverTime = false;
                SmsAuthCodeEnabled = false;

                SmsRequest = Visibility.Visible;
                SmsConfirm = Visibility.Collapsed;
                SmsComplete = Visibility.Collapsed;
                SmsMobileVisible = Visibility.Visible;

                //CommonLib.GetOtpYn(ref otpYn);

                if (otpYn.Equals("N"))
                {
                    OtpVisible = Visibility.Collapsed;
                }
                else
                {
                    OtpVisible = Visibility.Visible;

                    if (string.IsNullOrWhiteSpace(MainViewModel.LoginDataModel.otpNo))
                    {
                        OtpRequest = Visibility.Visible;
                        OtpConfirm = Visibility.Collapsed;
                        OtpComplete = Visibility.Collapsed;
                    }
                    else
                    {
                        OtpRequest = Visibility.Collapsed;
                        OtpConfirm = Visibility.Visible;
                        OtpComplete = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class TransferCheck_All
    {
        public virtual List<TransferCheck> list { get; set; }
        public TransferCheck_All()
        {
            list = new List<TransferCheck>();
        }
    }
}
