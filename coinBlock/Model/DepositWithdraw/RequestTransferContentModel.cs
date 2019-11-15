using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestTransferContentModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string cnKndCd { get; set; }

        public RequestTransferContentModel() : base("bt.withdrow.coinList.dp/proc.go") { }
    }
}