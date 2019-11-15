using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestAlarmContentModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestAlarmContentModel() : base("bt.getPushYn.dp/proc.go") { }
    }
}