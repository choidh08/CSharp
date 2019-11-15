using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestAlarmModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; } 

        public int pageIndex { get; set; } = 1;

        public int pageUnit { get; set; } 

        public RequestAlarmModel() : base("bt.getPushList.dp/proc.go") { this.RtpCall(this); }
    }
}