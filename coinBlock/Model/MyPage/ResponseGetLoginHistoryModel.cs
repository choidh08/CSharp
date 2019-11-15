using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.MyPage
{
    public class ResponseGetLoginHistoryModel : ResponseBaseModel
    {
        public ResponseGetLoginHistoryDataModel data { get; set; }
        public ResponseGetLoginHistoryModel()
        {
            data = new ResponseGetLoginHistoryDataModel();
        }
    }

    public class ResponseGetLoginHistoryDataModel
    {
        public virtual int pageIndex { get; set; }
        public virtual int pageSize { get; set; }

        public ObservableCollection<ResponseGetLoginHistoryListModel> list { get; set; }
        public ResponseGetLoginHistoryDataModel()
        {
            list = new ObservableCollection<ResponseGetLoginHistoryListModel>();
        }
    }

    public class ResponseGetLoginHistoryListModel
    {
        public virtual int no { get; set; }
        public virtual string userEmail { get; set; }
        public virtual string loginTme { get; set; }
        public virtual string brwsCd { get; set; }
        public virtual string loginYn { get; set; }
        public virtual string connIp { get; set; }
        public virtual string location { get; set; }
    }
}