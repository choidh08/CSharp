using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestLendingInfoModel : RequestBaseModel
    {
        public string cnKndCd { get; set; }

        public RequestLendingInfoModel() : base("bt.lending.Info.dp/proc.go") { }
    }
}