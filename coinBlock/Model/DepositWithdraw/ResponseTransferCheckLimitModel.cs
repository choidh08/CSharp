using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseTransferCheckLimitModel : ResponseBaseModel
    {
        public ResponseTransferCheckLimitDataModel data { get; set; }
        public ResponseTransferCheckLimitModel()
        {
            data = new ResponseTransferCheckLimitDataModel();
        }
    }

    public class ResponseTransferCheckLimitDataModel
    {
        public string failCd { get; set; }

        public decimal maxAmt { get; set; }

        public decimal remAmtDay { get; set; }

        public string unLimit { get; set; }
    }
}