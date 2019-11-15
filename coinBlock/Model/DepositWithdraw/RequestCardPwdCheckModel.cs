using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardPwdCheckModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestCardPwdCheckModel() : base("card.getCardAtmPwd.dp/proc.go") { }
    }
}