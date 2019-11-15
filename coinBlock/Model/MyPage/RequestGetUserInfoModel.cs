using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestGetUserInfoModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public RequestGetUserInfoModel() : base("bt.getUserInfo.dp/proc.go") { }
    }
}