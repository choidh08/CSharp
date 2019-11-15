using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Windows;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.Common;
using coinBlock.Utility;
using coinBlock.ViewModels.MyPage;
using System.Collections.ObjectModel;
using coinBlock.Views;
using System.Linq;
using coinBlock.Model.HelpDesk;

namespace coinBlock.ViewModels.HelpDesk
{
    [POCOViewModel]
    public class QnaWirteHelpDeskViewModel
    {
        public virtual Visibility visiblePage { get; set; } = Visibility.Hidden;

        public virtual string sName { get; set; } = MainViewModel.LoginDataModel.userNm;
        public virtual string sTitle { get; set; }
        public virtual string sContent { get; set; }
        public virtual string sPhoneNumber { get; set; }
        public virtual string sContentId { get; set; }

        public virtual string btn_inquiry_register { get; set; }
        public virtual string btn_inquiry_register_on { get; set; }
        public virtual string btn_inquiry_cancel { get; set; }
        public virtual string btn_inquiry_cancel_on { get; set; }

        public virtual ObservableCollection<CommonCombobox> cbxData { get; set; }
        public virtual CommonCombobox SelectedCbx { get; set; }

        protected QnaWirteHelpDeskViewModel()
        {
            Init();
            ImageSet();
            Messenger.Default.Register<QnaDataSend>(this, VisibleMessage);
        }

        public void Init()
        {
            try
            {
                cbxData = new ObservableCollection<CommonCombobox>();
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
        
        public static QnaWirteHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(()=>new QnaWirteHelpDeskViewModel());
        }       

        public void SaveContent()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sTitle))
                {
                    Alert alert = new Alert(Localization.Resource.QnaWrite_Pop_1);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(sContent))
                {
                    Alert alert = new Alert(Localization.Resource.QnaWrite_Pop_2);
                    alert.ShowDialog();
                    return;
                }

                using (RequestBoardUpdateModel req = new RequestBoardUpdateModel())
                {
                    req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.Qna);
                    req.catId = SelectedCbx.Value;
                    req.boardTitle = sTitle;
                    req.contentMsg = sContent;
                    req.userPhone = sPhoneNumber;
                    req.useYn = "Y";
                    req.contentId = sContentId;
                    req.lang = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];
                    req.regUser = MainViewModel.LoginDataModel.userEmail;
                    req.regIp = MainViewModel.LoginDataModel.regIp;

                    using (ResponseBoardUpdateModel res = WebApiLib.SyncCall<ResponseBoardUpdateModel, RequestBoardUpdateModel>(req))
                    {
                        string contentId = res.data.contentId;

                        using (RequestAdminMailSendModel req2 = new RequestAdminMailSendModel())
                        {
                            req2.regUser = MainViewModel.LoginDataModel.userEmail;
                            req2.contentId = contentId;

                            using (ResponseAdminMailSendModel res2 = WebApiLib.SyncCall<ResponseAdminMailSendModel, RequestAdminMailSendModel>(req2))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    Alert alert = new Alert(Localization.Resource.Common_Alert_2);
                                    alert.ShowDialog();

                                    Messenger.Default.Send(new QnaDataSend { Title = "Qna_WriteHidden", contentId = string.Empty });
                                    Messenger.Default.Send(new QnaDataSend { Title = "QnaVisible", contentId = string.Empty });
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

        public async void SearchContent()
        {
            try
            {
                using (RequestSelectBoardDetailModel req = new RequestSelectBoardDetailModel())
                {
                    req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.Qna);
                    req.contentId = sContentId;

                    using (ResponseSelectBoardDetailModel res = await WebApiLib.AsyncCall<ResponseSelectBoardDetailModel, RequestSelectBoardDetailModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            ResponseSelectBoardDetailDataModel item = res.data;
                            CommonCombobox selectItem = cbxData.Where(x => x.Value == res.data.catId).FirstOrDefault();
                            if (selectItem != null)
                            {
                                SelectedCbx = selectItem;
                            }
                            else
                            {
                                SelectedCbx = cbxData[0];
                            }
                            sTitle = item.boardTitle;
                            sContent = item.contentMsg;
                            sPhoneNumber = item.userPhone;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdWriteOk()
        {
            try
            {
                SaveContent();         
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdWriteCancel()
        {
            try
            {
                Messenger.Default.Send(new QnaDataSend { Title = "Qna_WriteHidden", contentId = string.Empty });
                Messenger.Default.Send(new QnaDataSend { Title = "QnaVisible", contentId = string.Empty });
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
                if (qds.Title.Equals("Qna_WriteVisible"))
                {
                    sContentId = qds.contentId;
                    if (!string.IsNullOrWhiteSpace(sContentId))
                    {
                        SearchContent();
                    }
                    else
                    {
                        sTitle = string.Empty;
                        sContent = string.Empty;
                        sPhoneNumber = string.Empty;

                    }
                    visiblePage = Visibility.Visible;
                }
                else if (qds.Title.Equals("Qna_WriteHidden"))
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
            string sLanguage = LoginViewModel.LanguagePack;

            btn_inquiry_register = sLanguage + "btn_inquiry_register.png";
            btn_inquiry_register_on = sLanguage + "btn_inquiry_register_on.png";
            btn_inquiry_cancel = sLanguage + "btn_inquiry_cancel.png";
            btn_inquiry_cancel_on = sLanguage + "btn_inquiry_cancel_on.png";
        }           
    }
}