using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    [POCOViewModel]
    public class RequestCoinAddressTableModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string userEmail { get; set; }

        public RequestCoinAddressTableModel() : base("bt.deposit.getCnAccList.dp/proc.go") { this.RtpCall(this); }
    }
}