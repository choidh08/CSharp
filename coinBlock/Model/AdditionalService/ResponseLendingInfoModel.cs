using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingInfoModel : ResponseBaseModel
    {
        public ResponseLendingInfoDataModel data { get; set; }
        public ResponseLendingInfoModel()
        {
            data = new ResponseLendingInfoDataModel();
        }
    }

    public class ResponseLendingInfoDataModel
    {
        public virtual decimal interest { get; set; }

        public virtual decimal minAmt { get; set; }

        public virtual decimal maxAmt { get; set; }

        public virtual string applyDt { get; set; }

        public virtual string expireDt { get; set; }
    }
}