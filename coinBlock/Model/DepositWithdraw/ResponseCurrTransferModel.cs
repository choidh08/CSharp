using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCurrTransferModel : ResponseBaseModel
    {
        public ResponseCurrTransferDataModel data { get; set; }
        public ResponseCurrTransferModel()
        {
            data = new ResponseCurrTransferDataModel();
        }
    }

    public class ResponseCurrTransferDataModel
    {
        public virtual string failCd { get; set; }
        public virtual string lvl { get; set; }
        public virtual string minAmt { get; set; }
        public virtual string maxAmt { get; set; }
        public virtual string wdrprc { get; set; }
        public virtual string krwPrc { get; set; }

        public ResponseCurrTransferDataModel()
        {

        }
    }
}