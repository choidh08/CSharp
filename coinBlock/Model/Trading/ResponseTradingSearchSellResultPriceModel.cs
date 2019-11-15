using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingSearchSellResultPriceModel : ResponseBaseModel
    {
        public ResponseTradingSearchSellResultPriceDataModel data { get; set; }
        public ResponseTradingSearchSellResultPriceModel()
        {
            data = new ResponseTradingSearchSellResultPriceDataModel();
        }
    }

    public class ResponseTradingSearchSellResultPriceDataModel
    {
        /// <summary>
        /// 수령통화 종류
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 미체결 최저 판매가
        /// </summary>
        public virtual string minPrc { get; set; }
    }
}
