using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestGetMarketLimitModel : RequestBaseModel
    {
        public RequestGetMarketLimitModel() : base("bt.getMarketTradeCheck.dp/proc.go") { }
    }
}