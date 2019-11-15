using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class RequestAutoServiceReqMdoel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string cnKndCd { get; set; }

        public RequestAutoServiceReqMdoel() : base("bt.extra.insAutoTradeService.dp/proc.go") { }
    }
}