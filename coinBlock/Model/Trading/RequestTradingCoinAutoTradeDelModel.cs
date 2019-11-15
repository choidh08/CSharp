using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingCoinAutoTradeDelModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestTradingCoinAutoTradeDelModel() : base("bt.trade.delAutoCoin.dp/proc.go") { }
    }
}