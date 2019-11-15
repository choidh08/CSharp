using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestSendCertSmsModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string userMobile { get; set; }

        public RequestSendCertSmsModel() : base("bt.auth.sendCertSms.dp/proc.go") { }
    }
}