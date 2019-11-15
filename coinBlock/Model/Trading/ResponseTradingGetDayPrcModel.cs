using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace coinBlock.Model.Trading
{
    public class ResponseTradingGetDayPrcModel : ResponseBaseModel
    {
        public ResponseTradingGetDayPrcDataModel data { get; set; }
        public ResponseTradingGetDayPrcModel()
        {
            data = new ResponseTradingGetDayPrcDataModel();
        }
    }

    public class ResponseTradingGetDayPrcDataModel
    {
        public virtual int pageIndex { get; set; }
        public virtual int pageSize { get; set; }

        public ObservableCollection<ResponseTradingGetDayPrcListModel> list { get; set; }
        public ResponseTradingGetDayPrcDataModel()
        {
            list = new ObservableCollection<ResponseTradingGetDayPrcListModel>();
        }
    }

    public class ResponseTradingGetDayPrcListModel
    {
        public virtual decimal? prcPer { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual string regDt { get; set; }
        public virtual decimal? fstPrc { get; set; }
        public virtual decimal? minPrc { get; set; }
        public virtual decimal? maxPrc { get; set; }
        public virtual decimal? lstPrc { get; set; }
        public virtual decimal? totAmt { get; set; }
    }
}