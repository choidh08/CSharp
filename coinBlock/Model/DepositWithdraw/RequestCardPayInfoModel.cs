using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardPayInfoModel : RequestBaseModel
    {
        public RequestCardPayInfoModel() : base("card.selectCardReqpayInfo.dp/proc.go") { }
    }
}