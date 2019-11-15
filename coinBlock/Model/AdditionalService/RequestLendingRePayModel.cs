using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingRepayModel : RequestBaseModel
    {
        public string renDftCode { get; set; }

        public RequestLendingRepayModel() : base("bt.lending.repaymentView.dp/proc.go") { }
    }
}