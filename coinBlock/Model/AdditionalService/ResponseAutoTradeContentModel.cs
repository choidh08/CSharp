using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseAutoTradeContentModel : ResponseBaseModel
    {
        public ResponseAutoTradeContentDataModel data { get; set; }
        public ResponseAutoTradeContentModel()
        {
            data = new ResponseAutoTradeContentDataModel();
        }
    }

    public class ResponseAutoTradeContentDataModel
    {
        public virtual string cnKndCd { get; set; }
        public virtual string userEmail { get; set; }
        public virtual string reqDt { get; set; }
        public virtual string endDt { get; set; }
        public virtual decimal? payAmt { get; set; }
    }
}