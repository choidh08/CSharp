using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageCoinPayInfoModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AC-003";
        public virtual string cnKndCd { get; set; }

        public RequestArbitrageCoinPayInfoModel() : base("bt.arbitrage.getArbitrageCoinPayInfo.dp/proc.go") { }
    }
}