using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardRemPrcModel : ResponseBaseModel
    {
        public ResponseCardRemPrcDataModel data { get; set; }
        public ResponseCardRemPrcModel()
        {
            data = new ResponseCardRemPrcDataModel();
        }
    }

    public class ResponseCardRemPrcDataModel
    {
        public virtual string failCd { get; set; }

        public virtual string rtnMsg { get; set; }

        public virtual decimal remPrc { get; set; }

        public virtual string bankNo { get; set; }
    }
}