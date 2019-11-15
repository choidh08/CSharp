using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestDepositCardModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual decimal crgPrc { get; set; }

        public virtual string cardNum { get; set; }

        public virtual string cardPwd { get; set; }

        public virtual string cardCvc { get; set; }

        public virtual string cardDate { get; set; }

        public RequestDepositCardModel() : base("card.deposit.card.dp/proc.go") { }
    }
}