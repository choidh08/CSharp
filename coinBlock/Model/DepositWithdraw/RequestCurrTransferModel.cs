using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCurrTransferModel : RequestBaseModel
    {
        public virtual string fromUserEmail { get; set; }

        public virtual string toUserEmail { get; set; }

        public virtual string userMobile { get; set; }

        public virtual decimal wdrPrc { get; set; }

        public virtual string regIp { get; set; }

        public RequestCurrTransferModel() : base("bt.withdrow.sendCash.dp/proc.go") { }
    }
}