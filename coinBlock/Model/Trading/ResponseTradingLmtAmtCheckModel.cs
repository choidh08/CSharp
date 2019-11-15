using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingLmtAmtCheckModel : ResponseBaseModel
    {
        public ResponseTradingLmtAmtCheckDataModel data { get; set; }
        public ResponseTradingLmtAmtCheckModel()
        {
            data = new ResponseTradingLmtAmtCheckDataModel();
        }
    }

    public class ResponseTradingLmtAmtCheckDataModel
    {
        public virtual string failCd { get; set; }

        public virtual decimal buyLmt1Once { get; set; }

        public virtual decimal buyLmt1Day { get; set; }

        public virtual decimal selLmt1Once { get; set; }

        public virtual decimal selLmt1Day { get; set; }
    }
}