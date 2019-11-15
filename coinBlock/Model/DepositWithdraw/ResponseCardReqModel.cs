using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardReqModel : ResponseBaseModel
    {
        public ResponseCardReqDataModel data { get; set; }
        public ResponseCardReqModel()
        {
            data = new ResponseCardReqDataModel();
        }
    }
    
    public class ResponseCardReqDataModel
    {
        public virtual string orderId { get; set; }
        public virtual string failCd { get; set; }
    }
}