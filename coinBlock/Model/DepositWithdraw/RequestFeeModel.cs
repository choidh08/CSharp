using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestFeeModel : RequestBaseModel
    {
        public RequestFeeModel() : base("bt.withdrow.getWithFee.dp/proc.go") { }
    }
}