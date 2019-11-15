using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestGetTradeHistoryModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string hisCode { get; set; }

        public virtual string orderState { get; set; }

        public virtual string curcyCd { get; set; }

        public virtual int pageIndex { get; set; }

        public virtual string stdDate { get; set; }

        public virtual string endDate { get; set; }

        public virtual string mkState { get; set; }

        public virtual string key01 { get; set; }             

        public RequestGetTradeHistoryModel() : base("bt.trade.tradeHistory.dp/proc.go") { }
    }
}