using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardReqContentModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string status { get; set; }

        public virtual string brhCode { get; set; }

        public RequestCardReqContentModel() : base("card.selectCardReqList.dp/proc.go") { }
    }
}