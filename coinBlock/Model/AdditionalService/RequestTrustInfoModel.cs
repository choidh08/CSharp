using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestTrustInfoModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string cnKndCd { get; set; }

        public RequestTrustInfoModel() :base("bt.trust.Info.dp/proc.go") { }
    }
}