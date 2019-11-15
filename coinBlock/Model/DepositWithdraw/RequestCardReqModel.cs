using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardReqModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string postCd { get; set; }

        public virtual string adrs { get; set; }

        public virtual string dtlAdrs { get; set; }

        public virtual string sendMthCd { get; set; }

        public virtual string reqType { get; set; }

        public virtual string cardActCode { get; set; }

        public virtual string inMthCd { get; set; }

        public virtual string userMobile { get; set; }

        public virtual string userNm { get; set; }

        public virtual decimal amount { get; set; }

        public RequestCardReqModel() : base("card.insReqCardInfo.dp/proc.go") { }
    }
}