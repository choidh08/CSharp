using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using System.Windows;
using coinBlock.ViewModels.MyPage;
using coinBlock.Model.Common;
using coinBlock.Utility;
using coinBlock.Views.Common;
using coinBlock.Views;

namespace coinBlock.ViewModels.HelpDesk
{
    [POCOViewModel]
    public class QnaHelpDeskViewModel
    {
        string sLanguage = LoginViewModel.LanguagePack;

        public virtual Visibility visiblePage { get; set; } = Visibility.Visible;

        public virtual ObservableCollection<DataPeriod> Period { get; set; }
        public virtual ObservableCollection<QnaList> qnaList { get; set; }
        public virtual ObservableCollection<CommonCombobox> cbxData { get; set; }
        public virtual CommonCombobox SelectedCbx { get; set; }

        public virtual Visibility listVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emptyVisible { get; set; } = Visibility.Collapsed;

        public virtual DateTime fromDate { get; set; } = DateTime.Now.AddDays(-7);
        public virtual DateTime toDate { get; set; } = DateTime.Now;

        public virtual int pageIndex { get; set; } = 1;
        public virtual int pageSize { get; set; } = 1;

        public virtual bool IsBusy { get; set; }

        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }
        public virtual string btn_inquiry { get; set; }
        public virtual string btn_inquiry_on { get; set; }
        public virtual string btn_inquiry_delete { get; set; }
        public virtual string btn_inquiry_delete_on { get; set; }
        public virtual string btn_inquiry_edit { get; set; }
        public virtual string btn_inquiry_edit_on { get; set; }

        protected QnaHelpDeskViewModel()
        {
            init();
            ImageSet();
            Messenger.Default.Register<QnaDataSend>(this, VisibleMessage);
        }

        public static QnaHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new QnaHelpDeskViewModel());
        }

        public void Loaded()
        {
            try
            {
                GetData();
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
                pageIndex = 1;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void init()
        {
            try
            {
                Period = new ObservableCollection<DataPeriod>();

                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = 0, perGb = DataPeriod.PeriodGb.all, imgName = sLanguage + "btn_notice_all.png", imgTemp = sLanguage + "btn_notice_all.png", imgName_On = sLanguage + "btn_notice_all_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = 0, perGb = DataPeriod.PeriodGb.day, imgName = sLanguage + "btn_notice_today.png", imgTemp = sLanguage + "btn_notice_today.png", imgName_On = sLanguage + "btn_notice_today_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -7, perGb = DataPeriod.PeriodGb.day, imgName = sLanguage + "btn_notice_7days.png", imgTemp = sLanguage + "btn_notice_7days.png", imgName_On = sLanguage + "btn_notice_7days_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -15, perGb = DataPeriod.PeriodGb.day, imgName = sLanguage + "btn_notice_15days.png", imgTemp = sLanguage + "btn_notice_15days.png", imgName_On = sLanguage + "btn_notice_15days_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -1, perGb = DataPeriod.PeriodGb.month, imgName = sLanguage + "btn_notice_1month.png", imgTemp = sLanguage + "btn_notice_1month.png", imgName_On = sLanguage + "btn_notice_1month_on.png" }));
                Period.Add(ViewModelSource.Create(() => new DataPeriod { periodNum = -3, perGb = DataPeriod.PeriodGb.month, imgName = sLanguage + "btn_notice_3months.png", imgTemp = sLanguage + "btn_notice_3months.png", imgName_On = sLanguage + "btn_notice_3months_on.png" }));

                CmdNumber(Period[2].imgName);

                cbxData = new ObservableCollection<CommonCombobox>();
                cbxData.Add(new CommonCombobox { Name = Localization.Resource.Qna_8, Value = "0" });
                cbxData.Add(new CommonCombobox { Name = Localization.Resource.Qna_9, Value = "01" });
                cbxData.Add(new CommonCombobox { Name = Localization.Resource.Qna_10, Value = "02" });
                cbxData.Add(new CommonCombobox { Name = Localization.Resource.Qna_11, Value = "03" });
                cbxData.Add(new CommonCombobox { Name = Localization.Resource.Qna_12, Value = "04" });
                cbxData.Add(new CommonCombobox { Name = Localization.Resource.Qna_13, Value = "05" });

                SelectedCbx = cbxData[0];
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
                GetData();
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
                GetData();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetData()
        {
            try
            {
                IsBusy = true;

                using (RequestSelectBoardModel req = new RequestSelectBoardModel())
                {                    
                    req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.Qna);
                    req.lang = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];
                    req.regUser = MainViewModel.LoginDataModel.userEmail;
                    req.pageIndex = pageIndex;
                    req.stdDate = fromDate.ToString("yyyy-MM-dd");
                    req.endDate = toDate.ToString("yyyy-MM-dd");
                    if (!SelectedCbx.Value.Equals("0"))
                        req.catId = SelectedCbx.Value;

                    using (ResponseSelectBoardModel res = await WebApiLib.AsyncCall<ResponseSelectBoardModel, RequestSelectBoardModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.list.Count > 0)
                            {
                                qnaList = new ObservableCollection<QnaList>();
                                foreach (ResponseSelectBoardListModel item in res.data.list)
                                {
                                    //string step1 = item.contentMsg.Replace("\",=\"\"", "',");
                                    //string step2 = step1.Replace("=\"\"", "");
                                    //string stepComp = step2.Replace(" \" ", "'");
                                    string stepComp = item.contentMsg;

                                    //string step1_reply = item.replyMsg.Replace("\",=\"\"", "',");
                                    //string step2_reply = step1_reply.Replace("=\"\"", "");
                                    //string stepComp_reply = step2_reply.Replace(" \" ", "'");
                                    string stepComp_reply = item.replyMsg;

                                    if (item.replyYn.Equals("N"))
                                    {
                                        qnaList.Add(ViewModelSource.Create(() => new QnaList() { num = item.no, title = item.boardTitle, date = item.regDt, content = stepComp, contentId = item.contentId, replyMsg = stepComp_reply, replyGb = Localization.Resource.Qna_6, replyColor = "#999", replyUpdateVisible = Visibility.Visible, replyComplateVisible = Visibility.Hidden }));
                                    }
                                    else
                                    {
                                        qnaList.Add(ViewModelSource.Create(() => new QnaList() { num = item.no, title = item.boardTitle, date = item.regDt, content = stepComp, contentId = item.contentId, replyMsg = stepComp_reply, replyGb = Localization.Resource.Qna_7, replyColor = "#0090d5", replyUpdateVisible = Visibility.Hidden, replyComplateVisible = Visibility.Visible }));
                                    }
                                }
                                pageSize = res.data.pageSize;

                                listVisible = Visibility.Visible;
                                emptyVisible = Visibility.Collapsed;
                            }
                            else
                            {
                                pageSize = 1;

                                listVisible = Visibility.Collapsed;
                                emptyVisible = Visibility.Visible;
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

        public void CmdNumber(string value)
        {
            try
            {
                foreach (var item in Period)
                {
                    if (item.imgName.Equals(value))
                    {
                        item.imgName = item.imgName_On;
                        if (item.perGb == DataPeriod.PeriodGb.all)
                        {
                            fromDate = DateTime.Parse("2017-01-01");
                        }
                        else if (item.perGb == DataPeriod.PeriodGb.day)
                        {
                            fromDate = DateTime.Now.AddDays(item.periodNum);
                        }
                        else if (item.perGb == DataPeriod.PeriodGb.month)
                        {
                            fromDate = DateTime.Now.AddMonths(item.periodNum);
                        }
                    }
                    else
                    {
                        item.imgName = item.imgTemp;
                    }
                }

                toDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdEdit(string contentId)
        {
            try
            {
                Messenger.Default.Send(new QnaDataSend { Title = "Qna_WriteVisible", contentId = contentId });
                Messenger.Default.Send(new QnaDataSend { Title = "QnaHidden", contentId = string.Empty });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void CmdDelete(string contentId)
        {
            try
            {
                Alert alert = new Alert(Localization.Resource.Common_Alert_14, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    using (RequestBoardDeleteModel req = new RequestBoardDeleteModel())
                    {
                        req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.Qna);
                        req.contentId = contentId;
                        using (ResponseBoardDeleteModel res = await WebApiLib.AsyncCall<ResponseBoardDeleteModel, RequestBoardDeleteModel>(req))
                        {
                            alert = new Alert(Localization.Resource.Common_Alert_15);
                            alert.ShowDialog();

                            GetData();
                        }
                    }
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
                Messenger.Default.Send(new QnaDataSend { Title = "Qna_WriteVisible", contentId = string.Empty });
                Messenger.Default.Send(new QnaDataSend { Title = "QnaHidden", contentId = string.Empty });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void VisibleMessage(QnaDataSend qds)
        {
            try
            {
                if (qds.Title.Equals("QnaVisible"))
                {
                    visiblePage = Visibility.Visible;
                    GetData();
                }
                else if (qds.Title.Equals("QnaHidden"))
                {
                    visiblePage = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
            btn_inquiry = sLanguage + "btn_inquiry.png";
            btn_inquiry_on = sLanguage + "btn_inquiry_on.png";
            btn_inquiry_delete = sLanguage + "btn_inquiry_delete.png";
            btn_inquiry_delete_on = sLanguage + "btn_inquiry_delete_on.png";
            btn_inquiry_edit = sLanguage + "btn_inquiry_edit.png";
            btn_inquiry_edit_on = sLanguage + "btn_inquiry_edit_on.png";
        }
    }

    #region Class

    public class QnaList : list
    {
        public virtual string contentId { get; set; } 

        public virtual string replyMsg { get; set; }

        public virtual string replyGb { get; set; }

        public virtual string replyColor { get; set; }

        public virtual Visibility replyUpdateVisible { get; set; }

        public virtual Visibility replyComplateVisible { get; set; }
    }

    public class DataPeriod
    {
        public enum PeriodGb
        {
            day,
            month,
            all
        }
        public virtual int periodNum { get; set; }
        public virtual PeriodGb perGb { get; set; }
        public virtual string imgName { get; set; }
        public virtual string imgTemp { get; set; }
        public virtual string imgName_On { get; set; }
    }

    public class QnaDataSend
    {
        public virtual string Title { get; set; }
        public virtual string contentId { get; set; }
    }

    #endregion
}