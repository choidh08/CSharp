using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestBankModel : RequestBaseModel
    {
        public string cmmCd { get; set; }

        public RequestBankModel() : base ("bt.withdrow.getBankList.dp/proc.go") { }
    }
}