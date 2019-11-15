using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingSearchSellPriceModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 판매통화
        /// </summary>
        public string sellCurcyCd { get; set; }

        public RequestTradingSearchSellPriceModel() : base("bt.trade.sellMenuView.dp/proc.go") { base.RtpCall(this); }
    }
}