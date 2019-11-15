using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingCoinToCoinBuyModel : RequestBaseModel
    {
        /// <summary>
        /// 구매하려는 통화
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 결제하려는 통화
        /// </summary>
        public virtual string payCurcyCd { get; set; }
        /// <summary>
        /// 사용자 Email
        /// </summary>
        public virtual string userEmail { get; set; }


        public RequestTradingCoinToCoinBuyModel() : base("bt.trade.maxBuyPrice.dp/proc.go") { base.RtpCall(this); }
    }
}