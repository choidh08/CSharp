using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingGetAutoSellModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string cnKndCd { get; set; }

        public string udFlag { get; set; }

        public string mkState { get; set; }

        public RequestTradingGetAutoSellModel() : base("bt.trade.getAutoSell.dp/proc.go") { }
    }
}