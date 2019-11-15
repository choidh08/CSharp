using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestPwdChangeModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public RequestPwdChangeModel() : base("bt.userPwdChange.dp/proc.go") { }
    }
}