using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardPayInfoModel : ResponseBaseModel
    {
        public ResponseCardPayInfoDataModel data { get; set; }
        public ResponseCardPayInfoModel()
        {
            data = new ResponseCardPayInfoDataModel();
        }
    }

    public class ResponseCardPayInfoDataModel
    {
        public virtual string cardActCode { get; set; }
        public virtual string actNo { get; set; }
        public virtual string bankNm { get; set; }
        public virtual decimal cardReqPrc { get; set; }
        public virtual decimal dlvr { get; set; }
    }
}