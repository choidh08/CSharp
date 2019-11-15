using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingTradeHistoryModel :RequestBaseModel
    {
        /// <summary>
        /// 사용자 메일
        /// </summary>
        public virtual string userEmail { get; set; }
        /// <summary>
        /// 통화 종료
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 리스트 개수
        /// </summary>
        public virtual int listSize { get; set; }

        public virtual string mkState { get; set; }

        public RequestTradingTradeHistoryModel() : base("bt.trade.list.dp/proc.go") { base.RtpCall(this); }
    }
}
