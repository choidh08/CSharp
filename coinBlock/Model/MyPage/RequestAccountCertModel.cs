using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestAccountCertModel : RequestBaseModel
    {
        public string userEmail { get; set; }
        public string bankCd { get; set; }
        public string birthDay { get; set; }
        public string searchAcctNo { get; set; }
        public string userNm { get; set; }

        public RequestAccountCertModel() : base("bt.user.acountCk.dp/proc.go") { }
    }
}