using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseTransferCoinModel : ResponseBaseModel
    {
        public ResponseTransferCoinDataModel data { get; set; }
        public ResponseTransferCoinModel()
        {
            data = new ResponseTransferCoinDataModel();
        }
    }

    public class ResponseTransferCoinDataModel
    {
        public virtual string failCd { get; set; }
    }
}