using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingAbleSellModel : ResponseBaseModel
    {
        public virtual ResponseTradingAbleSellDataModel data { get; set; }

        public ResponseTradingAbleSellModel()
        {
            data = new ResponseTradingAbleSellDataModel();
        }
    }

    public class ResponseTradingAbleSellDataModel
    {
        /// <summary>
        /// 판매가능수량
        /// </summary>
        public virtual decimal avaSellAmt { get; set; }
        /// <summary>
        /// 미체결최고 구매가
        /// </summary>
        public virtual decimal maxPrc { get; set; }
    }
}