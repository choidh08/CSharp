using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestGetTimePlusValueModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string stdDate { get; set; }
        public virtual string endDate { get; set; }

        public RequestGetTimePlusValueModel() : base("bt.trade.getTimeList.dp/proc.go") { }
    }
}