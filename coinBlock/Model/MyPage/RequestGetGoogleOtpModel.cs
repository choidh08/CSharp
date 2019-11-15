using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestGetGoogleOtpModel : RequestBaseModel
    {
        public virtual string urlHost { get; set; } = "CoinBlock";

        public RequestGetGoogleOtpModel() : base("otp.getQrAKey.dp/proc.go") { }
    }
}