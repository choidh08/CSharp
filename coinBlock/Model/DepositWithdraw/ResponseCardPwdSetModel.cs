using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardPwdSetModel : ResponseBaseModel
    {
        public ResponseCardPwdSetDataModel data { get; set; }
        public ResponseCardPwdSetModel()
        {
            data = new ResponseCardPwdSetDataModel();
        }
    }

    public class ResponseCardPwdSetDataModel
    {
        public virtual string failCd { get; set; }
    }
}