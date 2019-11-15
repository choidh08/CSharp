using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.Common;
using coinBlock.Utility;
using coinBlock.Views.Common;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace coinBlock.ViewModels.HelpDesk
{
    [POCOViewModel]
    public class NoticeHelpDeskViewModel
    {   
        public virtual ObservableCollection<list> Notice { get; set; }        

        public virtual Visibility listVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emptyVisible { get; set; } = Visibility.Collapsed;

        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }

        public virtual int pageIndex { get; set; } = 1;
        public virtual int pageSize { get; set; } = 1;

        public virtual string SearchTxt { get; set; } = string.Empty;

        public virtual bool IsBusy { get; set; }

        protected NoticeHelpDeskViewModel()
        {
            ImageSet();
        }
        
        public static NoticeHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new NoticeHelpDeskViewModel());
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
                    req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.Notice);
                    req.lang = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];
                    req.pageIndex = pageIndex;
                    if (!string.IsNullOrWhiteSpace(SearchTxt.Trim()))
                    {
                        req.searchWrd = SearchTxt;
                    }

                    using (ResponseSelectBoardModel res = await WebApiLib.AsyncCall<ResponseSelectBoardModel, RequestSelectBoardModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.list.Count > 0)
                            {
                                Notice = new ObservableCollection<list>();
                                foreach (ResponseSelectBoardListModel item in res.data.list)
                                {
                                    //string step1 = item.contentMsg.Replace("\",=\"\"", "',");
                                    //string step2 = step1.Replace("=\"\"", "");
                                    //string stepComp = step2.Replace(" \" ", "'");
                                    //string stepComp2 = stepComp.Replace("%29", "");
                                    //string stepComp2 = stepComp.Replace("/common.file", (ConfigLib.WebUrl + "common.file").ToString());
                                    Notice.Add(ViewModelSource.Create(() => new list() { num = item.no, title = item.boardTitle, date = item.regDt, content = item.contentMsg }));
                                }

                                pageSize = res.data.pageSize;

                                listVisible = Visibility.Visible;
                                emptyVisible = Visibility.Collapsed;
                            }
                            else
                            {
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

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            btn_search = sLanguage + "btn_search.png";
            btn_search_on = sLanguage + "btn_search_on.png";
        }
    }

    public class list
    {
        public virtual int num { get; set; }

        public virtual string title { get; set; }

        public virtual string date { get; set; }

        public virtual string content { get; set; }
    }
}