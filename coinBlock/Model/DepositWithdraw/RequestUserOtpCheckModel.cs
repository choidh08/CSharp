using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestUserOtpCheckModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestUserOtpCheckModel() : base("bt.userOtpCheck.dp/proc.go") { }
    }
}