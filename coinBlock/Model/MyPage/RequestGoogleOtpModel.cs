using System;
using DevExpress.Mvvm;

namespace PlanBit.Model.MyPage
{
    public class RequestGoogleOtpModel : RequestBaseModel
    {
        public virtual string urlHost { get; set; } = "PlanBit";

        public RequestGoogleOtpModel() : base("otp.getQrAKey.dp/proc.go") { }
    }
}