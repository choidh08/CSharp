using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingAbleSellModel : RequestBaseModel
    {
        /// <summary>
        /// 구매통화
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 사용자 메일
        /// </summary>
        public virtual string userEmail { get; set; }

        public RequestTradingAbleSellModel() : base("bt.trade.sellMenuView.dp/proc.go") { base.RtpCall(this); }
    }
}
