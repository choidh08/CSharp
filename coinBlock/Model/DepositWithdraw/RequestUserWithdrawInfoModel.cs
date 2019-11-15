using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestUserWithdrawInfoModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestUserWithdrawInfoModel() : base("bt.withdrow.krwView.dp/proc.go") { this.RtpCall(this); }
    }
}