using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardPwdCheckModel : ResponseBaseModel
    {
        public ResponseCardPwdCheckDataModel data { get; set; }
        public ResponseCardPwdCheckModel()
        {
            data = new ResponseCardPwdCheckDataModel();
        }
    }

    public class ResponseCardPwdCheckDataModel
    {
        public virtual string failCd { get; set; }
        public virtual string atmPwd { get; set; }
        public virtual string cardNum { get; set; }
    }
}