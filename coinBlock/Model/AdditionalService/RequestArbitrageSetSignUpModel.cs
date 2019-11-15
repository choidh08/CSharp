using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageSetSignUpModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AC-005";

        public string userEmail { get; set; }

        public RequestArbitrageSetSignUpModel() : base("bt.arbitrage.exchageSignUp.dp/proc.go") { }
    }
}