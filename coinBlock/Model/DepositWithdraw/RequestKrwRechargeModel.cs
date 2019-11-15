using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestKrwRechargeModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자이메일
        /// </summary>
        public string userEmail { get; set; }
        public string payKndCd { get; set; }

        public RequestKrwRechargeModel() : base("bt.deposit.krwList.dp/proc.go") { base.RtpCall(this); }
    }
}