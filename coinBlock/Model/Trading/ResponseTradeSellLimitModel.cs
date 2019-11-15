using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class ResponseTradeSellLimitModel : ResponseBaseModel
    {
        public ResponseTradeSellLimitDataModel data { get; set; }
        public ResponseTradeSellLimitModel()
        {
            data = new ResponseTradeSellLimitDataModel();
        }
    }

    public class ResponseTradeSellLimitDataModel
    {
        public virtual string tradeType { get; set; }
    }
}