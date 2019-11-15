using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingDelAutoBuyModel : RequestBaseModel
    {
        public string userEmail { get; set; }
        public string cnKndCd { get; set; }
        public string udFlag { get; set; }
        public string mkState { get; set; }
        public RequestTradingDelAutoBuyModel() : base("bt.trade.delAutoBuy.dp/proc.go") { }
    }
}