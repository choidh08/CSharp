using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model
{
    public class ResponseGetMarketLimitModel : ResponseBaseModel
    {
        public ResponseGetMarketLimitDataModel data { get; set; }
        public ResponseGetMarketLimitModel()
        {
            data = new ResponseGetMarketLimitDataModel();
        }
    }

    public class ResponseGetMarketLimitDataModel
    {                
        public ObservableCollection<ResponseGetMarketLimitListModel> list { get; set; }
        public ResponseGetMarketLimitDataModel()
        {
            list = new ObservableCollection<ResponseGetMarketLimitListModel>();
        }
    }

    public class ResponseGetMarketLimitListModel
    {
        public virtual string marketCd { get; set; }
        public virtual ObservableCollection<ResponseGetMarketLimitContentModel> list { get; set; }
    }

    public class ResponseGetMarketLimitContentModel
    {
        public virtual string curcyNm { get; set; }
        public virtual string curcyCd { get; set; }
        public virtual decimal buyLmt1Once { get; set; }
        public virtual decimal buyLmt1Day { get; set; }
        public virtual decimal selLmt1Once { get; set; }
        public virtual decimal selLmt1Day { get; set; }
    }
}