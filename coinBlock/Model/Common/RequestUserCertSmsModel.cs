using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestUserCertSmsModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string code { get; set; }

        public RequestUserCertSmsModel() : base("bt.auth.userCertSms.dp/proc.go") { }
    }
}