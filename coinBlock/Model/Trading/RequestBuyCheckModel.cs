using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestBuyCheckModel : RequestBaseModel
    {
        /// <summary>
        /// 통화코드
        /// </summary>
        public string mkKndCd { get; set; }
        //마켓 코드
        public string cnKndCd { get; set; }

        public decimal tradePrc { get; set; }

        public RequestBuyCheckModel() : base("bt.buyCheck.dp/proc.go") { this.RtpCall(this); }
    }
}