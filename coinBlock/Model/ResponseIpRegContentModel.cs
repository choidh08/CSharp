using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model
{
    public class ResponseIpRegContentModel : ResponseBaseModel
    {
        public ResponseIpRegContentDataModel data { get; set; }
        public ResponseIpRegContentModel()
        {
            data = new ResponseIpRegContentDataModel();
        }
    }

    public class ResponseIpRegContentDataModel
    {
        public ObservableCollection<ResponseIpRegContentListModel> list { get; set; }
        public ResponseIpRegContentDataModel()
        {
            list = new ObservableCollection<ResponseIpRegContentListModel>();
        }
    }
    public class ResponseIpRegContentListModel
    {
        public virtual string userEmail { get; set; }

        public virtual string ip { get; set; }

        public virtual string limtHr { get; set; }

        public virtual string loginTm { get; set; }

        public virtual string stdDt { get; set; }

        public virtual string endDt { get; set; }
    }
}