using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingCoinSwapModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 구매통화(받는 통화)
        /// </summary>
        public string buyCurcyCd { get; set; }
        /// <summary>
        /// 결제통화(파는통화)
        /// </summary>
        public string payCurcyCd { get; set; }
        /// <summary>
        /// 주문수량
        /// </summary>
        public string phsAmt { get; set; }
        /// <summary>
        /// 아이피
        /// </summary>
        public string regIp { get; set; }

        public RequestTradingCoinSwapModel() : base("bt.trade.coinSwapMQ.dp/proc.go") { this.RtpCall(this); }
    }
}