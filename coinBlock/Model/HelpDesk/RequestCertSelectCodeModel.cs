using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestCertSelectCodeModel : RequestBaseModel
    {
        public virtual string cmmUpperCd { get; set; }

        public RequestCertSelectCodeModel() : base("bt.auth.getAuthCode.dp/proc.go") { }
    }
}