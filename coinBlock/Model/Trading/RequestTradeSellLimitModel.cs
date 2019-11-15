using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradeSellLimitModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string curcyCd { get; set; }

        public string mkState { get; set; }

        public RequestTradeSellLimitModel() : base("bt.auth.userTradeCheck.dp/proc.go") { }
    }
}