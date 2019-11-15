using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Trading
{
    public class ResponseGetAskingModel : ResponseBaseModel
    {
        public ResponseGetAskingDataModel data { get; set; }
        public ResponseGetAskingModel()
        {
            data = new ResponseGetAskingDataModel();
        }
    }

    public class ResponseGetAskingDataModel
    {
        public virtual string marketCd { get; set; }
        public virtual string marketNm { get; set; }
        public virtual string failCd { get; set; }
        public virtual ObservableCollection<ResponseGetAskingListModel> list { get; set; }

        public ResponseGetAskingDataModel()
        {
            list = new ObservableCollection<ResponseGetAskingListModel>();
        }
    }

    public class ResponseGetAskingListModel
    {
        public decimal? minAmt { get; set; }
        public decimal? maxAmt { get; set; }
        public decimal asking { get; set; }
    }
}