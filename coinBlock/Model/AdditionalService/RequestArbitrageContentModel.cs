using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    [POCOViewModel] 
    public class RequestArbitrageContentModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AC-008";
        public virtual string userEmail { get; set; }

        public RequestArbitrageContentModel() : base("bt.arbitrage.selectArbitrageChk.dp/proc.go") { }
    }
}