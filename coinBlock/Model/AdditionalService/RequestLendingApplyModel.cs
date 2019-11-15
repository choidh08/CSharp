using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingApplyModel : RequestBaseModel
    {
        public string userEmail { get; set; }
        public string cnKndCd { get; set; }
        public string applyDt { get; set; }
        public decimal applyAmt { get; set; }
        public string mthCmt { get; set; }
        public string regIp { get; set; }

        public RequestLendingApplyModel() : base("bt.lending.Apply.dp/proc.go") { }

    }
}