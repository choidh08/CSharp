using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestPinNumberCheckModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestPinNumberCheckModel() : base("bt.auth.pinResetCheck.dp/proc.go") { }
    }
}