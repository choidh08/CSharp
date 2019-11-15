using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Windows;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.Model.MyPage;
using coinBlock.Views.Common;
using coinBlock.Views;

namespace coinBlock.ViewModels.MyPage
{
    [POCOViewModel]
    public class LoginHistoryMyPageViewModel
    {
        public virtual Visibility btnLoginInfo { get; set; } = Visibility.Visible;
        public virtual Visibility btnHtsIpInfo { get; set; } = Visibility.Collapsed;

        public virtual Visibility LoginListVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility LoginEmptyVisible { get; set; } = Visibility.Visible;

        public virtual Visibility HtsIpListVisible { get; set; } = Visibility.Visible;
        public virtual Visibility HtsIpEmptyVisible { get; set; } = Visibility.Collapsed;

        public virtual ObservableCollection<LoginHistoryData> LoginHisData { get; set; }
        public virtual ObservableCollection<HtsIpHistoryData> HtsIpHisData { get; set; }

        public virtual DateTime fromDate { get; set; } = DateTime.Now.AddMonths(-1);
        public virtual DateTime toDate { get; set; } = DateTime.Now;

        public virtual bool IsBusy { get; set; }

        public virtual int pageIndex { get; set; } = 1;
        public virtual int pageSize { get; set; } = 1;

        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }
        public virtual string btn_unregister { get; set; }
        public virtual string btn_unregister_on { get; set; }

        protected LoginHistoryMyPageViewModel()
        {
            ImageSet();
        }

        public static LoginHistoryMyPageViewModel Create()
        {
            return ViewModelSource.Create(() => new LoginHistoryMyPageViewModel());
        }

        public void Loaded()
        {
            try
            {
                GetLoginHistory();
                GetIpList();
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

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdPageCall(PageingDataEventArgs e)
        {
            try
            {
                string bUrl = e.baseUrl;
                pageIndex = e.PageNum;
                GetLoginHistory();
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
                pageIndex = 1;
                GetLoginHistory();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetLoginHistory()
        {
            try
            {
                IsBusy = true;

                using (RequestGetLoginHistoryModel req = new RequestGetLoginHistoryModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.stdDate = fromDate.ToString("yyyy-MM-dd");
                    req.endDate = toDate.ToString("yyyy-MM-dd");
                    req.pageIndex = pageIndex;

                    using (ResponseGetLoginHistoryModel res = await WebApiLib.AsyncCall<ResponseGetLoginHistoryModel, RequestGetLoginHistoryModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.list.Count > 0)
                            {
                                LoginHisData = new ObservableCollection<LoginHistoryData>();
                                foreach (ResponseGetLoginHistoryListModel item in res.data.list)
                                {
                                    LoginHisData.Add(ViewModelSource.Create(() => new LoginHistoryData { Num = item.no, LoginDt = item.loginTme, brwsCd = item.brwsCd, connIp=item.connIp, Location = item.location, loginYn = item.loginYn }));
                                }

                                pageSize = res.data.pageSize;

                                LoginListVisible = Visibility.Visible;
                                LoginEmptyVisible = Visibility.Collapsed;
                            }
                            else
                            {
                                pageSize = 1;

                                LoginListVisible = Visibility.Collapsed;
                                LoginEmptyVisible = Visibility.Visible;
                            }

                            IsBusy = false;
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

        public async void GetIpList()
        {
            try
            {
                int cnt = 1;

                using (RequestIpRegContentModel req = new RequestIpRegContentModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseIpRegContentModel res = await WebApiLib.AsyncCall<ResponseIpRegContentModel, RequestIpRegContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            HtsIpHisData = new ObservableCollection<HtsIpHistoryData>();

                            foreach (ResponseIpRegContentListModel item in res.data.list)
                            {
                                HtsIpHisData.Add(ViewModelSource.Create(() => new HtsIpHistoryData { Num = cnt, Period = item.stdDt + " ~ " + item.endDt, ConnDt = item.loginTm, regIp = item.ip }));
                                cnt++;
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

        public async void CmdIpCancel(string Ip)
        {
            try
            {
                //IP 등록 해재
                using (RequestIpUnRegModel req = new RequestIpUnRegModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.ip = Ip;
                    req.limtHr = 0;

                    using (ResponsetIpUnRegModel res = await WebApiLib.AsyncCall<ResponsetIpUnRegModel, RequestIpUnRegModel>(req))
                    {
                        //푸쉬전송
                        using (RequestSendPushModel req2 = new RequestSendPushModel())
                        {
                            req2.userEmail = MainViewModel.LoginDataModel.userEmail;
                            req2.regIp = MainViewModel.LoginDataModel.regIp;
                            req2.contents = Localization.Resource.LoginHistory_2_6 + "(" + Ip + ")";
                            req2.pushType = "CMMC00000000427";

                            using (ResponseSendPushModel res2 = await WebApiLib.AsyncCall<ResponseSendPushModel, RequestSendPushModel>(req2))
                            {
                                GetIpList();
                                Alert alert = new Alert(Localization.Resource.LoginHistory_2_6, 300);
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

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";

            btn_unregister = sLanguage + "btn_unregister.png";
            btn_unregister_on = sLanguage + "btn_unregister_on.png";
        }
    }

    public class LoginHistoryData
    {
        public virtual int Num { get; set; }        
        public virtual string LoginDt { get; set; }
        public virtual string brwsCd { get; set; }
        public virtual string serviceNm { get; set; } = "Login";
        public virtual string Location { get; set; }
        public virtual string connIp { get; set; }
        public virtual string loginYn { get; set; }        
    }

    public class HtsIpHistoryData
    {
        public virtual int Num { get; set; }
        public virtual string Period { get; set; }
        public virtual string ConnDt { get; set; }
        public virtual string regIp { get; set; }
    }
}