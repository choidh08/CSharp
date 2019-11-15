using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingTradeCancelModel : RequestBaseModel
    {
        /// <summary>
        /// 유저 이메일
        /// </summary>
        public virtual string userEmail { get; set; }
        /// <summary>
        /// 주문번호
        /// </summary>
        public virtual string orderNo { get; set; }

        public virtual string orderCd { get; set; }

        public virtual string regIp { get; set; }

        public virtual string mkState { get; set; }

        public RequestTradingTradeCancelModel() : base("bt.trade.tradeCancel.dp/proc.go") { base.RtpCall(this); }       
    }
}