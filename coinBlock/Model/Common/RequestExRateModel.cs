using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestExRateModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public RequestExRateModel() : base("bt.trade.getExRate.dp/proc.go") { }
    }
}