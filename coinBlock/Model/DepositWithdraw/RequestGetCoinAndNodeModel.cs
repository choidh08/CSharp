using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestGetCoinAndNodeModel : RequestBaseModel
    {
        public string cnKndCd { get; set; }
        public string langCd { get; set; }
        public string userEmail { get; set; }

        public RequestGetCoinAndNodeModel() : base("bt.getCoinNote.dp/proc.go") { }
    }
}