using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestSmsCodeModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestSmsCodeModel() : base("bt.auth.sendSmsAuthCode.dp/proc.go") { }
    }
}