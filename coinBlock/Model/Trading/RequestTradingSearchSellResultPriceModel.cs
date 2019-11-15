using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingSearchSellResultPriceModel :RequestBaseModel
    {
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 판매통화종류
        /// </summary>
        public string sellCurcyCd { get; set; }

        public RequestTradingSearchSellResultPriceModel() : base("bt.trade.recCoinView.dp/proc.go") { base.RtpCall(this); }
    }
}