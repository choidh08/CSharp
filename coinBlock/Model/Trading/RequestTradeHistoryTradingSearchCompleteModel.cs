using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradeHistoryTradingSearchCompleteModel : RequestBaseModel
    {
        /// <summary>
        /// 통화 종류(01:BTC,02:ETH,03:BCH,04:XRP)
        /// </summary>
        public string curcyCd { get; set; }
        /// <summary>
        /// 리스트갯수
        /// </summary>
        public int listSize { get; set; }

        public string mkState { get; set; }

        public RequestTradeHistoryTradingSearchCompleteModel() : base("bt.trade.compTradeList.dp/proc.go") { this.RtpCall(this); }
    }
}
