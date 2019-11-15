using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardPwdSetModel : RequestBaseModel
    {
        public string cardNum { get; set; }

        public string atmPwd { get; set; }

        public RequestCardPwdSetModel() : base("card.uptCardAtmPwd.dp/proc.go") { }
    }
}