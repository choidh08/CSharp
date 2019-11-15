using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestTrustContentModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public int listSize { get; set; }

        public RequestTrustContentModel() : base("bt.trust.list.dp/proc.go") { }
    }
}