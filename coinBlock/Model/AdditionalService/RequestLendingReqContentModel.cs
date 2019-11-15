using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingReqContentModel : RequestBaseModel
    {
        public string userEmail { get; set; }
        public decimal listSize { get; set; }

        public RequestLendingReqContentModel() : base("bt.lending.ApplyList.dp/proc.go") { }
    }
}