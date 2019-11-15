using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestGetCoinInfoModel : RequestBaseModel
    {
        public virtual string langCd { get; set; }

        public virtual string cnKndCd { get; set; }

        public RequestGetCoinInfoModel() : base("bt.getCoinInfo.dp/proc.go") { }
    }
}