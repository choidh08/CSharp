using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.Common;
using coinBlock.Utility;
using coinBlock.Views.Common;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class CoinTrendHelpDeskViewModel
    {
        public virtual ObservableCollection<list> CoinTrend { get; set; }

        public virtual Visibility listVisible { get; set; } = Visibility.Visible;
        public virtual Visibility emptyVisible { get; set; } = Visibility.Collapsed;

        public virtual int pageIndex { get; set; } = 1;
        public virtual int pageSize { get; set; } = 1;

        public CoinTrendHelpDeskViewModel()
        {

        }

        public static CoinTrendHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new CoinTrendHelpDeskViewModel());
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
                using (RequestSelectBoardModel req = new RequestSelectBoardModel())
                {
                    req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.Coin);
                    req.lang = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];
                    req.pageIndex = pageIndex;

                    using (ResponseSelectBoardModel res = await WebApiLib.AsyncCall<ResponseSelectBoardModel, RequestSelectBoardModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.list.Count > 0)
                            {
                                CoinTrend = new ObservableCollection<list>();
                                foreach (ResponseSelectBoardListModel item in res.data.list)
                                {
                                    string step1 = item.contentMsg.Replace("\",=\"\"", "',");
                                    string step2 = step1.Replace("=\"\"", "");
                                    string stepComp = step2.Replace(" \" ", "'");
                                    CoinTrend.Add(ViewModelSource.Create(() => new list() { num = item.no, title = item.boardTitle, date = item.regDt, content = stepComp }));
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

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}