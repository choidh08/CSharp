using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingSearchBuyPriceModel : ResponseBaseModel
    {
        public ResponseTradingSearchBuyPriceDataModel data { get; set; }

        public ResponseTradingSearchBuyPriceModel()
        {
            data = new ResponseTradingSearchBuyPriceDataModel();
        } 
    }

    public class ResponseTradingSearchBuyPriceDataModel
    {
        public virtual string price { get; set; }
        public virtual string minPrc { get; set; }
    }
}
