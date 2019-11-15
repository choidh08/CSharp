using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestTransferCoinModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자 아이디
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 송금액
        /// </summary>
        public decimal wdrReqAmt { get; set; }
        /// <summary>
        /// 송금 수수료
        /// </summary>
        public decimal cnSndFee { get; set; }
        /// <summary>
        /// 등록IP
        /// </summary>
        public string regIp { get; set; }
        /// <summary>
        /// 통화종류
        /// </summary>
        public string cnKndCd { get; set; }
        /// <summary>
        /// 송금할 주소
        /// </summary>
        public string wdrWletAdr { get; set; }
        /// <summary>
        /// 데스티네이션태그
        /// </summary>
        public string destiTag { get; set; }

        public string langCd { get; set; }

        public RequestTransferCoinModel() : base("bt.withdrow.coin.dp/proc.go") { }
    }
}