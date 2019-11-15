using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingBuyModel : RequestBaseModel
    {
        /// <summary>
        /// 구매타입 구분(Y:지정가 N:시장가)
        /// </summary>
        public string apntPhsYn { get; set; }
        /// <summary>
        /// 거내래용 CMMC00000000178: 지정가, CMMC00000000179:시장가, CMMC00000000180:자동거래, CMMC00000000181:코인교환
        /// </summary>
        public string apntPhsCd { get; set; }
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 구매통화 종류
        /// </summary>
        public string buyCurcyCd { get; set; }
        /// <summary>
        /// 결제통화 종류
        /// </summary>
        public string payCurcyCd { get; set; }
        /// <summary>
        /// 사용금액
        /// </summary>
        public decimal? usePrc { get; set; }
        /// <summary>
        /// 포인트사용금액
        /// </summary>
        public decimal? usePointPrc { get; set; }
        /// <summary>
        /// 주문가격
        /// </summary>
        public string phsPrc { get; set; }
        /// <summary>
        /// 주문수량
        /// </summary>
        public string phsAmt { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string regIp { get; set; }

        public string mkState { get; set; }

        public string tradeType { get; set; }

        public string autoTradeType { get; set; }

        public string curcyCd { get; set; }

        public RequestTradingBuyModel() : base("bt.trade.buyOrderMQ.dp/proc.go") { base.RtpCall(this); }
    }
}
