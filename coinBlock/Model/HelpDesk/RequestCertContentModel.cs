using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestCertContentModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public RequestCertContentModel() : base("bt.auth.getCertList.dp/proc.go") { }
    }
}