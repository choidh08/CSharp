using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestTrustInsertModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual decimal reqAmt { get; set; }
        public virtual decimal month { get; set; }
        public virtual decimal rate { get; set; }
        public virtual decimal expInterest { get; set; }
        public virtual string reqDt { get; set; }
        public virtual string expireDt { get; set; }
        public virtual string regIp { get; set; }

        public RequestTrustInsertModel() : base("bt.trust.insTrustCoin.dp/proc.go") { }
    }
}