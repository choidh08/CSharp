using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestCertDataSubmitModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string userMobile { get; set; }
        public virtual string certGrade { get; set; }
        public virtual string certSubGrade { get; set; }
        public virtual string certMsg { get; set; }

        public RequestCertDataSubmitModel() : base("bt.auth.InsCertInfo.dp/proc.go") { }
    }
}