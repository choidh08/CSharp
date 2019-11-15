using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestTrustCancelModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string trustReqCode { get; set; }

        public string cancelDt { get; set; }

        public RequestTrustCancelModel() : base("bt.trust.releaseTrust.dp/proc.go") { }
    }
}