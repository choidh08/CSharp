using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardReqCancelModel : RequestBaseModel
    {
        public string orderId { get; set; }

        public RequestCardReqCancelModel() : base("card.cardReqCancel.dp/proc.go") { }
    }
}