using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardRemPrcModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string cardNum { get; set; }

        public RequestCardRemPrcModel() : base("card.selectCardRemPrc.dp/proc.go") { }
    }
}