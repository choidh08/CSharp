using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseWithdrawMoneyModel : ResponseBaseModel
    {
        public ResponseWithdrawMoneyDataModel data { get; set; }
        public ResponseWithdrawMoneyModel()
        {
            data = new ResponseWithdrawMoneyDataModel();
        }
    }

    public class ResponseWithdrawMoneyDataModel
    {
        public virtual string failCd { get; set; }
    }
}