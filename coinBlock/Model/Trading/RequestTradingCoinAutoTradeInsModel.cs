using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingCoinAutoTradeInsModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 보유 코인
        /// </summary>
        public string selCnKndCd { get; set; }
        /// <summary>
        /// 보유코인 설정 당시 시세
        /// </summary>
        public decimal selCnPrc { get; set; }
        /// <summary>
        /// 보유코인 변동률
        /// </summary>
        public decimal selSetRate { get; set; }
        /// <summary>
        /// 보유코인 설정 시세
        /// </summary>
        public decimal selSetPrc { get; set; }
        /// <summary>
        /// 보유코인 판매 수량
        /// </summary>
        public decimal selAmt { get; set; }
        /// <summary>
        /// 교환코인코드
        /// </summary>
        public string chgCnKndCd { get; set; }
        /// <summary>
        /// 보유코인 설정 당시 시세
        /// </summary>
        public decimal chgCnPrc { get; set; }
        /// <summary>
        /// 교환코인 변동률
        /// </summary>
        public decimal chgSetRate { get; set; }
        /// <summary>
        /// 교환코인 설정시세
        /// </summary>
        public decimal chgSetPrc { get; set; }
        /// <summary>
        /// 교환 예상금액
        /// </summary>
        public decimal chgHopeAmt { get; set; }
        /// <summary>
        /// 등록IP
        /// </summary>
        public string regIp { get; set; }

        public RequestTradingCoinAutoTradeInsModel() : base("bt.trade.insAutoCoin.dp/proc.go") { }
    }
}