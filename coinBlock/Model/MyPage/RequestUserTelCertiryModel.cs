using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestUserTelCertiryModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestUserTelCertiryModel() : base("bt.getUserCertification.dp/proc.go") { }
    }
}