using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingCoinAutoTradeSelModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestTradingCoinAutoTradeSelModel() : base("bt.trade.getAutoCoin.dp/proc.go") { }
    }
}