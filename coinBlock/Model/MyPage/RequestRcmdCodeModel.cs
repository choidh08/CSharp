using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestRcmdCodeModel : RequestBaseModel
    {
        public string brhCode { get; set; }

        public RequestRcmdCodeModel() : base("bt.auth.getRcmdCode.dp/proc.go") { }
    }
}