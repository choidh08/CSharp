using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestCoinPayInfoModel : RequestBaseModel
    {
        public virtual string cnKndCd { get; set; }

        public RequestCoinPayInfoModel() : base("bt.getCoinPayInfo.dp/proc.go") { }
    }
}