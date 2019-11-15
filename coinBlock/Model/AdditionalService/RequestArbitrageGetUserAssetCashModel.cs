using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageGetUserAssetCashModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AA-002";

        public virtual string userEmail { get; set; }

        public virtual string regIp { get; set; }

        public virtual string isAllYn { get; set; }

        public virtual string targetId { get; set; }

        public virtual string cnKndNm { get; set; }

        public RequestArbitrageGetUserAssetCashModel() : base("bt.arbitrage.getUserTotalCash.dp/proc.go") { }
    }
}