using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestLmtWithdrowModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string curcyCd { get; set; }

        public RequestLmtWithdrowModel() : base("bt.withdrow.limitCheck.dp/proc.go") { }
    }
}