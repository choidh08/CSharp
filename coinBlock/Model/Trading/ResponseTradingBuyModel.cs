using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingBuyModel : ResponseBaseModel
    {
        public ResponseTradingBuyDataModel data { get; set; }
        public ResponseTradingBuyModel()
        {
            data = new ResponseTradingBuyDataModel();
        }
    }

    public class ResponseTradingBuyDataModel
    {
        public virtual string failCd { get; set; }
    }
}
