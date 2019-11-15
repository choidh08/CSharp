using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageSetMainExchangeModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AC-006";

        public string userEmail { get; set; }
        
        public string exchgeId { get; set; }

        public RequestArbitrageSetMainExchangeModel() : base("bt.arbitrage.setMainExchage.dp/proc.go") { }
    }
}