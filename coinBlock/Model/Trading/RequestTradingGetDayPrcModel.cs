using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestTradingGetDayPrcModel : RequestBaseModel
    {
        public virtual int pageIndex { get; set; }

        public virtual string cnKndCd { get; set; }

        public virtual string stdDate { get; set; }

        public virtual string endDate { get; set; }

        public virtual string mkState { get; set; }

        public RequestTradingGetDayPrcModel() : base("bt.getCoinDailyPrc.dp/proc.go") { }
    }
}