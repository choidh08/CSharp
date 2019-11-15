using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class RequestCardCheckModel : RequestBaseModel
    {
        public string userMobile { get; set; }

        public string userNm { get; set; }

        public RequestCardCheckModel() : base("card.checkUserMobileName.dp/proc.go") { }
    }
}