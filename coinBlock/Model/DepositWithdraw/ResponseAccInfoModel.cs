using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseAccInfoModel : ResponseBaseModel
    {
        public ResponseAccInfoDataModel data { get; set; }
        public ResponseAccInfoModel()
        {
            data = new ResponseAccInfoDataModel();
        }
    }

    public class ResponseAccInfoDataModel
    {
        public virtual string userMobile { get; set; }
        public virtual string BankAccNo { get; set; }
    }
}