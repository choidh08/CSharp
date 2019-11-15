using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestGetLoginHistoryModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string stdDate { get; set; }
        public virtual string endDate { get; set; }
        public virtual string searchWrd { get; set; }
        public virtual int pageIndex { get; set; }

        public RequestGetLoginHistoryModel() : base("bt.auth.getLoginHistory.dp/proc.go") { }
    }
}