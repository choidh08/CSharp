using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestWithdrawMoneyModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public decimal wdrPrc { get; set; }

        public decimal cnSndFee { get; set; }

        public string regIp { get; set; }

        public string bankCd { get; set; }

        public string bankAccNo { get; set; }

        public string payMthCd { get; set; }

        public string cardNo { get; set; }

        public string langCd { get; set; }

        public RequestWithdrawMoneyModel() : base("bt.withdrow.cash.dp/proc.go") { }
    }
}