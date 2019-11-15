using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageGetAssetModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AB-001";

        public string userEmail { get; set; }
        /// <summary>
        /// BTC, ETH, ...
        /// </summary>
        public string cnKndNm { get; set; }
        /// <summary>
        /// 구매 : B, 판매 : S
        /// </summary>
        public string type { get; set; }

        public RequestArbitrageGetAssetModel() : base("bt.arbitrage.getAsset.dp/proc.go") { }
    }
}