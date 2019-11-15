using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestTransferCheckLimitModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string cnKndCd { get; set; }

        public RequestTransferCheckLimitModel() : base("bt.withdrow.firstWithLimit.dp/proc.go") { }
    }
}