using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestOtpCheckModel : RequestBaseModel
    {
        public string encodedKey { get; set; }

        public string authCode { get; set; }

        public RequestOtpCheckModel() : base("otp.chkAuthKey.dp/proc.go") { }

    }
}