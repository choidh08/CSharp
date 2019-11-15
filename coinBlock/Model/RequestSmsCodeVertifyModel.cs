using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestSmsCodeVertifyModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string authCode { get; set; }

        public RequestSmsCodeVertifyModel() : base("bt.auth.SmsAuthCodeVertify.dp/proc.go") { }
    }
}