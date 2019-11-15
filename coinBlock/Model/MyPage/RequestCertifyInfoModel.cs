using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestCertifyInfoModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestCertifyInfoModel() : base("bt.auth.getUserAuth.dp/proc.go") { base.RtpCall(this); }
    }
}