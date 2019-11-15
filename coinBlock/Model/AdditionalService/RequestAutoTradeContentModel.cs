using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestAutoTradeContentModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public RequestAutoTradeContentModel() : base("bt.extra.selectAutoTrade.dp/proc.go") { }
    }
}