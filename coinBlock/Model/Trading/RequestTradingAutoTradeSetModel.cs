using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingAutoTradeSetModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string autoGubun { get; set; }

        public virtual string curcyCd { get; set; }

        public RequestTradingAutoTradeSetModel() : base("bt.autoTradeSet.dp/proc.go") { base.RtpCall(this); }
    }
}