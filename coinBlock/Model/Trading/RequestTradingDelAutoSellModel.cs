using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingDelAutoSellModel : RequestBaseModel
    {
        public string userEmail { get; set; }
        public string cnKndCd { get; set; }
        public string udFlag { get; set; }
        public string mkState { get; set; }

        public RequestTradingDelAutoSellModel() : base ("bt.trade.delAutoSell.dp/proc.go") { }
    }
}