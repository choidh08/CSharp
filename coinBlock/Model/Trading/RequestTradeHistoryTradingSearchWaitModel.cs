using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradeHistoryTradingSearchWaitModel :RequestBaseModel
    {
        /// <summary>
        /// 통화 종류(01:BTC,02:ETH,03:BCH,04:XRP)
        /// </summary>
        public string curcyCd { get; set; }
        /// <summary>
        /// 거래 구분(구매대기,판매대기)
        /// </summary>
        public string tradeCd { get; set; }
        /// <summary>
        /// 리스트갯수
        /// </summary>
        public int listSize { get; set; }

        public string mkState { get; set; }

        public RequestTradeHistoryTradingSearchWaitModel() : base("bt.trade.waitTradeList.dp/proc.go") { this.RtpCall(this); } 
    }
}
