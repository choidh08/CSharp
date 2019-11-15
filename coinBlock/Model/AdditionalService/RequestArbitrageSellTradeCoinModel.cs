using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageSellTradeCoinModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AA-005";
        public virtual string userEmail { get; set; }
        public virtual string regIp { get; set; }
        public virtual string type { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual string lowId { get; set; }
        public virtual string highId { get; set; }

        public RequestArbitrageSellTradeCoinModel() : base("bt.arbitrage.reqAbtrgSellCoin.dp/proc.go") { }
    }
}