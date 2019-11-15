using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageGetSignInfoModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AC-004";

        public string userEmail { get; set; }

        public RequestArbitrageGetSignInfoModel() : base("bt.arbitrage.getExchageSignInfo.dp/proc.go") { }
    }
}