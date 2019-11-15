using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingTodaySellAmtModel : ResponseBaseModel
    {
        public ResponseTradingTodaySellAmtDataModel data { get; set; }
        public ResponseTradingTodaySellAmtModel()
        {
            data = new ResponseTradingTodaySellAmtDataModel();
        }

        public class ResponseTradingTodaySellAmtDataModel
        {
            public virtual string failCd { get; set; }

            public virtual decimal? sellAmt { get; set; }
        }
    }
}