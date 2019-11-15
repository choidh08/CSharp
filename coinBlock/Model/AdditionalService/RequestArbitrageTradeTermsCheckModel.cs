using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageTradeTermsCheckModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AA-017";

        public string userEmail { get; set; }

        public RequestArbitrageTradeTermsCheckModel() : base("") { }
    }
}