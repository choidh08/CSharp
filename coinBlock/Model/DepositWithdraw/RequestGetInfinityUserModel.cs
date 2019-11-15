using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestGetInfinityUserModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestGetInfinityUserModel() : base("bt.trade.getFreeWithdraw.dp/proc.go") { }
    }
}