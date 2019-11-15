using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingRepayPopModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string renDftCode { get; set; }

        public string regIp { get; set; }

        public RequestLendingRepayPopModel() : base("bt.lending.repayment.dp/proc.go") { }
    }
}