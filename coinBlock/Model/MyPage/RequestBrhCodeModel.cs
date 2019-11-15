using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestBrhCodeModel : RequestBaseModel
    {
        public string natnCode { get; set; }

        public RequestBrhCodeModel() : base("bt.auth.getBrhCode.dp/proc.go") { }
    }
}