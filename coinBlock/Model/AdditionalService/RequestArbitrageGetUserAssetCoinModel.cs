using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageGetUserAssetCoinModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AA-006";

        public virtual string userEmail { get; set; }

        public virtual string regIp { get; set; }

        public virtual string isAllYn { get; set; }

        public virtual string targetId { get; set; }

        public virtual string cnKndNm { get; set; }

        public RequestArbitrageGetUserAssetCoinModel() : base("bt.arbitrage.getUserTotalCoin.dp/proc.go") { }
    }
}