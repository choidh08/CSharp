using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestRechargeKrwModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual decimal? crgPrc { get; set; }

        public virtual string regIp { get; set; }

        public RequestRechargeKrwModel() : base("bt.deposit.point.dp/proc.go") { base.RtpCall(this); }

    }
}