using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Linq;

namespace coinBlock.Model
{
    public class RequestLoginFailModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestLoginFailModel() : base("bt.auth.loginFail.dp/proc.go") { base.RtpCall(this); }
    }
}