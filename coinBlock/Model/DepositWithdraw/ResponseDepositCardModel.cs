using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseDepositCardModel : ResponseBaseModel
    {
        public ResponseDepositCardDataModel data { get; set; }
        public ResponseDepositCardModel()
        {
            data = new ResponseDepositCardDataModel();
        }
    }

    public class ResponseDepositCardDataModel
    {
        public virtual string failCd { get; set; }
    }
}