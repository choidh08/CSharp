using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Common
{
    public class ResponseGetPopupListModel : ResponseBaseModel
    {
        public ResponseGetPopupListDataModel data { get; set; }
        public ResponseGetPopupListModel()
        {
            data = new ResponseGetPopupListDataModel();
        }
    }

    public class ResponseGetPopupListDataModel
    {
        public virtual int listCnt { get; set; }
        public virtual ObservableCollection<ResponseGetPopupListListModel> list { get; set; }
        public ResponseGetPopupListDataModel()
        {
            list = new ObservableCollection<ResponseGetPopupListListModel>();
        }
    }

    public class ResponseGetPopupListListModel
    {
        public string atchFileId { get; set; }
        public string popUrl { get; set; }
    }
}