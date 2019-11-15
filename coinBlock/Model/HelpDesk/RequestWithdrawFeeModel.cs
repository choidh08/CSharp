using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestWithdrawFeeModel : RequestBaseModel
    {
        public RequestWithdrawFeeModel() : base("bt.withdrow.getWithFee.dp/proc.go") { }
    }
}