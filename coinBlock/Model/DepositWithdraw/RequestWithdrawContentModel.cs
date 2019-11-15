using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestWithdrawContentModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string payMthCd { get; set; }

        public RequestWithdrawContentModel() : base("bt.withdrow.cashList.dp/proc.go") { }
    }
}