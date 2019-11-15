using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingSearchBuyPriceModel : RequestBaseModel
    {
        /// <summary>
        /// 통화
        /// </summary>
        public string curcyCd { get; set; }
        public string userEmail { get; set; }

        public RequestTradingSearchBuyPriceModel() : base("bt.trade.buyMenuView.dp/proc.go") { base.RtpCall(this); }
    }
}
