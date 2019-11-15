using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingTodaySellAmtModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string curcyCd { get; set; }

        public RequestTradingTodaySellAmtModel() : base("bt.trade.getTodaySellAmt.dp/proc.go") { }
    }
}