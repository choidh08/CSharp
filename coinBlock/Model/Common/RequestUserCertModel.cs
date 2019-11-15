using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestUserCertModel : RequestBaseModel
    {

        public virtual string userEmail { get; set; }
        public virtual string emailCertYn { get; set; }
        public virtual string smsCertYn { get; set; }
        public virtual string otpSerial { get; set; }
        public virtual string kycCertYn { get; set; }
        public virtual string otpCertYn { get; set; }
        public virtual string accCertYn { get; set; }
        public virtual string userInfoYn { get; set; }

        public RequestUserCertModel() : base("bt.uptUserAuthYn.dp/proc.go") { }
    }
}