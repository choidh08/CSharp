using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingSearchSellPriceModel : ResponseBaseModel
    {
        public ResponseTradingSearchSellPriceDataModel data { get; set; }

        public ResponseTradingSearchSellPriceModel()
        {
            data = new ResponseTradingSearchSellPriceDataModel();
        }
    }

    public class ResponseTradingSearchSellPriceDataModel
    {
        /// <summary>
        /// 판매통화
        /// </summary>
        public virtual string sellCurcyCd { get; set; }
        /// <summary>
        /// 판매가능수량
        /// </summary>
        public virtual string avaSellAmt { get; set; }
        /// <summary>
        /// 미체결최고구매가
        /// </summary>
        public virtual string maxPrc { get; set; }
    }
}
