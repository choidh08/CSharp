using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;
using System.Windows;
using coinBlock.Model.Common;
using coinBlock.Utility;
using coinBlock.Views.Common;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class FaqHelpDeskViewModel
    {
        public virtual ObservableCollection<list> faqList { get; set; }
        public virtual ObservableCollection<btnFAQ> faqBtn { get; set; }

        public virtual Visibility listVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emptyVisible { get; set; } = Visibility.Collapsed;

        public virtual string btn_search { get; set; }
        public virtual string btn_search_on { get; set; }

        public virtual bool IsBusy { get; set; }

        public virtual int pageIndex { get; set; } = 1;
        public virtual int pageSize { get; set; } = 1;
        public virtual string nowCategory { get; set; }
        public virtual string SearchTxt { get; set; } = string.Empty;  
            

        protected FaqHelpDeskViewModel()
        {
            SetBtn();
            ImageSet();
        }
        public static FaqHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new FaqHelpDeskViewModel());
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

        public void SetBtn()
        {
            try
            {
                faqBtn = new ObservableCollection<btnFAQ>();

                faqBtn.Add(ViewModelSource.Create(() => new btnFAQ { sName = Localization.Resource.Faq_1, sValue = "0", sBackColor = "#e5e5e5", sForeColor = "#333" }));
                faqBtn.Add(ViewModelSource.Create(() => new btnFAQ { sName = Localization.Resource.Faq_2, sValue = "01", sBackColor = "#e5e5e5", sForeColor = "#333" }));
                faqBtn.Add(ViewModelSource.Create(() => new btnFAQ { sName = Localization.Resource.Faq_3, sValue = "02", sBackColor = "#e5e5e5", sForeColor = "#333" }));
                faqBtn.Add(ViewModelSource.Create(() => new btnFAQ { sName = Localization.Resource.Faq_4, sValue = "03", sBackColor = "#e5e5e5", sForeColor = "#333" }));
                faqBtn.Add(ViewModelSource.Create(() => new btnFAQ { sName = Localization.Resource.Faq_5, sValue = "04", sBackColor = "#e5e5e5", sForeColor = "#333" }));
                //faqBtn.Add(ViewModelSource.Create(() => new btnFAQ { sName = Localization.Resource.Faq_6, sValue = "05", sBackColor = "#e5e5e5", sForeColor = "#333" }));

                CmdNumber(faqBtn[0].sValue);
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

        public void CmdNumber(string value)
        {
            try
            {
                foreach (var item in faqBtn)
                {
                    if (item.sValue.Equals(value))
                    {
                        item.sBackColor = "#0090d5";
                        item.sForeColor = "#FFF";

                        nowCategory = value;
                        pageIndex = 1;
                        GetData();
                    }
                    else
                    {
                        item.sBackColor = "#e5e5e5";
                        item.sForeColor = "#333";
                    }
                }
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
                    req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.FAQ);
                    req.lang = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];
                    req.pageIndex = pageIndex;
                    if (!nowCategory.Equals("0"))
                        req.catId = nowCategory;
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
                                faqList = new ObservableCollection<list>();
                                foreach (ResponseSelectBoardListModel item in res.data.list)
                                {
                                    string step1 = item.contentMsg.Replace("\",=\"\"", "',");
                                    string step2 = step1.Replace("=\"\"", "");
                                    string stepComp = step2.Replace(" \" ", "'");
                                    faqList.Add(ViewModelSource.Create(() => new list() { num = item.no, title = item.boardTitle, type=item.catId, date = item.regDt, content = stepComp }));
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

        public virtual string type { get; set; }

        public virtual string date { get; set; }

        public virtual string content { get; set; }
    }

    public class btnFAQ
    {
        public virtual string sName { get; set; }

        public virtual string sValue { get; set; }

        public virtual string sBackColor { get; set; }

        public virtual string sForeColor { get; set; }
    }
}