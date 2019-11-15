using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageBuyTradeCoinModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AA-001";
        public virtual string userEmail { get; set; }
        public virtual string regIp { get; set; }
        public virtual string type { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual string lowId { get; set; }
        public virtual string highId { get; set; }

        public RequestArbitrageBuyTradeCoinModel() : base("bt.arbitrage.reqAbtrgBuyCoin.dp/proc.go") { }
    }
}