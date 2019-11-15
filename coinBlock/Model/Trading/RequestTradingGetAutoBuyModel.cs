using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingGetAutoBuyModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string cnKndCd { get; set; }

        public string udFlag { get; set; }

        public string mkState { get; set; }

        public RequestTradingGetAutoBuyModel() : base("bt.trade.getAutoBuy.dp/proc.go") { }
    }
}