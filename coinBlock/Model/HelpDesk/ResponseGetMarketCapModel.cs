using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.HelpDesk
{
    public class ResponseGetMarketCapModel : ResponseBaseModel
    {
        public ResponseGetMarketCapDataModel data { get; set; }
        public ResponseGetMarketCapModel()
        {
            data = new ResponseGetMarketCapDataModel();
        }
    }

    public class ResponseGetMarketCapDataModel
    {
        public ObservableCollection<ResponseGetMarketCapListModel> list { get; set; }
        public ResponseGetMarketCapDataModel()
        {
            list = new ObservableCollection<ResponseGetMarketCapListModel>();
        }
    }

    public class ResponseGetMarketCapListModel
    {
        public virtual string coinName { get; set; }
        public virtual decimal marketCap { get; set; }
        public virtual decimal price { get; set; }
        public virtual decimal volume { get; set; }
        public virtual decimal cirSupply { get; set; }
        public virtual decimal change { get; set; }
        public virtual string symbol { get; set; }
    }
}