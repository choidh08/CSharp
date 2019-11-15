using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingCancelModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 주문번호
        /// </summary>
        public string orderNo { get; set; }

        public RequestTradingCancelModel() : base("bt.trade.tradeCancel.dp/proc.go") { base.RtpCall(this); }
    }
}
