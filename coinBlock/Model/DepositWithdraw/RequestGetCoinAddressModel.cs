using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestGetCoinAddressModel : RequestBaseModel
    {
        /// <summary>
        /// 회원 계정
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 통화코드
        /// </summary>
        public string curcyCd { get; set; }
        /// <summary>
        /// 아이피
        /// </summary>
        public string regIp { get; set; }

        public RequestGetCoinAddressModel() : base("bt.deposit.getCnAcc.dp/proc.go") { base.RtpCall(this); }
    }
}