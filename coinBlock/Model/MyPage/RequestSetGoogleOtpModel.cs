using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestSetGoogleOtpModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string authCode { get; set; }
        public virtual string encodedKey { get; set; }
        public virtual string isUpt { get; set; }

        public RequestSetGoogleOtpModel() : base("otp.chkAuthKey.dp/proc.go") { }
    }
}