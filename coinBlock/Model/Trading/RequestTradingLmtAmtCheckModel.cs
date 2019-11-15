using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingLmtAmtCheckModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string mkState { get; set; }

        public string curcyCd { get; set; }

        public string tradeType { get; set; }

        public decimal phsAmt { get; set; }

        public RequestTradingLmtAmtCheckModel() : base("bt.trade.getAmtCheck.dp/proc.go") { }
    }
}