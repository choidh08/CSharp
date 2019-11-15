using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestTrustInterestListModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public int listSize { get; set; }

        public RequestTrustInterestListModel() : base("bt.trust.interestList.dp/proc.go") { }
    }
}