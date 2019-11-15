using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingRepaymentModel : RequestBaseModel
    {
        public string renDftCode { get; set; }

        public int listSize { get; set; }

        public RequestLendingRepaymentModel() : base("bt.lending.repaymentList.dp/proc.go") { }
    }
}