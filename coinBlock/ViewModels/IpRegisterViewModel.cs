using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using System.Windows;
using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.Views;
using System.Threading;
using System.Windows.Threading;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class IpRegisterViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }
        public virtual ObservableCollection<ComboBoxData> Select { get; set; }
        public virtual ObservableCollection<ComboBoxData> Day { get; set; }
        public virtual ObservableCollection<ComboBoxData> Time { get; set; }
        public virtual ComboBoxData Sel { get; set; }
        public virtual ComboBoxData SelDay { get; set; }
        public virtual ComboBoxData SelTime { get; set; }

        DateTime dt;
        DispatcherTimer RepeatTimer;

        public static string regIp { get; set; }
        public static string userEmail { get; set; }
        public static bool SubmitCheck { get; set; } = false;

        public virtual string img_text_ip_regist_notice { get; set; }
        public virtual string btn_ip_confirm { get; set; }
        public virtual string btn_ip_confirm_on { get; set; }
        public virtual string btn_popup_cancel { get; set; }
        public virtual string btn_popup_cancel_on { get; set; }
        public virtual string btn_cert_cancel { get; set; }
        public virtual string btn_cert_cancel_on { get; set; }
        public virtual string btn_cert_confirm { get; set; }
        public virtual string btn_cert_confirm_on { get; set; }
        public virtual string btn_cert_resend { get; set; }
        public virtual string btn_cert_resend_on { get; set; }
        public virtual string btn_certnum_send { get; set; }
        public virtual string btn_certnum_send_on { get; set; }
        
        public virtual bool radioDayEnabled { get; set; } = false;
        public virtual bool radioDayChecked { get; set; } = false;
        public virtual bool radioTimeEnabled { get; set; } = false;
        public virtual bool radioTimeChecked { get; set; } = false;
        public virtual bool comboDayEnabled { get; set; } = false;
        public virtual bool comboTimeEnabled { get; set; } = false;

        public virtual Visibility SmsRequest { get; set; } = Visibility.Visible;
        public virtual Visibility SmsConfirm { get; set; } = Visibility.Collapsed;
        public virtual Visibility SmsComplete { get; set; } = Visibility.Collapsed;

        public virtual bool SmsOverTime { get; set; } = false;
        public virtual bool SmsAuthCodeEnabled { get; set; } = false;
        public virtual string IP { get; set; }
        public virtual string SmsAuthCode { get; set; }
        public virtual string SmsTime { get; set; } = "10 : 00";

        public virtual bool IsBusy { get; set; }

        Alert alert = null;// new Alert();

        public static IpRegisterViewModel Create()
        {
            return ViewModelSource.Create(() => new IpRegisterViewModel());
        }
        public IpRegisterViewModel()
        {
            ImageSet();
            dt = new DateTime();
            dt = dt.AddMinutes(3);

            //타이머
            RepeatTimer = new DispatcherTimer();
            RepeatTimer.Tick += RepeatTimer_Tick;
            RepeatTimer.Interval = new TimeSpan(0, 0, 1);

            //IP 조회
            IP = regIp;

            #region Combobox 초기화
            Day = new ObservableCollection<ComboBoxData>();
            for (int i = 1; i <= 30; i++)
            {
                Day.Add(new ComboBoxData { Name = i.ToString() + Localization.Resource.IP_Registration_4_8, Value = i });
            }
            SelDay = Day[0];
            Time = new ObservableCollection<ComboBoxData>();
            for (int i = 1; i <= 23; i++)
            {
                Time.Add(new ComboBoxData { Name = i.ToString() + Localization.Resource.IP_Registration_4_9, Value = i });
            }
            SelTime = Time[0];

            Select = new ObservableCollection<ComboBoxData>();
            Select.Add(new ComboBoxData { Name = Localization.Resource.IP_Registration_4_7_1, Value = 0 });
            Select.Add(new ComboBoxData { Name = Localization.Resource.IP_Registration_4_7_2, Value = 1 });
            Sel = Select[0];
            #endregion
        }

        #region 사용기간 Checked, Enabled
        public void OnSelChanged()
        {
            if(Sel.Value.Equals(0))
            {
                radioDayEnabled = false;
                radioTimeEnabled = false;
                comboDayEnabled = false;
                comboTimeEnabled = false;
            }
            else
            {
                radioDayEnabled = true;
                radioTimeEnabled = true;
            }
        }

        public void CmdRadioDayCheckChanged(bool IsChecked)
        {
            radioDayChecked = IsChecked;
            comboDayEnabled = IsChecked;
            comboTimeEnabled = !IsChecked;
        }

        public void CmdRadioTimeCheckChanged(bool IsChecked)
        {
            radioTimeChecked = IsChecked;
            comboDayEnabled = !IsChecked;
            comboTimeEnabled = IsChecked;
        }
        #endregion

        #region SMS 인증
        /// <summary>
        /// 인증번호 요청
        /// </summary>
        public async void CmdSmsRequest()
        {
            try
            {
                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        alert = new Alert(Localization.Resource.IP_Registration_4_12, 400);
                        alert.ShowDialog();

                        SmsTime = dt.ToString("mm : ss");
                        RepeatTimer.Start();

                        SmsAuthCodeEnabled = true;
                        SmsRequest = Visibility.Collapsed;
                        SmsConfirm = Visibility.Visible;
                        SmsComplete = Visibility.Collapsed;
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
        public async void CmdSmsConfirm()
        {
            try
            {         
                if(SmsOverTime)
                {
                    alert = new Alert(Localization.Resource.IP_Registration_4_17_1 + "\n" + Localization.Resource.IP_Registration_4_17_2, 300);
                    alert.ShowDialog();
                    return;
                }
                
                using (RequestSmsCodeVertifyModel req = new RequestSmsCodeVertifyModel())
                {
                    req.userEmail = userEmail;
                    req.authCode = SmsAuthCode;

                    using (ResponseSmsCodeVertifyModel res = await WebApiLib.AsyncCall<ResponseSmsCodeVertifyModel, RequestSmsCodeVertifyModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            alert = new Alert(Localization.Resource.IP_Registration_4_13);
                            alert.ShowDialog();

                            RepeatTimer.Stop();
                            SmsRequest = Visibility.Collapsed;
                            SmsConfirm = Visibility.Collapsed;
                            SmsComplete = Visibility.Visible;
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
        public async void CmdSmsResend()
        {
            try
            {
                IsBusy = true;

                using (RequestSmsCodeModel req = new RequestSmsCodeModel())
                {
                    req.userEmail = userEmail;

                    using (ResponseSmsCodeModel res = await WebApiLib.AsyncCall<ResponseSmsCodeModel, RequestSmsCodeModel>(req))
                    {
                        alert = new Alert(Localization.Resource.IP_Registration_4_14, 300);
                        alert.ShowDialog();
                        dt = new DateTime();
                        dt = dt.AddMinutes(3);
                        SmsTime = dt.ToString("mm : ss");
                        RepeatTimer.Start();

                        SmsAuthCode = string.Empty;
                        SmsOverTime = false;
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
        public void CmdSmsCancel()
        {
            try
            {
                alert = new Alert(Localization.Resource.Common_Alert_6);
                if (alert.ShowDialog() == true)
                {
                    RepeatTimer.Stop();
                    dt = new DateTime();
                    dt = dt.AddMinutes(3);
                    SmsTime = dt.ToString("mm : ss");

                    SmsOverTime = false;
                    SmsAuthCode = string.Empty;
                    SmsAuthCodeEnabled = false;
                    SmsRequest = Visibility.Visible;
                    SmsConfirm = Visibility.Collapsed;
                    SmsComplete = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region 등록하기
        public async void CmdSubmit()
        {
            try
            {
                if (SmsComplete == Visibility.Visible)
                {
                    IsBusy = true;

                    int PeriodTime = 0;
                    if (Sel.Value.Equals(1))
                    {
                        if (radioDayChecked)
                        {
                            PeriodTime = SelDay.Value * 24;
                        }
                        else if (radioTimeChecked)
                        {
                            PeriodTime = SelTime.Value;
                        }
                    }
                    else
                    {
                        PeriodTime = 87600;
                    }

                    if (PeriodTime.Equals(0))
                    {
                        alert = new Alert(Localization.Resource.IP_Registration_4_18);
                        alert.ShowDialog();
                        return;
                    }

                    //아이피 등록
                    using (RequestIpRegModel req = new RequestIpRegModel())
                    {
                        req.userEmail = userEmail;
                        req.ip = IP;
                        req.limtHr = PeriodTime;
                        using (ResponseIpRegModel res = await WebApiLib.AsyncCall<ResponseIpRegModel, RequestIpRegModel>(req))
                        {
                            SubmitCheck = true;
                            WindowService.Close();

                            NoticePopup note = new NoticePopup(NoticePopup.KindNotice.HTS_IP_CHECK_2);
                            note.Title = Localization.Resource.NoticePopup_3;
                            note.ShowDialog();

                            IsBusy = false;
                        }
                    }
                }
                else
                {
                    Alert alert = new Alert(Localization.Resource.IP_Registration_4_15, 320);
                    alert.ShowDialog();
                    return;
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

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;
            
            btn_ip_confirm = sLanguage + "btn_popup_confirm_color.png";
            btn_ip_confirm_on = sLanguage + "btn_popup_confirm_color_on.png";
            btn_popup_cancel = sLanguage + "btn_popup_cancel.png";
            btn_popup_cancel_on = sLanguage + "btn_popup_cancel_on.png";
            btn_cert_cancel = sLanguage + "btn_cert_cancel.png";
            btn_cert_cancel_on = sLanguage + "btn_cert_cancel_on.png";
            btn_cert_confirm = sLanguage + "btn_cert_confirm.png";
            btn_cert_confirm_on = sLanguage + "btn_cert_confirm_on.png";
            btn_cert_resend = sLanguage + "btn_cert_resend.png";
            btn_cert_resend_on = sLanguage + "btn_cert_resend_on.png";
            btn_certnum_send = sLanguage + "btn_certnum_send.png";
            btn_certnum_send_on = sLanguage + "btn_certnum_send_on.png";
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            dt = dt.AddSeconds(-1);
            SmsTime = dt.ToString("mm : ss");
            if (dt.Minute.Equals(0) && dt.Second.Equals(0))
            {
                RepeatTimer.Stop();
                SmsOverTime = true;
            }
        }
    }

    public class ComboBoxData
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }    
}