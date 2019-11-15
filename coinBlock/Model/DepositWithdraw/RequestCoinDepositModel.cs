using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCoinDepositModel : RequestBaseModel
    {
        /// <summary>
        /// 회원 계정
        /// </summary>
        public string userEmail { get; set; }

        public RequestCoinDepositModel() : base("bt.deposit.cnList.dp/proc.go") { base.RtpCall(this); }
    }
}
