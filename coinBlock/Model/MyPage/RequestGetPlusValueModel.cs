using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestGetPlusValueModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public RequestGetPlusValueModel() : base("bt.trade.getPlusList.dp/proc.go") { }
    }
}