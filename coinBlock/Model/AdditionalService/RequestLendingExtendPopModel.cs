using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingExtendPopModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string mthCmt { get; set; }

        public string renDftCode { get; set; }

        public RequestLendingExtendPopModel() : base("bt.lending.extension.dp/proc.go") { }
    }
}