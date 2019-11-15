using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestGetAccountInfoModel : RequestBaseModel
    {
        /// <summary>
        /// 회원 계정
        /// </summary>
        public string userEmail { get; set; }

        public RequestGetAccountInfoModel() : base("bt.deposit.getAccList.dp/proc.go") { base.RtpCall(this); }
    }
}