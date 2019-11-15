using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class ResponseExRateModel : ResponseBaseModel
    {
        public ResponseExRateDataModel data { get; set; }
        public ResponseExRateModel()
        {
            data = new ResponseExRateDataModel();
        }
    }

    public class ResponseExRateDataModel
    {
        public virtual decimal exRate { get; set; }

        public virtual string cryCode { get; set; }
    }
}