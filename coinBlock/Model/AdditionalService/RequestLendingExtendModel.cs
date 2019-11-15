using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingExtendModel : RequestBaseModel
    {
        public string renDftCode { get; set; }

        public RequestLendingExtendModel() : base("bt.lending.extensionView.dp/proc.go") { }
    }
}