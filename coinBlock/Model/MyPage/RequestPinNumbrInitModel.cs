using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestPinNumbrInitModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string userPwd { get; set; }

        public RequestPinNumbrInitModel() : base("bt.auth.pinReset.dp/proc.go") { }
    }
}