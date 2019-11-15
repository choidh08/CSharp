using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingSellModel : RequestBaseModel
    {
        /// <summary>
        /// 판매구분(Y:지정가 N:시장가)
        /// </summary>
        public virtual string apntPhsYn { get; set; }
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public virtual string userEmail { get; set; }
        /// <summary>
        /// 판매통화 종류
        /// </summary>
        public virtual string sellCurcyCd { get; set; }
        /// <summary>
        /// 수령통화 종류
        /// </summary>
        public virtual string rcvCurcyCd { get; set; }
        /// <summary>
        /// 주문가격
        /// </summary>
        public virtual string phsPrc { get; set; }
        /// <summary>
        /// 주문수량
        /// </summary>
        public virtual string phsAmt { get; set; }
        /// <summary>
        /// 자동거래 구분(01:판매가, 02:손절가)
        /// </summary>
        public virtual string autoCd { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public virtual string regIp { get; set; }

        public virtual string mkState { get; set; }

        public string tradeType { get; set; }

        public string autoTradeType { get; set; }

        public string curcyCd { get; set; }

        public RequestTradingSellModel() : base("bt.trade.sellOrderMQ.dp/proc.go") { base.RtpCall(this); }
    }
}
