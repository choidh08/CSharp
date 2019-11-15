using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingAbleBuyModel : RequestBaseModel
    {
        /// <summary>
        /// 구매통화
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 사용자 메일
        /// </summary>
        public virtual string userEmail { get; set; }

        public RequestTradingAbleBuyModel() : base("bt.trade.buyMenuView.dp/proc.go") { base.RtpCall(this); }
    }
}
