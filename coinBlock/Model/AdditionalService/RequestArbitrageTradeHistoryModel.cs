using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestArbitrageTradeHistoryModel : RequestBaseModel
    {
        public virtual string TC { get; set; } = "AA-004";

        public virtual string userEmail { get; set; }

        public virtual string cnKndNm { get; set; }

        public virtual int listSize { get; set; }

        public RequestArbitrageTradeHistoryModel() : base("bt.arbitrage.reqAbtrgHist.dp/proc.go") { }
    }
}