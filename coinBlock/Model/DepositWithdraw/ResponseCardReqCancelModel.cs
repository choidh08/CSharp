using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardReqCancelModel : ResponseBaseModel
    {
        public ResponseCardReqCancelDataModel data { get; set; }
        public ResponseCardReqCancelModel()
        {
            data = new ResponseCardReqCancelDataModel();
        }
    }

    public class ResponseCardReqCancelDataModel
    {
        public virtual string failCd { get; set; }
    }
}