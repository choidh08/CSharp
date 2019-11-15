using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model
{
    public class ResponseBookMarkMenuModel : ResponseBaseModel
    {
        public virtual ResponseBookMarkMenuDataModel data { get; set; }
        public ResponseBookMarkMenuModel()
        {
            data = new ResponseBookMarkMenuDataModel();
        }
    }

    public class ResponseBookMarkMenuDataModel
    {
        public virtual ObservableCollection<ResponseBookMarkMenuListModel> list { get; set; }
        public ResponseBookMarkMenuDataModel()
        {
            list = new ObservableCollection<ResponseBookMarkMenuListModel>();
        }
    }

    public class ResponseBookMarkMenuListModel
    {
        public virtual string menuCd { get; set; }

        public virtual int sn { get; set; }
    }

}