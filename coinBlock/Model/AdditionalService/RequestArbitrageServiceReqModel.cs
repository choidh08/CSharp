using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageServiceReqModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AC-007";
        public virtual string userEmail { get; set; }
        public virtual string cnKndCd { get; set; }   

        public RequestArbitrageServiceReqModel() : base("bt.arbitrage.insService.dp/proc.go") { }
    }
}