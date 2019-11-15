using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestCertFileUploadModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string fileSn { get; set; }
        public virtual string certCode { get; set; }

        public RequestCertFileUploadModel() : base("bt.auth.uploadKycCertFileOne.dp/proc.go") { }
    }
}