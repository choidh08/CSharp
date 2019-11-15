using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestAccInfoModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestAccInfoModel() : base("bt.deposit.AccInfo.dp/proc.go") { }
    }
}